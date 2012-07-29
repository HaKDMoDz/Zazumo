using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Editor.Interfaces.DatabaseModel;

namespace Phat.Editor.Interfaces.Events
{
    public class AssetCreatedEvent
    {
        public Asset NewAsset { get; private set; }

        public AssetCreatedEvent(Asset newAsset)
        {
            this.NewAsset = newAsset;
        }
    }
}
