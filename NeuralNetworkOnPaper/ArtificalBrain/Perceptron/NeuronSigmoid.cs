using System;
using static NeuralNetworkOnPaper.BrainBooster.Config;

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
            double temp;

            // Multiply previously computed neuron with derivative
            Error = Error * Derivative();

            // Compute wages of dendrites
            foreach (Dendrite dendrite in Dendrites)
            {
                temp = dendrite.Weight;
                dendrite.Weight = dendrite.Weight + learningRate * Error * dendrite.SignalInput + alpha * (dendrite.Weight - dendrite.LastWeight);
                dendrite.LastWeight = temp;
            }

            // Compute wage of BIAS
            temp = Bias.Weight;
            Bias.Weight = Bias.Weight + learningRate * Error + alpha * (Bias.Weight - Bias.LastWeight);
            Bias.LastWeight = temp;
        }

        // Compute Error using Delta method
        public void Delta(double expectedResult)
        {
            Error = expectedResult - Axon.activatedSignal;
        }

        public override double ActivationFunction(double sum) 
        {
            if (IsUnipolar(NeuronType))
            {
                return 1.0 / (1.0 + Math.Pow(Math.E, -1 * sum)); // sigmoid
            }
            else // Is Bipolar
            {
                double e = Math.Pow(Math.E, 2*sum);

                return (e-1)/(e+1); // tangens hiperbolic
            }
        }

        // Used for computing neuron error value
        public double Derivative()
        {
            if (IsUnipolar(NeuronType))
            {
                return Axon.activatedSignal * (1.0 - Axon.activatedSignal);
            }
            else // Is Bipolar
            {
                return 1.0 - Math.Pow(ActivationFunction(Axon.activatedSignal), 2);
            }
        }
    }
}
