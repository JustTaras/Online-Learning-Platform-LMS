namespace LmsPlatform.Domain.Strategy;

public interface ITestGradingStrategy
{
    double CalculateScore(int correctAnswers, int totalQuestions);
    string GetGradeDescription();
    bool IsPassed(double score);
}
