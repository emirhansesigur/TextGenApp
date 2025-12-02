using System;
using System.Collections.Generic;
using System.Text;
using Vocabulary.Core.Entities;

namespace Vocabulary.Core.Repositories;

public interface IWordListRepository : IGenericRepository<WordList>
{
    Task<WordList?> GetWithWordsAsync(Guid id);
    Task<IEnumerable<WordList>> GetByUserIdAsync(Guid userId);
    void Remove(WordList wordList);
    Task UpdateAsync(WordList wordList);
}
