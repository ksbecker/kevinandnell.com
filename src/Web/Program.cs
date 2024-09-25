using Blazor.Analytics;
using Web.Components;
using Web.Features.ContactForm;
using Web.Features.ContactForm.SmtpEmail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOptions<SmtpEmailServiceOptions>()
    .Bind(builder.Configuration.GetSection(SmtpEmailServiceOptions.SectionName))
    .ValidateOnStart();

builder.Services.AddOptions<ContactFormEmailOptions>()
    .Bind(builder.Configuration.GetSection(ContactFormEmailOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddGoogleAnalytics("G-6YNME1EDCL");

builder.Services.AddTransient<IEmailService, SmtpEmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Error", createScopeForErrors: true);

    _ = app.UseHsts();
}

_ = app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();