using NeuralNetworkOnPaper.ArtificalBrain;
using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper
{
    //consider if not delete this class
    class LayerSigmoid : Layer
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        //
        public List<NeuronSigmoid> Neurons { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public LayerSigmoid()
        {

        }

        //
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount, LayerType layerType, NeuronType neuronType)
        {
            LayerType = layerType;
            base.Configure(LayerType);

            Neurons = new List<NeuronSigmoid>();

            Random random = new Random();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronSigmoid neuron = new NeuronSigmoid();
                neuron.Configure(previousLayerNeuronsAmount, LayerType, random, neuronType);
                Neurons.Add(neuron);
            }
        }

        //
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
                foreach (NeuronSigmoid neuron in Neurons)
                {
                    neuron.Run(DataSetInput);
                    DataSetOutput.AddLast(neuron.Axon.activatedSignal);
                }

            return DataSetOutput;
        }

        // Compute error value for neuron in output layer
        public void ComputeOutputErrors(LinkedList<double> expectedResults)
        {
            foreach (NeuronSigmoid neuron in Neurons)
            {
                neuron.Delta(expectedResults.First.Value);
                expectedResults.RemoveFirst();
            }
        }

        // Compute error value for neuron in hidden layer
        public void ComputeHiddenErrors(LinkedList<NeuronSigmoid> outputLayer)
        {
            int i = 0;
            foreach (NeuronSigmoid neuron in Neurons)
            {
                // compute error
                neuron.Error = 0;
                foreach (NeuronSigmoid neuronOutput in outputLayer)
                {
                    neuron.Error += neuronOutput.Dendrites[i].Weight * neuronOutput.Error;
                }
                neuron.Error = neuron.Error * neuron.Axon.activatedSignal;
                i++;
            }
        }

        // Runs chanhing wages functionality in neurons
        public void ChangeWages()
        {
            foreach (NeuronSigmoid neuron in Neurons)
                neuron.ChangeWages();
        }

        // Compute value of Objective Function
        public double ObjectiveFunction()
        {
            double sum = 0;
            foreach (NeuronSigmoid neuron in Neurons)
            {
                sum += Math.Pow(neuron.Error, 2);
            }

            return sum / 2;
        }
    }
}
