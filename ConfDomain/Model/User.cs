using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class User: Entity
{

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
