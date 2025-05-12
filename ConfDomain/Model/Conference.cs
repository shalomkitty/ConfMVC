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

    public int? Price { get; set; }

    [Display(Name = "Publication Id")]
    public int PublicationId { get; set; }

    [Display(Name = "Organizator Id")]
    public int OrganizatorId { get; set; }

    public byte[]? ImageData { get; set; }

    public string? ImageMimeType { get; set; }

    public virtual Organizator? Organizator { get; set; } 

    public virtual Publication? Publication { get; set; } 

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
