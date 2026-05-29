namespace LmsPlatform.Domain.State;

public class CourseStateContext
{
    private ICourseState _currentState;

    public ICourseState CurrentState => _currentState;

    public CourseStateContext()
    {
        _currentState = new DraftState();
    }

    public CourseStateContext(ICourseState initialState)
    {
        if (initialState == null)
            throw new ArgumentNullException(nameof(initialState));
        
        _currentState = initialState;
    }

    public void SetState(ICourseState state)
    {
        if (state == null)
            throw new ArgumentNullException(nameof(state));
        
        _currentState = state;
    }

    public void PublishCourse()
    {
        _currentState.PublishCourse(this);
    }

    public void ArchiveCourse()
    {
        _currentState.ArchiveCourse(this);
    }

    public void RevertToDraft()
    {
        _currentState.RevertToDraft(this);
    }

    public bool CanAddLessons() => _currentState.CanAddLessons();

    public bool CanModify() => _currentState.CanModify();
}
