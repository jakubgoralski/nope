namespace NeuralNetworkOnPaper
{
    public class Config
    {
        /*
         * PROPERTIES
         */

        //
        public const double synapseInitialWeightRange = 0.1; // means from -0.1 to 0.1

        //
        public const double learningRate = 0.1;

        //
        public enum learningMethod
        {
            Delta,
            Backpropagation,
            RecursiveLeastSquares
        }

        //
        public enum weightsUpdate
        {
            Online,
            Offline
        }
        
        //
        public enum layerType
        {
            Input,
            Hidden,
            Output
        }

        /*
         * METHODS
         */

        //
        public bool isInputLayer(layerType layerType)
        {
            return layerType == layerType.Input;
        }

        //
        public bool isOutputLayer(layerType layerType)
        {
            return layerType == layerType.Output;
        }
    }
}
