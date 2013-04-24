using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data.Sql;
using PanGu;
using PanGu.Dict;
using System.Data;

namespace sentiment_classify
{
    public static class GetAllData
    {      
        private static Dictionary<string, string> GetDataString(int Num)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            SqlConnection conn = new SqlConnection("Data Source=stcsrv-f14; Database=BingScoreWeiboData3; Trusted_Connection=True; user id=weibotool ;pwd=123456Aa");

            conn.Open();
            //SELECT TOP 1000 * FROM [BingScoreWeiboDataII].[dbo].[comment_profile] ORDER BY create_time DESC
            string strSQL = "SELECT Top " + Num + " [id],[text] FROM [BingScoreWeiboDataII].[dbo].[comment_profile] ORDER BY create_time DESC";

            SqlCommand cmd = new SqlCommand(strSQL, conn);
            SqlDataReader row = cmd.ExecuteReader();

            while (row.Read())
            {
                string text;
                if (!result.Keys.Contains(text = row["text"].ToString()))
                {
                    result.Add(text, "");
                }
                //Console.WriteLine(row["text"]);
            }

            row.Close();
            conn.Close();
            return result;
        }
        private static string segment(string s)
        {
            Segment segment = new Segment();
            ICollection<WordInfo> words = segment.DoSegment(s);
            StringBuilder wordsString = new StringBuilder();
            foreach (WordInfo wordInfo in words)
            {
                if (wordInfo == null)
                {
                    continue;
                }
                wordsString.AppendFormat("{0}\t", wordInfo.Word);
            }

            return wordsString.ToString();
        }
        public static void writeDataVector(Dictionary<string, GetEmoticons.Sentiment> TrainingAndTestingComments,int positiveNum,int negativeNum, string FeatureFilePath, string OutputFileName)
        {
            StreamWriter sw = null;
            Program.negativeNum = 0;
            Program.positiveNum = 0;
            try
            {
                sw = new StreamWriter(OutputFileName);
                Dictionary<int, string> featureDict = BuildFeatureDictionary.buildDictionary(FeatureFilePath);
                foreach (KeyValuePair<string, GetEmoticons.Sentiment> TrainingAndTestingComment in TrainingAndTestingComments)
                {
                    string outputLine = "";
                    if (TrainingAndTestingComment.Value == GetEmoticons.Sentiment.Positive)
                    {
                        outputLine += "+1" + " ";
                    }
                    else
                    {
                        outputLine += "-1" + " ";
                    }
                    for (int i = 0; i < featureDict.Count; i++)
                    {
                        if (TrainingAndTestingComment.Key.Contains(featureDict[i]))
                        {
                            int containNum = 0;
                            string[] commentWords = TrainingAndTestingComment.Key.Split('\t');
                            foreach (string commentWord in commentWords)
                            {
                                if (commentWord.Equals(featureDict[i]))
                                {
                                    containNum++;
                                }
                            }
                            if (containNum > 0)
                            {
                                outputLine += i + ":";
                                outputLine += containNum+" ";
                            }
                        }
                    }
                    if (outputLine.Length > 3)
                    {
                        sw.WriteLine(outputLine);
                        if (outputLine[0] == '+')
                        {
                            Program.positiveNum++;
                        }
                        else
                        {
                            Program.negativeNum++;
                        }
                    }
                }//foreach
            }//try
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sw.Close();
            }
        }

        public static void getAllDataFromDatabase(int dataNum, double percentOfDataForFeatureExtractMethod_1)
        {
            List<Dictionary<string, GetEmoticons.Sentiment>> result = new List<Dictionary<string, GetEmoticons.Sentiment>>();

            Dictionary<string, string> rows = GetDataString(dataNum);
            Dictionary<string, GetEmoticons.Sentiment> emoticons = GetEmoticons.getAllEmoticons();
            int numOfCommentsWithEmoticon = 0;
            foreach (string row in rows.Keys)
            {
                foreach (string emotion in emoticons.Keys)
                {
                    if (row.Contains(emotion))
                    {
                        numOfCommentsWithEmoticon ++;
                    }
                }
            }


            int numOfDataForFeature = (int)(percentOfDataForFeatureExtractMethod_1 * numOfCommentsWithEmoticon);
            foreach (KeyValuePair<string, string> row in rows)
            {
                string copyComment = row.Key;
                int posScore = 0;
                int negScore = 0;               

                foreach (KeyValuePair<string, GetEmoticons.Sentiment> emoticon in emoticons)
                {
                    if (row.Key.Contains(emoticon.Key))
                    {
                        copyComment = row.Key.Replace(emoticon.Key, "");

                        if (emoticon.Value == GetEmoticons.Sentiment.Positive)
                        {
                            posScore++;                      
                        }
                        else
                        {
                            negScore++;
                        }                        
                    }
                }
                string segmentComment = segment(copyComment);

                if (!string.IsNullOrEmpty(segmentComment))
                {                    
                    if (posScore == 0 && negScore ==0)
                    {
                        if (!Program.allCommentsWithoutEmoticon.Keys.Contains(segmentComment))
                        {
                            Program.allCommentsWithoutEmoticon.Add(segmentComment, 0);
                        }
                    }
                    else if (posScore >= negScore)
                    {                        
                        if (numOfDataForFeature > 0)
                        {
                            if (!Program.commentsWithEmoticonForMethod1.Keys.Contains(segmentComment))
                            {
                                Program.commentsWithEmoticonForMethod1.Add(segmentComment, GetEmoticons.Sentiment.Positive);
                                numOfDataForFeature--;
                            }
                        }
                        else
                        {
                            if (!Program.commentsWithEmoticonForTrainingAndTesting.Keys.Contains(segmentComment))
                            {
                                Program.commentsWithEmoticonForTrainingAndTesting.Add(segmentComment, GetEmoticons.Sentiment.Positive);
                            }
                        }
                        //Program.allCommentsWithEmoticon.Add(segmentComment, GetEmoticons.Sentiment.Positive);                        
                    }
                    else
                    {
                        if (numOfDataForFeature > 0)
                        {
                            if (!Program.commentsWithEmoticonForMethod1.Keys.Contains(segmentComment))
                            {
                                Program.commentsWithEmoticonForMethod1.Add(segmentComment, GetEmoticons.Sentiment.Negative);
                                numOfDataForFeature--;
                            }
                        }
                        else
                        {
                            if (!Program.commentsWithEmoticonForTrainingAndTesting.Keys.Contains(segmentComment))
                            {
                                Program.commentsWithEmoticonForTrainingAndTesting.Add(segmentComment, GetEmoticons.Sentiment.Negative);
                            }
                        }
                        //Program.allCommentsWithEmoticon.Add(segmentComment, GetEmoticons.Sentiment.Negative);                   
                    }
                }
                
            }
        }
    }
}
