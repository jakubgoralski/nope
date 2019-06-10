using System;

namespace NeuralNetworkOnPaper.ArtificalBrain
{
    public class NeuronAdaline : Neuron
    {
        /*
         * 
         * METHODS
         * 
         */

        public int OriginalIndex { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public NeuronAdaline()
        {
            Error = 1;
        }

        // Represents computation combine with Delta algorithm
        public void ChangeWagesSingleNeuron()
        {
            // Compute wages of dendrites
            foreach (Dendrite dendrite in Dendrites)
            {
                dendrite.Weight = dendrite.Weight + learningRate * Error * dendrite.SignalInput;
            }

            // Compute wage of BIAS
            Bias.Weight = Bias.Weight + learningRate * Error;
        }

        //
        public void ChangeWagesMRII()
        {
            // Stash actual output values which will be changed during that process
            double stashAxonSignal = Axon.signal;
            double stashAxonActivatedSignal = Axon.activatedSignal;

            bool isDesiredOutputValuePositive = stashAxonActivatedSignal > 0 ? true : false;

            // Compute new wages
            Random random = new Random();
            bool isDesiredChangeAchieved = false;
            do
            {
                foreach (Dendrite dendrite in Dendrites)
                {
                    dendrite.Weight = dendrite.Weight + learningRate * getInitialDendriteWeight(random, NeuronType);
                }
                Bias.Weight = Bias.Weight + learningRate * getInitialDendriteWeight(random, NeuronType);

                // Check if output is desired
                if (isDesiredOutputValuePositive)
                {
                    if (Axon.activatedSignal > 0)
                        isDesiredChangeAchieved = true;
                }
                else
                {
                    if (Axon.activatedSignal < 0)
                        isDesiredChangeAchieved = true;
                }
            } while (!isDesiredChangeAchieved);

            // Revert original output values
            Axon.signal = stashAxonSignal;
            Axon.activatedSignal = stashAxonActivatedSignal;
        }
    }
}
