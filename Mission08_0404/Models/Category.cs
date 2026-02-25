using System;
using System.Collections.Generic;

namespace Mission08_0404.Models;

public partial class Category
{
    public int CatId { get; set; }

    public string CatName { get; set; } = null!;

    public virtual ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
