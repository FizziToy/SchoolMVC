using System;
using System.Collections.Generic;

namespace SchoolDomain.Model;

public partial class Student: Entity
{
    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
