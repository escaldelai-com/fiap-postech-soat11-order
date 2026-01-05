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
    public async Task Create_Ok()
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
        var result = await useCase.Create(cpf);

        // Assert
        result.Should().NotBeNull();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public async Task Create_Invalid_CPF(string? value)
    {
        // Arrange
        var idService = new Mock<IIdentificationService>();
        var repo = new Mock<IOrderRepository>();
        var seq = new Mock<ISequenceRepository>();
        var useCase = new OrderInfoCreateUseCase(idService.Object, repo.Object, seq.Object);

        // Act
        var act = () => useCase.Create(value!);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async Task Create_Client_Not_Found()
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
        var act = () => useCase.Create(cpf);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

}
