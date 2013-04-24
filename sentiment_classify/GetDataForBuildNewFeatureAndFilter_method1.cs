using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace sentiment_classify
{
    public static class GetDataForBuildNewFeatureAndFilter_method1
    {
        public static void get(double percentOfDataForExtractingFeature)
        {
            if (percentOfDataForExtractingFeature > 1 || percentOfDataForExtractingFeature < 0)
            {
                Console.WriteLine("Please input correct percent in [0,1]");
                return;
            }      
            int count =(int) (Program.commentsWithEmoticonForMethod1.Count * percentOfDataForExtractingFeature);

            bool change = false;
            foreach (KeyValuePair<string, GetEmoticons.Sentiment> data in Program.commentsWithEmoticonForMethod1)
            {               

                if (count > 0 && change)
                {
                    Program.commentsWithEmoticonForMethod1_BuildNewFeature.Add(data.Key, data.Value);
                    change = false;
                }
                else
                {
                    Program.commentsWithEmoticonForMethod1_CorrespondenceFilter.Add(data.Key, data.Value);
                    change = true;
                }
                count--;
            }
        }
    }
}
