namespace LmsPlatform.Domain;

/// <summary>
/// Виняток, який викидається при невалідній операції на курсом
/// </summary>
public class InvalidCourseOperationException : Exception
{
    /// <summary>
    /// Конструктор з повідомленням про помилку
    /// </summary>
    public InvalidCourseOperationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Конструктор з повідомленням та внутрішнім винятком
    /// </summary>
    public InvalidCourseOperationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
