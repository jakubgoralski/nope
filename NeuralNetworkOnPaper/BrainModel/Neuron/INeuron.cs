using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel
{
    public interface INeuron
    {
        /*
         * PROPERTIES
         */

        //
        List<Synapse> Synapses { get; set; }

        //
        Axon Axon { get; set; }

        /*
         * METHODS
         */

        //
        void Configure(int synapsesAmount, bool isNeuronInInputLayer = false);

        //
        double Run(LinkedList<double> signals);
    }
}
