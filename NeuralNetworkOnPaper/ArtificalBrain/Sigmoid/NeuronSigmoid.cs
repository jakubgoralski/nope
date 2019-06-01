namespace NeuralNetworkOnPaper.ArtificalBrain
{
    class NeuronSigmoid : Neuron
    {
        // Constructor
        public NeuronSigmoid()
        {

        }

        // Represents computation combine with Delta algorithm, Momentum Method and given error
        public void ChangeWages()
        {
            // Compute wages of dendrites
            foreach (Dendrite dendrite in Dendrites)
            {
                dendrite.PenultimateWeight = dendrite.Weight;
                dendrite.Weight = dendrite.Weight + learningRate * Error * dendrite.SignalInput + alpha * (dendrite.Weight - dendrite.LastWeight);
                dendrite.LastWeight = dendrite.PenultimateWeight;
            }

            // Compute wage of BIAS
            Bias.PenultimateWeight = Bias.Weight;
            Bias.Weight = Bias.Weight + learningRate * Error + alpha * (Bias.Weight - Bias.LastWeight);
            Bias.LastWeight = Bias.PenultimateWeight;
        }
    }
}
