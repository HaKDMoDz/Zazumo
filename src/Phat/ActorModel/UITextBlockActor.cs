using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Phat;
using Microsoft.Xna.Framework.Graphics;

namespace Phat.ActorModel
{
    public class UITextBlockActor : UIActor
    {
        public enum Alignment
        {
            Left,
            Center,
            Right
        }

        public UITextBlockActor()
        {
            TextAlignment = Alignment.Left;
            Text = String.Empty;
            Color = Color.White;
            FontKey = "Regular";
            Scale = 1.0f;
        }

        public Color Color { get; set; }
        public Alignment TextAlignment { get; set; }
        public String Text { get; set; }
        public String FontKey { get; set; }
        public Single Scale { get; set; }
    }
}
