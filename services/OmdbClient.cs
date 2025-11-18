using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using ProjetoFilmes.Models;

namespace ProjetoFilmes.Services;

public sealed class OmdbOptions
{
    public string BaseUrl { get; set; } = "";
    public string ApiKey  { get; set; } = "";
}

public sealed class OmdbClient
{
    private readonly HttpClient _http;
    private readonly OmdbOptions _opt;

    public OmdbClient(HttpClient http, IOptions<OmdbOptions> opt)
    {
        _http = http;
        _opt  = opt.Value;
    }

    private string WithKey(string query) =>
        $"{_opt.BaseUrl}?apikey={_opt.ApiKey}&{query}";

    public async Task<OmdbSearchResponse?> SearchAsync(string query, int page = 1, string? type = null, int? year = null)
    {
        var qp = $"s={Uri.EscapeDataString(query)}&page={page}";
        if (!string.IsNullOrWhiteSpace(type)) qp += $"&type={type}";
        if (year is not null) qp += $"&y={year}";
        return await _http.GetFromJsonAsync<OmdbSearchResponse>(WithKey(qp));
    }

    public async Task<OmdbTitleResponse?> GetByImdbIdAsync(string imdbId, bool fullPlot = false)
    {
        var qp = $"i={Uri.EscapeDataString(imdbId)}&plot={(fullPlot ? "full" : "short")}";
        return await _http.GetFromJsonAsync<OmdbTitleResponse>(WithKey(qp));
    }

    public async Task<OmdbTitleResponse?> GetByTitleAsync(string title, int? year = null, bool fullPlot = false)
    {
        var qp = $"t={Uri.EscapeDataString(title)}&plot={(fullPlot ? "full" : "short")}";
        if (year is not null) qp += $"&y={year}";
        return await _http.GetFromJsonAsync<OmdbTitleResponse>(WithKey(qp));
    }
}
