namespace LmsPlatform.Domain;

/// <summary>
/// Сутність "Прогрес студента"
/// </summary>
public class StudentProgress : Entity, IValidatable
{
    private Guid _studentId;
    private Guid _courseId;
    private int _completedLessons;
    private int _totalLessons;
    private double _progressPercentage;

    /// <summary>
    /// ID студента
    /// </summary>
    public Guid StudentId
    {
        get => _studentId;
        private set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ID студента не може бути порожніст.", nameof(value));
            
            _studentId = value;
        }
    }

    /// <summary>
    /// ID курсу
    /// </summary>
    public Guid CourseId
    {
        get => _courseId;
        private set
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ID курсу не може бути порожніст.", nameof(value));
            
            _courseId = value;
        }
    }

    /// <summary>
    /// Кількість завершених уроків
    /// </summary>
    public int CompletedLessons
    {
        get => _completedLessons;
        private set
        {
            if (value < 0)
                throw new ArgumentException("Кількість завершених уроків не може бути негативною.", nameof(value));
            
            _completedLessons = value;
        }
    }

    /// <summary>
    /// Загальна кількість уроків у курсі
    /// </summary>
    public int TotalLessons
    {
        get => _totalLessons;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Загальна кількість уроків повинна бути більше нуля.", nameof(value));
            
            _totalLessons = value;
        }
    }

    /// <summary>
    /// Відсоток прогресу
    /// </summary>
    public double ProgressPercentage
    {
        get => _progressPercentage;
        private set
        {
            if (value < 0 || value > 100)
                throw new ArgumentException("Прогрес повинен бути від 0 до 100 відсотків.", nameof(value));
            
            _progressPercentage = value;
        }
    }

    /// <summary>
    /// Конструктор прогресу студента
    /// </summary>
    public StudentProgress(Guid studentId, Guid courseId, int totalLessons)
    {
        StudentId = studentId;
        CourseId = courseId;
        TotalLessons = totalLessons;
        CompletedLessons = 0;
        ProgressPercentage = 0;
    }

    /// <summary>
    /// Оновлює кількість завершених уроків та відсоток прогресу
    /// </summary>
    public void UpdateProgress(int completedLessons)
    {
        if (completedLessons < 0 || completedLessons > _totalLessons)
            throw new ArgumentException("Кількість завершених уроків повинна бути від 0 до загальної кількості.", nameof(completedLessons));
        
        CompletedLessons = completedLessons;
        ProgressPercentage = (_completedLessons / (double)_totalLessons) * 100;
    }

    /// <summary>
    /// Перевіряє коректність прогресу
    /// </summary>
    public bool Validate()
    {
        return _studentId != Guid.Empty &&
               _courseId != Guid.Empty &&
               _completedLessons >= 0 &&
               _totalLessons > 0 &&
               _completedLessons <= _totalLessons &&
               _progressPercentage >= 0 &&
               _progressPercentage <= 100;
    }
}
