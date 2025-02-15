using BonoSearch.Interfaces;
using BonoSearch.Models.DTOs;
using BonoSearch.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BonoSearch.Tests
{
    public class SearchServiceTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository;
        private readonly Mock<ILogger<SearchService>> _mockLogger;
        private readonly SearchService _searchService;

        public SearchServiceTests()
        {
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockLogger = new Mock<ILogger<SearchService>>();
            _searchService = new SearchService(_mockMovieRepository.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SemanticSearchAsync_ReturnsResults_WhenRepositoryHasMatches()
        {
            // Arrange
            var query = "test query";
            var expectedResults = new List<MovieDto>
            {
                new MovieDto { Name = "Test Movie 1", Plot = "Test Plot 1" },
                new MovieDto { Name = "Test Movie 2", Plot = "Test Plot 2" }
            };

            _mockMovieRepository.Setup(repo => repo.SemanticSearch(query))
                .ReturnsAsync(expectedResults);

            // Act
            var results = await _searchService.SemanticSearchAsync(query);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedResults.Count, results.Count());
            Assert.Equal(expectedResults, results);
            _mockMovieRepository.Verify(repo => repo.SemanticSearch(query), Times.Once);
        }

        [Fact]
        public async Task SemanticSearchAsync_LogsNoResults_WhenRepositoryReturnsEmpty()
        {
            // Arrange
            var query = "no results query";
            _mockMovieRepository.Setup(repo => repo.SemanticSearch(query))
                .ReturnsAsync(new List<MovieDto>());

            // Act
            var results = await _searchService.SemanticSearchAsync(query);

            // Assert
            Assert.Empty(results);
            _mockLogger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains("No results found")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task SemanticSearchAsync_ThrowsApplicationException_WhenRepositoryThrows()
        {
            // Arrange
            var query = "error query";
            var expectedException = new Exception("Repository error");
            _mockMovieRepository.Setup(repo => repo.SemanticSearch(query))
                .ThrowsAsync(expectedException);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(
                () => _searchService.SemanticSearchAsync(query));
            
            Assert.Equal("Failed to perform semantic search", exception.Message);
            Assert.Same(expectedException, exception.InnerException);
        }

        [Fact]
        public async Task LexicalSearchAsync_ReturnsResults_WhenRepositoryHasMatches()
        {
            // Arrange
            var query = "test query";
            var expectedResults = new List<MovieDto>
            {
                new MovieDto { Name = "Test Movie 1", Plot = "Test Plot 1" },
                new MovieDto { Name = "Test Movie 2", Plot = "Test Plot 2" }
            };

            _mockMovieRepository.Setup(repo => repo.LexicalSearch(query))
                .ReturnsAsync(expectedResults);

            // Act
            var results = await _searchService.LexicalSearchAsync(query);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedResults.Count, results.Count());
            Assert.Equal(expectedResults, results);
            _mockMovieRepository.Verify(repo => repo.LexicalSearch(query), Times.Once);
        }

        [Fact]
        public async Task HybridSearchAsync_ReturnsResults_WhenRepositoryHasMatches()
        {
            // Arrange
            var query = "test query";
            var expectedResults = new List<MovieDto>
            {
                new MovieDto { Name = "Test Movie 1", Plot = "Test Plot 1" },
                new MovieDto { Name = "Test Movie 2", Plot = "Test Plot 2" }
            };

            _mockMovieRepository.Setup(repo => repo.HybridSearch(query))
                .ReturnsAsync(expectedResults);

            // Act
            var results = await _searchService.HybridSearchAsync(query);

            // Assert
            Assert.NotNull(results);
            Assert.Equal(expectedResults.Count, results.Count());
            Assert.Equal(expectedResults, results);
            _mockMovieRepository.Verify(repo => repo.HybridSearch(query), Times.Once);
        }
    }
}