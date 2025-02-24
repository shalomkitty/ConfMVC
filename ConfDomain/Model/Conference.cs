using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Conference : Entity
{

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int OrganizatorId { get; set; }

    public byte[] Date { get; set; } = null!;

    public DateOnly PublicationDeadline { get; set; }

    public int PublicationId { get; set; }

    public virtual Organizator Organizator { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
