namespace LmsPlatform.Domain.Strategy;

public class PassFailGradingStrategy : ITestGradingStrategy
{
    private const double PassingScore = 0.5;

    public double CalculateScore(int correctAnswers, int totalQuestions)
    {
        if (totalQuestions <= 0)
            throw new ArgumentException("Кількість запитань повинна бути більше нуля.", nameof(totalQuestions));
        
        return (double)correctAnswers / totalQuestions;
    }

    public string GetGradeDescription()
    {
        return "Проста система: Успішно/Невдало (50% для проходження)";
    }

    public bool IsPassed(double score)
    {
        return score >= PassingScore;
    }
}
