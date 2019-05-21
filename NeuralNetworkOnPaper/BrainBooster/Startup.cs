using System;

namespace NeuralNetworkOnPaper.BrainBooster
{
    class Startup : Config
    {
        public static double getInitialSynapseWeight(Random random)
        {                
            return 10*(random.NextDouble() * (synapseInitialWeightRange - (synapseInitialWeightRange * -1)) + (synapseInitialWeightRange * -1));
        }
    }
}
