using NeuralNetworkOnPaper.BrainModel.Layer;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.ArtificalBrain.ADALINE
{
    class LayerAdaline : Layer
    {
        public List<NeuronAdaline> neurons { get; set; }

        public LayerAdaline()
        {

        }

        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount)
        {
            base.Configure(false);
            neurons = new List<NeuronAdaline>();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronAdaline neuron = new NeuronAdaline();
                neuron.Configure(previousLayerNeuronsAmount, this.isInputLayer);
                neurons.Add(neuron);
            }
        }

        public LinkedList<double> Run(LinkedList<double> dataSet)
        {
            dataSetInput = dataSet;
            dataSetOutput.Clear();

            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Run(dataSetInput);
                dataSetOutput.AddLast(neuron.Axon.signal);
            }

            return dataSetOutput;
        }

        public void Delta(LinkedList<double> resultSet)
        {
            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Delta(resultSet.First.Value);
                resultSet.RemoveFirst();
            }
        }
    }
}
