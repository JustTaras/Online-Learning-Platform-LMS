using LmsPlatform.Domain;

namespace LmsPlatform.Tests;

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
    }

    [Fact]
    public void Course_ShouldThrowExceptionForInvalidTitle()
    {
        // Assert
        Assert.Throws<ArgumentException>(() => new Course("", "Опис"));
    }

    [Fact]
    public void Course_ShouldAddLessonWithPlusOperator()
    {
        // Arrange
        var course = new Course("C# 101", "Основи C#");
        var lesson = new Lesson("Вступ", "Вступний урок", 1);

        // Act
        course += lesson;

        // Assert
        Assert.Single(course.Lessons);
    }

    [Fact]
    public void Course_ShouldValidateWithLessons()
    {
        // Arrange
        var course = new Course("C# 101", "Основи C#");
        var lesson = new Lesson("Вступ", "Вступний урок", 1);

        // Act
        course.AddLesson(lesson);
        var isValid = course.Validate();

        // Assert
        Assert.True(isValid);
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
        Assert.Equal(1, lesson.OrderNumber);
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
}
