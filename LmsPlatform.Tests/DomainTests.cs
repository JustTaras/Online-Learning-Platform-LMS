using LmsPlatform.Domain;

namespace LmsPlatform.Tests;

/// <summary>
/// Тести для базового класу Entity та інтерфейсу IValidatable
/// </summary>
public class EntityTests
{
    [Fact]
    public void Entity_ShouldHaveUniqueGuid()
    {
        // Arrange & Act
        var course1 = new Course("C#", "Основи");
        var course2 = new Course("Python", "Основи");

        // Assert
        Assert.NotEqual(course1.Id, course2.Id);
        Assert.NotEqual(Guid.Empty, course1.Id);
        Assert.NotEqual(Guid.Empty, course2.Id);
    }
}

/// <summary>
/// Тести для класу Course
/// </summary>
public class CourseTests
{
    [Fact]
    public void Course_ShouldCreateWithValidData()
    {
        // Arrange & Act
        var course = new Course("C# 101", "Основи C#");

        // Assert
        Assert.NotNull(course);
        Assert.Equal("C# 101", course.Title);
        Assert.Equal("Основи C#", course.Description);
        Assert.Empty(course.Lessons);
    }

    [Fact]
    public void Course_ShouldThrowExceptionForEmptyTitle()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Course("", "Опис"));
    }

    [Fact]
    public void Course_ShouldThrowExceptionForEmptyDescription()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Course("Курс", ""));
    }

    [Fact]
    public void Course_ShouldThrowExceptionForLongTitle()
    {
        // Arrange
        string longTitle = new string('a', 256);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Course(longTitle, "Опис"));
    }

    [Fact]
    public void Course_ShouldAddLessonWithMethod()
    {
        // Arrange
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Вступний урок", 1);

        // Act
        course.AddLesson(lesson);

        // Assert
        Assert.Single(course.Lessons);
        Assert.Contains(lesson, course.Lessons);
    }

    [Fact]
    public void Course_ShouldAddLessonWithPlusOperator()
    {
        // Arrange
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Вступний урок", 1);

        // Act
        course += lesson;

        // Assert
        Assert.Single(course.Lessons);
    }

    [Fact]
    public void Course_ShouldThrowWhenAddingNullLesson()
    {
        // Arrange
        var course = new Course("C#", "Основи");

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => course.AddLesson(null!));
    }

    [Fact]
    public void Course_ShouldValidateWithLessons()
    {
        // Arrange
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Вступний урок", 1);
        course.AddLesson(lesson);

        // Act
        var isValid = course.Validate();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void Course_ShouldNotValidateWithoutLessons()
    {
        // Arrange
        var course = new Course("C#", "Основи");

        // Act
        var isValid = course.Validate();

        // Assert
        Assert.False(isValid);
    }
}

/// <summary>
/// Тести для класу Lesson
/// </summary>
public class LessonTests
{
    [Fact]
    public void Lesson_ShouldCreateWithValidData()
    {
        // Arrange & Act
        var lesson = new Lesson("Вступ до C#", "Базові поняття", 1);

        // Assert
        Assert.NotNull(lesson);
        Assert.Equal("Вступ до C#", lesson.Title);
        Assert.Equal("Базові поняття", lesson.Description);
        Assert.Equal(1, lesson.OrderNumber);
    }

    [Fact]
    public void Lesson_ShouldThrowExceptionForEmptyTitle()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Lesson("", "Опис", 1));
    }

    [Fact]
    public void Lesson_ShouldThrowExceptionForInvalidOrder()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new Lesson("Урок", "Опис", 0));
    }

    [Fact]
    public void Lesson_ShouldValidate()
    {
        // Arrange
        var lesson = new Lesson("Урок", "Опис", 1);

        // Act
        var isValid = lesson.Validate();

        // Assert
        Assert.True(isValid);
    }
}

