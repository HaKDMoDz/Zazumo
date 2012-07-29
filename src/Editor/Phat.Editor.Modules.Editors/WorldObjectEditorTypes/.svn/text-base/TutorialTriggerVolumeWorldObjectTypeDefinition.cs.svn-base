using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.ActorResources;
using Phat.Editor.Modules.Editors.ViewModels;

namespace Phat.Editor.Modules.Editors.WorldObjectEditorTypes
{
    [ExportWorldEditorType(ViewNames.WorldEditorProperties_TutorialTriggerVolumeWorldObject)]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
    public class TutorialTriggerVolumeWorldObjectTypeDefinition : WorldEditorTypeDefinition<TutorialTriggerVolumeWorldObject>
    {
        [ImportingConstructor]
        public TutorialTriggerVolumeWorldObjectTypeDefinition()
            : base("Tutorial Trigger Volume")
        {
        }

        protected override void SetDefaultValues(TutorialTriggerVolumeWorldObject obj)
        {
            obj.Width = 1;
            obj.Height = 1;
            obj.Behavior = "TutorialTextTriggerVolume";
            obj.Text = String.Empty;
        }

        public override WorldObjectViewModel CreateWorldObjectViewModel()
        {
            return new TutorialTriggerVolumeWorldObjectViewModel();
        }
    }
}
