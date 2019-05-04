using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel.Layer
{
    class LayerLinear : Learn
    {
        public List<NeuronLinear> neuronsLinear { get; set; }
        public LinkedList<double> dataSetInput { get; set; }
        public LinkedList<double> dataSetOutput { get; set; }
        public bool isInputLayer { get; set; }

        public LayerLinear(int neuronsAmount, int previousLayerNeuronsAmount, bool isInputLayer)
        {
            this.isInputLayer = isInputLayer;
            for (int i = 0; i < neuronsAmount; i++)
                neuronsLinear.Add(new NeuronLinear(previousLayerNeuronsAmount, this.isInputLayer));
        }

        public LinkedList<double> Run(LinkedList<double> dataSet)
        {
            dataSetInput = dataSet;
            dataSetOutput.Clear();

            foreach (NeuronLinear neuron in neuronsLinear)
            {
                neuron.Run(dataSetInput);
                dataSetOutput.AddLast(neuron.axon.signal);
            }

            return dataSetOutput;
        }
    }
}
