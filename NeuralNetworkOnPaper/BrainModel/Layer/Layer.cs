using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel.Layer
{
    public class Layer : Learn, ILayer
    {
        //list of specific neurons is implemented in every version of layers in ArtificalBrain folder.
        public LinkedList<double> DataSetInput { get; set; }
        public LinkedList<double> DataSetOutput { get; set; }
        public bool IsInputLayer { get; set; }

        //
        public Layer()
        {

        }

        //
        public void Configure(bool isInputLayer = false)
        {
            IsInputLayer = isInputLayer;
            DataSetInput = new LinkedList<double>();
            DataSetOutput = new LinkedList<double>();
        }
    }
}
