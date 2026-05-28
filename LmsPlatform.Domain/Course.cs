namespace LmsPlatform.Domain;

/// <summary>
/// Сутність "Курс"
/// </summary>
public class Course : Entity, IValidatable
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private List<Lesson> _lessons = new();

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
    /// Конструктор курсу
    /// </summary>
    public Course(string title, string description)
    {
        Title = title;
        Description = description;
        _lessons = new List<Lesson>();
    }

    /// <summary>
    /// Додає урок до курсу
    /// </summary>
    public void AddLesson(Lesson lesson)
    {
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson), "Урок не може бути null.");
        
        if (!lesson.Validate())
            throw new ArgumentException("Урок не проходить валідацію.", nameof(lesson));
        
        _lessons.Add(lesson);
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
