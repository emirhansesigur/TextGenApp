using Vocabulary.Application.Models.DataTransfer;

namespace Vocabulary.Application.Services;

public interface ITextGenService
{
    Task<WordListGenerationDto> GetWordListGenerationAsync(CancellationToken cancellationToken);
}
