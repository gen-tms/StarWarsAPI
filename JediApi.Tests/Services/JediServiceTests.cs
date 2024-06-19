using System.Collections.Generic;
using System.Threading.Tasks;
using JediApi.Models;
using JediApi.Repositories;
using JediApi.Services;
using Moq;
using Xunit;

namespace JediApi.Tests.Services
{
    public class JediServiceTests
    {
        // não mexer
        private readonly JediService _service;
        private readonly Mock<IJediRepository> _repositoryMock;

        public JediServiceTests()
        {
            // não mexer
            _repositoryMock = new Mock<IJediRepository>();
            _service = new JediService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetById_Success()
        {
            // Arrange
            int id = 1;
            var expectedJedi = new Jedi { Id = id, Name = "Luke Skywalker" };
            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(expectedJedi);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedJedi.Id, result.Id);
            Assert.Equal(expectedJedi.Name, result.Name);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            // Arrange
            int id = 1;
            _repositoryMock.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Jedi)null);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAll()
        {
            // Arrange
            var expectedJediList = new List<Jedi>
            {
                new Jedi { Id = 1, Name = "Luke Skywalker" },
                new Jedi { Id = 2, Name = "Yoda" }
            };
            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedJediList);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedJediList.Count, result.Count);
            Assert.Equal(expectedJediList[0].Name, result[0].Name);
            Assert.Equal(expectedJediList[1].Name, result[1].Name);
        }
    }
}