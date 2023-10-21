using IisReader.Models.DataModels;
using System.Collections.Generic;

namespace IisReader.Repository
{
    internal interface IItemsRepository
    {
        IEnumerable<Item> GetItems();
        Item GetItem(int id);
    }
}
