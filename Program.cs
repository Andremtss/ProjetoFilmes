using ProjetoFilmes.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OmdbOptions>(
    builder.Configuration.GetSection("Omdb"));

builder.Services.AddHttpClient<OmdbClient>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/movies/search", async (string q, int page, string? type, int? year, OmdbClient omdb) =>
{
    page = page <= 0 ? 1 : page;
    var resp = await omdb.SearchAsync(q, page, type, year);
    if (resp is null) return Results.Problem("Sem resposta da OMDb.");
    if (!string.Equals(resp.Response, "True", StringComparison.OrdinalIgnoreCase))
        return Results.NotFound(new { error = resp.Error });

    return Results.Ok(resp);
});


app.MapGet("/movies/{imdbId}", async (string imdbId, bool fullPlot, OmdbClient omdb) =>
{
    var resp = await omdb.GetByImdbIdAsync(imdbId, fullPlot);
    if (resp is null) return Results.Problem("Sem resposta da OMDb.");
    if (!string.Equals(resp.Response, "True", StringComparison.OrdinalIgnoreCase))
        return Results.NotFound(new { error = resp.Error });

    return Results.Ok(resp);
});


app.MapGet("/movies/by-title", async (string title, int? year, bool fullPlot, OmdbClient omdb) =>
{
    var resp = await omdb.GetByTitleAsync(title, year, fullPlot);
    if (resp is null) return Results.Problem("Sem resposta da OMDb.");
    if (!string.Equals(resp.Response, "True", StringComparison.OrdinalIgnoreCase))
        return Results.NotFound(new { error = resp.Error });

    return Results.Ok(resp);
});

app.UseDefaultFiles();   
app.UseStaticFiles(); 

app.Run();
