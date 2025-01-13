using System.Windows;

namespace Bridger.App.Views;

public partial class NewCollection : Window {
    public NewCollection() {
        InitializeComponent();
    }

    private void ButtonCriar_OnClick(object sender, RoutedEventArgs e) {
        DialogResult = true;
    }

    private void ButtonCancelar_OnClick(object sender, RoutedEventArgs e) {
        DialogResult = false;
    }
}