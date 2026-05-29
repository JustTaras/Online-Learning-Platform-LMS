namespace LmsPlatform.Domain;

using LmsPlatform.Domain.Strategy;

/// <summary>
/// Сутність "Тест"
/// </summary>
public class Test : Entity, IValidatable
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private int _totalQuestions;
    private ITestGradingStrategy _gradingStrategy = null!;

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
    /// Поточна стратегія оцінювання
    /// </summary>
    public ITestGradingStrategy GradingStrategy
    {
        get => _gradingStrategy;
        private set
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value), "Стратегія оцінювання не може бути null.");
            
            _gradingStrategy = value;
        }
    }

    /// <summary>
    /// Конструктор тесту зі стратегією оцінювання
    /// </summary>
    public Test(string title, string description, int totalQuestions, ITestGradingStrategy gradingStrategy)
    {
        Title = title;
        Description = description;
        TotalQuestions = totalQuestions;
        GradingStrategy = gradingStrategy ?? throw new ArgumentNullException(nameof(gradingStrategy));
    }

    /// <summary>
    /// Конструктор тесту зі стратегією оцінювання за замовчуванням (PassFailGradingStrategy)
    /// </summary>
    public Test(string title, string description, int totalQuestions)
    {
        Title = title;
        Description = description;
        TotalQuestions = totalQuestions;
        GradingStrategy = new PassFailGradingStrategy();
    }

    /// <summary>
    /// Змінює стратегію оцінювання
    /// </summary>
    public void SetGradingStrategy(ITestGradingStrategy strategy)
    {
        GradingStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
    }

    /// <summary>
    /// Обчислює оцінку на основі поточної стратегії
    /// </summary>
    public double CalculateScore(int correctAnswers)
    {
        return _gradingStrategy.CalculateScore(correctAnswers, _totalQuestions);
    }

    /// <summary>
    /// Перевіряє чи пройдено тест на основі поточної стратегії
    /// </summary>
    public bool IsPassed(int correctAnswers)
    {
        var score = _gradingStrategy.CalculateScore(correctAnswers, _totalQuestions);
        return _gradingStrategy.IsPassed(score);
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
