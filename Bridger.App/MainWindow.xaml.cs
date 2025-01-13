using System.Windows;
using Bridger.App.Repository;
using Bridger.App.Views;
using Microsoft.Win32;

namespace Bridger.App;

public partial class MainWindow {
    public MainWindow() {
        InitializeComponent();
        MainFrame.Navigate(new HomePage());
        if (string.IsNullOrEmpty(MongoRepository.ConnectionString))
        {
            new RegisterConnectionStringView().ShowDialog();
        }
    }
}