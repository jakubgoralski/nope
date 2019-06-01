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
        private List<LayerSigmoid> Layers { get; set; }

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
        public void Configure(int[] neuronsAmount, NeuronType neuronType = NeuronType.Bipolar)
        {
            Layers = new List<LayerSigmoid>();

            for (int i = 0; i < neuronsAmount.Length; i++)
            {
                LayerSigmoid layer = new LayerSigmoid();
                layer.Configure(neuronsAmount[i],
                                i == 0 ? 1 : neuronsAmount[i - 1],
                                GetLayerType(i, neuronsAmount.Length),
                                neuronType);
                Layers.Add(layer);
            }
        }

        //
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult, int epochAmount = -1, LearningMethod method = LearningMethod.BackpropagationOnline, bool verbose = false)
        {
            int iteration = 0;

            bool errorCondition = false;
            if (epochAmount < 1)
            {
                errorCondition = true;
                epochAmount = 0;
            }

            do
            {
                // Stop learning condition
                if (errorCondition)
                {
                    if (iteration++ != 0 && Layers.Last().ObjectiveFunction() < permittedError)
                        break;
                }
                else
                {
                    if (iteration++ > epochAmount)
                        break;
                }

                Shuffle(ref dataSet, ref expectedResult);

                var data = dataSet.Zip(expectedResult, (n, w) => new { dataSet = n, expectedResult = w });
                foreach (var row in data)
                {
                    Examine(new LinkedList<double>(row.dataSet));

                    if (IsOnline(method))
                        Backpropagation(row.expectedResult);
                }

                if (IsOffline(method))
                    Backpropagation(expectedResult.Last());
            } while (true);

           // if (verbose)
                Console.WriteLine($"Epochs number: {iteration}; Objective Function: {Layers.Last().ObjectiveFunction()}");
        }

        //
        public LinkedList<double> Examine(LinkedList<double> signals)
        {
            foreach(LayerSigmoid layer in Layers)
                signals = layer.Run(new LinkedList<double>(signals));

            return signals;
        }

        //
        public void Backpropagation(LinkedList<double> expectedResult)
        {
            Layers.Reverse(); // from end to beginning

            // Step 1: Count errors
            int i = 0; // means current index of output layer
            foreach (LayerSigmoid layer in Layers)
            {
                if (IsInputLayer(layer.LayerType))
                    break;

                if (IsOutputLayer(layer.LayerType))
                {
                    layer.ComputeOutputErrors(new LinkedList<double>(expectedResult));
                    continue;
                }

                layer.ComputeHiddenErrors(new LinkedList<NeuronSigmoid>(Layers[i++].Neurons));
            }

            // Step 2: Change wages
            foreach (LayerSigmoid layer in Layers)
            {
                if (IsInputLayer(layer.LayerType))
                    break;

                layer.ChangeWages();
            }

            Layers.Reverse(); // from beginning to end
        }

        //
        public void PrintLayers()
        {
            Console.WriteLine("+---------------------------------------------------------+");
            Console.WriteLine("|                NETWORK PREVIEW                          |");
            Console.WriteLine("+---------------------------------------------------------+");
            foreach (LayerSigmoid layer in Layers)
            {
                Console.WriteLine($"|                    {layer.LayerType.ToString().Substring(0, 2)} LAYER                             |");
                Console.WriteLine("+---------------------------------------------------------+");
                int i = 1;
                foreach(NeuronSigmoid neuron in layer.Neurons)
                {
                    Console.WriteLine($"| Neuron {i++}:                                               |");
                    int j = 1;
                    foreach(Dendrite synapse in neuron.Dendrites)
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
