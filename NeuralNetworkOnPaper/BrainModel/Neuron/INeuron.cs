using NeuralNetworkOnPaper.BrainModel;
using System;
using System.Collections.Generic;
using static NeuralNetworkOnPaper.Config;

namespace NeuralNetworkOnPaper
{
    public interface INeuron
    {
        /*
         * PROPERTIES
         */

        //
        List<Synapse> Synapses { get; set; }

        //
        Synapse Bias { get; set; }

        //
        Axon Axon { get; set; }

        /*
         * METHODS
         */

        //
        void Configure(int synapsesAmount, layerType layerType, Random random);

        //
        void Run(LinkedList<double> signals);

        //
        double ActivationFunction(double sum);
    }
}
