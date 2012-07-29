using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Phat.ProfessionalBurglar.Resources;
using Phat.Editor.Interfaces;
using Microsoft.Practices.Prism.ViewModel;
using System.Windows;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    public abstract class WorldObjectViewModel : NotificationObject
    {
        public Guid Id { get; set; }

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

        private String _behvaior;
        public String Behavior
        {
            get { return _behvaior; }
            set
            {
                if (value == _behvaior)
                    return;

                this._behvaior = value;
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
                RaisePropertyChanged(() => Position);
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
                RaisePropertyChanged(() => Position);
                NotifyWorldObjectValueChanged("Y", value);
            }
        }

        public IWorldEditorContext Context { get; set; }

        public Thickness Position
        {
            get { return new Thickness(X, Y, 0, 0); }
        }
                
        public void SetValue(String propertyName, Object value)
        {
            var property = this.GetType().GetProperty(propertyName);

            if (property == null)
                return;

            property.SetValue(this, value, null);
        }

        protected IPackageRepository Repository { get; set; }

        public void SetRepository(IPackageRepository repository)
        {
            this.Repository = repository;
        }

        public virtual void SetModel(IWorldObject model)
        {
            this.Id = model.Id;
            this.X = model.X;
            this.Y = model.Y;
            this.Name = model.Name;
            this.Behavior = model.Behavior;
        }

        public abstract IWorldObject MoveViewToModel();
        
        public WorldObjectViewModel()
        {

        }

        protected void NotifyWorldObjectValueChanged(String propertyName, Object value)
        {
            this.Context.NotifyPropertyWindowWorlObjectChanged(Id, propertyName, value);
        }
    }
}
