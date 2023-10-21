using IisReader.Models.DataModels;
using System.Collections.Generic;

namespace IisReader.Repository
{
    internal interface IDataRawsRepository
    {
        IEnumerable<DataRaw> GetDataRaws();
        DataRaw GetDataRaw(int id);
    }
}
