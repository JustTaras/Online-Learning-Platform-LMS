namespace LmsPlatform.App.DTOs;

public class StudentProgressDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public int CompletedLessons { get; set; }
    public int TotalLessons { get; set; }
    public double ProgressPercentage { get; set; }
}
