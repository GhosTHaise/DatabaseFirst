using System;
using System.Collections.Generic;

namespace DatabaseFirst.Database;

public partial class Archivage
{
    public int IdArchivage { get; set; }

    public string TitreArchivage { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; } = new List<Message>();
}
