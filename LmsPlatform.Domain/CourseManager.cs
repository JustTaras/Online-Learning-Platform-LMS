namespace LmsPlatform.Domain;

/// <summary>
/// Менеджер курсів з управління та складними запитами
/// </summary>
public class CourseManager
{
    private readonly List<Course> _courseList = new();
    private readonly Dictionary<Guid, Course> _courseDict = new();

    /// <summary>
    /// Додає курс до менеджера
    /// </summary>
    public void AddCourse(Course course)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course));
        
        if (_courseDict.ContainsKey(course.Id))
            throw new InvalidCourseOperationException($"Курс з ID {course.Id} вже існує.");
        
        _courseList.Add(course);
        _courseDict.Add(course.Id, course);
    }

    /// <summary>
    /// Видаляє курс з менеджера
    /// </summary>
    public bool RemoveCourse(Guid courseId)
    {
        if (!_courseDict.ContainsKey(courseId))
            return false;
        
        var course = _courseDict[courseId];
        _courseList.Remove(course);
        _courseDict.Remove(courseId);
        return true;
    }

    /// <summary>
    /// Отримує курс за ID
    /// </summary>
    public Course GetCourseById(Guid courseId)
    {
        if (!_courseDict.TryGetValue(courseId, out var course))
            throw new EntityNotFoundException($"Курс з ID {courseId} не знайдено.");
        
        return course;
    }

    /// <summary>
    /// Отримує всі курси
    /// </summary>
    public IReadOnlyList<Course> GetAllCourses()
    {
        return _courseList.AsReadOnly();
    }

    /// <summary>
    /// Знаходить уроки за складністю курсу (використовує GroupBy та Join)
    /// LINQ Method Syntax Запит 1: GroupBy та Join
    /// </summary>
    public IEnumerable<(Course Course, int LessonCount, string Difficulty)> GetLessonsByCourseDifficulty()
    {
        return _courseList
            .Join(
                _courseList.GroupBy(c => c.Lessons.Count),
                course => course.Lessons.Count,
                group => group.Key,
                (course, group) => new { course, group }
            )
            .Select(x => (
                Course: x.course,
                LessonCount: x.course.Lessons.Count,
                Difficulty: x.course.Lessons.Count switch
                {
                    <= 2 => "Легкий",
                    <= 5 => "Середній",
                    _ => "Складний"
                }
            ))
            .OrderBy(x => x.LessonCount);
    }

    /// <summary>
    /// Агрегує та сортує прогрес студентів за курсом
    /// LINQ Method Syntax Запит 2: Агрегація та сортування
    /// </summary>
    public IEnumerable<(Guid CourseId, int TotalStudents, double AverageProgress, int MaxCompleted)> 
        GetStudentProgressAggregate(IEnumerable<StudentProgress> studentProgresses)
    {
        if (studentProgresses == null)
            throw new ArgumentNullException(nameof(studentProgresses));
        
        return studentProgresses
            .Where(sp => _courseDict.ContainsKey(sp.CourseId))
            .GroupBy(sp => sp.CourseId)
            .Select(group => (
                CourseId: group.Key,
                TotalStudents: group.Count(),
                AverageProgress: group.Average(sp => sp.ProgressPercentage),
                MaxCompleted: group.Max(sp => sp.CompletedLessons)
            ))
            .OrderByDescending(x => x.AverageProgress)
            .ThenByDescending(x => x.TotalStudents);
    }
}
