using Microsoft.AspNetCore.Components;

namespace BlazorFrontend.Services;

public abstract class ApiServiceBase
{
    protected readonly NavigationManager NavigationManager;
    protected readonly HttpClient HttpClient;

    protected ApiServiceBase(NavigationManager navigationManager, HttpClient httpClient)
    {
        NavigationManager = navigationManager;
        HttpClient = httpClient;
    }
}
