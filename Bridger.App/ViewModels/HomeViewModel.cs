using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bridger.App.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bridger.App.ViewModels;

public class HomeViewModel : ViewModelBase {
    private ObservableCollection<string> CollectionsProperty { get; set; }

    private string HostProperty { get; set; }

    public string Host {
        get => HostProperty;
        set {
            HostProperty = value;
            OnPropertyChanged();
        }
    }

    private string DatabaseProperty { get; set; }

    public string Database {
        get => DatabaseProperty;
        set {
            DatabaseProperty = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> Collections {
        get => CollectionsProperty;
        set {
            CollectionsProperty = value;
            OnPropertyChanged();
        }
    }

    private string CollectionSelectedProperty { get; set; }

    public string CollectionSelected {
        get => CollectionSelectedProperty;
        set {
            CollectionSelectedProperty = value;
            OnPropertyChanged();
        }
    }

    private string JsonAttributeProperty { get; set; }

    public string JsonAttribute {
        get => JsonAttributeProperty;
        set {
            JsonAttributeProperty = value;
            OnPropertyChanged();
        }
    }

    private string JsonProperty { get; set; }

    public string Json {
        get => JsonProperty;
        set {
            JsonProperty = value;
            OnPropertyChanged();
        }
    }

    public HomeViewModel() : base() {
        LoadCollections();
    }

    public void LoadCollections() {
        var database = MongoRepository.GetDatabase();
        var collections = database.ListCollectionNames().ToList();
        Collections = new ObservableCollection<string>(collections);
        Host = MongoRepository.Host;
        Database = MongoRepository.DatabaseName;
    }

    private ICommand? BridgeJsonToDbCommandProperty { get; set; }

    public ICommand? BridgeJsonToDbCommand => BridgeJsonToDbCommandProperty ??= new RelayCommand(() => {
            try
            {
                var json = Json.Trim().Replace("\r\n", string.Empty);
                var data = JObject.Parse(json);
                if (!data.ContainsKey(JsonAttribute))
                {
                    throw new ArgumentException("O atributo JSON informado não foi encontrado. Tente novamente");
                }

                var jsonAttribute = data[JsonAttribute]!.ToList();
                if (jsonAttribute.Count > 0)
                {
                    foreach (var model in jsonAttribute.Select(obj => new BsonDocument() {
                                 { JsonAttribute, obj.ToString() },
                                 { "DataInsercao", DateTime.UtcNow }
                             }))
                    {
                        MongoRepository.GetDatabase().GetCollection<BsonDocument>(CollectionSelected)
                            .InsertOne(model);
                    }
                }
                else
                {
                    MongoRepository.GetDatabase().GetCollection<BsonDocument>(CollectionSelected).InsertOne(
                        new BsonDocument() {
                            { JsonAttribute, data[JsonAttribute]!.ToString() },
                            { "DataInsercao", DateTime.UtcNow }
                        });
                }

                MessageBox.Show("Todos os registros foram importados com sucesso ao banco de dados", "Sucesso!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        },
        () => !string.IsNullOrEmpty(CollectionSelected) && !string.IsNullOrEmpty(JsonAttribute));
}