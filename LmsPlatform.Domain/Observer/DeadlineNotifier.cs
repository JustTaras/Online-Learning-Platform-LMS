namespace LmsPlatform.Domain.Observer;

public class DeadlineNotifier
{
    private readonly Dictionary<Guid, DateTime> _courseDeadlines = new();
    
    public event EventHandler<DeadlineEventArgs>? DeadlineApproaching;
    public event EventHandler<DeadlineEventArgs>? DeadlinePassed;

    public void RegisterDeadline(Guid courseId, string courseName, DateTime deadline)
    {
        if (courseId == Guid.Empty)
            throw new ArgumentException("ID курсу не може бути порожніст.", nameof(courseId));
        
        if (string.IsNullOrWhiteSpace(courseName))
            throw new ArgumentException("Назва курсу не може бути порожною.", nameof(courseName));
        
        _courseDeadlines[courseId] = deadline;
    }

    public void RemoveDeadline(Guid courseId)
    {
        _courseDeadlines.Remove(courseId);
    }

    public void CheckDeadlines()
    {
        var now = DateTime.UtcNow;
        var coursesToCheck = new List<Guid>(_courseDeadlines.Keys);

        foreach (var courseId in coursesToCheck)
        {
            var deadline = _courseDeadlines[courseId];
            var daysRemaining = (deadline - now).TotalDays;

            if (daysRemaining <= 0)
            {
                OnDeadlinePassed(courseId, deadline);
            }
            else if (daysRemaining <= 3)
            {
                OnDeadlineApproaching(courseId, deadline);
            }
        }
    }

    protected virtual void OnDeadlineApproaching(Guid courseId, DateTime deadline)
    {
        DeadlineApproaching?.Invoke(this, new DeadlineEventArgs(courseId, $"Course_{courseId}", deadline));
    }

    protected virtual void OnDeadlinePassed(Guid courseId, DateTime deadline)
    {
        DeadlinePassed?.Invoke(this, new DeadlineEventArgs(courseId, $"Course_{courseId}", deadline));
        RemoveDeadline(courseId);
    }

    public void Subscribe(EventHandler<DeadlineEventArgs> handler, bool approaching = true)
    {
        if (handler == null)
            throw new ArgumentNullException(nameof(handler));
        
        if (approaching)
        {
            DeadlineApproaching += handler;
        }
        else
        {
            DeadlinePassed += handler;
        }
    }

    public void Unsubscribe(EventHandler<DeadlineEventArgs> handler, bool approaching = true)
    {
        if (handler == null)
            throw new ArgumentNullException(nameof(handler));
        
        if (approaching)
        {
            DeadlineApproaching -= handler;
        }
        else
        {
            DeadlinePassed -= handler;
        }
    }
}
