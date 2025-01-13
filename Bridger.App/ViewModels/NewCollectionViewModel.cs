using System.Windows.Input;
using Bridger.App.Repository;

namespace Bridger.App.ViewModels;

public class NewCollectionViewModel : ViewModelBase {
    private string? CollectionNameProperty { get; set; }

    public string? CollectionName {
        get => CollectionNameProperty;
        set {
            CollectionNameProperty = value;
            OnPropertyChanged();
        }
    }

    private ICommand? SaveCollectionCommandProperty { get; set; }

    public ICommand SaveCollectionCommand => SaveCollectionCommandProperty ??=
        new RelayCommand(() => { MongoRepository.GetDatabase().CreateCollection(CollectionName); },
            () => !string.IsNullOrEmpty(CollectionName));
}