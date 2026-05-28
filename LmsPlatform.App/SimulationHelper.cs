namespace LmsPlatform.App;

/// <summary>
/// Помічник для симуляції операцій з БД з політикою повторних спроб
/// </summary>
public static class SimulationHelper
{
    private const int MaxRetries = 3;
    private const int InitialDelayMs = 100;

    /// <summary>
    /// Симулює підключення до БД з експоненціальною затримкою та політикою повторних спроб
    /// </summary>
    public static void SimulateDbConnect()
    {
        int retryCount = 0;
        int delayMs = InitialDelayMs;

        while (retryCount <= MaxRetries)
        {
            try
            {
                Console.WriteLine($"Спроба підключення до БД (спроба {retryCount + 1}/{MaxRetries + 1})...");
                
                // Симуляція випадкової помилки
                if (Random.Shared.Next(0, 2) == 0)
                {
                    throw new InvalidOperationException("Помилка підключення до БД");
                }
                
                Console.WriteLine("Успішне підключення до БД");
                return;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Виняток: {ex.Message}");
                
                if (retryCount >= MaxRetries)
                {
                    Console.WriteLine($"Максимальна кількість спроб ({MaxRetries + 1}) перевищена.");
                    throw;
                }

                Console.WriteLine($"Чекаємо {delayMs} мс перед повторною спробою...");
                Thread.Sleep(delayMs);
                
                // Експоненціальна затримка
                delayMs *= 2;
                retryCount++;
            }
            finally
            {
                Console.WriteLine("--- Завершення спроби підключення ---");
            }
        }
    }
}
