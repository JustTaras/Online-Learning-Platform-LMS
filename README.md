# LMS Platform - Система управління онлайн-навчанням

Комплексна реалізація системи управління навчанням (LMS), яка демонструє корпоративні паттерни проектування та принципи чистої архітектури на C#.

## Огляд архітектури

Рішення організовано в три основних проєкти:

### LmsPlatform.Domain
Основна бізнес-логіка та моделі домену, що реалізують кілька паттернів проектування:

- **Сутності**: `Course`, `Lesson`, `StudentProgress`, `Test`
- **Паттерн Repository**: Загальна `Repository<T>` для абстракції доступу до даних
- **Валідація**: Інтерфейс `IValidatable` для валідації об'єктів домену
- **Паттерни проектування**:
  - **Паттерн State**: Управління життєвим циклом курсу (Draft → Published → Archived)
  - **Паттерн Strategy**: Замінювані стратегії оцінювання (PassFail, Strict)
  - **Паттерн Factory**: `CourseFactory` для створення об'єктів
  - **Паттерн Observer**: `DeadlineNotifier` для сповіщень про терміни
- **Методи розширення**: Утиліти для операцій з колекціями курсів

### LmsPlatform.App
Прикладний шар з DTO, мапуванням та збереженням:

- **DTO**: `CourseDto`, `LessonDto`, `StudentProgressDto`
- **Мапування**: `DtoMapper` для трансформацій Domain ↔ DTO
- **Збереження**: `JsonPersistence` з використанням `System.Text.Json`
  - Збереження/завантаження курсів та прогресу студентів у/з JSON
  - Правильно обробляє циклічні посилання через розгортання DTO
- **CLI**: Командний рядок для демонстрації функцій LMS

### LmsPlatform.Tests
Комплексний набір тестів з використанням xUnit, Moq та FluentAssertions:

- **Тести паттерна Strategy**: Валідація алгоритмів оцінювання
- **Тести паттерна State**: Переходи станів курсу
- **Тести Repository**: Ізоляція доступу до даних та верифікація
- **Інтеграція Moq**: Mock-тестування поведінки репозиторію

## Реалізовані паттерни проектування

### 1. Паттерн State
Управління життєвим циклом курсу через стани:
```
Draft → Published → Archived (з можливістю повернення до Draft)
```
- Контролює дозволені операції в кожному стані
- Запобігає недійсним переходам станів

### 2. Паттерн Strategy
Замінювані алгоритми оцінювання:
- `PassFailGradingStrategy`: поріг складання 50%
- `StrictGradingStrategy`: поріг складання 80%
- Легко додавати нові стратегії через `ITestGradingStrategy`

### 3. Паттерн Factory
`CourseFactory` інкапсулює створення курсів з послідовною ініціалізацією.

### 4. Паттерн Observer
`DeadlineNotifier` спостерігає терміни курсів та сповіщає підписників через користувацькі події.

### 5. Паттерн Repository
Загальна `Repository<T>` забезпечує:
- Типобезпечний доступ до даних
- Інкапсульоване внутрішнє зберігання
- Чітке розділення завдань

## Технологічний стек

- **.NET 10.0**
- **Мова**: C# 13
- **Тестування**: xUnit, Moq, FluentAssertions
- **Серіалізація**: System.Text.Json

## Початок роботи

### Передумови
- .NET 10.0 SDK або пізніша версія
- Visual Studio 2022 / VS Code / Rider

### Збирання та запуск

```bash
# Відновлення пакетів
dotnet restore

# Збирання рішення
dotnet build LmsPlatform.slnx

# Запуск консольного додатку
dotnet run --project LmsPlatform.App

# Запуск тестів
dotnet test

# Запуск тестів з покриттям
dotnet test /p:CollectCoverage=true
```

## Структура проєкту

