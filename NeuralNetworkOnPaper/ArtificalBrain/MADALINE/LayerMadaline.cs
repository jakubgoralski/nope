using NeuralNetworkOnPaper.BrainModel.Layer;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.ArtificalBrain.ADALINE
{
    //consider if not delete this class
    class LayerMadaline : Layer
    {
        /*
         * PROPERTIES
         */

        //
        public List<NeuronAdaline> neurons { get; set; }

        /*
         * METHODS
         */

        //
        public LayerMadaline()
        {

        }

        //
        public void Configure(int neuronsAmount, int previousLayerNeuronsAmount, bool isInputLayer)
        {
            IsInputLayer = isInputLayer;
            base.Configure(false);
            neurons = new List<NeuronAdaline>();
            for (int i = 0; i < neuronsAmount; i++)
            {
                NeuronAdaline neuron = new NeuronAdaline();
                neuron.Configure(previousLayerNeuronsAmount, IsInputLayer);
                neurons.Add(neuron);
            }
        }

        //
        public LinkedList<double> Run(LinkedList<double> dataSet)
        {
            DataSetInput = dataSet;
            DataSetOutput.Clear();

            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Run(DataSetInput);
                DataSetOutput.AddLast(neuron.Axon.signal);
            }

            return DataSetOutput;
        }

        //
        public void Delta()
        {
        }

        //
        public void Delta(LinkedList<double> resultSet)
        {
            foreach (NeuronAdaline neuron in neurons)
            {
                neuron.Delta(resultSet.First.Value);
                resultSet.RemoveFirst();
            }
        }

        //
        public void ComputeError(LayerMadaline outputLayer)
        {

        }
    }
}
