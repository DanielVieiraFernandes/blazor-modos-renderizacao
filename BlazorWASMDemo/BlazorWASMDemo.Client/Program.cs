using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

//-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
// Baixa os assemblies .dll necessários para o Blazor WebAssembly
// Inicializa o runtime do Blazor WebAssembly no navegador
// Executa o componente raiz App no navegador
// e conecta ao DOM da página
//-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
await builder.Build().RunAsync();
