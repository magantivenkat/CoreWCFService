using Contract;
using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Description;
using DotNetCoreWCFServiceProject;
using Microsoft.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<EchoService>();
    serviceBuilder.AddServiceEndpoint<EchoService, IEchoService>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service.svc");
    var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    serviceMetadataBehavior.HttpsGetEnabled = true;
});

app.Run();
