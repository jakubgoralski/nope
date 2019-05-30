using System;

namespace NeuralNetworkOnPaper
{
    public class Config
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // If neuron is unipolar then it's range from 0.001 to this value, if bipolar then real range is from -value to +value
        public const double synapseInitialWeightRange = 0.1; 

        // Defines how big changes of wages can be made during learning process
        public const double learningRate = 0.1;

        // Implemented types of learning neural networks
        public enum LearningMethod
        {
            Delta,
            BackpropagationOnline,
            BackpropagationOffline
        }
        
        // Network contains only three types of layers, Input is without biases and weights, others are normal
        public enum LayerType
        {
            Input,
            Hidden,
            Output
        }

        // Unipolar means operating between values 0 and 1, bipolar between -1 and 1
        public enum NeuronType
        {
            Unipolar,
            Bipolar
        }

        /*
         * 
         * METHODS
         * 
         */

        // Returns random weight depending on range and neuron type
        public double getInitialSynapseWeight(Random random, NeuronType neuronType = NeuronType.Bipolar)
        {
            if(IsUnipolar(neuronType))
                return (random.NextDouble() * (synapseInitialWeightRange - 0.001) + 0.001);
            else
                return (random.NextDouble() * (synapseInitialWeightRange + synapseInitialWeightRange) + synapseInitialWeightRange);
        }

        // Returns true if this layer is input
        public bool IsInputLayer(LayerType layerType)
        {
            return layerType == LayerType.Input;
        }

        // Returns true if this layer is output
        public bool IsOutputLayer(LayerType layerType)
        {
            return layerType == LayerType.Output;
        }

        // Returns true if this layer is output
        public bool IsUnipolar(NeuronType neuronType)
        {
            return neuronType == NeuronType.Unipolar;
        }
    }
}
