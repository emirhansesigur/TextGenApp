using Vocabulary.Application.Models.DataTransfer;
using Vocabulary.Application.Services;

namespace Vocabulary.Infrastructure.Services;

public class TextGenService(HttpClient _httpClient) : ITextGenService
{
    public Task<WordListGenerationDto> GetWordListGenerationAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
