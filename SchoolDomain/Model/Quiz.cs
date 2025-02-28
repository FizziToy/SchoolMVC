using System;
using System.Collections.Generic;

namespace SchoolDomain.Model;

public partial class Quiz: Entity
{
    public string? Name { get; set; }

    public int? MaxScoreQuiz { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }
}
