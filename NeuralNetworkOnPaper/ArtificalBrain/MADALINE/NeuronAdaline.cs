namespace NeuralNetworkOnPaper.ArtificalBrain
{
    public class NeuronAdaline : Neuron
    {
        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public NeuronAdaline()
        {
            Error = 1;
        }

        // Represents computation combine with Delta algorithm
        public void ChangeWages()
        {
            // Compute wages of dendrites
            foreach (Dendrite dendrite in Dendrites)
            {
                dendrite.Weight = dendrite.Weight + learningRate * Error * dendrite.SignalInput;
            }

            // Compute wage of BIAS
            Bias.Weight = Bias.Weight + learningRate * Error;
        }
    }
}
