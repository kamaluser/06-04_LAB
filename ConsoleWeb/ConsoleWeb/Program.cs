

using ConsoleWeb;
using ConsoleWeb.Entities;

AppDbContext appDbContext = new AppDbContext();



var appBuilder = WebApplication.CreateBuilder();

appBuilder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = appBuilder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () =>
{
    return "Hello, World!";
});

app.MapGet("/products", () =>
{
    return appDbContext.Products.ToList();
});

app.MapGet("/products/search", (string name) =>
{
    return appDbContext.Products.Where(x=>x.Name.Contains(name)).ToList();
}); 


app.MapGet("/products/{id}", (int id) =>
{
    var data = appDbContext.Products.Find(id);

    if (data == null) return Results.NotFound("product not found!");

    return Results.Ok(data);
});

app.MapPost("/products", (Product product) =>
{
    appDbContext.Add(product);
    appDbContext.SaveChanges();

    return Results.Ok();
});

app.MapPut("/products/{id}", (int id,Product product) =>
{
    Product existProduct = appDbContext.Products.Find(id);

    if (existProduct == null) return Results.NotFound("product not found!");

    existProduct.Name = product.Name;
    existProduct.Price = product.Price;

    appDbContext.Add(product);
    appDbContext.SaveChanges();

    return Results.NoContent();
});


app.MapDelete("/products/{id}", (int id) =>
{
    Product product = appDbContext.Products.Find(id);

    if (product == null) return Results.NotFound("product not found!");

    appDbContext.Remove(product);
    appDbContext.SaveChanges();

    return Results.NoContent();
});



app.Run();

