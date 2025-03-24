using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Publication
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int UserId { get; set; }

    public DateOnly UploadDate { get; set; }

    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();

    public virtual User User { get; set; } = null!;
}
