using System;
using static NeuralNetworkOnPaper.BrainBooster.Config;

namespace NeuralNetworkOnPaper
{
    public class Dendrite
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // Current weight of a dendrite
        public double Weight { get; set; }
        
        // It's used for momentum method
        public double LastWeight { get; set; }

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
        public Dendrite(Random random, NeuronType neuronType, bool isSynapseInNeuronInInputLayer = false)
        {
            IsInInputLayer = isSynapseInNeuronInInputLayer;
            Weight = IsInInputLayer ? 1 : getInitialDendriteWeight(random, neuronType);
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
