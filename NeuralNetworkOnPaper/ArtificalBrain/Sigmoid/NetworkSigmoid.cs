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
         * 
         * PROPERTIES
         * 
         */

        // List of 1 * input, n * hiddens, 1 * output layers
        private List<LayerSigmoid> layers { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public NetworkSigmoid()
        {

        }

        // Configuration, i.e. creation of new layers, neurons, dendrites and axons
        public void Configure(int[] neuronsAmount)
        {
            layers = new List<LayerSigmoid>();

            for (int i = 0; i < neuronsAmount.Length; i++)
            {
                LayerSigmoid layer = new LayerSigmoid();
                layer.Configure(neuronsAmount[i],
                                i == 0 ? 1 : neuronsAmount[i - 1],
                                GetLayerType(i, neuronsAmount.Length));
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
        public void PrintLayers()
        {
            Console.WriteLine("+---------------------------------------------------------+");
            Console.WriteLine("|                NETWORK PREVIEW                          |");
            Console.WriteLine("+---------------------------------------------------------+");
            foreach (LayerSigmoid layer in layers)
            {
                Console.WriteLine($"|                    {layer.LayerType.ToString().Substring(0, 2)} LAYER                             |");
                Console.WriteLine("+---------------------------------------------------------+");
                int i = 1;
                foreach(NeuronSigmoid neuron in layer.neurons)
                {
                    Console.WriteLine($"| Neuron {i++}:                                               |");
                    int j = 1;
                    foreach(Synapse synapse in neuron.Synapses)
                    {
                        Console.WriteLine($"| Syn. {j++} (I {String.Format("{0:00.000000}", synapse.SignalInput)} W: {String.Format("{0:00.000000}", synapse.Weight)});                      |");
                    }
                    if(! IsInputLayer(layer.LayerType))
                        Console.WriteLine($"| Bias (I {String.Format("{0:00.000000}", neuron.Bias.SignalInput)} W: {String.Format("{0:00.000000}", neuron.Bias.Weight)});                        |");

                    Console.WriteLine($"| Axon (O {String.Format("{0:00.000000}", neuron.Axon.signal)} AO: {String.Format("{0:00.000000}", neuron.Axon.activatedSignal)})                        |");
                    Console.WriteLine("+---------------------------------------------------------+");

                }
            }
            Console.WriteLine("|                   END PREVIEW                           |");
            Console.WriteLine("+---------------------------------------------------------+");
        }
    }
}
