namespace LmsPlatform.Domain;

using LmsPlatform.Domain.Factory;
using LmsPlatform.Domain.Observer;
using LmsPlatform.Domain.State;
using LmsPlatform.Domain.Strategy;

/// <summary>
/// Демонстрація використання всіх впроваджених паттернів
/// </summary>
public class DesignPatternsDemonstration
{
    /// <summary>
    /// Демонструє паттерн Factory для створення попередньо налаштованих курсів
    /// </summary>
    public static void DemonstrateFactoryPattern()
    {
        var beginnerFactory = CourseFactory.CreateBeginnerCourseFactory();
        var beginnerCourse = beginnerFactory.CreateCourse();
        
        var intermediateFactory = CourseFactory.CreateIntermediateCourseFactory();
        var intermediateCourse = intermediateFactory.CreateCourse();
        
        var advancedFactory = CourseFactory.CreateAdvancedCourseFactory();
        var advancedCourse = advancedFactory.CreateCourse();
    }

    /// <summary>
    /// Демонструє паттерн State для управління станом курсу
    /// </summary>
    public static void DemonstrateStatePattern()
    {
        var course = new Course("Курс C#", "Вивчення мови C#");
        
        // Перебуває у стані Draft - можна додавати уроки
        course.AddLesson(new Lesson("Вступ до C#", "Основні концепції", 1));
        course.AddLesson(new Lesson("Змінні та типи", "Робота зі змінними", 2));
        
        // Публікування курсу
        course.PublishCourse(); // Перехід у стан Published
        
        // У стані Published не можна додавати уроки
        try
        {
            course.AddLesson(new Lesson("Нові матеріали", "Не можна додати", 3));
        }
        catch (InvalidCourseOperationException)
        {
            // Очікувана помилка
        }
        
        // Архівування курсу
        course.ArchiveCourse(); // Перехід у стан Archived
        
        // У стані Archived також не можна додавати уроки
        try
        {
            course.AddLesson(new Lesson("Ще матеріали", "Не можна додати", 4));
        }
        catch (InvalidCourseOperationException)
        {
            // Очікувана помилка
        }
        
        // Повернення до Draft стану
        course.RevertToDraft(); // Перехід назад у стан Draft
    }

    /// <summary>
    /// Демонструє паттерн Strategy для різних стратегій оцінювання тестів
    /// </summary>
    public static void DemonstrateStrategyPattern()
    {
        // Тест з суворою стратегією оцінювання (90% для проходження)
        var strictStrategy = new StrictGradingStrategy();
        var strictTest = new Test("Математика", "Тест з математики", 10, strictStrategy);
        
        // Тест з простою стратегією (50% для проходження)
        var passFailStrategy = new PassFailGradingStrategy();
        var passFailTest = new Test("Історія", "Тест з історії", 10, passFailStrategy);
        
        // Оцінювання за суворою стратегією
        double strictScore = strictTest.CalculateScore(8); // 8/10 = 0.8 (80%) - не пройдено
        bool strictPassed = strictTest.IsPassed(8);
        
        // Оцінювання за простою стратегією
        double passFailScore = passFailTest.CalculateScore(6); // 6/10 = 0.6 (60%) - пройдено
        bool passFailPassed = passFailTest.IsPassed(6);
        
        // Зміна стратегії оцінювання для існуючого тесту
        strictTest.SetGradingStrategy(new PassFailGradingStrategy());
        bool nowPassed = strictTest.IsPassed(6); // Тепер 6/10 буде пройдено
    }

    /// <summary>
    /// Демонстрює паттерн Observer для сповіщень про дедлайни
    /// </summary>
    public static void DemonstrateObserverPattern()
    {
        var notifier = new DeadlineNotifier();
        
        // Підписання на сповіщення про наближення дедлайну
        EventHandler<DeadlineEventArgs>? approachingHandler = (sender, e) =>
        {
            Console.WriteLine($"Дедлайн для курсу {e.CourseName} наближається! Днів залишилось: {e.DaysRemaining}");
        };
        
        // Підписання на сповіщення про минулий дедлайн
        EventHandler<DeadlineEventArgs>? passedHandler = (sender, e) =>
        {
            Console.WriteLine($"Дедлайн для курсу {e.CourseName} вже пройшов!");
        };
        
        // Підписання на події
        notifier.Subscribe(approachingHandler, approaching: true);
        notifier.Subscribe(passedHandler, approaching: false);
        
        // Реєстрація дедлайнів
        var courseId1 = Guid.NewGuid();
        var courseId2 = Guid.NewGuid();
        
        notifier.RegisterDeadline(courseId1, "Курс C#", DateTime.UtcNow.AddDays(2));
        notifier.RegisterDeadline(courseId2, "Курс JavaScript", DateTime.UtcNow.AddDays(-1));
        
        // Перевірка дедлайнів
        notifier.CheckDeadlines();
        
        // Відписання від подій для запобігання витокам пам'яті
        notifier.Unsubscribe(approachingHandler, approaching: true);
        notifier.Unsubscribe(passedHandler, approaching: false);
    }

    /// <summary>
    /// Демонструє комбіноване використання всіх паттернів
    /// </summary>
    public static void DemonstrateCombinedPatterns()
    {
        // Використання Factory для створення курсу
        var courseFactory = CourseFactory.CreateIntermediateCourseFactory();
        var course = courseFactory.CreateCourse();
        
        // Використання State для управління станом
        course.PublishCourse();
        
        // Використання Strategy для тестування
        var test = new Test("Проміжний тест", "Перевірка знань", 20, new StrictGradingStrategy());
        
        // Використання Observer для оповіщення про дедлайни
        var notifier = new DeadlineNotifier();
        var deadline = DateTime.UtcNow.AddDays(7);
        notifier.RegisterDeadline(course.Id, course.Title, deadline);
        
        // Підписання на сповіщення
        EventHandler<DeadlineEventArgs>? handler = (sender, e) =>
        {
            Console.WriteLine($"Навчання курсу '{e.CourseName}' - дедлайн через {e.DaysRemaining} днів");
        };
        
        notifier.Subscribe(handler);
        notifier.CheckDeadlines();
        
        // Відписання для уникнення витоків пам'яті
        notifier.Unsubscribe(handler);
    }
}
