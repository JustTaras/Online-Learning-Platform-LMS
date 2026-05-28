namespace LmsPlatform.Domain;

/// <summary>
/// Генеричний репозиторій для управління сутностями
/// </summary>
/// <typeparam name="T">Тип сутності, яка розширює Entity</typeparam>
public class Repository<T> where T : Entity
{
    private readonly List<T> _items = new();

    /// <summary>
    /// Додає сутність до репозиторію
    /// </summary>
    public void Add(T item)
    {
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        
        if (_items.Any(x => x.Id == item.Id))
            throw new InvalidOperationException($"Сутність з ID {item.Id} вже існує.");
        
        _items.Add(item);
    }

    /// <summary>
    /// Видаляє сутність з репозиторію за ID
    /// </summary>
    public bool Remove(Guid id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
            return false;
        
        _items.Remove(item);
        return true;
    }

    /// <summary>
    /// Повертає всі сутності
    /// </summary>
    public IReadOnlyList<T> GetAll()
    {
        return _items.AsReadOnly();
    }
}
