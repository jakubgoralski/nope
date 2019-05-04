using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel
{
    class NeuronLinear
    {
        public List<Synapse> synapses { get; set; }
        public Axon axon { get; set; }
        public bool isInputLayer { get; set; }

        public NeuronLinear(int synapsesAmount, bool isNeuronInInputLayer = false)
        {
            isInputLayer = isNeuronInInputLayer;
            synapsesAmount = isInputLayer ? 1 : synapsesAmount;
            for (int i = 0; i < synapsesAmount; i++)
                synapses.Add(new Synapse(isInputLayer));
            axon = new Axon();
        }

        public double Run(LinkedList<double> signals)
        {
            if (isInputLayer)
                RunInput(signals.First.Value);
            else
                RunNeuron(signals);
            return axon.signal;
        }

        public void RunInput(double signal)
        {
            axon.signal = synapses[0].Run(signal);
        }

        public void RunNeuron(LinkedList<double> signals)
        {
            int i = 0;
            axon.signal = 0;
            foreach(double signal in signals)
            {
                axon.signal += synapses[i++].Run(signal);
            }
        }

        public void Delta(double expectedResult)
        {

        }
    }
}
