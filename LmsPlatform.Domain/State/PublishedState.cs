namespace LmsPlatform.Domain.State;

public class PublishedState : ICourseState
{
    public string StateName => "Published";

    public void PublishCourse(CourseStateContext context)
    {
        throw new InvalidCourseOperationException("Курс вже опублікований.");
    }

    public void ArchiveCourse(CourseStateContext context)
    {
        context.SetState(new ArchivedState());
    }

    public void RevertToDraft(CourseStateContext context)
    {
        context.SetState(new DraftState());
    }

    public bool CanAddLessons() => false;

    public bool CanModify() => false;
}
