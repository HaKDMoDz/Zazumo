using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Phat.Editor.Infrastructure;
using System.ComponentModel.Composition;
using Phat.Editor.Moduels.Core.ViewModels;

namespace Phat.Editor.Modules.Core.Views
{
    /// <summary>
    /// Interaction logic for MainMenuView.xaml
    /// </summary>
    [Export]
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the ViewModel.
        /// </summary>
        /// <remarks>
        /// This set-only property is annotated with the <see cref="ImportAttribute"/> so it is injected by MEF with
        /// the appropriate view model.
        /// </remarks>
        [Import(AllowRecomposition=true)]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1044:PropertiesShouldNotBeWriteOnly", Justification = "Needs to be a property to be composed by MEF")]
        public MainMenuViewModel ViewModel
        {
            set { this.DataContext = value; }
        }
    }
}
