using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.ArtificalBrain.ADALINE
{
    class LayerAdaline : Layer
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
        public LayerAdaline()
        {

        }

        //
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount, NeuronType neuronType)
        {
            base.Configure();
            neurons = new List<NeuronAdaline>();
            Random random = new Random();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronAdaline neuron = new NeuronAdaline();
                neuron.Configure(previousLayerNeuronsAmount, LayerType.Output, random, neuronType);
                neurons.Add(neuron);
            }
        }

        //
        public LinkedList<double> Run(LinkedList<double> dataSet)
        {
            DataSetInput = dataSet;
            DataSetOutput.Clear();

            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Run(DataSetInput);
                DataSetOutput.AddLast(neuron.Axon.signal);
            }

            return DataSetOutput;
        }

        //
        public void Delta(LinkedList<double> expectedResults)
        {
            foreach (NeuronAdaline neuron in neurons)
            {
                // Compute error
                neuron.Delta(expectedResults.First.Value);
                expectedResults.RemoveFirst();

                // Compute new wages
                neuron.ChangeWages();
            }
        }
    }
}