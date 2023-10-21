using System;
using System.Collections.Generic;

namespace IisReader.Models.DataModels;

public partial class DataRaw
{
    public int Layer { get; set; }

    public int ArchiveItemid { get; set; }

    public long SourceTime { get; set; }

    public long? ServerTime { get; set; }

    public int StatusCode { get; set; }

    public double? Value { get; set; }

    public string? SValue { get; set; }
}
