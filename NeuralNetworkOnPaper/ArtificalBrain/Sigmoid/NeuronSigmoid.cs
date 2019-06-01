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
                synapse.PenultimateWeight = synapse.Weight;
                synapse.Weight = synapse.Weight + learningRate * Error * synapse.SignalInput + alpha * (synapse.Weight - synapse.LastWeight);
                synapse.LastWeight = synapse.PenultimateWeight;
            }
            Bias.PenultimateWeight = Bias.Weight;
            Bias.Weight = Bias.Weight + learningRate * Error + alpha * (Bias.Weight - Bias.LastWeight);
            Bias.LastWeight = Bias.PenultimateWeight;
        }
    }
}
