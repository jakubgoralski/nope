using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel
{
    public class Neuron : Config, INeuron
    {
        public List<Synapse> Synapses { get; set; }
        public Synapse Bias { get; set; }
        public Axon Axon { get; set; }

        //
        public bool isInputLayer { get; set; }

        //
        public double error { get; set; }
        
        //
        public Neuron()
        {

        }

        //
        public void Configure(int SynapsesAmount, bool isNeuronInInputLayer = false)
        {
            Synapses = new List<Synapse>();
            Bias = new Synapse();
            isInputLayer = isNeuronInInputLayer;
            SynapsesAmount = isInputLayer ? 1 : SynapsesAmount;
            for (int i = 0; i < SynapsesAmount; i++)
                Synapses.Add(new Synapse(isInputLayer));
            Axon = new Axon();
        }

        //
        public double Run(LinkedList<double> signals)
        {
            if (isInputLayer)
                RunInput(signals.First.Value);
            else
                RunNeuron(signals);
            return Axon.signal;
        }

        //
        public void RunInput(double signal)
        {
            Axon.signal = Synapses[0].Run(signal);
        }

        //
        public void RunNeuron(LinkedList<double> signals)
        {
            int i = 0;
            Axon.signal = 0;
            foreach(double signal in signals)
            {
                Axon.signal += Synapses[i++].Run(signal);
            }
            Axon.signal += Bias.Run(1);
        }
    }
}
