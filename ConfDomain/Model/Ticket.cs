using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Ticket
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public string Place { get; set; } = null!;

    public int? Price { get; set; }

    public int ConferenceId { get; set; }

    public int UserId { get; set; }

    public virtual Conference Conference { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
