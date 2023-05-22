using CSharpFinalProject.Controllers;
using CSharpFinalProject.Helpers;

namespace CSharpFinalProject.Data;

public class DataQueryApiService
{
    private readonly HttpClient _http;

    public DataQueryApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<T>?> GetQueryDataListAsync<T>(string apiPath, DataQueryApiArgs args)
    {
        var queryParams = "?" + args.GetQueryString();

        return await _http.GetFromJsonAsync<List<T>>(apiPath + queryParams);
    }

    public async Task UpdateQueryDataItem<T>(string apiPath, T item)
    {
        await _http.PutAsJsonAsync(apiPath, item);
    }
    
    public async Task CreateQueryDataItem<T>(string apiPath, T item)
    {
        await _http.PostAsJsonAsync(apiPath, item);
    }

    public async Task DeleteQueryDataItem(string apiPath)
    {
        await _http.DeleteAsync(apiPath);
    }

}
