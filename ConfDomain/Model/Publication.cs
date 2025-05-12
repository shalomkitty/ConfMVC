using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Publication
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [Display(Name = "User Id")]
    public int UserId { get; set; }

    [Display(Name = "Upload date")]
    public DateTime UploadDate { get; set; }

    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();

    public virtual User? User { get; set; }
}
