using Vocabulary.Core.Entities;
using Vocabulary.Core.Repositories;

namespace Vocabulary.Application.Services;

public class WordListService
{
    private readonly IWordListRepository _repo;

    public WordListService(IWordListRepository repo)
    {
        _repo = repo;
    }

    public async Task<WordList> CreateAsync(Guid userId, string name, string? level)
    {
        var entity = new WordList
        {
            UserId = userId,
            Name = name,
            Level = level
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        return entity;
    }

    public Task<WordList?> GetWithWordsAsync(Guid id)
        => _repo.GetWithWordsAsync(id);

    public Task<IEnumerable<WordList>> GetByUserAsync(Guid userId)
        => _repo.GetByUserIdAsync(userId);

    public async Task DeleteAsync(Guid id)
    {
        var wordList = await _repo.GetByIdAsync(id);
        if (wordList == null)
        {
            throw new KeyNotFoundException($"Word list with ID {id} not found.");
        }

        _repo.Remove(wordList);
        await _repo.SaveChangesAsync();
    }

    public async Task<WordList?> UpdateAsync(Guid id, string name, string? level)
    {
        var wordList = await _repo.GetByIdAsync(id);
        if (wordList == null)
        {
            throw new KeyNotFoundException($"Word list with ID {id} not found.");
        }

        wordList.Name = name;
        wordList.Level = level;

        await _repo.UpdateAsync(wordList);
        await _repo.SaveChangesAsync();

        return wordList;
    }
}
