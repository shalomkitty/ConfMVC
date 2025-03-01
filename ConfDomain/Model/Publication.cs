using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConfDomain.Model;

public partial class Publication
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    [Display(Name = "User ID")]
    public int UserId { get; set; }

    [Display(Name = "Upload date")]
    public DateOnly UploadDate { get; set; }

    public virtual User User { get; set; } = null!;
}
