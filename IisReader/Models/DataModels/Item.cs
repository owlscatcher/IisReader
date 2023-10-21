using System;
using System.Collections.Generic;

namespace IisReader.Models.DataModels;

public partial class Item
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public long Itemid { get; set; }

    public string? Path { get; set; }

    public string? Name { get; set; }

    public long? FirstTime { get; set; }

    public long? LastTime { get; set; }

    public long Count { get; set; }

    public int Type { get; set; }
}
