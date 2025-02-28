﻿using System;
using System.Collections.Generic;

namespace SchoolDomain.Model;

public partial class StudentCourse: Entity
{
    public int StudentId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student Student { get; set; } = null!;
}
