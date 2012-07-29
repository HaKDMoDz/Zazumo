
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Phat.ActorResources;
using Phat.Editor.Interfaces.DatabaseModel;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Input;
using System.Windows.Threading;
using Phat.Animations;
using System.Windows.Markup;
using System.Windows;

namespace Phat.Editor.Modules.Editors.ViewModels
{
    [Export]
    [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
    public class StoryboardEditorViewModel : EditorViewModel<Storyboard>
    {
        private String _xaml;
        public String Xaml
        {
            get { return _xaml; }
            set
            {
                _xaml = value;
                this.RaisePropertyChanged(() => this.Xaml);
                this.MarkUnsaved();
            }
        }

        [ImportingConstructor]
        public StoryboardEditorViewModel()
        {
        }
        
        protected override Storyboard MoveViewToModel()
        {
            var model = new Storyboard();
            model.Key = this.Asset.Key;

            var context = new ParserContext();
            // context.BaseUri = new Uri("http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");

            try
            {
                var paddedXaml = String.Format(@"<Storyboard xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' {0}", Xaml.Trim().Remove(0, "<Storyboard".Length));
                System.Windows.Media.Animation.Storyboard xamlStoryboard = 
                    (System.Windows.Media.Animation.Storyboard)XamlReader.Parse(paddedXaml, context);

                var targets = new List<Phat.Animations.StoryboardTarget>();

                foreach (var child in xamlStoryboard.Children)
                {
                    if (child is System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)
                    {
                        var sbTarget = new Phat.Animations.StoryboardTarget();
                        
                        var targetProperty = System.Windows.Media.Animation.Storyboard.GetTargetProperty(child);
                        if (targetProperty.PathParameters.Count > 0)
                        {
                            var propertyPath = ((DependencyProperty)targetProperty.PathParameters.Single()).Name;
                            sbTarget.TargetProperty = propertyPath;
                        }
                        else
                        {
                            sbTarget.TargetProperty = targetProperty.Path;
                        }

                        sbTarget.TargetName = System.Windows.Media.Animation.Storyboard.GetTargetName(child);
                        targets.Add(sbTarget);

                        var timeline = new SingleAnimationUsingKeyFrames();
                        var timelineKeyFrames = new List<SingleKeyFrame>();

                        foreach (var keyframe in ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)child).KeyFrames)
                        {
                            if (keyframe is System.Windows.Media.Animation.EasingDoubleKeyFrame)
                            {
                                var timelineKeyFrame = new LinearSingleKeyFrame();
                                timelineKeyFrame.KeyTime = new KeyTime() { TimeSpan = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)keyframe).KeyTime.TimeSpan };
                                timelineKeyFrame.Value = (Single)((System.Windows.Media.Animation.EasingDoubleKeyFrame)keyframe).Value;
                                timelineKeyFrames.Add(timelineKeyFrame);
                            }
                        }

                        timeline.KeyFrames = timelineKeyFrames.ToArray();
                        sbTarget.Timeline = timeline;
                    }
                }

                model.Children = targets.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("There was an error parsing the Xaml.{0}{1}", Environment.NewLine, ex.Message));
            }            

            return model;
        }

        protected override void MoveModelToView(Storyboard model)
        {
        }
    }
}
