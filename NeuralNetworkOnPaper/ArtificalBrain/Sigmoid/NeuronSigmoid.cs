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
                synapse.Weight = synapse.Weight + learningRate * Error * synapse.SignalInput;
            }
            Bias.Weight = Bias.Weight + learningRate * Error;
        }
    }
}
