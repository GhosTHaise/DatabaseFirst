using System;
using System.Collections.Generic;

namespace DatabaseFirst.Database;

public partial class Message
{
    public int IdMessage { get; set; }

    public string Messages { get; set; } = null!;

    public DateTime DateEnvoieMessage { get; set; }

    public int IdBroadcast { get; set; }

    public int? IdArchivage { get; set; }

    public virtual Archivage? IdArchivageNavigation { get; set; }

    public virtual BroadcastGroup IdBroadcastNavigation { get; set; } = null!;
}
