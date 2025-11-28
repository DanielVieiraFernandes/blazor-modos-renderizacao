# 🚀 Blazor - Modos de Renderização (.NET 8 e .NET 9)

Bem-vindo ao repositório **blazor-modos-renderizacao**. Este projeto é um laboratório prático para demonstrar e explicar os diferentes **Modos de Renderização** disponíveis no Blazor moderno, cobrindo as atualizações do .NET 8 e .NET 9.

O objetivo é entender não apenas como usar, mas como o framework opera internamente (SignalR, WebAssembly Runtime, Hydration e SSR).

## 📋 Visão Geral Técnica

A aplicação explora os três principais comportamentos de renderização e suas configurações no `Program.cs`:

1.  **SSR (Static Server Rendering)**: Renderização estática com suporte a Streaming.
2.  **Interactive Server**: Renderização via WebSocket/SignalR.
3.  **Interactive WebAssembly**: Renderização no cliente via download de DLLs.
4.  **Interactive Auto**: Tenta WebAssembly, com fallback para Server.

---

## 📚 Detalhamento dos Modos e Configurações

### 1. 📄 Static Server Rendering (SSR) & Stream Rendering
Neste modo, o HTML é gerado no servidor e enviado ao navegador. Não há conexão WebSocket persistente nem C# rodando no navegador.

**Conceitos Chave demonstrados:**
* **Interatividade Limitada:** Eventos como `@onclick` **não funcionam** (são inoperantes), pois não há JavaScript/WASM ou SignalR para processá-los.
* **Stream Rendering:** Permite que o servidor envie o HTML progressivamente enquanto aguarda dados assíncronos, melhorando a UX.
* **Roteamento:** Uso de atributos para impedir que o roteador interativo assuma o controle de páginas estáticas.

**Exemplo de Código (`Noticias.razor`):**
```csharp
@page "/noticias"
// Permite envio progressivo do HTML (carregamento suave)
@attribute [StreamRendering]
// Garante que a página seja tratada como estática, ignorando o router interativo
@attribute [ExcludeFromInteractiveRouting]

<button @onclick="HandleClick">Clique</button> ```

### 2. 🖥️ Interactive Server (Blazor Server)
Toda a lógica roda no servidor. O navegador atua como um "terminal", recebendo atualizações do DOM e enviando eventos via SignalR.

**Configuração do Pipeline (`Program.cs`):**
Para que funcione, é necessário registrar os serviços e mapear os endpoints do SignalR:

1.  **Injeção de Dependência:**
    ```csharp
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents(); // Registra serviços de circuitos, sincronização de DOM e SignalR.
    ```
2.  **Pipeline (Middleware):**
    ```csharp
    app.MapRazorComponents<App>()
        .AddInteractiveServerRenderMode(); // Mapeia os endpoints WebSocket necessários. Sem isso, o SignalR não conecta.
    ```

### 3. 🌐 Interactive WebAssembly (Blazor WASM)
O código C# é compilado e executado diretamente no navegador do usuário.

**Fluxo de Execução:**
1.  **Carga Inicial:** A página pode ser pré-renderizada no servidor (SSR) para SEO e velocidade visual.
2.  **Hydration:** O navegador baixa o runtime .NET e as DLLs da aplicação (`BlazorWASMDemo.Client.dll`).
3.  **Interatividade:** O componente se torna interativo localmente.

**Configuração Crítica:**
É necessário informar ao servidor onde estão os componentes do cliente (Client Project):

```csharp
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    // Linka o assembly do projeto Client para que o servidor saiba o que enviar
    .AddAdditionalAssemblies(typeof(BlazorWASMDemo.Client._Imports).Assembly);


### 4. 🤖 Interactive Auto (WebAssembly com Fallback para Server)
Este modo tenta renderizar no cliente via WebAssembly, mas se o navegador não suportar, recai para o modo Server.
**Configuração:**
```csharp
app.MapRazorComponents<App>()
    .AddInteractiveAutoRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorWASMDemo.Client._Imports).Assembly);
    ```
