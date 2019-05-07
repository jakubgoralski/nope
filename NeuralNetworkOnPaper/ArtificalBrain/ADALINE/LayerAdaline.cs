﻿using NeuralNetworkOnPaper.BrainModel.Layer;
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
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount)
        {
            base.Configure();
            neurons = new List<NeuronAdaline>();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronAdaline neuron = new NeuronAdaline();
                neuron.Configure(previousLayerNeuronsAmount);
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
        public void Delta(LinkedList<double> resultSet)
        {
            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Delta(resultSet.First.Value);
                resultSet.RemoveFirst();
            }
        }
    }
}