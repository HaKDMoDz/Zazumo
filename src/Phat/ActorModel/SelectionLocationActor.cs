using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;

namespace Phat.ActorModel
{
    public class SelectionLocationActor : Actor<SelectionLocationActor>
    {
        public void AllowSelection()
        {
            OnAllowSelection();
        }

        public void DisallowSelection()
        {
            OnDisallowSelection();
        }

        protected virtual void OnAllowSelection()
        {
            this.SetSprite("SelectionLocationOKSprite");
        }

        protected virtual void OnDisallowSelection()
        {
            this.SetSprite("SelectionLocationBadSprite");
        }
    }
}
