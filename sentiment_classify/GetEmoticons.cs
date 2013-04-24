using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class GetEmoticons
    {
        private static char[] TrimChar = {'\t',' '};
        private static string positiveEmoticonsFileName = "C:\\Users\\v-jipe\\Desktop\\Positive.txt";
        private static string negativeEmoticonsFileName = "C:\\Users\\v-jipe\\Desktop\\Negative.txt";
        public enum Sentiment
        {
            Positive=0,
            Negative=1
        }
        public static Dictionary<string, GetEmoticons.Sentiment> getAllEmoticons()
        {
            Dictionary<string, GetEmoticons.Sentiment> Emoticons = new Dictionary<string, GetEmoticons.Sentiment>();
            //get positive emoticons
            StreamReader sr = null;
            string line = "";
            try
            {
                sr = new StreamReader(positiveEmoticonsFileName);
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim(TrimChar);
                    if (!string.IsNullOrEmpty(line))
                    {
                        Emoticons.Add(line, Sentiment.Positive);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                //wrong note
            }
            finally
            {
                sr.Close();
            }

            //get negtive emoticons
            try
            {
                sr = new StreamReader(negativeEmoticonsFileName);
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim(TrimChar);
                    if (!string.IsNullOrEmpty(line))
                    {
                        Emoticons.Add(line,Sentiment.Negative);
                    }                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                //wrong note
            }
            finally
            {
                sr.Close();
            }
            return Emoticons;
        }
    }
}