```
LmsPlatform/
├── LmsPlatform.Domain/          # Основна бізнес-логіка
│   ├── Course.cs
│   ├── Lesson.cs
│   ├── StudentProgress.cs
│   ├── Repository.cs
│   ├── State/                   # Реалізація паттерна State
│   │   ├── ICourseState.cs
│   │   ├── DraftState.cs
│   │   ├── PublishedState.cs
│   │   └── ArchivedState.cs
│   ├── Strategy/                # Реалізація паттерна Strategy
│   │   ├── ITestGradingStrategy.cs
│   │   ├── PassFailGradingStrategy.cs
│   │   └── StrictGradingStrategy.cs
│   ├── Factory/                 # Паттерн Factory
│   │   └── CourseFactory.cs
│   └── Observer/                # Паттерн Observer
│       └── DeadlineNotifier.cs
│
├── LmsPlatform.App/             # Прикладний шар
│   ├── Program.cs               # Точка входу CLI
│   ├── DTOs/                    # Об'єкти передачі даних
│   │   ├── CourseDto.cs
│   │   ├── LessonDto.cs
│   │   └── StudentProgressDto.cs
│   ├── Mapping/                 # Логіка мапування DTO
│   │   └── DtoMapper.cs
│   └── Persistence/             # JSON-збереження
│       └── JsonPersistence.cs
│
└── LmsPlatform.Tests/           # Модульні тести
    ├── DomainTests.cs           # Тести паттернів Strategy, State, Repository
    └── LmsPlatform.Tests.csproj
```

## Основні функції

### Управління курсами
- Створення та управління курсами з уроками
- Життєвий цикл курсу на основі станів (Draft, Published, Archived)
- Додавання/видалення уроків на основі стану курсу
- Валідація цілісності курсу

### Відстеження прогресу студентів
- Відстеження завершених уроків для кожного студента
- Розрахунок відсотка прогресу
- Агрегація прогресу студентів та курсів
- Оновлення прогресу з валідацією

### Стратегії оцінювання
- Кілька замінюваних алгоритмів оцінювання
- PassFail (поріг 50%)
- Strict (поріг 80%)
- Легко розширювана для нових стратегій

### Збереження даних
- Серіалізація курсів та прогресу у JSON
- Обробка циклічних посилань через DTO
- Завантаження даних назад у об'єкти домену
- Типобезпечна серіалізація з System.Text.Json

### Тестування
- Модульні тести для всіх паттернів
- Валідація паттерна Strategy
- Тестування переходів станів
- Mock-тестування репозиторію для ізоляції
- Структура AAA (Arrange-Act-Assert) скрізь

## Приклади тестів

### Тест паттерна Strategy
```csharp
[Fact]
public void PassFailGradingStrategy_IsPassed_WithScoreAboveThreshold_ShouldReturnTrue()
{
    // Arrange
    var strategy = new PassFailGradingStrategy();
    double passingScore = 0.5;

    // Act
    bool result = strategy.IsPassed(passingScore);

    // Assert
    result.Should().BeTrue();
}
```

### Тест паттерна State
```csharp
[Fact]
public void CourseStateContext_PublishCourse_WithLessons_ShouldChangeToPublishedState()
{
    // Arrange
    var course = new Course("Test Course", "Test Description");
    var lesson = new Lesson("Lesson 1", "Content", 1);
    course += lesson;

    // Act
    course.PublishCourse();

    // Assert
    course.CourseStatus.Should().Be("Published");
}
```

### Тест Mock репозиторію
```csharp
[Fact]
public void CourseManager_WithMockedRepository_ShouldUseRepositoryMethods()
{
    // Arrange
    var mockRepository = new Mock<Repository<Course>>();
    // Налаштування поведінки mock
    
    // Act & Assert з верифікацією
    mockRepository.Verify(r => r.GetAll(), Times.Once);
}
```

## Розширення системи

### Додавання нової стратегії оцінювання
1. Реалізуйте `ITestGradingStrategy`
2. Надайте реалізації `CalculateScore()`, `GetGradeDescription()` та `IsPassed()`
3. Зареєструйте стратегію в контейнері залежностей

### Додавання нового стану курсу
1. Реалізуйте `ICourseState`
2. Визначте поведінку, специфічну для стану
3. Оновіть `CourseStateContext` з переходами стану

### Додавання збереження у базу даних
1. Реалізуйте інтерфейс `IPersistence`
2. Створіть клас `DbPersistence` з використанням Entity Framework
3. Замініть `JsonPersistence` на `DbPersistence` в інжекції залежностей

## Внесок

Цей проєкт демонструє найкращі практики, включно з:
- Принципами SOLID
- Паттернами проектування
- Чистим кодом
- Комплексним тестуванням
- Проектуванням на основі домену
- Готовністю до інжекції залежностей

## Ліцензія

Цей проєкт призначений для освітніх цілей.
