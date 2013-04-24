using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sentiment_classify
{
    public class ConfigInformation
    {
        public string AllDataFileName { get; set; }

        public string TrainDataFileName { get; set; }
        public string TestDataFileName { get; set; }

        public string NewFeatureFileName { get; set; }
        public string FilterFeatureFileName { get; set; }

        public string BasicFeatureFilePath { get; set; }
        public double percentForExtract { get; set; }
        public string FeatureFilePath { get; set; }

        public double PercentOfTraingData { get; set; }
        public int DataNum { get; set; }
        public int FeatureRank { get; set; }
        public double DataPercentForFeatureBuildingAndFilter { get; set; }

        public double percentForExtractMethod2  { get; set; }
        public string emotionWordFilePath { get; set; }
        public string stopWordFilePath { get; set; }
        public string feature2FileName { get; set; }
    }
}
