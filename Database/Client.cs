using System;
using System.Collections.Generic;

namespace DatabaseFirst.Database;

public partial class Client
{
    public int IdClient { get; set; }

    public string FirstnameClient { get; set; } = null!;

    public string LastnameClient { get; set; } = null!;

    public string EmailClient { get; set; } = null!;

    public virtual ICollection<BroadcastGroup> IdBroadcasts { get; } = new List<BroadcastGroup>();
}
