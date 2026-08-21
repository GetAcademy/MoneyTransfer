using MoneyTransfer.Core.ApplicationService;
using MoneyTransfer.Core.DomainServices;
using MoneyTransfer.OnlyApiAndInfrastructure.DTO;
using MoneyTransfer.OnlyApiAndInfrastructure.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAccountRepository, FileAccountRepository>();
builder.Services.AddScoped<MoneyTransferService>();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapPost("/transfer", (TransferMoneyRequest request, MoneyTransferService service) =>
{
    try
    {
        service.Transfer(request.FromAccountNo, request.ToAccountNo, request.Amount);
        return Results.Ok();
    }
    catch (Exception e)
    {
        return Results.BadRequest(e.Message);
    }
});
app.Run();