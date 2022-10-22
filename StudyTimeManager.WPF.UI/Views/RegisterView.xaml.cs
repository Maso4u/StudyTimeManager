using StudyTimeManager.WPF.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace StudyTimeManager.WPF.UI.Views
{
    /// <summary>
    /// Interaction logic for ResgisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private void pbxPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is null)
            {
                return;
            }
            PasswordBox passwordBox = ((PasswordBox)sender);
            if (passwordBox.Name.Equals("pbxPassword"))
            {
                ((RegisterViewModel)DataContext).Password = passwordBox.Password;
            }

            if (passwordBox.Name.Equals("pbxConfirmPassword"))
            {
                ((RegisterViewModel)DataContext).ConfirmPassword = passwordBox.Password;
            }
        }
    }
}
