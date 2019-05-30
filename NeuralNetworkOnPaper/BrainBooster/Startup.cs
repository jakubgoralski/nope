using System;

namespace NeuralNetworkOnPaper.BrainBooster
{
    class Startup : Config
    {
        public static double getInitialSynapseWeight(Random random)
        {                
            return (random.NextDouble() * (0.9 - 0.1) + 0.1);
        }
    }
}
