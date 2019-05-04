using NeuralNetworkOnPaper.BrainBooster;
using NeuralNetworkOnPaper.BrainModel.Layer;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel.Network
{
    class NeuralNetworkLinear : Learn
    {
        public List<LayerLinear> layers { get; set; }

        public NeuralNetworkLinear(int layersAmount, int[] neuronsAmount)
        {
            for (int i = 0; i < layersAmount; i++)
                layers.Add(new LayerLinear(neuronsAmount[i], i == 0 ? 1 : neuronsAmount[i - 1], i == 0));
        }

        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> resultSet, int epochAmount)
        {
            for(int i = 0; i < epochAmount; i++)
            {
                Shuffle(ref dataSet,ref resultSet);
                int j = 0;
                foreach (LinkedList<double> signals in dataSet)
                {
                    LinkedList<double> currentUsedSignals = signals;
                    foreach (LayerLinear layer in layers)
                    {
                        currentUsedSignals = layer.Run(currentUsedSignals);
                    }

                    double costFunction = CostFunction(resultSet[j++], currentUsedSignals);
                }
            }
        }
    }
}
