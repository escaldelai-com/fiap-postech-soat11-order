using Bogus.Extensions.Brazil;
using FluentAssertions;
using Moq;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Application.Interfaces.ExternalServices;
using Restaurant.Order.Application.Interfaces.Repository;
using Restaurant.Order.Application.UseCases;
using Restaurant.Order.Model;

namespace Restaurant.Order.Application.Test;

public class OrderInfoCreateUseCaseTest : TestBase
{

    [Fact]
    public async Task CreateByCpf_Ok()
    {
        // Arrange
        var cpf = faker.Person.Cpf();
        var orderId = GetGuid();
        var clientDto = new ClientDto
        {
            Id = GetGuid(),
            Nome = faker.Name.FullName(),
            CPF = cpf,
            Email = faker.Internet.Email()
        };
        var orderInfoDto = new OrderInfoDto
        {
            Id = orderId,
            Cliente = clientDto,
            Data = faker.Date.Past(),
            Numero = faker.Random.Int(1, 9999),
            Status = OrderStatus.Elaboration
        };
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);
        seq.Setup(x => x.Get("order")).ReturnsAsync(faker.Random.Int(1, 9999));
        idService.Setup(x => x.Get(cpf)).ReturnsAsync(clientDto);
        repo.Setup(x => x.Create(It.IsAny<OrderInfoDto>())).ReturnsAsync(orderId);

        // Act
        var result = await useCase.CreateByCpf(cpf);

        // Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task CreateByCpf_Invalid_CPF(string? value)
    {
        // Arrange
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);

        // Act
        var act = () => useCase.CreateByCpf(value!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateByCpf_Client_Not_Found()
    {
        // Arrange
        var cpf = faker.Person.Cpf();
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);
        seq.Setup(x => x.Get("order")).ReturnsAsync(faker.Random.Int(1, 9999));
        idService.Setup(x => x.Get(cpf)).ReturnsAsync(() => null);

        // Act
        var act = () => useCase.CreateByCpf(cpf);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task CreateById_Ok()
    {
        // Arrange
        var clientId = GetGuid();
        var orderId = GetGuid();
        var clientDto = new ClientDto
        {
            Id = GetGuid(),
            Nome = faker.Name.FullName(),
            CPF = faker.Person.Cpf(),
            Email = faker.Internet.Email()
        };
        var orderInfoDto = new OrderInfoDto
        {
            Id = orderId,
            Cliente = clientDto,
            Data = faker.Date.Past(),
            Numero = faker.Random.Int(1, 9999),
            Status = OrderStatus.Elaboration
        };
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);
        seq.Setup(x => x.Get("order")).ReturnsAsync(faker.Random.Int(1, 9999));
        idService.Setup(x => x.GetById(clientId)).ReturnsAsync(clientDto);
        repo.Setup(x => x.Create(It.IsAny<OrderInfoDto>())).ReturnsAsync(orderId);

        // Act
        var result = await useCase.CreateById(clientId);

        // Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task CreateById_Invalid_Id(string? value)
    {
        // Arrange
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);

        // Act
        var act = () => useCase.CreateById(value!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task CreateById_Client_Not_Found()
    {
        // Arrange
        var clientId = GetGuid();
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);
        seq.Setup(x => x.Get("order")).ReturnsAsync(faker.Random.Int(1, 9999));
        idService.Setup(x => x.GetById(clientId)).ReturnsAsync(() => null);

        // Act
        var act = () => useCase.CreateById(clientId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
