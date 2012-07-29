using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;
using Phat.Editor.Interfaces;

namespace Phat.Editor.Modules.Editors
{
    [ModuleExport(typeof(EditorsModule))]
    [Module(ModuleName = "Editors", OnDemand = false)]
    public class EditorsModule : IModule
    {        
        [Import]
        public BehaviorRegistry BehaviorRegistry { get; set; }

        public void Initialize()
        {
            System.Windows.ResourceDictionary dictionary = new System.Windows.ResourceDictionary();
            dictionary.Source = new Uri("pack://application:,,,/Phat.Editor.Modules.Editors;Component/Views/WorldEditorObjectViewDictionary.xaml");
            Application.Current.Resources.MergedDictionaries.Add(dictionary);

            System.Windows.ResourceDictionary archetypeEditordictionary = new System.Windows.ResourceDictionary();
            archetypeEditordictionary.Source = new Uri("pack://application:,,,/Phat.Editor.Modules.Editors;Component/Views/ArchetypeDataViewDictionary.xaml");
            Application.Current.Resources.MergedDictionaries.Add(archetypeEditordictionary);

            BehaviorRegistry.RegisterBehavior("ScriptedSearchable");


            BehaviorRegistry.RegisterBehavior("PermanentArrow");
            BehaviorRegistry.RegisterBehavior("UserArrow");
            BehaviorRegistry.RegisterBehavior("RotatingArrow");
            BehaviorRegistry.RegisterBehavior("ExplodableArrow");
            BehaviorRegistry.RegisterBehavior("Decorator");

            BehaviorRegistry.RegisterBehavior("Bomb");
            BehaviorRegistry.RegisterBehavior("ExplodableWall");

            BehaviorRegistry.RegisterBehavior("RetractableSpike");
            BehaviorRegistry.RegisterBehavior("RetractableSpikeButton");

            BehaviorRegistry.RegisterBehavior("LockedDoor");
            BehaviorRegistry.RegisterBehavior("Key");
            
            BehaviorRegistry.RegisterBehavior("MoneyBag");
            BehaviorRegistry.RegisterBehavior("Medal");

            BehaviorRegistry.RegisterBehavior("Mirror");

            BehaviorRegistry.RegisterBehavior("Terrain");

            BehaviorRegistry.RegisterBehavior("StartPosition");
        }
    }
}
