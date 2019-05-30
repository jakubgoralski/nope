using NeuralNetworkOnPaper.ArtificalBrain;
using NeuralNetworkOnPaper.BrainBooster;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuralNetworkOnPaper
{
    public class NetworkSigmoid : Learn
    {
        /*
         * PROPERTIES
         */

        public double WartoscFunkcjiCelu { get; set; }
        //
        private List<LayerSigmoid> layers { get; set; }

        /*
         * METHODS
         */

        //
        public NetworkSigmoid()
        {

        }

        public void FunkcjaCelu()
        {
            double sum = 0;
            foreach (NeuronSigmoid neuron in layers.Last().neurons)
            {
                sum += Math.Pow(neuron.error, 2);
                WartoscFunkcjiCelu = sum / 2;
            }
        }

        // 3 layers
        public void Configure(int[] neuronsAmount)
        {
            int i = 0;
            layers = new List<LayerSigmoid>();
            foreach (layerType type in Enum.GetValues(typeof(layerType)))
            {
                LayerSigmoid layer = new LayerSigmoid();
                layer.Configure(neuronsAmount[i],
                                i == 0 ? 1 : neuronsAmount[i - 1],
                                type);
                i++;
                layers.Add(layer);
            }
        }

        //
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult, int epochAmount, learningMethod method)
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

                if (iteration++ > epochAmount)
                    break;


            } while (true);
            Console.WriteLine($"Epochs: {iteration}");
        }

        //
        public LinkedList<double> Examine(LinkedList<double> signals)
        {
            foreach(LayerSigmoid layer in layers)
                signals = layer.Run(new LinkedList<double>(signals));
            return signals;
        }

        //
        public void Backpropagation(LinkedList<double> expectedResults)
        {
            layers.Last().ComputeOutputErrors(new LinkedList<double>(expectedResults));


            //minimalizacja funkcji celu
            layers[1].ComputeHiddenErrors(new LinkedList<NeuronSigmoid>(layers.Last().neurons));

            layers.Last().Delta();
            layers[1].Delta();
        }

        //
        public void RecursiveLastSquares()
        {

        }

        //
        public void PrintLayers()
        {
            foreach (LayerSigmoid layer in layers)
            {
                Console.Write("layer " + layer.LayerType.ToString() + ": ");
                int i = 1;
                foreach(NeuronSigmoid neuron in layer.neurons)
                {
                    Console.Write($"neuron no {i++}: ");
                    int j = 1;
                    foreach(Synapse synapse in neuron.Synapses)
                    {
                        Console.WriteLine($"synapse no {j++}: I {synapse.signalInput.ToString()} W: {synapse.weight.ToString()} ");
                    }
                    if(! isInputLayer(layer.LayerType))
                        Console.WriteLine($"BIAS: I {neuron.Bias.signalInput.ToString()} W: {neuron.Bias.weight.ToString()} ");

                    Console.WriteLine($"| O {neuron.Axon.signal.ToString()} AO: {neuron.Axon.activatedSignal.ToString()}");

                }
                Console.WriteLine();
            }
        }
    }
}
