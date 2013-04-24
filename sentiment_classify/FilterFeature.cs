using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class FilterFeature
    {
        public static List<string> Filter(string featureFilePath,Dictionary<string,GetEmoticons.Sentiment> dataForFilter,string filterFeatureFileName)
        {
            List<string> result = new List<string>();
            double k = 0.0, n = 0.0, a = 0.0, d = 0.0, b = 0.0, c = 0.0;
            
            Dictionary<string, GetEmoticons.Sentiment> features = new Dictionary<string,GetEmoticons.Sentiment>();

            //read file featureFilePath to gain features;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(featureFilePath);
                string line = "";
                while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                {
                    string[] featureAndEmotion = line.Split('\t');
                    if (featureAndEmotion[1].Equals("positive"))
                    {
                        features.Add(featureAndEmotion[0], GetEmoticons.Sentiment.Positive);
                    }
                    else
                    {
                        features.Add(featureAndEmotion[0],GetEmoticons.Sentiment.Negative);
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

            Dictionary<string, GetEmoticons.Sentiment> filterFeatures = new Dictionary<string, GetEmoticons.Sentiment>();

            foreach (KeyValuePair<string, GetEmoticons.Sentiment> feature in features)
            {
                foreach(KeyValuePair<string ,GetEmoticons.Sentiment> data in dataForFilter)
                {
                    if (data.Key.Contains(feature.Key))
                    {
                        if (data.Value == feature.Value)
                        {
                            a++;
                        }
                        else
                        {
                            b++;
                        }
                    }
                    else
                    {
                        if (data.Value == feature.Value)
                        {
                            c++;
                        }
                        else
                        {
                            d++;
                        }
                    }
                }
                n = a + b + c + d;
                k = Math.Sqrt(n * (a * d - b * c) * (a * d - b * c) / ((a + b) * (c + d) * (a + c) * (b + d)));

                if (k * k > 2.706)
                {
                    filterFeatures.Add(feature.Key,feature.Value);
                }
            }

            //output to filterFeatureFileName
            StreamWriter sw = null;
            try 
            { 
                sw = new StreamWriter(filterFeatureFileName);
                foreach (KeyValuePair<string, GetEmoticons.Sentiment> filterFeature in filterFeatures)
                {
                    result.Add(filterFeature.Key);
                    sw.WriteLine(filterFeature.Key);
                }
                                
            }
            catch(Exception e)
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
