using NeuralNetworkOnPaper.BrainModel;
using System;
using System.Collections.Generic;
using static NeuralNetworkOnPaper.BrainBooster.Config;

namespace NeuralNetworkOnPaper
{
    public interface INeuron
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // Infors in which layer neuron is
        LayerType LayerType { get; set; }

        // Informs which type neuron is
        NeuronType NeuronType { get; set; }

        // Represents inputs of neuron with wages
        List<Dendrite> Dendrites { get; set; }

        // Is using as one synapse to move activation function in a chart
        Dendrite Bias { get; set; }

        // Represents one output of neuron
        Axon Axon { get; set; }

        // Value of neuron output error
        double Error { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Sets up all inputs, outputs and other settings of this neuron
        void Configure(int dendritesAmount, LayerType layerType, Random random, NeuronType neuronType);

        // Returns calculation of activation function of this neuron
        void Run(LinkedList<double> signals);

        // Returns output signal from last neuron block
        double ActivationFunction(double sum);
    }
}
