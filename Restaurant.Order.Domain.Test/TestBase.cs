using Bogus;

namespace Restaurant.Order.Domain.Test;

public abstract class TestBase
{
    protected Faker Faker { get; } = new("pt_BR");

    protected static string GetGuid()
    {
        return Guid.NewGuid().ToString("n");
    }
}
