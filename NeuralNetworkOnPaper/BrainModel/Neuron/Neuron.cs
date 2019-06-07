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

        // Configuration: means creating dendrites, BIAS and axon
        public void Configure(int dendritesAmount, LayerType layerType, Random random, NeuronType neuronType)
        {
            NeuronType = neuronType;
            LayerType = layerType;
            Dendrites = new List<Dendrite>();

            if (! IsInputLayer(layerType))
                Bias = new Dendrite(random, NeuronType);

            dendritesAmount = IsInputLayer(LayerType) ? 1 : dendritesAmount;
            for (int i = 0; i < dendritesAmount; i++)
                Dendrites.Add(new Dendrite(random, NeuronType, IsInputLayer(LayerType)));

            Axon = new Axon();
        }

        // Represents suming module of neuron
        public void Run(LinkedList<double> signals)
        {
            if (IsInputLayer(LayerType))
                RunInput(signals.First.Value);
            else
                RunNeuron(signals);

            Axon.activatedSignal = ActivationFunction(Axon.signal);
        }

        // Computing sum for neuron in input layer
        private void RunInput(double signal)
        {
            Axon.signal = Dendrites[0].Run(signal);  
        }

        // Computing sum for neuron in hidden and output layers
        private void RunNeuron(LinkedList<double> signals)
        {
            int i = 0;
            Axon.signal = 0;
            foreach(double signal in signals)
            {
                Axon.signal += Dendrites[i++].Run(signal);
            }
            Axon.signal += Bias.Run(1);
        }

        // Compute Error using Delta method
        public void Delta(double expectedResult)
        {
            Error = expectedResult - Axon.activatedSignal;
        }

        // Returns final neuron output 
        public virtual double ActivationFunction(double sum) // means basic Threshold Device used by first artifical neurons
        {
            if (IsUnipolar(NeuronType))
            {
                if (sum > 0.5)
                    return 1;
                else
                    return 0;
            }
            else // Is Bipolar
            {
                if (sum > 0)
                    return 1;
                else
                    return -1;
            }
        }
    }
}
