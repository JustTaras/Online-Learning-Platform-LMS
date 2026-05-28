namespace LmsPlatform.Domain;

/// <summary>
/// Сутність "Тест"
/// </summary>
public class Test : Entity, IValidatable
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private int _totalQuestions;

    /// <summary>
    /// Назва тесту
    /// </summary>
    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва тесту не може бути порожною.", nameof(value));
            
            if (value.Length > 255)
                throw new ArgumentException("Назва тесту не може перевищувати 255 символів.", nameof(value));
            
            _title = value;
        }
    }

    /// <summary>
    /// Опис тесту
    /// </summary>
    public string Description
    {
        get => _description;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Опис тесту не може бути порожним.", nameof(value));
            
            _description = value;
        }
    }

    /// <summary>
    /// Загальна кількість запитань у тесті
    /// </summary>
    public int TotalQuestions
    {
        get => _totalQuestions;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Кількість запитань повинна бути більше нуля.", nameof(value));
            
            _totalQuestions = value;
        }
    }

    /// <summary>
    /// Конструктор тесту
    /// </summary>
    public Test(string title, string description, int totalQuestions)
    {
        Title = title;
        Description = description;
        TotalQuestions = totalQuestions;
    }

    /// <summary>
    /// Перевіряє коректність тесту
    /// </summary>
    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(_title) &&
               _title.Length <= 255 &&
               !string.IsNullOrWhiteSpace(_description) &&
               _totalQuestions > 0;
    }
}
