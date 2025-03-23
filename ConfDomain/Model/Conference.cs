using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Conference: Entity
{

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Place { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int PublicationId { get; set; }

    public int OrganizatorId { get; set; }

    public virtual Organizator? Organizator { get; set; }

    public virtual Publication? Publication { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
