using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;

namespace NeuralNetworkOnPaper
{
    public class Layer : Learn, ILayer
    {
        //list of specific neurons is implemented in every version of layers in ArtificalBrain folder.
        public LinkedList<double> DataSetInput { get; set; }
        public LinkedList<double> DataSetOutput { get; set; }
        public layerType LayerType { get; set; }

        //
        public Layer()
        {

        }

        //
        public void Configure(layerType layerType = layerType.Output)
        {
            LayerType = layerType;
            DataSetInput = new LinkedList<double>();
            DataSetOutput = new LinkedList<double>();
        }
    }
}
