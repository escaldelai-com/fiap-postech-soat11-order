using Restaurant.Order.Application.Interfaces.Repository;
using StackExchange.Redis;

namespace Restaurant.Order.Data.Repositories;

public class SequenceRepository(
    IDatabase context) : ISequenceRepository
{

    public async Task<int> Get(string prefix)
    {
        var key = $"{prefix}:seq:{DateTime.Now:yyyyMMdd}";
        var seq = await context.HashIncrementAsync(key, "seq", 1);

        await context.KeyExpireAsync(key, TimeSpan.FromDays(1));

        return Convert.ToInt32(seq);
    }

}
