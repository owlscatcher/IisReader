using IisReader.Models.DataModels;
using System.Collections.ObjectModel;

namespace IisReader.Utilits
{

    public class ItemsCollectionSingleton
    {
        private static ItemsCollectionSingleton _source;
        public ObservableCollection<Item> ItemsCollection;

        private ItemsCollectionSingleton()
        {
            ItemsCollection = new ObservableCollection<Item>();
        }

        public static ItemsCollectionSingleton GetInstance()
        {
            _source ??= new ItemsCollectionSingleton();

            return _source;
        }
    }
}

