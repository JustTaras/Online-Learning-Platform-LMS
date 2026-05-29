namespace LmsPlatform.Domain.State;

public interface ICourseState
{
    string StateName { get; }
    void PublishCourse(CourseStateContext context);
    void ArchiveCourse(CourseStateContext context);
    void RevertToDraft(CourseStateContext context);
    bool CanAddLessons();
    bool CanModify();
}
