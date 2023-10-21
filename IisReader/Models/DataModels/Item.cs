using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IisReader.Models.DataModels;

public partial class Item : NotifyPropertyChanged
{
    public int Id { get; set; }

    public int ProjectId { get; set; }

    public long Itemid { get; set; }

    public string? Path { get; set; }

    public string? Name { get; set; }

    public long FirstTime { get; set; } = 0;

    public long LastTime { get; set; } = 0;

    public long Count { get; set; }

    public int Type { get; set; }

    [NotMapped]
    public DateTime ConvertedFirstTime { get; set; }

    [NotMapped]
    public DateTime ConvertedLastTime { get; set; }
    [NotMapped]
    public double? Value { get; set; }

    public void SetNormalizeDateTime()
    {
        ConvertedFirstTime = Utilits.DateTimeConverter.FileTimeToDateTime(FirstTime);
        ConvertedLastTime = Utilits.DateTimeConverter.FileTimeToDateTime(LastTime);
    }
}
