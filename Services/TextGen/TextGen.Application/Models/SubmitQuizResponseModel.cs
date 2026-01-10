namespace TextGen.Application.Models;

public record SubmitQuizResponseModel
{
    public int TotalQuestions { get; set; }
    public int CorrectAnswersCount { get; set; }
    public double Score { get; set; }
    public List<QuizResultItemDto> Details { get; set; } = new();
}

public record QuizResultItemDto(Guid QuestionId, bool IsCorrect, int SelectedAnswer, int CorrectAnswer);