using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace sentiment_classify
{
    public static class BuildTrainingData
    {
        public static void buildTainAndTestData(string AllDataFileName, int positiveNum,int negativeNum,string TrainDataFileName,string TestDataFileName, double percentOfTraingData)
        {
            if (percentOfTraingData > 1 || percentOfTraingData < 0)
            {
                Console.WriteLine("Please input correct percent in [0,1]");
                return;
            }           

            StreamWriter swTrain = null;
            StreamWriter swTest = null;
            StreamReader srAll = null;
            
            try{
                swTrain = new StreamWriter(TrainDataFileName);
                swTest = new StreamWriter(TestDataFileName);
                srAll = new StreamReader(AllDataFileName);

                string line = null;

                int MinOfPosAndNeg = Math.Min(positiveNum, negativeNum);
                int trainPositive = (int)(MinOfPosAndNeg * percentOfTraingData);
                int trainNegative =(int)( MinOfPosAndNeg * percentOfTraingData);
                int testPositive =(int)( MinOfPosAndNeg *(1- percentOfTraingData));
                int testNegative =(int)( MinOfPosAndNeg *(1- percentOfTraingData));
                bool turnToTestPosi = false;                
                bool turnToTestNega = false;               
                
                while(!string.IsNullOrEmpty(line = srAll.ReadLine()))
                {
                    if ((trainPositive == 0) && (trainNegative == 0) && (testPositive == 0) && (testNegative == 0))
                    {
                        break;
                    }
                    if(line[0] == '+')
                    {
                        if((trainPositive > 0) && (!turnToTestPosi))
                        {
                            swTrain.WriteLine(line);
                            trainPositive--;
                            turnToTestPosi = true;
                        }
                        else if((testPositive >0) && turnToTestPosi)
                        {
                            swTest.WriteLine(line);
                            testPositive--;
                            turnToTestPosi = false;
                        }
                    }
                    else
                    {
                        if((trainNegative > 0) && (!turnToTestNega))
                        {
                            swTrain.WriteLine(line);
                            trainNegative--;
                            turnToTestNega = true;
                        }
                        else if ((testNegative > 0) && turnToTestNega)
                        {
                            swTest.WriteLine(line);
                            testNegative--;
                            turnToTestNega = false;
                        }
                    }
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                swTrain.Close();
                swTest.Close();
                srAll.Close();
            }
        }
    }
}
