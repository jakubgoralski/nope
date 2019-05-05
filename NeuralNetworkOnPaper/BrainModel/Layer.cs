using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel.Layer
{
    public class Layer : Learn
    {
        //list of neurons
        public LinkedList<double> dataSetInput { get; set; }
        public LinkedList<double> dataSetOutput { get; set; }
        public bool isInputLayer { get; set; }

        public Layer()
        {

        }

        public void Configure(bool isInputLayer)
        {
            this.isInputLayer = isInputLayer;
            this.dataSetInput = new LinkedList<double>();
            this.dataSetOutput = new LinkedList<double>();
        }
    }
}
