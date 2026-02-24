using System;
using System.Collections.Generic;

namespace Mission08_0404.Models;

public partial class Task
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public DateOnly DueDate { get; set; }

    public int QuadrantId { get; set; }

    public int CatId { get; set; }

    public int CompletedFlag { get; set; }

    public virtual Category Cat { get; set; } = null!;

    public virtual Quadrant Quadrant { get; set; } = null!;
}
