using TextGen.Application.Models.DataTransfer;

namespace TextGen.Application.Services;
public interface IVocabularyService
{
    Task<UserWordListDto> GetUserWordListAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> SaveGeneratedWordListAsync(UserWordListDto userWordListDto, CancellationToken cancellationToken);
}