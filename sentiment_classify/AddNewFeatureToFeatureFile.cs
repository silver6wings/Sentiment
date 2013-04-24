using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class AddNewFeatureToFeatureFile
    {
        internal static char[] trimChar = {' ','\t','\n'};
        public static void add(List<string> newFeatures, string basicFeatureFileName,string featureFileName)
        {
            StreamReader srBasicFeatureFile = null;
            try
            {
                srBasicFeatureFile = new StreamReader(basicFeatureFileName);
                string line;
                while ((line = srBasicFeatureFile.ReadLine()) != null)
                {
                    line = line.Trim(trimChar);
                    if (!string.IsNullOrEmpty(line))
                    {
                        newFeatures.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                srBasicFeatureFile.Close();
            }

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(featureFileName);
                foreach (string newFeature in newFeatures)
                {
                    sw.WriteLine(newFeature);
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
            
            

        }
    }
}
