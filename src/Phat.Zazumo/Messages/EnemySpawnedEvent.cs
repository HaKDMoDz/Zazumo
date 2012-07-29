using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.Zazumo.Messages
{
    public class EnemySpawnedEvent
    {
        public Guid SpawnedActorId { get; private set; }

        public EnemySpawnedEvent(Guid spawnedActorId)
        {
            this.SpawnedActorId = spawnedActorId;
        }
    }
}
