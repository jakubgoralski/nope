using NeuralNetworkOnPaper.BrainModel;
using NeuralNetworkOnPaper.BrainModel.Layer;

namespace NeuralNetworkOnPaper.ArtificalBrain
{
    public class NeuronAdaline : Neuron
    {
        private Layer layer { get; set; }

        public NeuronAdaline()
        {

        }

        public void Delta(double expectedResult)
        {
            error = expectedResult - Axon.signal;
            foreach (Synapse synapse in Synapses)
            {
                synapse.weight = synapse.weight + learningRate * error * synapse.signalInput;
            }
            Bias.weight = Bias.weight + learningRate * error;
        }
    }
}
