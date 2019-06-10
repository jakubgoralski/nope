using NeuralNetworkOnPaper.ArtificalBrain;
using System;
using System.Collections.Generic;

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

        //
        public List<NeuronAdaline> Neurons { get; set; }

        //
        public List<NeuronAdaline> PreviousNeurons { get; set; }

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
                    neuron.Run(DataSetInput);
                    DataSetOutput.AddLast(neuron.Axon.activatedSignal);
                }

            return DataSetOutput;
        }

        // use to learn output layer
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

        // Compute error value for neuron in hidden layer
        public void ComputeHiddenErrors(LinkedList<NeuronAdaline> outputLayer)
        {
            int i = 0;
            foreach (NeuronAdaline neuron in Neurons)
            {
                // compute error
                neuron.Error = 0;
                foreach (NeuronAdaline neuronOutput in outputLayer)
                {
                    neuron.Error += neuronOutput.Dendrites[i].Weight * neuronOutput.Error;
                }
                neuron.Error = neuron.Error * neuron.Axon.activatedSignal;
                i++;
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
