using System.Windows;
using System.Windows.Controls;
using Bridger.App.Repository;
using Bridger.App.ViewModels;
using Microsoft.Win32;
using MongoDB.Driver.Core.Configuration;

namespace Bridger.App.Views;

public partial class HomePage : Page {
    public HomePage() {
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e) {
        var result = new NewCollection().ShowDialog();
        var viewModel = (HomeViewModel)DataContext;
        if (false == result)
        {
            return;
        }
        viewModel.LoadCollections();
    }
}