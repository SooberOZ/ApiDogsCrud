using ApiDogsCrud.Contracts;
using ApiDogsCrud.Models;
using ApiDogsCrud.WebApi.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace ApiDogsCrud.WebApi.Tests
{
    public class DogControllerTests
    {
        private readonly Mock<IDogService> _dogServiceMock;

        public DogControllerTests()
        {
            _dogServiceMock = new Mock<IDogService>();
        }

        [Fact]
        public void Ping_ShouldReturnStringReponse()
        {
            // Arrange
            var sut = new DogController(_dogServiceMock.Object);
            var expected = "Dogs house service. Version 1.0.1";

            // Act
            var actual = sut.Ping();

            // Assert
            actual.Value.Should().Be(expected);
        }

        [Fact]
        public async Task GetDogsAsync_ShouldReturnOkResultAsync()
        {
            // Arrange
            var filter = new DogsFilter();
            var dogs = new List<DogDto>();

            _dogServiceMock
                .Setup(x => x.GetDogsAsync(It.IsAny<DogsFilter>()))
                .ReturnsAsync(dogs);

            var sut = new DogController(_dogServiceMock.Object);

            // Act
            var actual = await sut.GetDogsAsync(filter);

            // Arrange
            _dogServiceMock.Verify(x => x.GetDogsAsync(It.IsAny<DogsFilter>()), Times.Once);

            actual.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(dogs);
        }

        [Fact]
        public async Task GetDogsAsync_ShouldProcessExceptionAsync()
        {
            // Arrange
            var filter = new DogsFilter();
            var errorMessage = "msg";

            _dogServiceMock
                .Setup(x => x.GetDogsAsync(It.IsAny<DogsFilter>()))
                .ThrowsAsync(new ValidationException(errorMessage));

            var sut = new DogController(_dogServiceMock.Object);

            // Act
            var actual = await sut.GetDogsAsync(filter);

            // Arrange
            _dogServiceMock.Verify(x => x.GetDogsAsync(It.IsAny<DogsFilter>()), Times.Once);

            actual.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be(errorMessage);
        }

        [Fact]
        public async Task CreateDogAsync_ShouldSuccessfullyCreateDogAsync()
        {
            // Arrange
            var dogDto = new DogDto();
            const string message = "Dog created successfully";

            _dogServiceMock
                .Setup(x => x.CreateDogAsync(It.IsAny<DogDto>()))
                .Returns(Task.CompletedTask);

            var sut = new DogController(_dogServiceMock.Object);

            // Act
            var actual = await sut.CreateDogAsync(dogDto);

            // Assert
            _dogServiceMock.Verify(x => x.CreateDogAsync(It.IsAny<DogDto>()), Times.Once);
            actual.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().Be(message);
        }

        [Fact]
        public async Task CreateDogAsync_ShouldProcessExceptionAsync()
        {
            // Arrange
            var dogDto = new DogDto();
            const string message = "msg";

            _dogServiceMock
                .Setup(x => x.CreateDogAsync(It.IsAny<DogDto>()))
                .ThrowsAsync(new ValidationException(message));

            var sut = new DogController(_dogServiceMock.Object);

            // Act
            var actual = await sut.CreateDogAsync(dogDto);

            // Assert
            _dogServiceMock.Verify(x => x.CreateDogAsync(It.IsAny<DogDto>()), Times.Once);
            actual.Should().BeOfType<BadRequestObjectResult>()
                .Which.Value.Should().Be(message);
        }
    }
}