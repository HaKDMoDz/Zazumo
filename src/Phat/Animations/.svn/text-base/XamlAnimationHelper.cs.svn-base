/*using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Collections.Generic;
using wa = System.Windows.Media.Animation;

namespace Phat.Animations
{
    public static class XamlAnimationHelper
    {
        public static Storyboard FromXaml(String xaml)
        {
            wa.Storyboard wsb = (wa.Storyboard)XamlReader.Load(xaml);

            Storyboard s = new Storyboard();

            List<StoryboardTarget> children = new List<StoryboardTarget>();

            foreach (var child in wsb.Children)
            {
                var sbt = new StoryboardTarget();
                sbt.TargetName = (String)child.GetValue(wa.Storyboard.TargetNameProperty);
                sbt.TargetProperty = ((PropertyPath)child.GetValue(wa.Storyboard.TargetPropertyProperty)).Path;
                children.Add(sbt);                

                if (child is wa.DoubleAnimationUsingKeyFrames)
                {
                    var da = child as wa.DoubleAnimationUsingKeyFrames;
                    var timeLine = new SingleAnimationUsingKeyFrames();
                    sbt.Timeline = timeLine;
                    var keyFrames = new List<SingleKeyFrame>();

                    foreach (var keyFrame in da.KeyFrames)
                    {
                        SingleKeyFrame kf = null;

                        if (keyFrame is wa.SplineDoubleKeyFrame)
                        {
                            kf = new SplineSingleKeyFrame();
                            var sskf = kf as SplineSingleKeyFrame;
                            sskf.X1 = (Single)((wa.SplineDoubleKeyFrame)keyFrame).KeySpline.ControlPoint1.X;
                            sskf.X2 = (Single)((wa.SplineDoubleKeyFrame)keyFrame).KeySpline.ControlPoint2.X;
                            sskf.Y1 = (Single)((wa.SplineDoubleKeyFrame)keyFrame).KeySpline.ControlPoint1.Y;
                            sskf.Y2 = (Single)((wa.SplineDoubleKeyFrame)keyFrame).KeySpline.ControlPoint2.Y;                            
                        }
                        else if (keyFrame is wa.LinearDoubleKeyFrame)
                        {
                            kf = new LinearSingleKeyFrame();
                        }
                        else if (keyFrame is wa.DiscreteDoubleKeyFrame)
                        {
                            kf = new DiscreteSingleKeyFrame();
                        }

                        kf.KeyTime = new KeyTime { TimeSpan = keyFrame.KeyTime.TimeSpan };
                        kf.Value = (Single)keyFrame.Value;

                        keyFrames.Add(kf);
                    }

                    timeLine.KeyFrames = keyFrames.ToArray();

                }
            }

            s.Children = children.ToArray();
            return s;
        }
    }
}
*/