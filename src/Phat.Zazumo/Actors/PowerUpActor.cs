using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ActorModel;
using Phat.Zazumo.Resources;
using Phat.Messages;
using Phat.Zazumo.Messages;

namespace Phat.Zazumo.Actors
{
    public class PowerUpActor : CharacterActor<PowerUpActor>
    {
        private TimeSpan ResizeDuration = TimeSpan.FromMilliseconds(350);
        private const Single ResizeRate = 0.5f;

        public Int32 Size { get; set; }
        public ZazumoShape Shape { get; private set; }

        public Single OffsetX { get; set; }
        public Single OffsetY { get; set; }

        public Single AmmoLevel { get; set; }

        private Single _height;
        private Single _width;


        public Boolean IsHeld { get; private set; }

        private PowerUpData _data;

        protected override void OnInitializing(Object initializationData)
        {
            base.OnInitializing(initializationData);

            this._data = (PowerUpData)initializationData;
            this._height = _data.Height;
            this._width = _data.Width;
            this.AmmoLevel = 1.0f;

            this.Shape = _data.Shape;

            Size = 1;
        }

        protected override void OnAttached(Actor actor, Microsoft.Xna.Framework.Vector2 offset)
        {
            base.OnAttached(actor, offset);
            this.IsHeld = true;

            var ratio = _width / _height;

            this.AnimateProperty("WidthOffset", 0f, (ResizeRate * (Size - 1)), ResizeDuration);
            this.AnimateProperty("HeightOffset", 0f, (ResizeRate * (Size - 1)), ResizeDuration);
            this.AnimateProperty("XOffset", 0f, (ResizeRate * (Size - 1)) / -2f, ResizeDuration);
            this.AnimateProperty("YOffset", 0f, (ResizeRate * (Size - 1)) / -2f, ResizeDuration);
            OffsetX = (ResizeRate * (Size - 1)) / -2f;
            OffsetY = (ResizeRate * (Size - 1)) / -2f;
        }

        protected override void OnDettached()
        {
            base.OnDettached();
            this.IsHeld = false;

            var ratio = _width / _height;

            this.AnimateProperty("WidthOffset", (ResizeRate * ratio * (Size)), (ResizeRate * ratio * (0)), ResizeDuration);
            this.AnimateProperty("HeightOffset", (ResizeRate * (Size)), (ResizeRate * (0)), ResizeDuration);
            this.AnimateProperty("XOffset", (ResizeRate * ratio * (Size)) / -2f, (ResizeRate * ratio * (0)) / -2f, ResizeDuration);
            this.AnimateProperty("YOffset", (ResizeRate * (Size)) / -2f, (ResizeRate * (0)) / -2f, ResizeDuration);
            OffsetX = (ResizeRate * ratio * (Size + 1)) / -2f;
            OffsetY = (ResizeRate * (Size + 1)) / -2f;

            this.SetProperty("Opacity", AmmoLevel + 0.15f);
        }

        protected override void OnActorCollided(ActorCollidedEvent @event)
        {
            @event.Cancel = true;
        }

        public void Grow()
        {
            if (Size == 7)
                return;

            Size++;
            var ratio = _width / _height;

            this.AnimateProperty("WidthOffset", ResizeRate * (Size - 2), ResizeRate * (Size - 1), ResizeDuration);
            this.AnimateProperty("HeightOffset", ResizeRate * (Size - 2), ResizeRate * (Size - 1), ResizeDuration);
            this.AnimateProperty("XOffset", (ResizeRate * (Size - 2)) / -2f, (ResizeRate * (Size - 1)) / -2f, ResizeDuration);
            this.AnimateProperty("YOffset", (ResizeRate * (Size - 2)) / -2f, (ResizeRate * (Size - 1)) / -2f, ResizeDuration);
            OffsetX = (ResizeRate * ratio * (Size)) / -2f;
            OffsetY = (ResizeRate * (Size)) / -2f;
        }

        public void Shrink()
        {
            if (Size == 1)
                return;

            Size--;

            var ratio = _width / _height;

            this.AnimateProperty("WidthOffset", (ResizeRate * ratio * (Size)), (ResizeRate * ratio * (Size - 1)), ResizeDuration);
            this.AnimateProperty("HeightOffset", (ResizeRate * (Size)), (ResizeRate * (Size - 1)), ResizeDuration);
            this.AnimateProperty("XOffset", (ResizeRate * (Size)) / -2f, (ResizeRate * (Size - 1)) / -2f, ResizeDuration);
            this.AnimateProperty("YOffset", (ResizeRate * (Size)) / -2f, (ResizeRate * (Size - 1)) / -2f, ResizeDuration);
            OffsetX = (ResizeRate * ratio * (Size)) / -2f;
            OffsetY = (ResizeRate * (Size)) / -2f;
        }


    }
}
