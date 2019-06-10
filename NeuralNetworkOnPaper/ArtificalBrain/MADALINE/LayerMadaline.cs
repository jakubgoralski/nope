using NeuralNetworkOnPaper.ArtificalBrain;
using System;
using System.Collections.Generic;
using static NeuralNetworkOnPaper.BrainBooster.Config;

namespace NeuralNetworkOnPaper
{
    //consider if not delete this class
    class LayerMadaline : Layer
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // List of all neurons in this layer
        public List<NeuronAdaline> Neurons { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public LayerMadaline()
        {

        }

        // Configure: create neurons etc...
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount, LayerType layerType, NeuronType neuronType)
        {
            LayerType = layerType;
            base.Configure(LayerType);
            Neurons = new List<NeuronAdaline>();
            Random random = new Random();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronAdaline neuron = new NeuronAdaline();
                neuron.Configure(previousLayerNeuronsAmount, LayerType, random, neuronType);
                neuron.OriginalIndex = i;
                Neurons.Add(neuron);
            }
        }

        // Fire all neurons in this layer with received data
        public LinkedList<double> Run(LinkedList<double> dataSet)
        {
            DataSetInput = dataSet;
            DataSetOutput.Clear();

            if (IsInputLayer(LayerType))
            {
                int i = 0;
                foreach (double data in dataSet)
                {
                    LinkedList<double> temp = new LinkedList<double>();
                    temp.AddFirst(data);
                    Neurons[i].Run(temp);
                    DataSetOutput.AddLast(Neurons[i++].Axon.activatedSignal);
                }
            }
            else
                foreach (NeuronAdaline neuron in Neurons)
                {
                    if (! neuron.freezed)
                        neuron.Run(DataSetInput);
                    DataSetOutput.AddLast(neuron.Axon.activatedSignal);
                }

            return DataSetOutput;
        }

        // Use to learn output layer
        public void Delta(LinkedList<double> expectedResults)
        {
            foreach (NeuronAdaline neuron in Neurons)
            {
                // Compute error
                neuron.Delta(expectedResults.First.Value);
                expectedResults.RemoveFirst();

                // Compute new wages
                neuron.ChangeWagesSingleNeuron();
            }
        }

        // Compute error value for neuron in output layer
        public void ComputeOutputErrors(LinkedList<double> expectedResults)
        {
            foreach (NeuronAdaline neuron in Neurons)
            {
                neuron.Delta(expectedResults.First.Value);
                expectedResults.RemoveFirst();
            }
        }

        // Compute value of Objective Function
        public double ObjectiveFunction()
        {
            double sum = 0;
            foreach (NeuronAdaline neuron in Neurons)
            {
                sum += Math.Pow(neuron.Error, 2);
            }

            return sum / 2;
        }
    }
}
