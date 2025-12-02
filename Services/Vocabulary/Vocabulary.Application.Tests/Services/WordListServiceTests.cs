using System;
using System.Threading.Tasks;
using Moq;
using Vocabulary.Application.Services;
using Vocabulary.Core.Entities;
using Vocabulary.Core.Repositories;
using Xunit;

namespace Vocabulary.Application.Tests.Services
{
    public class WordListServiceTests
    {
        private readonly Mock<IWordListRepository> _mockRepo;
        private readonly WordListService _service;

        public WordListServiceTests()
        {
            _mockRepo = new Mock<IWordListRepository>();
            _service = new WordListService(_mockRepo.Object);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveWordList_WhenWordListExists()
        {
            // Arrange
            var wordListId = Guid.NewGuid();
            var wordList = new WordList { Id = wordListId };

            _mockRepo.Setup(r => r.GetByIdAsync(wordListId)).ReturnsAsync(wordList);

            // Act
            await _service.DeleteAsync(wordListId);

            // Assert
            _mockRepo.Verify(r => r.Remove(wordList), Times.Once);
            _mockRepo.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowKeyNotFoundException_WhenWordListDoesNotExist()
        {
            // Arrange
            var wordListId = Guid.NewGuid();

            _mockRepo.Setup(r => r.GetByIdAsync(wordListId)).ReturnsAsync((WordList?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.DeleteAsync(wordListId));
        }
    }
}