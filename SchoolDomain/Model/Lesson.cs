﻿using System;
using System.Collections.Generic;

namespace SchoolDomain.Model;

public partial class Lesson: Entity
{
    public int? TeacherId { get; set; }

    public int? CourseId { get; set; }

    public string? Description { get; set; }

    public string? Name { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Teacher? Teacher { get; set; }
}
