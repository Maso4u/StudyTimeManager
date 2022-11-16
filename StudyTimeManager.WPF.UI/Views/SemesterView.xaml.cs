using StudyTimeManager.WPF.UI.ViewModels;
using System;
using System.Windows.Controls;

namespace StudyTimeManager.WPF.UI.Views
{
    /// <summary>
    /// Interaction logic for CreateSemesterView.xaml
    /// </summary>
    public partial class SemesterView : UserControl
    {
        public SemesterView()
        {
            InitializeComponent();
            var dc = this.DataContext as SemesterViewModel;
        }

        
    }
}
