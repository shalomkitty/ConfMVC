using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Ticket : Entity
{

    public int? Price { get; set; }

    public int ConferenceId { get; set; }

    public int UserId { get; set; }

    public virtual Conference? Conference { get; set; }

    public virtual User? User { get; set; }
}
