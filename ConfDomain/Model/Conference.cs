using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Conference : Entity
{
    [Display(Name = "Title")]
    public string Title { get; set; } = null!;
    [Display(Name = "Description")]

    public string Description { get; set; } = null!;
    [Display(Name = "Organizator ID")]

    public int OrganizatorId { get; set; }
    [Display(Name = "Date")]

    public byte[] Date { get; set; } = null!;
    [Display(Name = "Publication deadline")]

    public DateOnly PublicationDeadline { get; set; }
    [Display(Name = "Publication ID")]

    public int PublicationId { get; set; }

    public virtual Organizator Organizator { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();
}
