using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entities;
using Vocabulary.Core.Repositories;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Infrastructure.Repositories;

public class WordListRepository : GenericRepository<WordList>, IWordListRepository
{
    public WordListRepository(VocabularyDbContext context) : base(context)
    {
    }

    public async Task<WordList?> GetWithWordsAsync(Guid id)
    {
        return await _context.WordLists
            .Include(x => x.Words)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<WordList>> GetByUserIdAsync(Guid userId)
    {
        return await _context.WordLists
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public void Remove(WordList wordList)
    {
        _context.WordLists.Remove(wordList);
    }

    public async Task UpdateAsync(WordList wordList)
    {
        _context.WordLists.Update(wordList);
    }
}
