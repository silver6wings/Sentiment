using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
namespace sentiment_classify
{
    public static class ReadConfigurationFile
    {
        internal static string Regex_AllDataFileName = "AllDataFileName=\"\\S{0,}\"";
        
        //bug?
        internal static string Regex_BasicFeatureFilePath = "BasicFeatureFilePath=\"\\S{0,}\"";

        internal static string Regex_TrainDataFileName = "TrainDataFileName=\"\\S{0,}\"";
        internal static string Regex_TestDataFileName = "TestDataFileName=\"\\S{0,}\"";
        internal static string Regex_NewFeatureFileName = "NewFeatureFileName=\"\\S{0,}\"";
        internal static string Regex_FilterFeatureFileName = "FilterFeatureFileName=\"\\S{0,}\"";
        internal static string Regex_FeatureFilePath = "FeatureFilePath=\"\\S{0,}\"";
        internal static string Regex_emotionWordFilePath = "emotionWordFilePath=\"\\S{0,}\"";
        internal static string Regex_stopWordFilePath = "stopWordFilePath=\"\\S{0,}\"";
        internal static string Regex_feature2FileName = "feature2FileName=\"\\S{0,}\"";

        internal static string Regex_DataNum = "DataNum=\\S{0,}";
        internal static string Regex_FeatureRank = "FeatureRank=\\S{0,}";
        internal static string Regex_percentForExtract = "percentForExtract=\\S{0,}";
        internal static string Regex_percentForExtractMethod2 = "percentForExtractMethod2=\\S{0,}";
        internal static string Regex_PercentOfTraingData = "PercentOfTraingData=\\S{0,}";   
        internal static string Regex_DataPercentForFeatureBuildingAndFilter = "DataPercentForFeatureBuildingAndFilter=\\S{0,}";

        internal static string[] Names = 
        {
             "AllDataFileName",
             "TrainDataFileName",
             "TestDataFileName",
             "NewFeatureFileName",
             "FilterFeatureFileName",
             "FeatureFilePath",
             "PercentOfTraingData",
             "DataNum",
             "FeatureRank",
             "DataPercentForFeatureBuildingAndFilter",
             "wrongType",
             "percentForExtract",
             "percentForExtractMethod2",
             "emotionWordFilePath",
             "stopWordFilePath",
             "feature2FileName",
             "BasicFeatureFilePath"
        };
        internal static char[] trimChar = { ' ', '\t', '\n', '\"' };
        
        public static ConfigInformation read(string fileName)
        {

            Dictionary<string, int> names = new Dictionary<string,int>();
            for(int i=0 ; i<Names.Count() ; i++)
            {
                 names.Add(Names[i],Names[i].Length);
            }
            
            ConfigInformation result = new ConfigInformation();
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(fileName);
                string line = "";

                while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                {
                    //Console.WriteLine(" Read :" + line);

                    if (new Regex(Regex_AllDataFileName).IsMatch(line))
                    {
                        result.AllDataFileName = line.Substring(names["AllDataFileName"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 01:" + result.DataNum);
                    }

                    if (new Regex(Regex_DataNum).IsMatch(line))
                    {
                        result.DataNum = int.Parse(line.Substring(names["DataNum"] + 1).Trim(trimChar));
                        Console.WriteLine("!!! 02:" + result.DataNum);
                    }
                    
                    if (new Regex(Regex_DataPercentForFeatureBuildingAndFilter).IsMatch(line))
                    {
                        result.DataPercentForFeatureBuildingAndFilter = double.Parse(line.Substring(names["DataPercentForFeatureBuildingAndFilter"] + 1).Trim(trimChar));
                    }
                    
                    if (new Regex(Regex_FeatureFilePath).IsMatch(line))
                    {
                        result.FeatureFilePath = line.Substring(names["FeatureFilePath"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 03:" + result.FeatureFilePath);
                    }
                    
                    if (new Regex(Regex_FeatureRank).IsMatch(line))
                    {
                        result.FeatureRank = int.Parse(line.Substring(names["FeatureRank"] + 1).Trim(trimChar));
                        Console.WriteLine("!!! 04:" + result.FeatureRank);
                    }
                    
                    if (new Regex(Regex_FilterFeatureFileName).IsMatch(line))
                    {
                        result.FilterFeatureFileName = line.Substring(names["FilterFeatureFileName"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 05:" + result.FilterFeatureFileName);
                    }
                    
                    if (new Regex(Regex_NewFeatureFileName).IsMatch(line))
                    {
                        result.NewFeatureFileName = line.Substring(names["NewFeatureFileName"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 06:" + result.NewFeatureFileName);
                    }
                    
                    if (new Regex(Regex_PercentOfTraingData).IsMatch(line))
                    {
                        result.PercentOfTraingData = double.Parse(line.Substring(names["PercentOfTraingData"] + 1).Trim(trimChar));
                        Console.WriteLine("!!! 07:" + result.PercentOfTraingData);
                    }
                    
                    if (new Regex(Regex_TestDataFileName).IsMatch(line))
                    {
                        result.TestDataFileName = line.Substring(names["TestDataFileName"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 08:" + result.TestDataFileName);
                    }
                    if (new Regex(Regex_TrainDataFileName).IsMatch(line))
                    {
                        result.TrainDataFileName = line.Substring(names["TrainDataFileName"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 09:" + result.TrainDataFileName);
                    }
                    if (new Regex(Regex_percentForExtract).IsMatch(line))
                    {
                        result.percentForExtract = double.Parse(line.Substring(names["percentForExtract"] + 1).Trim(trimChar));
                        Console.WriteLine("!!! 10:" + result.percentForExtract);
                    }
                    if (new Regex(Regex_percentForExtractMethod2).IsMatch(line))
                    {
                        result.percentForExtractMethod2 = double.Parse(line.Substring(names["percentForExtractMethod2"] + 1).Trim(trimChar));
                        Console.WriteLine("!!! 11:" + result.percentForExtractMethod2);
                    }    
                    if (new Regex(Regex_emotionWordFilePath).IsMatch(line))
                    {
                        result.emotionWordFilePath = line.Substring(names["emotionWordFilePath"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 12:" + result.emotionWordFilePath);
                    }
                    if (new Regex(Regex_stopWordFilePath).IsMatch(line))
                    {
                        result.stopWordFilePath = line.Substring(names["stopWordFilePath"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 13:" + result.stopWordFilePath);
                    }
                    if (new Regex(Regex_feature2FileName).IsMatch(line))
                    {
                        result.feature2FileName = line.Substring(names["feature2FileName"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 14:" + result.feature2FileName);
                    }

                    // Caution "FeatureFilePath" Match Above
                    if (new Regex(Regex_BasicFeatureFilePath).IsMatch(line))
                    {
                        result.BasicFeatureFilePath = line.Substring(names["BasicFeatureFilePath"] + 1).Trim(trimChar);
                        Console.WriteLine("!!! 15:" + result.BasicFeatureFilePath);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                sr.Close();
            }
            return result;
        }
    }
}
