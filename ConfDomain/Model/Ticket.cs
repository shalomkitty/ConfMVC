using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Ticket
{
    public int Id { get; set; }

    [Display(Name = "Conference Id")]
    public int ConferenceId { get; set; }

    [Display(Name = "User Id")]
    public int UserId { get; set; }

    public virtual Conference? Conference { get; set; }

    public virtual User? User { get; set; } 
}
