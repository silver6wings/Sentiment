using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class BuildFeatureDictionary
    {
       // private static string NegativeAttributeFilePath = "";
        private static char[] trimChar = { '\t', ' ' ,'\n'};
        public static Dictionary<int, string> buildDictionary(string FeatureFilePath)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(FeatureFilePath);
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim(trimChar);
                    if (!string.IsNullOrEmpty(line))
                    {
                        result.Add(i++, line);
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
