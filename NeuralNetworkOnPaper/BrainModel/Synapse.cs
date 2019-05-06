using NeuralNetworkOnPaper.BrainBooster;

namespace NeuralNetworkOnPaper
{
    public class Synapse
    {
        public double weight { get; set; }
        public double signalInput { get; set; }
        public double signalOutput { get; set; }
        public bool isInputLayer { get; set; }

        public Synapse(bool isSynapseInNeuronInInputLayer = false)
        {
            isInputLayer = isSynapseInNeuronInInputLayer;
            weight = isInputLayer ? 1 : Startup.getInitialSynapseWeight();
            signalInput = 0;
            signalOutput = 0;
        }

        public double Run(double signal)
        {
            signalInput = signal;
            if (isInputLayer)
                signalOutput = signalInput;
            else
                signalOutput = signalInput * weight;
            return signalOutput;
        }
    }
}
