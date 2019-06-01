using NeuralNetworkOnPaper.BrainModel;
using System;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper
{
    public class Neuron : Config, INeuron
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        public new LayerType LayerType { get; set; }
        public new NeuronType NeuronType { get; set; }
        public List<Dendrite> Dendrites { get; set; }
        public Dendrite Bias { get; set; }
        public Axon Axon { get; set; }
        public double Error { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public Neuron()
        {

        }

        public void Configure(int dendritesAmount, LayerType layerType, Random random, NeuronType neuronType = NeuronType.Bipolar)
        {
            NeuronType = neuronType;
            LayerType = layerType;
            Dendrites = new List<Dendrite>();
            if (! IsInputLayer(layerType))
                Bias = new Dendrite(random, neuronType);
            dendritesAmount = IsInputLayer(LayerType) ? 1 : dendritesAmount;
            for (int i = 0; i < dendritesAmount; i++)
                Dendrites.Add(new Dendrite(random, neuronType, IsInputLayer(LayerType)));
            Axon = new Axon();
        }

        public void Run(LinkedList<double> signals)
        {
            if (IsInputLayer(LayerType))
                RunInput(signals.First.Value);
            else
                RunNeuron(signals);

            Axon.activatedSignal = ActivationFunction(Axon.signal);
        }

        public void RunInput(double signal)
        {
            Axon.signal = Dendrites[0].Run(signal);  
        }

        public void RunNeuron(LinkedList<double> signals)
        {
            int i = 0;
            Axon.signal = 0;
            foreach(double signal in signals)
            {
                Axon.signal += Dendrites[i++].Run(signal);
            }
            Axon.signal += Bias.Run(1);
        }

        //
        //public virtual double ActivationFunction(double sum) // means basic Threshold Device
        //{
        //    if (sum > 0)
        //        return 1;
        //    else
        //        return -1;
        //}

        public virtual double ActivationFunction(double sum) // means tangens hiperbolic
        {
            //1 / (1 + e ^ (-activation))
                return 1.0 / (1.0 + Math.Pow(Math.E, -1 * sum));
            /*
            double eBs = Math.Pow(Math.E, sum);
            double emBs = Math.Pow(Math.E, -1 * sum);
            return (eBs - emBs) / (eBs + emBs);*/
        }
    }
}
