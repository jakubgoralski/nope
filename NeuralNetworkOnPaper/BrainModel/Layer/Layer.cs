using NeuralNetworkOnPaper.BrainBooster;
using System.Collections.Generic;
using static NeuralNetworkOnPaper.BrainBooster.Config;

namespace NeuralNetworkOnPaper
{
    public class Layer : Learn, ILayer
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // (list of specific neurons is implemented in every version of layers in ArtificalBrain folder)

        public LinkedList<double> DataSetInput { get; set; }

        public LinkedList<double> DataSetOutput { get; set; }

        public LayerType LayerType { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // Constructor
        public Layer()
        {

        }

        public void Configure(LayerType layerType = LayerType.Output)
        {
            LayerType = layerType;
            DataSetInput = new LinkedList<double>();
            DataSetOutput = new LinkedList<double>();
        }
    }
}
