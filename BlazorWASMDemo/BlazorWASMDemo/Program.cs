using BlazorWASMDemo.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    // Registro os serviços necessários para componentes interativos do Blazor WebAssembly
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    // Permite que os componentes Razor sejam renderizados no modo interativo do Blazor WebAssembly
    // inicialmente os componentes são renderizados no servidor como SSR e depois se tornam interativos
    // no navegador via WebAssembly assim, apenas os componentes que explicitamente usam recursos
    // do Blazor WebAssembly precisam ser carregados no cliente
    //-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    .AddInteractiveWebAssemblyRenderMode()
    // Informa ao servidor onde estão os componentes interativos no projeto cliente Blazor WebAssembly
    .AddAdditionalAssemblies(typeof(BlazorWASMDemo.Client._Imports).Assembly);

app.Run();
