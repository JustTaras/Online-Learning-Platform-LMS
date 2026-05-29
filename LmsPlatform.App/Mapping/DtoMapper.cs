using LmsPlatform.App.DTOs;
using LmsPlatform.Domain;

namespace LmsPlatform.App.Mapping;

public static class DtoMapper
{
    public static CourseDto ToCourseDto(Course course)
    {
        if (course == null)
            throw new ArgumentNullException(nameof(course));

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description,
            Lessons = course.Lessons.Select(ToLessonDto).ToList(),
            CourseStatus = course.CourseStatus
        };
    }

    public static LessonDto ToLessonDto(Lesson lesson)
    {
        if (lesson == null)
            throw new ArgumentNullException(nameof(lesson));

        return new LessonDto
        {
            Id = lesson.Id,
            Title = lesson.Title,
            Description = lesson.Description,
            OrderNumber = lesson.OrderNumber
        };
    }

    public static Course ToCourse(CourseDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var course = new Course(dto.Title, dto.Description);
        foreach (var lessonDto in dto.Lessons)
        {
            course.AddLesson(ToLesson(lessonDto));
        }
        return course;
    }

    public static Lesson ToLesson(LessonDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new Lesson(dto.Title, dto.Description, dto.OrderNumber);
    }

    public static StudentProgressDto ToStudentProgressDto(StudentProgress progress)
    {
        if (progress == null)
            throw new ArgumentNullException(nameof(progress));

        return new StudentProgressDto
        {
            Id = progress.Id,
            StudentId = progress.StudentId,
            CourseId = progress.CourseId,
            CompletedLessons = progress.CompletedLessons,
            TotalLessons = progress.TotalLessons,
            ProgressPercentage = progress.ProgressPercentage
        };
    }

    public static StudentProgress ToStudentProgress(StudentProgressDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var progress = new StudentProgress(dto.StudentId, dto.CourseId, dto.TotalLessons);
        if (dto.CompletedLessons > 0)
        {
            for (int i = 0; i < dto.CompletedLessons; i++)
            {
                progress.UpdateProgress(i + 1);
            }
        }
        return progress;
    }
}
