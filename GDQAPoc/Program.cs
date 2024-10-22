using CommunityToolkit.Mvvm.DependencyInjection;

using ConfigurationSubstitution;

using GDQAPoc.Data;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GDQAPoc;

internal static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		Ioc.Default.ConfigureServices(GetServices());

		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();
		Application.Run(new Form1());
	}

	private static IServiceProvider GetServices()
	{
		var settings = new HostApplicationBuilderSettings()
		{
			Configuration = new ConfigurationManager(),
			DisableDefaults = true
		};
		settings.Configuration.AddEnvironmentVariables();
		settings.Configuration.AddJsonFile("config.json", false, true);
		settings.Configuration.EnableSubstitutions("%", "%", UnresolvedVariableBehaviour.Throw);

		var app = Host.CreateEmptyApplicationBuilder(settings);
		app.Services.AddOptionsWithValidateOnStart<Config>()
			.BindConfiguration("");
		app.Services.AddSingleton<IQAEntryRepository, QAFile>();

		return app.Build().Services;
	}
}