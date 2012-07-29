using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Interactivity;

namespace Phat.Editor.Actions
{
    public class CloseWindowAction : TriggerAction<DependencyObject>
    {        
        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the Action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            var viewObject = AssociatedObject;

            while(!(viewObject is Window))
            {
                viewObject = VisualTreeHelper.GetParent(viewObject);
            }

            ((Window)viewObject).Close();            
        }
    }
}
