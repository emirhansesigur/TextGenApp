using System;
using System.Collections.Generic;
using System.Text;
using TextGen.Application.Models.DataTransfer;

namespace TextGen.Application.Services;

public interface IVocabularyService
{
    // Metot ismini ihtiyacına göre düzenle
    Task<List<UserWordListDto>> GetUserWordListsAsync();
}