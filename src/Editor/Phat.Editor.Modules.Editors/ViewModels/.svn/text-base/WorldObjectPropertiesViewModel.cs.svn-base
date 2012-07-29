using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.ViewModel;
using Phat.Editor.Interfaces;
using Phat.ProfessionalBurglar.Resources;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public class WorldObjectPropertiesViewModel : NotificationObject
    {
        public Guid Id { get; set; }

        public IWorldEditorContext Context { get; set; }

        private String _name;
        public String Name
        {
            get { return _name; }
            set
            {
                if (value == _name)
                    return;

                this._name = value;
                RaisePropertyChanged(() => Name);
                NotifyWorldObjectValueChanged("Name", value);
            }
        }

        private readonly ObservableCollection<String> _behaviors = new ObservableCollection<String>();
        public IEnumerable<String> Behaviors
        {
            get { return _behaviors; }
        }

        private BehaviorRegistry _behaviorRegistry;

        [Import]
        public BehaviorRegistry BehaviorRegistry
        {
            get { return _behaviorRegistry; }
            set
            {
                _behaviorRegistry = value;
                _behaviors.Clear();

                foreach (var behavior in _behaviorRegistry.GetRegisteredBehaviors())
                    _behaviors.Add(behavior);
            }
        }

        public String _behavior;
        public String Behavior
        {
            get { return _behavior; }
            set
            {
                if (value == _behavior)
                    return;

                this._behavior = value;
                RaisePropertyChanged(() => Behavior);
                NotifyWorldObjectValueChanged("Behavior", value);
            }
        }

        private Single _x;
        public Single X
        {
            get { return _x; }
            set
            {
                if (value == _x)
                    return;

                this._x = value;
                RaisePropertyChanged(() => X);
                NotifyWorldObjectValueChanged("X", value);
            }
        }

        private Single _y;
        public Single Y
        {
            get { return _y; }
            set
            {
                if (value == _y)
                    return;

                this._y = value;
                RaisePropertyChanged(() => Y);
                NotifyWorldObjectValueChanged("Y", value);
            }
        }

        public virtual void MoveModelToView(IWorldObject model)
        {
            this.Id = model.Id;
            this.Name = model.Name;

            this.X = model.X;
            this.Y = model.Y;
        
            this.Behavior = model.Behavior;
        }

        protected void NotifyWorldObjectValueChanged(String propertyName, Object value)
        {
            this.Context.NotifyWorldObjectPropertyChanged(Id, propertyName, value);
        }

        public void SetProperty(string propertyName, object value)
        {
            var property = this.GetType().GetProperty(propertyName);

            if (property == null)
                return;

            property.SetValue(this, value, null);
        }
    }
}
