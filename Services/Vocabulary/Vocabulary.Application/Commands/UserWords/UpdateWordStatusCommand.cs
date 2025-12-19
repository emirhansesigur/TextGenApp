using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Interfaces;
using Vocabulary.Core.Enum;

namespace Vocabulary.Application.Commands.UserWords;

public class UpdateWordStatusCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public LearningStatus NewStatus { get; set; }
}


public class UpdateWordStatusCommandHandler(IVocabularyDbContext _context) : IRequestHandler<UpdateWordStatusCommand, bool>
{
    public async Task<bool> Handle(UpdateWordStatusCommand request, CancellationToken cancellationToken)
    {
        var word = await _context.UserWords
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (word == null) return false;

        word.Status = request.NewStatus;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}