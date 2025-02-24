using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Publication : Entity
{

    public string Title { get; set; } = null!;

    public int UserId { get; set; }

    public int ConferenceId { get; set; }

    public DateOnly UploadDate { get; set; }

    public virtual Conference Conference { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
