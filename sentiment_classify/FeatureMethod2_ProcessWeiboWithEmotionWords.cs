using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class FeatureMethod2_ProcessWeiboWithEmotionWords
    {
        internal static char[] trimchar = {' ','\n','\t'};
        internal class ComparsionSample : IComparer<WordAndFrequency>
        {
            public int Compare(WordAndFrequency x, WordAndFrequency y)
            {
                if (x.appearNum < y.appearNum)
                {
                    return 1;
                }
                else if (x.appearNum == y.appearNum)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
        }
        public static List<string> process(Dictionary<string, int> dataDict, string emotionWordFilePath, string stopWordFilePath, string feature2FileName)
        {
            StreamReader sr_emotionWords = null;
            StreamReader sr_stopWords= null;
            StreamWriter sw = null;
            Dictionary<string, string> emotionWords = new Dictionary<string, string>();
            Dictionary<string, string> stopWords = new Dictionary<string, string>();
            try
            {
                sr_emotionWords = new StreamReader(emotionWordFilePath);
                sr_stopWords = new StreamReader(stopWordFilePath);
                string line = "";
                while (!string.IsNullOrEmpty(line = sr_emotionWords.ReadLine()))
                {
                    string trimLine = line.Trim(trimchar);
                    if (!emotionWords.Keys.Contains(trimLine))
                    {
                        emotionWords.Add(trimLine, "");
                    }
                }
                while (!string.IsNullOrEmpty(line = sr_stopWords.ReadLine()))
                {
                    string trimLine = line.Trim(trimchar);
                    if (!stopWords.Keys.Contains(trimLine))
                    {
                        stopWords.Add(line.Trim(trimchar), "");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sr_emotionWords.Close();
                sr_stopWords.Close();
            }

            //delete the comment without emotion words
            List<string> remove = new List<string>();
            foreach (string data in dataDict.Keys)
            {
                bool containsEmotionWord = false;
                foreach (string emotionWord in emotionWords.Keys)
                {
                    if (data.Contains(emotionWord))
                    {
                        containsEmotionWord = true;
                    }
                }
                if (!containsEmotionWord)
                {
                    remove.Add(data);                    
                }
            }
            foreach (string remo in remove)
            {
                dataDict.Remove(remo);
            }
            //frequency count
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>();
            foreach(string data in dataDict.Keys)
            {
                string[] dataWords = data.Split('\t');
                for (int i = 0; i < dataWords.Length; i++)
                {
                    if (!wordFrequency.Keys.Contains(dataWords[i]))
                    {
                        wordFrequency.Add(dataWords[i], 1);
                    }
                    else
                    {
                        wordFrequency[dataWords[i]]++;
                    }
                }
            }

            foreach (string stopWord in stopWords.Keys)
            {
                if (wordFrequency.Keys.Contains(stopWord))
                {
                    wordFrequency.Remove(stopWord);
                }
            }

            List<WordAndFrequency> wordAndFs = new List<WordAndFrequency>();
            foreach (KeyValuePair<string, int> Frequency in wordFrequency)
            {
                WordAndFrequency wf = new WordAndFrequency();
                wf.word = Frequency.Key;
                wf.appearNum = Frequency.Value;
                wordAndFs.Add(wf);
            }
            wordAndFs.Sort(new ComparsionSample());


            List<string> result = new List<string>();
            try
            {
                sw = new StreamWriter(feature2FileName);
                int num = Math.Min(1000, wordAndFs.Count);
                for (int i = 0; i < num; i++)
                {
                    sw.WriteLine(wordAndFs[i].word + "\t" + wordAndFs[i].appearNum);
                    result.Add(wordAndFs[i].word);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sw.Close();
            }
            return result;
        }
    }
}
