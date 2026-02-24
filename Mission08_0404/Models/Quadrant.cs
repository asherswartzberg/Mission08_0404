using System;
using System.Collections.Generic;

namespace Mission08_0404.Models;

public partial class Quadrant
{
    public int QuadId { get; set; }

    public string QuadName { get; set; } = null!;

    public string? QuadDescription { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
