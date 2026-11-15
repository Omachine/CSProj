using DInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IMyService, MyService>(); //Singleton
//builder.Services.AddScoped<IMyService, MyService>();    //Scoped
//builder.Services.AddTransient<IMyService, MyService>(); //Transient

var app = builder.Build();

app.Use(
    async(context, next)=>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();   //Get the service from the requests service provider
        myService.LogCreation("First Middleware");
        await next();
    }
);

app.Use(
    async(context, next)=>
    {
        var myService = context.RequestServices.GetRequiredService<IMyService>();   //Get the same service instance in another middleware
        myService.LogCreation("Second Middleware");
        await next();
    }
);

app.MapGet(
    "/", 
    (IMyService myService)=>
    {
        myService.LogCreation("Root");
        return Results.Ok("Check the console for service creation logs");
    }
)

app.Run();


