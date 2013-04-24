using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sentiment_classify
{
    public static class GetDataForBuidNewFeatureMethod2AndFeatureFilter
    {
        public static void get(double percentOfDataForExtractFeature)
        {
            int count = (int)(Program.allCommentsWithoutEmoticon.Count * percentOfDataForExtractFeature);
            bool change = false;
            foreach (KeyValuePair<string, int> data in Program.allCommentsWithoutEmoticon)
            {

                if (count > 0 && change)
                {
                    Program.CommentsWithoutEmoticonForMethod2.Add(data.Key, data.Value);
                    change = false;
                }
                else
                {
                    Program.CommentsWithoutEmoticonForObscureFilter.Add(data.Key, data.Value);
                    change = true;
                }
                count--;
            }
        }
    }
}
