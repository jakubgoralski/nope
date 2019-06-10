using System;

namespace NeuralNetworkOnPaper.BrainBooster
{
    public static class Config
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // If neuron is unipolar then it's range from 0.001 to this value, if bipolar then real range is from -value to +value
        public static double dendriteInitialWeightRange = 0.1;

        // Defines how big changes of wages can be made during learning process
        public static double learningRate = 0.1;

        // It's a parameter to  count a momentum
        public static double alpha = 0.9;

        // If epoch amount is not defined then learning algoritm run until Objective Function value is less than this value
        public static double permittedError = 0.00000001;

        // Implemented types of learning neural networks
        public enum LearningMethod
        {
            Delta,
            MadalineRuleII,
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
        public static double getInitialDendriteWeight(Random random, NeuronType neuronType)
        {
            if (IsUnipolar(neuronType))
                return (random.NextDouble() * (dendriteInitialWeightRange - 0.001) + 0.001);
            else
            {
                double minimum = -1 * dendriteInitialWeightRange;
                return random.NextDouble() * (dendriteInitialWeightRange - minimum) + minimum;
            }
        }

        // Returns true if this layer is input
        public static bool IsInputLayer(LayerType layerType)
        {
            return layerType == LayerType.Input;
        }

        // Returns true if this layer is output
        public static bool IsOutputLayer(LayerType layerType)
        {
            return layerType == LayerType.Output;
        }

        // Returns true if this layer is output
        public static bool IsUnipolar(NeuronType neuronType)
        {
            return neuronType == NeuronType.Unipolar;
        }

        // Returns type of layer given in position order
        public static LayerType GetLayerType(int currentLayerNumber, int layersAmount)
        {
            if (currentLayerNumber == 0)
                return LayerType.Input;
            else if (currentLayerNumber == layersAmount - 1)
                return LayerType.Output;
            else
                return LayerType.Hidden;
        }

        // Returns true if method is Backpropagation online
        public static bool IsOnline(LearningMethod learningMethod)
        {
            return learningMethod == LearningMethod.BackpropagationOnline;
        }

        // Returns true if method is Backpropagation offline
        public static bool IsOffline(LearningMethod learningMethod)
        {
            return learningMethod == LearningMethod.BackpropagationOffline;
        }

        // Returns true if method is Delta
        public static bool IsDelta(LearningMethod learningMethod)
        {
            return learningMethod == LearningMethod.Delta;
        }

        // Returns true if method is Madaline Rule II
        public static bool IsMRII(LearningMethod learningMethod)
        {
            return learningMethod == LearningMethod.MadalineRuleII;
        }
    }
}
