using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat
{
    public class ViewPort
    {
        public Int32 Top { get; set; }
        public Int32 Left { get; set; }
        public Int32 ResolutionX { get; set; }
        public Int32 ResolutionY { get; set; }

        public Single ZoomFactor { get; set; }
        

        public ViewPort()
        {
            Top = 0;
            Left = 0;

            ZoomFactor = 1.0f;
        }
    }
}
