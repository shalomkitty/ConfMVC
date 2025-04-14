using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Conference
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Place { get; set; } = null!;

    public DateTime Date { get; set; }

    [Display(Name="Price (in UAH)")]
    public int? Price { get; set; }
    [Display(Name = "Publication ID")]
    public int PublicationId { get; set; }
    [Display(Name = "Organizator ID")]
    public int OrganizatorId { get; set; }

    public string? Image_path { get; set; }

    public virtual Organizator? Organizator { get; set; }

    public virtual Publication? Publication { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
