using System;
using System.Collections.Generic;

namespace ConfDomain.Model;

public partial class Organizator : Entity
{

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Party { get; set; } = null!;

    public virtual ICollection<Conference> Conferences { get; set; } = new List<Conference>();
}
