using ApiDogsCrud.Contracts;
using ApiDogsCrud.DataLayer.Entity;
using ApiDogsCrud.Models;
using FluentAssertions;
using AutoMapper;
using FluentValidation;
using Moq;
using Xunit;

namespace ApiDogsCrud.BusinessLogic.Tests
{
    public class DogServiceTests
    {
        private readonly Mock<IRepository<Dog>> _dogRepositoryMock;
        private readonly Mock<IValidator<Dog>> _validatorMock;
        private readonly Mock<IDogService> _dogService;
        private readonly Mock<IValidator<DogsFilter>> _filterValidatorMock;
        private readonly Mock<IMapper> _mapperMock;

        public DogServiceTests()
        {
            _dogRepositoryMock = new Mock<IRepository<Dog>>();
            _validatorMock = new Mock<IValidator<Dog>>();
            _filterValidatorMock = new Mock<IValidator<DogsFilter>>();
            _mapperMock = new Mock<IMapper>();
            _dogService = new Mock<IDogService>();
        }

        [Fact]
        public async Task GetDogsAsync_ShouldReturnDogsWithAnyFilterAsync()
        {
            // Arrange
            var dogDtos = new List<DogDto>
            {
                new DogDto { Id = Guid.NewGuid(), Name = "Dog1", Color = "Black", TailLength = 10, Weight = 10 }
            };

            _dogService
                .Setup(x => x.GetDogsAsync(It.IsAny<DogsFilter>()))
                .ReturnsAsync(dogDtos);

            var sut = new DogService(_dogRepositoryMock.Object, _validatorMock.Object, _filterValidatorMock.Object, _mapperMock.Object);

            // Act
            var result = await sut.GetDogsAsync(new DogsFilter());

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateDogAsync_ShouldCreateDogAsync()
        {

            // Arrange
            var dogToCreate = new DogDto { Name = "Dog1", Color = "Black", TailLength = 10, Weight = 10 };
            var createdDog = new Dog { Id = Guid.NewGuid(), Name = "Dog1", Color = "Black", TailLength = 10, Weight = 10 };

            _mapperMock
                .Setup(x => x.Map<Dog>(It.IsAny<DogDto>()))
                .Returns(createdDog);

            var sut = new DogService(_dogRepositoryMock.Object, _validatorMock.Object, _filterValidatorMock.Object, _mapperMock.Object);

            // Act
            await sut.CreateDogAsync(dogToCreate);

            // Assert
            _mapperMock.Verify(x => x.Map<Dog>(dogToCreate), Times.Once);
            _dogRepositoryMock.Verify(x => x.AddAsync(createdDog), Times.Once);
        }
    }
}