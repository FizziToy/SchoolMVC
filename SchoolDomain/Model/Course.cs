using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolDomain.Model;

public partial class Course : Entity
{
    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Тема")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Поле не має бути порожнім")]
    [Display(Name = "Пояснення про тему")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Ціна є обов'язковою")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Ціна повинна бути більше 0")]
    [Display(Name = "Ціна UAH")]
    public decimal? Price { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
