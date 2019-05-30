namespace NeuralNetworkOnPaper.ArtificalBrain
{
    //
    class NeuronSigmoid : Neuron
    {
        //
        public NeuronSigmoid()
        {

        }

        //
        public void ChangeWages()
        {
            foreach (Synapse synapse in Synapses)
            {
                synapse.weight = synapse.weight + learningRate * error * synapse.signalInput;
            }
            Bias.weight = Bias.weight + learningRate * error;
        }
    }
}
