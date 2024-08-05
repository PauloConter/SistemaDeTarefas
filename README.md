# Sistema de Tarefas

## Descrição

Este projeto é um sistema de tarefas com um front-end desenvolvido com Bootstrap e jQuery para interações via Ajax. O back-end é uma API .NET.

## Estrutura do Projeto

- `index.html`: Página principal do sistema.
- `css/estilos.css`: Arquivo de estilos personalizados.
- `js/scripts.js`: Arquivo com scripts JavaScript para carregar e exibir tarefas.
- `README.md`: Este arquivo.

## Requisitos

- Servidor da API rodando em `https://localhost:44388/api/Tarefas`.
- Navegador moderno com suporte a ES6.
- Configuração CORS no back-end para permitir requisições do front-end.

## Como Usar

1. Clone este repositório.
2. Abra o arquivo `index.html` no seu navegador.
3. O sistema carregará automaticamente a lista de tarefas.
4. Clique em uma tarefa para ver os detalhes.

## Dependências

- [Bootstrap](https://getbootstrap.com/)
- [jQuery](https://jquery.com/)

## Configuração do Backend

Adicione a configuração CORS no seu arquivo `Program.cs` ou `Startup.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Add DbContext and other services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authentication and Authorization configurations...
// Swagger configurations...

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SistemaDeTarefas v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
