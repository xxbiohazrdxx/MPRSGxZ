using WebAmp.Services;
using WebAmp.Settings;
using WebAmp.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AmplifierStackSettings>(builder.Configuration.GetSection("AmplifierStackSettings"));
builder.Services.AddSingleton<IAmplifierService, AmplifierService>();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

//endpoints.MapHub<AmpHub>("/ampHub");
app.MapControllers();

app.MapBlazorHub();
app.MapHub<AmpHub>("/amphub");
app.MapFallbackToPage("/_Host");

app.Run();
