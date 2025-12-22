using MediatR;
using Vocabulary.Application.Interfaces;
using Vocabulary.Core.Entities;
using Vocabulary.Core.Enum;

namespace Vocabulary.Application.Commands.UserWordLists;

public class CreateGeneratedWordListCommand : IRequest<UserWordList>
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = default!;
    public string? Level { get; set; }
    public List<UserWord> UserWords { get; set; } = new();
}
public class CreateGeneratedWordListCommandHandler(IVocabularyDbContext _context)
    : IRequestHandler<CreateGeneratedWordListCommand, UserWordList>
{
    public async Task<UserWordList> Handle(CreateGeneratedWordListCommand request, CancellationToken cancellationToken)
    {
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001");

        var wordList = new UserWordList
        {
            Id = Guid.NewGuid(),
            UserId = userIdFromAuth,
            Name = request.Name,
            Level = request.Level
        };

        wordList.UserWords = request.UserWords.Select(userWord => new UserWord
        {
            Id = Guid.NewGuid(),
            UserWordListId = wordList.Id,
            Text = userWord.Text,
            Meaning = userWord.Meaning,
            Status = LearningStatus.ToLearn
        }).ToList();

        _context.UserWordLists.Add(wordList);
        await _context.SaveChangesAsync(cancellationToken);

        return wordList;
    }
}