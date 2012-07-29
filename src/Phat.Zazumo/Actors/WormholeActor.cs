using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Zazumo.Resources;
using Microsoft.Xna.Framework;
using Phat.Zazumo.Messages;

namespace Phat.Zazumo.Actors
{
    public class WormholeActor : Actor<WormholeActor>
    {
        private WormholeData _data;
        
        public Int32 Size { get; set; }
        public ZazumoShape Shape { get; set; }
        public LevelSpawnData MiniBossData { get; set; }
                
        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);


            if (!(initializationData is WormholeData))
                return;

            _data = (WormholeData)initializationData;

            Size = _data.Size;
            Shape = _data.Shape;
            MiniBossData = _data.MiniBossData;
        }        
    }
}
