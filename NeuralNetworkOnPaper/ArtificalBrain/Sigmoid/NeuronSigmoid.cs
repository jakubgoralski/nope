using System;

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
            // Multiply previously computed neuron with derivative
            Error = Error * Derivative();

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

        //
        public override double ActivationFunction(double sum) // means tangens hiperbolic
        {
            if (IsUnipolar(NeuronType))
            {
                return 1.0 / (1.0 + Math.Pow(Math.E, -1 * sum));
            }
            else // Is Bipolar
            {
                double e = Math.Pow(Math.E, 2*sum);

                return (e-1)/(e+1);
            }
        }

        //
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
