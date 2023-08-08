using System;
using System.Collections.Generic;

namespace DatabaseFirst.Database;

public partial class BroadcastGroup
{
    public int IdBroadcast { get; set; }

    public string TitreBroadcast { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; } = new List<Message>();

    public virtual ICollection<Client> IdClients { get; } = new List<Client>();
}
