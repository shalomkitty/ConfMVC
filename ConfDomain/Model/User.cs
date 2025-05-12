using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class User
{
    public int Id { get; set; }

    [Display(Name = "Full name")]
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    [Display(Name = "Date of birth")]
    public DateOnly DateOfBirth { get; set; }

    public string Password { get; set; } = null!;

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
