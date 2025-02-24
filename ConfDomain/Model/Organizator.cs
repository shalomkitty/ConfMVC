using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Organizator : Entity
{
    [Display(Name = "Name")]
    public string Name { get; set; } = null!;
    [Display(Name = "Description")]

    public string Description { get; set; } = null!;
    [Display(Name = "Party")]

    public string? Party { get; set; }

    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();
}
