using System;

namespace NeuralNetworkOnPaper
{
    public class Synapse : Config
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        public double Weight { get; set; }
        public double SignalInput { get; set; }
        public double SignalOutput { get; set; }
        public bool IsInInputLayer { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor with configuration
        public Synapse(Random random, NeuronType neuronType, bool isSynapseInNeuronInInputLayer = false)
        {
            IsInInputLayer = isSynapseInNeuronInInputLayer;
            Weight = IsInInputLayer ? 1 : getInitialSynapseWeight(random, neuronType);
            SignalInput = 0;
            SignalOutput = 0;
        }

        // Returns output signal of dendrite
        public double Run(double signal)
        {
            SignalInput = signal;
            if (IsInInputLayer)
                SignalOutput = SignalInput;
            else
                SignalOutput = SignalInput * Weight;
            return SignalOutput;
        }
    }
}
