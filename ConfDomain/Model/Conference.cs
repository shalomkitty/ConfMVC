using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Conference
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly Date { get; set; }

    [Display(Name = "Publication ID")]
    public int PublicationId { get; set; }

    [Display(Name = "Publication ID")]
    public int OrganizatorId { get; set; }

    public virtual Organizator Organizator { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
