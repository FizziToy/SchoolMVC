using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolDomain.Model;
using SchoolInfrastructure;

namespace SchoolInfrastructur.Controllers
{
    public class LessonsController : Controller
    {
        private readonly DblabContext _context;

        public LessonsController(DblabContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Courses", "Index");

            // Знаходження книжок за категорією
            ViewBag.CourseId = id;
            ViewBag.CourseName = name;
            var bookByCategory = _context.Lessons.Where(b => b.CourseId == id).Include(b => b.Course);

            return View(await bookByCategory.ToListAsync());
        }


        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Course)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // GET: Lessons/Create
        public IActionResult Create(int courseId)
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
            return View(new Lesson { CourseId = courseId });
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Lessons?courseId=5
        public async Task<IActionResult> Index(int? courseId)
        {
            if (courseId == null)
            {
                // Якщо courseId не вказаний, повертаємо всі уроки або повідомлення
                var allLessons = await _context.Lessons
                    .Include(l => l.Course)
                    .Include(l => l.Teacher)
                    .ToListAsync();
                ViewBag.Message = "Будь ласка, виберіть курс для перегляду уроків.";
                return View(allLessons);
            }

            // Отримуємо курс, щоб передати його назву
            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.Id == courseId);
            if (course == null)
            {
                return NotFound("Курс не знайдено.");
            }

            // Отримуємо уроки за курсом
            var lessonsByCourse = await _context.Lessons
                .Where(l => l.CourseId == courseId)
                .Include(l => l.Course)
                .Include(l => l.Teacher)
                .ToListAsync();

            // Передаємо дані у ViewBag
            ViewBag.CourseId = courseId;
            ViewBag.CourseName = course.Description;

            if (!lessonsByCourse.Any())
            {
                ViewBag.Message = $"Уроки у курсі '{course.Description}' відсутні.";
            }

            return View(lessonsByCourse);
        }


        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", lesson.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", lesson.TeacherId);
            return View(lesson);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,CourseId,Description,Name,Date,Id")] Lesson lesson)
        {
            if (id != lesson.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonExists(lesson.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Description", lesson.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", lesson.TeacherId);
            return View(lesson);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lesson = await _context.Lessons
                .Include(l => l.Course)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lesson == null)
            {
                return NotFound();
            }

            return View(lesson);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _context.Lessons.FindAsync(id);
            if (lesson != null)
            {
                _context.Lessons.Remove(lesson);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
