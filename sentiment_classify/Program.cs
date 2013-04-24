using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Diagnostics;



namespace sentiment_classify
{
    class Program
    {
        /*
         
        private static string AllDataFileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\All";
        private static string PositiveAndNegativeDataNumberFileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\NumberFile";
        private static string TrainDataFileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\heart";
        private static string TestDataFileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\test";

        private static string NewFeatureFileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\feature.txt";
        private static string FilterFeatureFileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\filterFeature.txt";

        public static string TestData = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\testData.txt";
        public static string TestFile = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\test.txt";

        private static string FeatureFilePath = "C:\\Users\\v-jipe\\Desktop\\Features.txt";

        private static double PercentOfTraingData = 0.6;
        private static int DataNum = 500000;
        private static int FeatureRank = 500;
        private static double DataPercentForFeatureBuildingAndFilter = 0.3;
         
        */

        //public static Dictionary<string, GetEmoticons.Sentiment> allCommentsWithEmoticon;

        public static Dictionary<string, int> allCommentsWithoutEmoticon;
        public static Dictionary<string, GetEmoticons.Sentiment> commentsWithEmoticonForTrainingAndTesting;
        public static Dictionary<string, GetEmoticons.Sentiment> commentsWithEmoticonForMethod1;
        public static Dictionary<string, GetEmoticons.Sentiment> commentsWithEmoticonForMethod1_BuildNewFeature, commentsWithEmoticonForMethod1_CorrespondenceFilter;
        public static Dictionary<string, int> CommentsWithoutEmoticonForMethod2, CommentsWithoutEmoticonForObscureFilter;

        public static int positiveNum;
        public static int negativeNum;

        static void Main(string[] args)
        {
            //allCommentsWithEmoticon = new Dictionary<string, GetEmoticons.Sentiment>();

            allCommentsWithoutEmoticon = new Dictionary<string, int>() ;

            commentsWithEmoticonForTrainingAndTesting = new Dictionary<string, GetEmoticons.Sentiment>();

            commentsWithEmoticonForMethod1 = new Dictionary<string, GetEmoticons.Sentiment>();

            commentsWithEmoticonForMethod1_BuildNewFeature = new Dictionary<string, GetEmoticons.Sentiment>();

            commentsWithEmoticonForMethod1_CorrespondenceFilter = new Dictionary<string, GetEmoticons.Sentiment>();

            CommentsWithoutEmoticonForMethod2 = new Dictionary<string, int>();

            CommentsWithoutEmoticonForObscureFilter = new Dictionary<string, int>();

            positiveNum = 0;
            negativeNum = 0;

            //ConfigInformation info = ReadConfigurationFile.read(args[0]);
            ConfigInformation info = ReadConfigurationFile.read("../libsvm-3.12/windows/ConfigFile.txt");

            /*
            GetAllData.getAllDataFromDatabase(info.DataNum, info.DataPercentForFeatureBuildingAndFilter);

            //extract feature method1 
            GetDataForBuildNewFeatureAndFilter_method1.get(info.percentForExtract);
            GetEmotionWordsFromWeibo.GetEmotionWords(commentsWithEmoticonForMethod1_BuildNewFeature, info.NewFeatureFileName, info.FeatureRank);
            List<string> newFeature1 = FilterFeature.Filter(info.NewFeatureFileName, commentsWithEmoticonForMethod1_CorrespondenceFilter, info.FilterFeatureFileName);

            //extract feature method2
            GetDataForBuidNewFeatureMethod2AndFeatureFilter.get(info.percentForExtractMethod2);
            List<string> newFeature2 =FeatureMethod2_ProcessWeiboWithEmotionWords.process(CommentsWithoutEmoticonForMethod2, info.emotionWordFilePath, info.stopWordFilePath, info.feature2FileName);

            //add new features into feature file
            AddNewFeatureToFeatureFile.add(newFeature1, info.BasicFeatureFilePath, info.FeatureFilePath);
            AddNewFeatureToFeatureFile.add(newFeature2, info.FeatureFilePath, info.FeatureFilePath);

            //delete the useless feature in feature file
            DeleteUselessFeature.delete(info.FeatureFilePath);

            //write all data
            GetAllData.writeDataVector(commentsWithEmoticonForTrainingAndTesting, positiveNum, negativeNum, info.FeatureFilePath, info.AllDataFileName);

            //build training data and testing data
            BuildTrainingData.buildTainAndTestData(info.AllDataFileName, positiveNum, negativeNum,info.TrainDataFileName, info.TestDataFileName, info.PercentOfTraingData);
            */

            /*
            //training
            Process trainProcess = new Process();
            trainProcess.StartInfo.FileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\svm-train";
            trainProcess.StartInfo.Arguments = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\heart C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\heart.model";
            trainProcess.Start();
            // 
            trainProcess.Dispose();

            //build test data
            //string selectComment = "";
            //List<string> testComments = GetDataString();
            //test
            Process testProcess = new Process();
            testProcess.StartInfo.FileName = "C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\svm-predict";
            testProcess.StartInfo.Arguments = "C:\\Users\\v-jipe\\Desktop\\test C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\heart.model C:\\Users\\v-jipe\\Downloads\\libsvm-3.12\\libsvm-3.12\\windows\\test.result";
            testProcess.Start();
            // 
            testProcess.Dispose();      
            */

            Console.WriteLine("=== END ===");
            Console.ReadKey();
        }
    }
}
