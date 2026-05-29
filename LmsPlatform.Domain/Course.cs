namespace LmsPlatform.Domain;

using LmsPlatform.Domain.State;

/// <summary>
/// Сутність "Курс"
/// </summary>
public class Course : Entity, IValidatable
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private List<Lesson> _lessons = new();
    private CourseStateContext _stateContext;

    /// <summary>
    /// Назва курсу
    /// </summary>
    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва курсу не може бути порожною.", nameof(value));
            
            if (value.Length > 255)
                throw new ArgumentException("Назва курсу не може перевищувати 255 символів.", nameof(value));
            
            _title = value;
        }
    }

    /// <summary>
    /// Опис курсу
    /// </summary>
    public string Description
    {
        get => _description;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Опис курсу не може бути порожним.", nameof(value));
            
            _description = value;
        }
    }

    /// <summary>
    /// Список уроків курсу (тільки для читання)
    /// </summary>
    public IReadOnlyList<Lesson> Lessons => _lessons.AsReadOnly();

    /// <summary>
    /// Поточний стан курсу
    /// </summary>
    public string CourseStatus => _stateContext.CurrentState.StateName;

    /// <summary>
    /// Контекст стану курсу
    /// </summary>
    public CourseStateContext StateContext => _stateContext;

    /// <summary>
    /// Конструктор курсу
    /// </summary>
    public Course(string title, string description)
    {
        Title = title;
        Description = description;
        _lessons = new List<Lesson>();
        _stateContext = new CourseStateContext();
    }

    /// <summary>
    /// Додає урок до курсу
    /// </summary>
    public void AddLesson(Lesson lesson)
    {
        if (!_stateContext.CanAddLessons())
            throw new InvalidCourseOperationException($"Не можна додавати уроки у стані {CourseStatus}.");
        
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson), "Урок не може бути null.");
        
        if (!lesson.Validate())
            throw new ArgumentException("Урок не проходить валідацію.", nameof(lesson));
        
        _lessons.Add(lesson);
    }

    /// <summary>
    /// Публікує курс
    /// </summary>
    public void PublishCourse()
    {
        if (_lessons.Count == 0)
            throw new InvalidCourseOperationException("Не можна публікувати курс без уроків.");
        
        _stateContext.PublishCourse();
    }

    /// <summary>
    /// Архівує курс
    /// </summary>
    public void ArchiveCourse()
    {
        _stateContext.ArchiveCourse();
    }

    /// <summary>
    /// Повертає курс до стану Draft
    /// </summary>
    public void RevertToDraft()
    {
        _stateContext.RevertToDraft();
    }

    /// <summary>
    /// Перевіряє коректність курсу
    /// </summary>
    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(_title) &&
               _title.Length <= 255 &&
               !string.IsNullOrWhiteSpace(_description) &&
               _lessons.Count > 0 &&
               _lessons.All(l => l.Validate());
    }

    /// <summary>
    /// Перевантаження оператора + для додавання уроку до курсу
    /// </summary>
    public static Course operator +(Course course, Lesson lesson)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course), "Курс не може бути null.");
        
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson), "Урок не може бути null.");
        
        course.AddLesson(lesson);
        return course;
    }
}
