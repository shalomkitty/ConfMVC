using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class User : Entity
{

    [Display(Name = "Full Name")]
    public string FullName { get; set; } = null!;

    [Display(Name = "Email")]
    public string Email { get; set; } = null!;
    [Display(Name = "Date of birth")]

    public DateOnly DateOfBirth { get; set; }

    [Display(Name = "Password")]
    public string Password { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
