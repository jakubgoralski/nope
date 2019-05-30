using NeuralNetworkOnPaper.ArtificalBrain;
using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper
{
    //consider if not delete this class
    class LayerSigmoid : Layer
    {
        /*
         * PROPERTIES
         */

        //
        public List<NeuronSigmoid> neurons { get; set; }

        /*
         * METHODS
         */

        //
        public LayerSigmoid()
        {

        }

        //
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount, layerType layerType)
        {
            LayerType = layerType;
            base.Configure(LayerType);
            neurons = new List<NeuronSigmoid>();
            Random random = new Random();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronSigmoid neuron = new NeuronSigmoid();
                neuron.Configure(previousLayerNeuronsAmount, LayerType, random);
                neurons.Add(neuron);
            }
        }

        //
        public LinkedList<double> Run(LinkedList<double> dataSet)
        {
            DataSetInput = dataSet;
            DataSetOutput.Clear();

            if (isInputLayer(LayerType))
            {
                int i = 0;
                foreach (double data in dataSet)
                {
                    LinkedList<double> temp = new LinkedList<double>();
                    temp.AddFirst(data);
                    neurons[i].Run(temp);
                    DataSetOutput.AddLast(neurons[i++].Axon.activatedSignal);
                }
            }
            else
                foreach (NeuronSigmoid neuron in neurons)
                {
                    neuron.Run(DataSetInput);
                    DataSetOutput.AddLast(neuron.Axon.activatedSignal);
                }

            return DataSetOutput;
        }

        // use to learn output layer
        public void ComputeOutputErrors(LinkedList<double> expectedResults)
        {
            foreach (NeuronSigmoid neuron in neurons)
            {
                neuron.error = expectedResults.First.Value - neuron.Axon.activatedSignal;
                expectedResults.RemoveFirst();
            }
        }

        // use to learn hidden layer
        public void ComputeHiddenErrors(LinkedList<NeuronSigmoid> outputLayer)
        {
            int i = 0;
            foreach (NeuronSigmoid neuron in neurons)
            {
                // compute error
                neuron.error = 0;
                foreach (NeuronSigmoid neuronOutput in outputLayer)
                {
                    neuron.error += neuronOutput.Synapses[i].weight * neuronOutput.error;
                }
                neuron.error = neuron.error * neuron.Axon.activatedSignal;
                i++;
            }
        }

        // use to learn hidden layer
        public void Delta()
        {
            int i = 0;
            foreach (NeuronSigmoid neuron in neurons)
            {
                neuron.error = neuron.error * (neuron.Axon.activatedSignal * (1.0 - neuron.Axon.activatedSignal));
                i++;
                //compute new wages
                neuron.ChangeWages();
            }
        }
    }
}
