using System.Collections.Generic;
using static NeuralNetworkOnPaper.Config;

namespace NeuralNetworkOnPaper
{
    interface ILayer
    {
        /*
         * 
         * PROPERTIES
         * 
         */

        // Informs what type layer is
        LayerType LayerType { get; set; }

        // Contains list of received values from previous layer or from data set if it's input layer
        LinkedList<double> DataSetInput { get; set; }

        // Contains list of computed values in this layer
        LinkedList<double> DataSetOutput { get; set; }

        /*
         * 
         * METHODS
         * 
         */

        // As base set up type of layer, as new set up list of neurons
        void Configure(LayerType layerType);
    }
}
