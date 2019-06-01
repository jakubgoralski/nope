using NeuralNetworkOnPaper.BrainModel;

namespace NeuralNetworkOnPaper.ArtificalBrain
{
    public class NeuronAdaline : Neuron
    {
        /*
         * PROPERTIES
         */

        /*
         * METHODS
         */

        //
        public NeuronAdaline()
        {

        }

        //
        public void Delta()
        {
            foreach (Dendrite synapse in Dendrites)
            {
                synapse.Weight = synapse.Weight - learningRate * Error * synapse.SignalInput;
            }
            Bias.Weight = Bias.Weight - learningRate * Error;
        }
    }
}
