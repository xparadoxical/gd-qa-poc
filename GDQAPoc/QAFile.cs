namespace GDQAPoc;
public sealed class QAFile(string path)
{
	private static readonly int MaxIssueLength = "bgp, unrd, mem, ovdeco, sync, ic 1 2 3, nci 1 2 3, fc 1 2 3".Length;

	public async Task<Issue[]?> TryRead(uint level)
	{
		var handle = File.OpenHandle(path, options: FileOptions.SequentialScan);
		var buffer = new byte[4096];

		var stream = File.Open(path, new FileStreamOptions() { Options = FileOptions.SequentialScan });
		using var reader = new StreamReader(stream);

		while (true)
		{
			if (await reader.SkipUntil(level.ToString()) is false)
				return null;

			await reader.SkipUntil('|');


		}
	}
}
