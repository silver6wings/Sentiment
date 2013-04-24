using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class DeleteUselessFeature
    {
        internal static char[] trimChar = {'\t',' '};
        public static void delete(string featureFilePath)
        {
            Dictionary<string,string>features = new Dictionary<string,string>(); 
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(featureFilePath);
                string line = "";
                while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                {
                    line = line.Trim(trimChar);
                    features.Add(line, "");
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

            bool contain = false;
            List<string> deleteFeatures = new List<string>();
            foreach(string feature in features.Keys)
            {
                foreach (string comment in Program.CommentsWithoutEmoticonForObscureFilter.Keys)
                {
                    if(comment.Contains(feature))
                    {
                        contain = true;
                    }
                }
                if(!contain)
                {
                    deleteFeatures.Add(feature);
                }
            }
            foreach(string deleteFeature in deleteFeatures)
            {
                features.Remove(deleteFeature);
            }

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(featureFilePath);
                foreach (string feature in features.Keys)
                {
                    sw.WriteLine(feature);
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
