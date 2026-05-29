# LmsPlatform - UML Class Diagram

## Architecture Overview
Comprehensive UML Class Diagram representing the LmsPlatform domain layer with implemented design patterns: State, Strategy, Observer, and Factory.

```mermaid
classDiagram
    %% Base Classes
    class Entity {
        -Guid Id
        #Entity()
        #Entity(Guid id)
    }

    %% Interfaces
    class IValidatable {
        <<interface>>
        +Validate() bool
    }

    %% Core Domain Entities
    class Course {
        -string _title
        -string _description
        -List~Lesson~ _lessons
        -CourseStateContext _stateContext
        +string Title
        +string Description
        +IReadOnlyList~Lesson~ Lessons
        +string CourseStatus
        +CourseStateContext StateContext
        +Course(string title, string description)
        +AddLesson(Lesson lesson) void
        +PublishCourse() void
        +Validate() bool
    }

    class Lesson {
        -string _title
        -string _description
        -int _orderNumber
        +string Title
        +string Description
        +int OrderNumber
        +Lesson(string, string, int)
        +Validate() bool
    }

    class Test {
        -string _title
        -string _description
        -int _totalQuestions
        -ITestGradingStrategy _gradingStrategy
        +string Title
        +string Description
        +int TotalQuestions
        +ITestGradingStrategy GradingStrategy
        +Test(string, string, int, ITestGradingStrategy)
        +Test(string, string, int)
        +Validate() bool
    }

    class StudentProgress {
        -Guid _studentId
        -Guid _courseId
        -int _completedLessons
        -int _totalLessons
        -double _progressPercentage
        +Guid StudentId
        +Guid CourseId
        +int CompletedLessons
        +int TotalLessons
        +double ProgressPercentage
        +StudentProgress(Guid, Guid, int)
        +Validate() bool
    }

    %% State Pattern
    class ICourseState {
        <<interface>>
        +string StateName
        +PublishCourse(CourseStateContext) void
        +ArchiveCourse(CourseStateContext) void
        +RevertToDraft(CourseStateContext) void
        +CanAddLessons() bool
        +CanModify() bool
    }

    class DraftState {
        +string StateName
        +PublishCourse(CourseStateContext) void
        +ArchiveCourse(CourseStateContext) void
        +RevertToDraft(CourseStateContext) void
        +CanAddLessons() bool
        +CanModify() bool
    }

    class PublishedState {
        +string StateName
        +PublishCourse(CourseStateContext) void
        +ArchiveCourse(CourseStateContext) void
        +RevertToDraft(CourseStateContext) void
        +CanAddLessons() bool
        +CanModify() bool
    }

    class ArchivedState {
        +string StateName
        +PublishCourse(CourseStateContext) void
        +ArchiveCourse(CourseStateContext) void
        +RevertToDraft(CourseStateContext) void
        +CanAddLessons() bool
        +CanModify() bool
    }

    class CourseStateContext {
        -ICourseState _currentState
        +ICourseState CurrentState
        +CourseStateContext()
        +CourseStateContext(ICourseState initialState)
        +SetState(ICourseState) void
        +PublishCourse() void
        +ArchiveCourse() void
        +RevertToDraft() void
        +CanAddLessons() bool
        +CanModify() bool
    }

    %% Strategy Pattern
    class ITestGradingStrategy {
        <<interface>>
        +CalculateScore(int, int) double
        +GetGradeDescription() string
        +IsPassed(double) bool
    }

    class PassFailGradingStrategy {
        -double PassingScore$
        +CalculateScore(int, int) double
        +GetGradeDescription() string
        +IsPassed(double) bool
    }

    class StrictGradingStrategy {
        -double PassingScore$
        +CalculateScore(int, int) double
        +GetGradeDescription() string
        +IsPassed(double) bool
    }

    %% Observer Pattern
    class DeadlineEventArgs {
        +Guid CourseId
        +string CourseName
        +DateTime Deadline
        +int DaysRemaining
        +DeadlineEventArgs(Guid, string, DateTime)
    }

    class DeadlineNotifier {
        -Dictionary~Guid, DateTime~ _courseDeadlines
        +event EventHandler~DeadlineEventArgs~ DeadlineApproaching
        +event EventHandler~DeadlineEventArgs~ DeadlinePassed
        +RegisterDeadline(Guid, string, DateTime) void
        +RemoveDeadline(Guid) void
        +CheckDeadlines() void
        +Subscribe(EventHandler, bool) void
        +Unsubscribe(EventHandler, bool) void
    }

    %% Factory Pattern
    class CourseFactory {
        <<abstract>>
        +CreateCourse() Course*
        +CreateBeginnerCourseFactory()$ BeginnerCourseFactory
        +CreateIntermediateCourseFactory()$ IntermediateCourseFactory
        +CreateAdvancedCourseFactory()$ AdvancedCourseFactory
    }

    class BeginnerCourseFactory {
        +CreateCourse() Course
    }

    class IntermediateCourseFactory {
        +CreateCourse() Course
    }

    class AdvancedCourseFactory {
        +CreateCourse() Course
    }

    %% Relationships
    Entity <|-- Course
    Entity <|-- Lesson
    Entity <|-- Test
    Entity <|-- StudentProgress
    
    Course --|> IValidatable
    Lesson --|> IValidatable
    Test --|> IValidatable
    StudentProgress --|> IValidatable
    
    Course *-- CourseStateContext
    CourseStateContext --> ICourseState
    
    ICourseState <|.. DraftState
    ICourseState <|.. PublishedState
    ICourseState <|.. ArchivedState
    
    Course *-- "0..*" Lesson
    
    Test --> ITestGradingStrategy
    
    ITestGradingStrategy <|.. PassFailGradingStrategy
    ITestGradingStrategy <|.. StrictGradingStrategy
    
    DeadlineNotifier --> DeadlineEventArgs
    
    CourseFactory <|-- BeginnerCourseFactory
    CourseFactory <|-- IntermediateCourseFactory
    CourseFactory <|-- AdvancedCourseFactory
    
    BeginnerCourseFactory --> Course
    IntermediateCourseFactory --> Course
    AdvancedCourseFactory --> Course
```

## Design Patterns Implemented

### 1. **State Pattern** (State Folder)
- **Context**: `CourseStateContext`
- **States**: `DraftState`, `PublishedState`, `ArchivedState`
- **Interface**: `ICourseState`
- Manages course lifecycle through state transitions

### 2. **Strategy Pattern** (Strategy Folder)
- **Context**: `Test` class
- **Strategy Interface**: `ITestGradingStrategy`
- **Implementations**: `PassFailGradingStrategy`, `StrictGradingStrategy`
- Dynamically switches grading algorithms

### 3. **Observer Pattern** (Observer Folder)
- **Subject**: `DeadlineNotifier`
- **Events**: `DeadlineApproaching`, `DeadlinePassed`
- **Event Args**: `DeadlineEventArgs`
- Notifies subscribers about deadline changes

### 4. **Factory Pattern** (Factory Folder)
- **Abstract Factory**: `CourseFactory`
- **Concrete Factories**: `BeginnerCourseFactory`, `IntermediateCourseFactory`, `AdvancedCourseFactory`
- Creates pre-configured course instances

## Key Relationships

- **Inheritance**: All entities inherit from `Entity` base class
- **Interface Implementation**: Validatable entities implement `IValidatable`
- **Composition**: Course contains multiple Lessons
- **Aggregation**: Course uses CourseStateContext
- **Dependency Injection**: Test uses ITestGradingStrategy
