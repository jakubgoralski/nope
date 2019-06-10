using NeuralNetworkOnPaper.ArtificalBrain;
using NeuralNetworkOnPaper.BrainBooster;
using System;
using System.Collections.Generic;
using System.Linq;
using static NeuralNetworkOnPaper.BrainBooster.Config;

namespace NeuralNetworkOnPaper
{
    public class NetworkMadaline : Learn
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // List of all Madaline layers
        private List<LayerMadaline> Layers { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public NetworkMadaline()
        {

        }

        // Configuration, i.e. creation of new layers, neurons, dendrites and axons
        public void Configure(int[] neuronsAmount, NeuronType neuronType = NeuronType.Bipolar, bool verbose = false)
        {
            Layers = new List<LayerMadaline>();

            for (int i = 0; i < neuronsAmount.Length; i++)
            {
                LayerMadaline layer = new LayerMadaline();
                layer.Configure(neuronsAmount[i],
                                i == 0 ? 1 : neuronsAmount[i - 1],
                                GetLayerType(i, neuronsAmount.Length),
                                neuronType);
                Layers.Add(layer);
            }

            if (verbose)
                PrintLayers();
        }

        // Running network with learning part
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult, int epochAmount, LearningMethod method, bool verbose = false)
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

                if (IsDelta(method))
                    Delta(dataSet, expectedResult);
                else if (IsMRII(method))
                    MRII(dataSet, expectedResult);
                else
                    break; // no other learning method implemented for MADALINE
            } while (true);

            if (verbose)
            {
                Console.WriteLine("Epochs: " + iteration.ToString());
                PrintLayers();
            }
        }

        // Implementation of learning Delta Rule
        private void Delta(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult)
        {
            var data = dataSet.Zip(expectedResult, (n, w) => new { dataSet = n, resultSet = w });
            foreach (var row in data)
            {
                Examine(new LinkedList<double>(row.dataSet));
                Layers.Last().Delta(new LinkedList<double>(row.resultSet));
            }
        }

        // Implementation of learning Madaline Rule II
        private void MRII(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult)
        {
            var data = dataSet.Zip(expectedResult, (n, w) => new { dataSet = n, resultSet = w });
            bool start = true;
            foreach (var row in data)
            {
                // MRII Step 1
                Examine(new LinkedList<double>(row.dataSet));

                // MRII Step 2
                Layers.Last().ComputeOutputErrors(new LinkedList<double>(row.resultSet));
                double lastError = Layers.Last().ObjectiveFunction();
                if (start || lastError >= permittedError)
                {
                    start = false;
                    // MRII Step 3
                    foreach (LayerMadaline layer in Layers)
                    {
                        if (IsInputLayer(layer.LayerType))
                            continue;

                        // MRII Step 3.1
                        LayerMadaline sortedLayer = new LayerMadaline();
                        sortedLayer.Neurons = layer.Neurons.OrderBy(n => Math.Abs(n.Axon.signal)).ToList();
                        foreach (NeuronAdaline neuron in sortedLayer.Neurons)
                        {
                            //MRII Step 3.2
                            layer.Neurons[neuron.OriginalIndex].Axon.signal = layer.Neurons[neuron.OriginalIndex].Axon.signal * -1;
                            layer.Neurons[neuron.OriginalIndex].freezed = true;

                            // Examine network again
                            Examine(new LinkedList<double>(row.dataSet));

                            layer.Neurons[neuron.OriginalIndex].freezed = false;


                            Layers.Last().ComputeOutputErrors(new LinkedList<double>(row.resultSet));
                            if (Layers.Last().ObjectiveFunction() < lastError)
                            {
                                // Change is successfull, so then change the weights
                                layer.Neurons[neuron.OriginalIndex].ChangeWagesMRII();
                                continue;
                            }
                            // Change is unsuccessfull, so go ahead
                        }
                    }
                }
            }
        }

        // Running network without learning part
        public LinkedList<double> Examine(LinkedList<double> signals)
        {
            foreach(LayerMadaline layer in Layers)
                signals = layer.Run(new LinkedList<double>(signals));
            return signals;
        }

        // Helps to see how looks wages and signals inside the network
        public void PrintLayers()
        {
            foreach (LayerMadaline layer in Layers)
            {
                Console.Write("layer " + layer.LayerType.ToString() + ": ");
                int i = 1;
                foreach(NeuronAdaline neuron in layer.Neurons)
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