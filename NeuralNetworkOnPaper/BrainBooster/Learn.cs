using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainBooster
{
    class Learn : Config
    {
        public List<LinkedList<double>> examineDataSet;
        private Random random = new Random();

        public void LearnLayer()
        {

        }

        public void Shuffle(ref List<LinkedList<double>> dataSet, ref List<LinkedList<double>> resultSet) // Fisher-Yates shuffle algorithm
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

        public double CostFunction(LinkedList<double> expectedResult, LinkedList<double> givenResult) // objective function
        {
            double expected, given, sum = 0;
            int n = expectedResult.Count;

            for (int i = 0; i < n; i++)
            {
                expected = expectedResult.First.Value;
                given = givenResult.First.Value;

                expectedResult.RemoveFirst();
                givenResult.RemoveFirst();

                sum += Math.Pow(expected - given, 2);
            }

            return sum / 2;
        }
    }
}
