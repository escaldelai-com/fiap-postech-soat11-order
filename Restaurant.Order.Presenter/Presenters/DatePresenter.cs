using Microsoft.Extensions.Configuration;
using Restaurant.Order.Application.Interfaces.Presenter;

namespace Restaurant.Order.Presenter.Services;

public class DatePresenter(
    IConfiguration configuration) : IDatePresenter
{

    private readonly TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(configuration["TimeZone"]
        ?? throw new ArgumentNullException("TimeZone configuration is missing"));


    public DateTime? ToTimeZone(DateTime? dateTime)
    {
        return dateTime != null
            ? TimeZoneInfo.ConvertTimeFromUtc(dateTime.Value, timeZone)
            : null;
    }
}
