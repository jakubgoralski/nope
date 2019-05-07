namespace NeuralNetworkOnPaper
{
    public class Config
    {
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
    }
}
