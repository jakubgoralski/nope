using System.Collections.Generic;

namespace NeuralNetworkOnPaper.BrainModel.Layer
{
    interface ILayer
    {
        /*
         * PROPERTIES
         */

        //
        LinkedList<double> DataSetInput { get; set; }

        //
        LinkedList<double> DataSetOutput { get; set; }

        //
        bool IsInputLayer { get; set; }

        /*
         * METHODS
         */

        //
        void Configure(bool isInputLayer);
    }
}
