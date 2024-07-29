using CommunityToolkit.HighPerformance;

namespace GDQAPoc;

public sealed class QAFile(string path)
{
	public async Task<QAEntry?> TryRead(uint level)
	{
		var stream = File.Open(path, new FileStreamOptions() { Options = FileOptions.SequentialScan | FileOptions.Asynchronous });
		using var reader = new StreamReader(stream);

		//find id
		if (await reader.SkipUntil($"| {level} |") is false)
			return null;

		var data = await reader.ReadUntil('\n').SelectMany(segm => segm.ToArray().ToAsyncEnumerable()).ToArrayAsync();
		return SyncRest(data); //hack

		QAEntry SyncRest(ReadOnlySpan<char> data)
		{
			data = ReadIssues(data, out var issues);
			var remarks = new string(data.ConsumeUntil('|'));
			var coins = CoinGuides.Parse(data.ConsumeUntil('|'));
			return new(level, remarks, issues, coins);

			static ReadOnlySpan<char> ReadIssues(ReadOnlySpan<char> data, out Issue[] issues)
			{
				var separator = data.IndexOf('|');
				var read = data[..separator];
				var remaining = data[(separator + 1)..];

				if (read.Contains('-'))
				{
					issues = [];
					return remaining;
				}

				issues = new Issue[System.MemoryExtensions.Count(data, ',') + 1];

				var i = 0;
				foreach (var issueString in data.Tokenize(','))
					issues[i++] = Issue.Parse(issueString.Trim(' '));

				return remaining;
			}
		}
	}

	public async Task<bool> Exists(uint level)
	{
		var stream = File.Open(path, new FileStreamOptions() { Options = FileOptions.SequentialScan | FileOptions.Asynchronous });
		using var reader = new StreamReader(stream);

		return await reader.SkipUntil($"| {level} |");
	}

	public async Task Append(uint level, QAEntry entry)
	{
		var stream = File.Open(path, new FileStreamOptions() { Access = FileAccess.Write, Options = FileOptions.WriteThrough | FileOptions.Asynchronous });
		using var writer = new StreamWriter(stream);
		stream.Seek(0, SeekOrigin.End);

		await writer.WriteAsync(entry.ToString());
	}
}
