﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    [Serializable]
    public class ArrowWorldObjectArchetypeData : ConcreteWorldObjectArchetypeData
    {
        public ArrowDirection ArrowDirection { get; set; }
    }
}