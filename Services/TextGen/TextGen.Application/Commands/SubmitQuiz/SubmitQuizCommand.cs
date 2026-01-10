using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Commands.SubmitQuiz;

public class SubmitQuizCommand : IRequest<SubmitQuizResponseModel>
{
    public Guid TextId { get; set; }
    public List<UserAnswerDto> Answers { get; set; } = new();
}
public record UserAnswerDto(Guid QuestionId, int SelectedAnswerIndex);

public class SubmitQuizCommandHandler(ITextGenDbContext _dbContext) : IRequestHandler<SubmitQuizCommand, SubmitQuizResponseModel>
{
    public async Task<SubmitQuizResponseModel> Handle(SubmitQuizCommand request, CancellationToken cancellationToken)
    {
        var generatedText = await _dbContext.GeneratedTexts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.TextId, cancellationToken);

        if (generatedText == null)
            throw new Exception($"{request.TextId} ID'li içerik bulunamadı.");

        var response = new SubmitQuizResponseModel
        {
            TotalQuestions = generatedText.Quiz.Count
        };

        foreach (var userAnswer in request.Answers)
        {
            var originalQuestion = generatedText.Quiz.FirstOrDefault(q => q.Id == userAnswer.QuestionId);

            if (originalQuestion == null)
            {
                throw new Exception($"Geçersiz Soru ID: {userAnswer.QuestionId}. Bu soru, ilgili metne ait değil.");
            }

            bool isCorrect = originalQuestion.CorrectAnswer == userAnswer.SelectedAnswerIndex;

            if (isCorrect) response.CorrectAnswersCount++;

            response.Details.Add(new QuizResultItemDto(
                originalQuestion.Id,
                isCorrect,
                userAnswer.SelectedAnswerIndex,
                originalQuestion.CorrectAnswer
            ));
        }

        response.Score = response.TotalQuestions > 0
            ? Math.Round((double)response.CorrectAnswersCount / response.TotalQuestions * 100, 2)
            : 0;

        return response;
    }
}