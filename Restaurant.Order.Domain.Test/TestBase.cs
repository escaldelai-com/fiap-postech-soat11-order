using Bogus;

namespace Restaurant.Order.Domain.Test;

public abstract class TestBase
{

    protected Faker faker = new("pt_BR");

    protected string GetGuid() => Guid.NewGuid().ToString("n");

}
