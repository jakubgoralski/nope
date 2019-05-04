using System;

namespace NeuralNetworkOnPaper.BrainBooster
{
    class Startup : Config
    {
        public static double getInitialSynapseWeight()
        {
            Random random = new Random();
            return random.NextDouble() * (synapseInitialWeightRange - (synapseInitialWeightRange * -1)) + (synapseInitialWeightRange * -1);
        }
    }
}
