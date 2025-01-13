using System.Windows.Input;
using Bridger.App.Repository;
using Microsoft.Win32;

namespace Bridger.App.ViewModels;

public class RegisterConnectionStringViewModel : ViewModelBase {
    private string? ConnectionStringProperty { get; set; }

    public string? ConnectionString {
        get => ConnectionStringProperty;
        set {
            ConnectionStringProperty = value;
            OnPropertyChanged();
        }
    }

    private ICommand SaveConnectionStringCommand { get; set; }

    public ICommand SaveConnectionString => SaveConnectionStringCommand ??=
        new RelayCommand(
            () => { MongoRepository.ConnectionString = ConnectionString; },
            () => !string.IsNullOrEmpty(ConnectionString));
}