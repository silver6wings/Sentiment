using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace sentiment_classify
{
    public static class GetEmotionWordsFromWeibo
    {
        internal class ComparsionSample : IComparer<WordAndFrequency>
        {
            public int Compare(WordAndFrequency x, WordAndFrequency y)
            {
                if( x.appearNum < y.appearNum)
                {
                    return 1 ;
                }
                else if(x.appearNum == y.appearNum)
                {
                    return 0;
                }
                else 
                {
                    return -1;
                }
            }
        }

        public static void GetEmotionWords(Dictionary<string, GetEmoticons.Sentiment> comments, string newFeatureFilePath, int highFrenquencyRank)
        {
            Dictionary<string, int> PositiveWords = new Dictionary<string, int>();
            Dictionary<string, int> NegativeWords = new Dictionary<string, int>();

            foreach (KeyValuePair<string, GetEmoticons.Sentiment> comment in comments)
            {
                string[] words = comment.Key.Split('\t');
                if (comment.Value == GetEmoticons.Sentiment.Positive)
                {
                    foreach (string word in words)
                    {
                        if (!PositiveWords.Keys.Contains(word))
                        {
                            PositiveWords.Add(word, 1);
                        }
                        else
                        {
                            PositiveWords[word]++;
                        }
                    }
                }
                else
                {
                    foreach (string word in words)
                    {
                        if (!NegativeWords.Keys.Contains(word))
                        {
                            NegativeWords.Add(word, 1);
                        }
                        else
                        {
                            NegativeWords[word]++;
                        }
                    }
                }
            }

            List<WordAndFrequency> posiListWords = new List<WordAndFrequency>();
            List<WordAndFrequency> negaListWords = new List<WordAndFrequency>();
            foreach(KeyValuePair<string,int> positiveWord in PositiveWords)
            {
                WordAndFrequency wf = new WordAndFrequency();
                wf.word = positiveWord.Key;
                wf.appearNum = positiveWord.Value;
                posiListWords.Add(wf);
            }
            foreach(KeyValuePair<string,int> negativeWord in NegativeWords)
            {
                WordAndFrequency wf = new WordAndFrequency();
                wf.word = negativeWord.Key;
                wf.appearNum = negativeWord.Value;
                negaListWords.Add(wf);
            }

            posiListWords.Sort(new ComparsionSample());
            negaListWords.Sort(new ComparsionSample());
            Dictionary<string, GetEmoticons.Sentiment> features = new Dictionary<string, GetEmoticons.Sentiment>();

            int bound = Math.Min(posiListWords.Count,negaListWords.Count);
            bound = Math.Min(highFrenquencyRank,bound);
            for (int i = 0; i < bound; i++)
            {
                features.Add(posiListWords[i].word,GetEmoticons.Sentiment.Positive);
            }
            for (int i = 0; i < bound; i++)
            {
                if (features.Keys.Contains(negaListWords[i].word))
                {
                    features.Remove(negaListWords[i].word);
                }
                else
                {
                    features.Add(negaListWords[i].word, GetEmoticons.Sentiment.Negative);
                }                
            }

            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(newFeatureFilePath);
                foreach (KeyValuePair<string, GetEmoticons.Sentiment> feature in features)
                {
                    sw.WriteLine(feature.Key + "\t" + feature.Value);
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
