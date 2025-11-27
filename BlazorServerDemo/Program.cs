using BlazorServerDemo.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
//-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
// Registra todos os serviços necessários no container de injeção de dependência
// para permitir a renderização dos componentes Razor
// a interatividade do lado do servidor via SignalR
// os circuitos do Blazor Server
// a sincronização do DOM
// e o gerenciamento de eventos interativos como cliques de botão e entradas de formulário.
//-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    // Mapeia os endpoints http e websocket necessários no pipeline do ASP.NET Core
    // para servir os componentes Razor como páginas e habilitar os endpoints do
    // SignalR para comunicação interativa em tempo real entre o cliente e o servidor.
    // Sem isso, mesmo com os serviços registrados, os componentes Razor não funcionariam
    // corretamente no modo interativo do Blazor Server, pois o SignalR não seria acessível
    // no navegador.
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    .AddInteractiveServerRenderMode();

app.Run();
