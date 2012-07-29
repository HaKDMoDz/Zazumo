using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace Phat.Editor.Interfaces
{
    /// <summary>
    /// MEF Metadata attribute to ensure menu items are registered appropriately
    /// </summary>
    /// <remarks>
    /// This works in conjunction with <see cref="ViewFactory"/> to locate views from the <see cref="System.ComponentModel.Composition.Hosting.CompositionContainer"/>
    /// </remarks>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportGlobalCommand : ExportAttribute
    {
        public ExportGlobalCommand()
            : base(typeof(IGlobalCommand))
        {
        }
    }

    /// <summary>
    /// Represents a global command.
    /// </summary>
    public interface IGlobalCommand
    {
        String GestureText { get; }
        InputGesture InputGesture { get; }
        ICommand Command { get; }
    }

    /// <summary>
    /// When inherited implements the base functionality for the <cref see="IGlobalCommand"/> interface.
    /// </summary>
    public abstract class GlobalCommand : IGlobalCommand
    {
        /// <summary>
        /// Gets the command to exectue when the input gesture is performed.
        /// </summary>
        public ICommand Command { get; private set; }

        /// <summary>
        /// Gets the input gesture the command is linked to.
        /// </summary>
        public InputGesture InputGesture { get; private set; }

        /// <summary>
        /// Gets the text to describe the input gesture.
        /// </summary>
        public String GestureText { get; private set; }

        public GlobalCommand(String gestureText, ICommand command, InputGesture gesture)
        {
            this.GestureText = gestureText;
            this.Command = command;
            this.InputGesture = gesture;
        }
    }
}
