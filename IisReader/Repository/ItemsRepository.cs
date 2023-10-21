using IisReader.Data;
using IisReader.Models.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace IisReader.Repository
{
    internal class ItemsRepository : IItemsRepository
    {
        private IisdbContext context;

        public ItemsRepository(IisdbContext context)
            => this.context = context;

        public IEnumerable<Item> GetItems()
           => context.Items.ToList();

        public Item GetItem(int id)
            => context.Items.SingleOrDefault(c => c.Id == id);
    }
}
