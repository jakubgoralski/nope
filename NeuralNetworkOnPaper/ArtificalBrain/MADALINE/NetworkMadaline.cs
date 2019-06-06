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
         * 
         * PROPERTIES
         * 
         */

        //
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

        // CURRENTLY ONLY ONE LAYER!!!
        public void Learn(List<LinkedList<double>> dataSet, List<LinkedList<double>> expectedResult, int epochAmount, LearningMethod method)
        {
            for (int i = 0; i < epochAmount; i++)
            {
                Shuffle(ref dataSet, ref expectedResult);

                var data = dataSet.Zip(expectedResult, (n, w) => new { dataSet = n, resultSet = w });
                foreach (var row in data)
                {
                    if (IsDelta(method))
                    {
                        Examine(new LinkedList<double>(row.dataSet));
                        Layers.Last().Delta(new LinkedList<double>(row.resultSet));
                    }
                    else if (IsMRII(method))
                    {
                        Console.WriteLine("MRII");
                        MRII(new LinkedList<double>(row.dataSet), new LinkedList<double>(row.resultSet));
                    }
                }
            }
        }

        public void MRII(LinkedList<double> dataSet, LinkedList<double> resultSet)
        {
            // Part 1: Count wrong outputs amount
            //int sum = 0,
            //    i   = 0;
            //foreach (double desired in resultSet)
            //    if (Layers.Last().Neurons[i++].Axon.activatedSignal != desired)
            //        sum++;
            Examine(new LinkedList<double>(dataSet));
            double originalOFValue = Layers.Last().ObjectiveFunction();

            // Part 5: 
            Layers.Reverse();
            foreach(LayerMadaline layer in Layers)
            {
                if (IsInputLayer(layer.LayerType))
                    break;

                // Part 2: Get minimum sum signal from some neuron of last layer (Minimal disturbance principle)
                List<int> previousChoosenIndexes = new List<int>();
                layer.PreviousNeurons = layer.Neurons;
                for (int j = 0; j < layer.Neurons.Count(); j++)
                {
                    int indexOfNeuronWithSmallestSum = 0;
                    int i = 0;
                    double smallestSum = 0;
                    foreach (NeuronAdaline neuron in layer.Neurons)
                    {
                        Console.Write("a");
                        if (smallestSum == 0 || (Math.Abs(neuron.Axon.signal) < smallestSum && !previousChoosenIndexes.Contains(i)))
                        {
                            indexOfNeuronWithSmallestSum = i;
                            smallestSum = neuron.Axon.signal;
                        }
                        i++;
                    }

                    // Part 3: Change wages in choosen neuron
                    layer.Neurons[indexOfNeuronWithSmallestSum].ChangeWages();

                    //Part 4: Exam and check if't better if not reset and try again if yes go ahead
                    Examine(new LinkedList<double>(dataSet));

                    if (originalOFValue > Layers.Last().ObjectiveFunction())
                        break;
                    else
                    {
                        layer.Neurons = layer.PreviousNeurons;
                        previousChoosenIndexes.Add(indexOfNeuronWithSmallestSum);
                    }
                }
            }
            Layers.Reverse();



        }

        //
        public LinkedList<double> Examine(LinkedList<double> signals)
        {
            foreach(LayerMadaline layer in Layers)
                signals = layer.Run(new LinkedList<double>(signals));
            return signals;
        }

        //
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