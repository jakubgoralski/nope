using NeuralNetworkOnPaper.ArtificalBrain.ADALINE;
using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkOnPaper.ArtificalBrain
{
    public class NetworkAdaline : Learn
    {
        /*
         * PROPERTIES
         */
        
        //
        private LayerAdaline layer { get; set; }

        /*
         * METHODS
         */

        //
        public NetworkAdaline()
        {

        }

        //
        public void Configure(int synapseAmount, int neuronAmount, NeuronType neuronType = NeuronType.Bipolar)
        {
            layer = new LayerAdaline();
            layer.Configure(neuronAmount, synapseAmount, neuronType);
        }

        //
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> resultSet, int epochAmount)
        {
            for (int i = 0; i < epochAmount; i++)
            {
                Shuffle(ref dataSet, ref resultSet);

                var data = dataSet.Zip(resultSet, (n, w) => new { dataSet = n, resultSet = w });
                foreach (var row in data)
                {
                    Examine(new LinkedList<double>(row.dataSet));
                    layer.Delta(new LinkedList<double>(row.resultSet));
                }
            }
        }

        //
        public double Examine(LinkedList<double> signals)
        {
            layer.Run(signals);
            return layer.neurons[0].Axon.signal;
        }
    }
}