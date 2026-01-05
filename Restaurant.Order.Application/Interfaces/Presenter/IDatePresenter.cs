namespace Restaurant.Order.Application.Interfaces.Presenter;

public interface IDatePresenter
{

    DateTime? ToTimeZone(DateTime? dateTime);

}
