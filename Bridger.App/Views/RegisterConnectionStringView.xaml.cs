using System.Windows;

namespace Bridger.App.Views;

public partial class RegisterConnectionStringView {
    public RegisterConnectionStringView() {
        InitializeComponent();
    }

    private void Pronto_OnClick(object sender, RoutedEventArgs e) {
        Close();
    }
}