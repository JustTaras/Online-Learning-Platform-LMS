namespace LmsPlatform.Domain;

/// <summary>
/// Виняток, який викидається коли сутність не знайдена
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Конструктор з повідомленням про помилку
    /// </summary>
    public EntityNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Конструктор з повідомленням та внутрішнім винятком
    /// </summary>
    public EntityNotFoundException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
