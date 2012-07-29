using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Phat.Editor.Interfaces;
using Phat.Editor.Modules.Core.Properties;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using System.Windows;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace Phat.Editor.Modules.Core.ApplicationCommands
{
    /// <summary>
    /// Represents the file exit menu item.
    /// </summary>
    [ExportMenuItem(MenuItems.File_Exit, MenuPosition.AppendTo, MenuItems.File, ShouldPrependSeparator=true)]
    public class FileExitMenuItem : MenuItem
    {
        [ImportingConstructor]
        public FileExitMenuItem(ExitCommand exitCommand) : base(Strings.MainMenu_File_Exit, exitCommand.Command) { }
    }

    [ExportGlobalCommand]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GlobalExitCommand : GlobalCommand
    {
        [ImportingConstructor]
        public GlobalExitCommand(ExitCommand exitCommand) : base("Alt+F4", exitCommand.Command, new KeyGesture(Key.F4, ModifierKeys.Alt)) { }
    }

    /// <summary>
    /// Represents the application exit command.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ExitCommand
    {
        public ICommand Command { get; private set; }

        private readonly InteractionRequest<Confirmation> _exitConfirmationInteractionRequest;

        /// <summary>
        /// Gets the interaction request for confirming exiting the application.
        /// </summary>
        public IInteractionRequest ExitConfirmationInteractionRequest
        {
            get
            {
                return this._exitConfirmationInteractionRequest;
            }
        }

        public ExitCommand()            
        {
            this.Command = new DelegateCommand(ExitExecute);
            
            this._exitConfirmationInteractionRequest = new InteractionRequest<Confirmation>();
        }

        public void ExitExecute()
        {
            var result = MessageBox.Show("Save before exiting?", "Phat Editor", MessageBoxButton.YesNoCancel);

            if (result == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