/// <summary>
/// Тести для класу StudentProgress
/// </summary>
public class StudentProgressTests
{
    [Fact]
    public void StudentProgress_ShouldCreateWithValidData()
    {
        // Arrange
        var studentId = Guid.NewGuid();
        var courseId = Guid.NewGuid();

        // Act
        var progress = new StudentProgress(studentId, courseId, 5);

        // Assert
        Assert.Equal(studentId, progress.StudentId);
        Assert.Equal(courseId, progress.CourseId);
        Assert.Equal(5, progress.TotalLessons);
        Assert.Equal(0, progress.CompletedLessons);
        Assert.Equal(0, progress.ProgressPercentage);
    }

    [Fact]
    public void StudentProgress_ShouldThrowForEmptyStudentId()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new StudentProgress(Guid.Empty, Guid.NewGuid(), 5));
    }

    [Fact]
    public void StudentProgress_ShouldThrowForEmptyCourseId()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new StudentProgress(Guid.NewGuid(), Guid.Empty, 5));
    }

    [Fact]
    public void StudentProgress_ShouldThrowForInvalidTotalLessons()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            new StudentProgress(Guid.NewGuid(), Guid.NewGuid(), 0));
    }

    [Fact]
    public void StudentProgress_ShouldUpdateProgress()
    {
        // Arrange
        var progress = new StudentProgress(Guid.NewGuid(), Guid.NewGuid(), 10);

        // Act
        progress.UpdateProgress(5);

        // Assert
        Assert.Equal(5, progress.CompletedLessons);
        Assert.Equal(50, progress.ProgressPercentage);
    }

    [Fact]
    public void StudentProgress_ShouldValidate()
    {
        // Arrange
        var progress = new StudentProgress(Guid.NewGuid(), Guid.NewGuid(), 5);
        progress.UpdateProgress(2);

        // Act
        var isValid = progress.Validate();

        // Assert
        Assert.True(isValid);
    }

    [Fact]
    public void StudentProgress_ShouldThrowForInvalidCompletedLessons()
    {
        // Arrange
        var progress = new StudentProgress(Guid.NewGuid(), Guid.NewGuid(), 5);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            progress.UpdateProgress(10)); // Більше, ніж всього
    }
}



/// <summary>
/// Тести для паттерну State (стани курсу)
/// </summary>
public class CourseStateTests
{
    [Fact]
    public void Course_ShouldBeInDraftStateByDefault()
    {
        // Arrange & Act
        var course = new Course("C#", "Основи");

        // Assert
        Assert.Equal("Draft", course.CourseStatus);
    }

    [Fact]
    public void Course_ShouldPublishWhenHasLessons()
    {
        // Arrange
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Опис", 1);
        course.AddLesson(lesson);

        // Act
        course.PublishCourse();

        // Assert
        Assert.Equal("Published", course.CourseStatus);
    }

    [Fact]
    public void Course_ShouldThrowWhenPublishingWithoutLessons()
    {
        // Arrange
        var course = new Course("C#", "Основи");

        // Act & Assert
        Assert.Throws<InvalidCourseOperationException>(() => course.PublishCourse());
    }

    [Fact]
    public void Course_ShouldArchiveWhenPublished()
    {
        // Arrange
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Опис", 1);
        course.AddLesson(lesson);
        course.PublishCourse();

        // Act
        course.ArchiveCourse();

        // Assert
        Assert.Equal("Archived", course.CourseStatus);
    }
}

/// <summary>
/// Тести для Repository<T>
/// </summary>
public class RepositoryTests
{
    [Fact]
    public void Repository_ShouldAddAndStoreItem()
    {
        // Arrange
        var repository = new Repository<Course>();
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Опис", 1);
        course.AddLesson(lesson);

        // Act
        repository.Add(course);
        var all = repository.GetAll().ToList();

        // Assert
        Assert.Single(all);
        Assert.Contains(course, all);
    }

    [Fact]
    public void Repository_ShouldRemoveItem()
    {
        // Arrange
        var repository = new Repository<Course>();
        var course = new Course("C#", "Основи");
        var lesson = new Lesson("Вступ", "Опис", 1);
        course.AddLesson(lesson);
        repository.Add(course);

        // Act
        repository.Remove(course.Id);
        var all = repository.GetAll().ToList();

        // Assert
        Assert.Empty(all);
    }
}
