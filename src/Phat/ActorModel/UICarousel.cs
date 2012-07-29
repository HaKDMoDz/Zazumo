using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat;
using Phat.Messages;

namespace Phat.ActorModel
{
    public class UICarousel : Actor<UICarousel>
    {

        private readonly List<String> _spriteKeys;

        private Single _currentPosition;
        public Single CurrentPosition
        {
            get { return _currentPosition; }
            set
            {
                if (_currentPosition == value)
                    return;

                _currentPosition = value;

                if (_currentPosition < 0)
                    _currentPosition = 0;

                if (_currentPosition > (_spriteKeys.Count - 1))
                    _currentPosition = _spriteKeys.Count - 1;

                SelectedIndex = (Int32)Math.Round(_currentPosition);
            }
        }

        private Int32 _selectedIndex = 0;
        public Int32 SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex == value)
                    return;

                _selectedIndex = value;
                this.Publish(new SelectionChangedEvent(this, value));
            }
        }

        public Single HorizontalRadius { get; set; }
        public Single VerticalRadius { get; set; }
        public Single ItemWidth { get; set; }
        public Single ItemHeight { get; set; }
        public Single Spacing { get; set; }
        public Single Velocity { get; set; }
        public Single Damping { get; set; }

        public IList<String> SpriteKeys { get { return _spriteKeys; } }

        public UICarousel()
        {
            this._spriteKeys = new List<String>();

            this.SelectedIndex = 0;
            this.HorizontalRadius = 250;
            this.VerticalRadius = 40;
            this.Spacing = (Single)Math.PI / 6f;
            this.ItemWidth = 128;
            this.ItemHeight = 70;
            this.Damping = 25;
        }

        public void Add(String item)
        {
            _spriteKeys.Add(item);
        }
    }
}
