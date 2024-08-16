using System.Text.Json;

namespace GDQAPoc;
public sealed class JsonConfigProvider
{
	//TODO make this non-static and async, figure out where to await
	public static Config Read()
	{
		using var fs = File.OpenRead("config.json");
		var config = JsonSerializer.Deserialize<Config>(fs, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
		return config! with { FilePath = Environment.ExpandEnvironmentVariables(config.FilePath) };
	}
}
