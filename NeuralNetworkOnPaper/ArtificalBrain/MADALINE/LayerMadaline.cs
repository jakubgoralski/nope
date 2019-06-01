using NeuralNetworkOnPaper.ArtificalBrain;
using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper
{
    //consider if not delete this class
    class LayerMadaline : Layer
    {
        /*
         * PROPERTIES
         */

        //
        public List<NeuronAdaline> neurons { get; set; }

        /*
         * METHODS
         */

        //
        public LayerMadaline()
        {

        }

        //
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount, LayerType layerType)
        {
            LayerType = layerType;
            base.Configure(LayerType);
            neurons = new List<NeuronAdaline>();
            Random random = new Random();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronAdaline neuron = new NeuronAdaline();
                neuron.Configure(previousLayerNeuronsAmount, LayerType, random);
                neurons.Add(neuron);
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
                    neurons[i].Run(temp);
                    DataSetOutput.AddLast(neurons[i++].Axon.activatedSignal);
                }
            }
            else
                foreach (NeuronAdaline neuron in neurons)
                {
                    neuron.Run(DataSetInput);
                    DataSetOutput.AddLast(neuron.Axon.activatedSignal);
                }

            return DataSetOutput;
        }

        // use to learn output layer
        public void Delta(LinkedList<double> expectedResults)
        {
            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Error = Math.Pow(expectedResults.First.Value - neuron.Axon.signal,2)/2; // objective function: error = expected result - given result
                expectedResults.RemoveFirst();
                neuron.Delta();
            }
        }

        // use to learn hidden layer
        public void Delta(LinkedList<NeuronAdaline> outputLayer)
        {
            int i = 0;
            foreach (NeuronAdaline neuron in neurons)
            {
                // compute error
                neuron.Error = 0;
                foreach(NeuronAdaline neuronOutput in outputLayer)
                {
                    neuron.Error += neuronOutput.Dendrites[i].Weight * neuronOutput.Error;
                }
                i++;
                //compute new wages
                neuron.Delta();
            }
        }
    }
}
