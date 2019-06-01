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

        // Current weight of a synapse
        public double Weight { get; set; }
        
        // It's used for momentum method
        public double LastWeight { get; set; }

        // It's used to properly count LastWeight value
        public double PenultimateWeight { get; set; }

        // Represents singal given from previous neuron or data set
        public double SignalInput { get; set; }

        // It's equal to SignalInput*Weight
        public double SignalOutput { get; set; }

        // Informs about position in Input Layer
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
