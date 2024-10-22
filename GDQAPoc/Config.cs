using System.ComponentModel.DataAnnotations;

using Microsoft.Extensions.Options;

namespace GDQAPoc;

public sealed class Config
{
	[Required]
	public required string FilePath { get; set; }
}

[OptionsValidator]
public partial class ConfigValidator : IValidateOptions<Config>;
