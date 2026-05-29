namespace LmsPlatform.App.DTOs;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<LessonDto> Lessons { get; set; } = new();
    public string CourseStatus { get; set; } = string.Empty;
}
