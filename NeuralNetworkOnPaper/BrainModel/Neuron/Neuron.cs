using NeuralNetworkOnPaper.BrainModel;
using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper
{
    public class Neuron : Config, INeuron
    {
        public List<Synapse> Synapses { get; set; }
        public Synapse Bias { get; set; }
        public Axon Axon { get; set; }

        //
        public layerType LayerType { get; set; }

        //
        public double error { get; set; }
        
        //
        public Neuron()
        {

        }

        //
        public void Configure(int SynapsesAmount, layerType layerType, Random random)
        {
            LayerType = layerType;
            Synapses = new List<Synapse>();
            if (! isInputLayer(layerType))
                Bias = new Synapse(random);
            SynapsesAmount = isInputLayer(LayerType) ? 1 : SynapsesAmount;
            for (int i = 0; i < SynapsesAmount; i++)
                Synapses.Add(new Synapse(random, isInputLayer(LayerType)));
            Axon = new Axon();
        }

        //
        public void Run(LinkedList<double> signals)
        {
            if (isInputLayer(LayerType))
                RunInput(signals.First.Value);
            else
                RunNeuron(signals);

            Axon.activatedSignal = ActivationFunction(Axon.signal);
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

        //
        public virtual double ActivationFunction(double sum) // means basic Threshold Device
        {
            if (sum > 0)
                return 1;
            else
                return -1;
        }
    }
}
