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
            foreach (Synapse synapse in Synapses)
            {
                synapse.weight = synapse.weight + learningRate * error * synapse.signalInput;
            }
            Bias.weight = Bias.weight + learningRate * error;
        }
    }
}
