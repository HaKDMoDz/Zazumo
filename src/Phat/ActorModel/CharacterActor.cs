using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.Messages;

namespace Phat.ActorModel
{
    public interface ICharacterActor { }

    public class CharacterActor<TActor> : Actor<TActor>, ICharacterActor
        where TActor : CharacterActor<TActor>
    {
        protected override void OnInitializing(object initializationData)
        {
            base.OnInitializing(initializationData);

            Handle<ActorCollidedEvent>()
                .ApplyDefaultHandler(OnActorCollided);
        }

        protected virtual void OnActorCollided(ActorCollidedEvent @event)
        {

        }
    }
}
