using NeuralNetworkOnPaper.ArtificalBrain.ADALINE;
using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkOnPaper.ArtificalBrain.MADALINE
{
    class NetworkMadaline : Learn
    {
        /*
         * PROPERTIES
         */

        //
        private List<LayerMadaline> layers { get; set; }

        /*
         * METHODS
         */

        //
        public NetworkMadaline()
        {

        }

        //
        public void Configure(int[] neuronsAmount)
        {
            for (int i = 0; i < 3; i++)
            {
                LayerMadaline layer = new LayerMadaline();
                layer.Configure(neuronsAmount[i], i == 0 ? 1 : neuronsAmount[i-1], i == 0);
                layers.Add(layer);
            }
        }

        //
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> resultSet, int epochAmount, learningMethod method)
        {
            for (int i = 0; i < epochAmount; i++)
            {
                Shuffle(ref dataSet, ref resultSet);

                var data = dataSet.Zip(resultSet, (n, w) => new { dataSet = n, resultSet = w });
                foreach (var row in data)
                {
                    Backpropagation( //online method of backpropagation
                        Examine(
                            new LinkedList<double>(row.dataSet)
                        ),
                        row.resultSet
                    );
                    
                }
            }
        }

        //
        public LinkedList<double> Examine(LinkedList<double> signals)
        {
            foreach(LayerMadaline layer in layers)
                signals = layer.Run(signals);
            return signals;
        }

        //
        public void Backpropagation(LinkedList<double> givenResults, LinkedList<double> expectedResults)
        {
            double error = ObjectiveFunction(givenResults, expectedResults);

            //set wages of hidden layer

            //set wages of output layer
            
        }

        public double ObjectiveFunction(LinkedList<double> givenResults, LinkedList<double> expectedResults)
        {
            return 0;
        }

        //
        public void RecursiveLastSquares()
        {

        }
    }
}
