# Sistema de Tarefas

## Descri��o

Este projeto � um sistema de tarefas com um front-end desenvolvido com Bootstrap e jQuery para intera��es via Ajax. O back-end � uma API .NET.

## Estrutura do Projeto

- `index.html`: P�gina principal do sistema.
- `css/estilos.css`: Arquivo de estilos personalizados.
- `js/scripts.js`: Arquivo com scripts JavaScript para carregar e exibir tarefas.
- `README.md`: Este arquivo.

## Requisitos

- Servidor da API rodando em `https://localhost:44388/api/Tarefas`.
- Navegador moderno com suporte a ES6.
- Configura��o CORS no back-end para permitir requisi��es do front-end.

## Como Usar

1. Clone este reposit�rio.
2. Abra o arquivo `index.html` no seu navegador.
3. O sistema carregar� automaticamente a lista de tarefas.
4. Clique em uma tarefa para ver os detalhes.

## Depend�ncias

- [Bootstrap](https://getbootstrap.com/)
- [jQuery](https://jquery.com/)

## Configura��o do Backend

Adicione a configura��o CORS no seu arquivo `Program.cs` ou `Startup.cs`:

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
