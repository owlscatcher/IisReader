using System;
using System.Collections.Generic;

namespace IisReader.Models.DataModels;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Guid { get; set; } = null!;
}
