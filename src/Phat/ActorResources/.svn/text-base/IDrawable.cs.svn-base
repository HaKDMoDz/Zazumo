using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phat.ActorResources
{
    public interface IDrawable
    {
        String SpriteKey { get; set; }
        Single Height { get; set; }
        Single Width { get; set; }
    }

    [Serializable]
    public class Drawable : ArchetypeData,  IDrawable
    {
        public String SpriteKey { get; set; }
        public Single Height { get; set; }
        public Single Width { get; set; }

        public Drawable()
        {

        }

        public Drawable(String spriteKey, Single height, Single width)
        {
            this.SpriteKey = spriteKey;
            this.Height = height;
            this.Width = width;
        }
    }
}
