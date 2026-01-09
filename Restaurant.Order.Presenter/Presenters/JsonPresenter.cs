using Restaurant.Order.Application.Interfaces.Presenter;
using Newtonsoft.Json;

namespace Restaurant.Order.Presenter.Serializers;

public class JsonPresenter : IJsonPresenter
{

    public T? Deserialize<T>(string? json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default;

        return JsonConvert.DeserializeObject<T>(json);
    }

    public string? Serialize(object? obj)
    {
        if (obj == null)
            return null;

        return JsonConvert.SerializeObject(obj);
    }

}
