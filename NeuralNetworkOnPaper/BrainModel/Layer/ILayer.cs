using System.Collections.Generic;
using static NeuralNetworkOnPaper.Config;

namespace NeuralNetworkOnPaper
{
    interface ILayer
    {
        /*
         * PROPERTIES
         */

        //
        layerType LayerType { get; set; }

        //
        LinkedList<double> DataSetInput { get; set; }

        //
        LinkedList<double> DataSetOutput { get; set; }

        /*
         * METHODS
         */

        //
        void Configure(layerType layerType);
    }
}
