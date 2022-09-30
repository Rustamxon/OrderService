using Newtonsoft.Json;
using OrderService.Domain.Configuration;
using OrderService.Service.Helpers;

namespace OrderService.Service.Extensions;

public static class CollectionExtension
{
    public static IEnumerable<T> ToPaged<T>(
        this IQueryable<T> sources,
        PaginationParams @params)
    {
        var metaData = new PaginationData(sources.Count(), @params);

        var json = JsonConvert.SerializeObject(metaData);

        if (HttpContextHelper.ResponseHeader.Keys.Contains("X-Pagination"))
            HttpContextHelper.ResponseHeader.Remove("X-Pagination");

        HttpContextHelper.ResponseHeader.Add("X-Pagination", json);

        return @params.PageSize >= 0 && @params.PageIndex >= 0
               ? sources.Skip(@params.PageSize * (@params.PageIndex - 1)).Take(@params.PageSize)
               : sources;
    }
}
