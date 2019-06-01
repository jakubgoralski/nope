using NeuralNetworkOnPaper.ArtificalBrain;
using NeuralNetworkOnPaper.BrainBooster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkOnPaper
{
    public class NetworkMadaline : Learn
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

        // 3 layers
        public void Configure(int[] neuronsAmount)
        {
            int i = 0;
            layers = new List<LayerMadaline>();
            foreach (LayerType type in Enum.GetValues(typeof(LayerType)))
            {
                LayerMadaline layer = new LayerMadaline();
                layer.Configure(neuronsAmount[i],
                                i == 0 ? 1 : neuronsAmount[i - 1],
                                type);
                i++;
                layers.Add(layer);
            }
        }

        //
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult, int epochAmount, LearningMethod method)
        {
            int iteration = 0;
            do
            {
                Shuffle(ref dataSet, ref expectedResult);

                var data = dataSet.Zip(expectedResult, (n, w) => new { dataSet = n, expectedResult = w });
                foreach (var row in data)
                {
                    Examine(new LinkedList<double>(row.dataSet));
                    Backpropagation(row.expectedResult); //online method of backpropagation
                }

                if (iteration > 5 && 0.5 > layers.Last().neurons.Last().Error)
                    break;

                if (iteration++ > epochAmount)
                    break;


            } while (true);
            Console.WriteLine($"Epochs: {iteration}");
        }

        //
        public LinkedList<double> Examine(LinkedList<double> signals)
        {
            foreach(LayerMadaline layer in layers)
                signals = layer.Run(new LinkedList<double>(signals));
            return signals;
        }

        //
        public void Backpropagation(LinkedList<double> expectedResults)
        {
            //compute new wages for output layer
            layers.Last().Delta(new LinkedList<double>(expectedResults));

            //compute new wages for hidden layer
            layers[1].Delta(new LinkedList<NeuronAdaline>(layers.Last().neurons));
        }

        //
        public void RecursiveLastSquares()
        {

        }

        //
        public void PrintLayers()
        {
            foreach (LayerMadaline layer in layers)
            {
                Console.Write("layer " + layer.LayerType.ToString() + ": ");
                int i = 1;
                foreach(NeuronAdaline neuron in layer.neurons)
                {
                    Console.Write($"neuron no {i++}: ");
                    int j = 1;
                    foreach(Dendrite synapse in neuron.Dendrites)
                    {
                        Console.WriteLine($"synapse no {j++}: I {synapse.SignalInput.ToString()} W: {synapse.Weight.ToString()} ");
                    }
                    Console.WriteLine($"| O {neuron.Axon.signal.ToString()} AO: {neuron.Axon.activatedSignal.ToString()}");

                }
                Console.WriteLine();
            }
        }
    }
}
