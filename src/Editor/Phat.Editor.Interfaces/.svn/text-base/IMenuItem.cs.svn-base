using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Phat.Editor.Interfaces
{
    /// <summary>
    /// Represents an item for the main menu.
    /// </summary>
    public interface IMenuItem
    {
        String HeaderText { get; }
        ICommand Command { get; }
    }

    /// <summary>
    /// When inherited implements the base functionality for the <cref see="IMenuItem"/> interface.
    /// </summary>
    public abstract class MenuItem : IMenuItem
    {
        /// <summary>
        /// Gets the menu item's header text.
        /// </summary>
        public String HeaderText { get; private set; }

        /// <summary>
        /// Gets the command to exectue when the menu item is clicked.
        /// </summary>
        public ICommand Command { get; private set; }

        public MenuItem(String headerText)
        {
            this.HeaderText = headerText;
            this.Command = null;
        }

        public MenuItem(String headerText, ICommand command)
        {
            this.HeaderText = headerText;
            this.Command = command;
        }

        public MenuItem(LocalizedString headerText)
        {
            this.HeaderText = headerText.Text;
            this.Command = null;
        }

        public MenuItem(LocalizedString headerText, ICommand command)
        {
            this.HeaderText = headerText.Text;
            this.Command = command;
        }
    }
}
