namespace LmsPlatform.Domain;

/// <summary>
/// Інтерфейс для валідованих сутностей
/// </summary>
public interface IValidatable
{
    /// <summary>
    /// Перевіряє коректність сутності
    /// </summary>
    /// <returns>true якщо сутність валідна, інакше false</returns>
    bool Validate();
}
