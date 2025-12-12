using System;
using System.Collections.Generic;
using System.Text;
using TextGen.Application.Models.DataTransfer;

namespace TextGen.Application.Services;

public interface IVocabularyService
{
    Task<UserWordListDto> GetUserWordListAsync(Guid id, CancellationToken cancellationToken);
}