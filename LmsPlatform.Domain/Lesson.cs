namespace LmsPlatform.Domain;

/// <summary>
/// Сутність "Урок"
/// </summary>
public class Lesson : Entity, IValidatable
{
    private string _title = string.Empty;
    private string _description = string.Empty;
    private int _orderNumber;

    /// <summary>
    /// Назва уроку
    /// </summary>
    public string Title
    {
        get => _title;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Назва уроку не може бути порожною.", nameof(value));
            
            if (value.Length > 255)
                throw new ArgumentException("Назва уроку не може перевищувати 255 символів.", nameof(value));
            
            _title = value;
        }
    }

    /// <summary>
    /// Опис уроку
    /// </summary>
    public string Description
    {
        get => _description;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Опис уроку не може бути порожним.", nameof(value));
            
            _description = value;
        }
    }

    /// <summary>
    /// Порядковий номер уроку
    /// </summary>
    public int OrderNumber
    {
        get => _orderNumber;
        private set
        {
            if (value <= 0)
                throw new ArgumentException("Порядковий номер повинен бути більше нуля.", nameof(value));
            
            _orderNumber = value;
        }
    }

    /// <summary>
    /// Конструктор уроку
    /// </summary>
    public Lesson(string title, string description, int orderNumber)
    {
        Title = title;
        Description = description;
        OrderNumber = orderNumber;
    }

    /// <summary>
    /// Перевіряє коректність уроку
    /// </summary>
    public bool Validate()
    {
        return !string.IsNullOrWhiteSpace(_title) &&
               _title.Length <= 255 &&
               !string.IsNullOrWhiteSpace(_description) &&
               _orderNumber > 0;
    }
}
