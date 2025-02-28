using System;
using System.Collections.Generic;

namespace SchoolDomain.Model;

public partial class Teacher: Entity
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
