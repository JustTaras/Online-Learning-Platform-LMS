namespace LmsPlatform.Domain;

/// <summary>
/// Абстрактний базовий клас для всіх сутностей
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Унікальний ідентифікатор сутності
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Конструктор сутності
    /// </summary>
    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    /// <summary>
    /// Конструктор з конкретним ID
    /// </summary>
    protected Entity(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("ID не може бути порожніст.", nameof(id));
        
        Id = id;
    }
}
