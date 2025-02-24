using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Ticket : Entity
{

    public int ConferenceId { get; set; }

    public byte[] Date { get; set; } = null!;

    public string Place { get; set; } = null!;

    public int? Price { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
