using Restaurant.Order.Application.Interfaces.Presenter;
using Newtonsoft.Json;

namespace Restaurant.Order.Presenter.Serializers;

public class JsonPresenter : IJsonPresenter
{

    public T? Deserialize<T>(string? json)
    {
        return string.IsNullOrWhiteSpace(json)
            ? default
            : JsonConvert.DeserializeObject<T>(json);
    }

    public string? Serialize(object? obj)
    {
        return obj == null
            ? null
            : JsonConvert.SerializeObject(obj);
    }

}
