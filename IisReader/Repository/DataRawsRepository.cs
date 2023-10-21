using IisReader.Data;
using IisReader.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IisReader.Repository
{
    internal class DataRawsRepository : IDataRawsRepository
    {
        private IisdbContext context;

        public DataRawsRepository(IisdbContext context)
            => this.context = context;

        public DataRaw GetDataRaw(int archiveItemId)
            => context.DataRaws.SingleOrDefault(c => c.ArchiveItemid == archiveItemId);

        public IEnumerable<DataRaw> GetDataRaws()
            => context.DataRaws.ToList();
    }
}
