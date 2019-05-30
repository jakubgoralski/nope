﻿using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainBooster
{
    public class Learn : Config
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // Helps to generate real random values to improve FY Shuffle algorithm performance
        private Random random = new Random();

        /*
         * 
         * METHODS
         * 
         */

        // Returns shuffled list of signals to improve learning performance. Inside use mathematical 'Fisher-Yates' shuffle algorithm
        public void Shuffle(ref List<LinkedList<double>> dataSet, ref List<LinkedList<double>> resultSet) 
        {
            int n = dataSet.Count;
            LinkedList<double> tempData;
            LinkedList<double> tempResult;

            while(n>1)
            {
                --n;
                int k = random.Next(1, n+1);

                tempData = dataSet[k];
                tempResult = resultSet[k];

                dataSet[k] = dataSet[n];
                resultSet[k] = resultSet[n];

                dataSet[n] = tempData;
                resultSet[n] = tempResult;
            }
        }

        ////
        //public double ComputeError(LinkedList<double> expectedResult, LinkedList<double> givenResult)
        //{
        //    double expected, given, sum = 0;
        //    int n = expectedResult.Count;

        //    for (int i = 0; i < n; i++)
        //    {
        //        expected = expectedResult.First.Value;
        //        given = givenResult.First.Value;

        //        expectedResult.RemoveFirst();
        //        givenResult.RemoveFirst();

        //        sum += Math.Pow(expected - given, 2);
        //    }

        //    return sum / 2;
        //}
    }
}
