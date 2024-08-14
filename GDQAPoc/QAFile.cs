using System.Buffers;

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

		var data = await reader.ReadUntil('\n').ToArrayAsync();
		return SyncRest(data); //hack c# 13

		QAEntry SyncRest(ReadOnlySpan<char> data)
		{
			data = ReadIssues(data, out var issues);
			var remarks = new string(data.ConsumeUntil('|'));
			var coins = CoinGuides.Parse(data.ConsumeUntil('|'));
			return new(level, remarks, new(), coins); //TODO parse

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

	public async Task Overwrite(uint level, QAEntry entry)
	{
		var handle = File.OpenHandle(path, access: FileAccess.ReadWrite, options: FileOptions.Asynchronous);
		using var stream = new FileStream(handle, FileAccess.Read);

		var reader = new StreamReader(stream, leaveOpen: true);

		var toFind = $"| {level} |";
		await reader.SkipUntil(toFind);
		var entryPos = stream.Position - toFind.Length;

		await reader.SkipUntil(Environment.NewLine);
		var oldLength = stream.Position - Environment.NewLine.Length - entryPos;

		var newEntry = entry.ToString();
		var diff = newEntry.Length - oldLength;

		var fileLength = RandomAccess.GetLength(handle);
		const int bufLength = 64 * 1024;
		var buf = ArrayPool<byte>.Shared.Rent(bufLength);
		var bufMem = buf.AsMemory(..bufLength);

		if (diff > 0)
			await MoveForwards();
		else if (diff < 0)
			await MoveBackwards();

		await RandomAccess.WriteAsync(handle, newEntry.AsMemory().AsBytes(), entryPos);

		ArrayPool<byte>.Shared.Return(buf);
		return;

		async Task MoveForwards()
		{
			RandomAccess.SetLength(handle, fileLength + diff);

			var startPos = entryPos + oldLength;
			var bytesToMove = fileLength - startPos;
			var lastChunkSize = bytesToMove % bufLength;
			//from end to start, move forwards
			var last = true;
			for (var pos = fileLength - lastChunkSize; pos >= entryPos + oldLength; pos -= bufLength)
			{
				var read = await RandomAccess.ReadAsync(handle, bufMem[..(int)(last ? lastChunkSize : bufLength)], pos);
				await RandomAccess.WriteAsync(handle, bufMem[..read], pos + diff);
				last = false;
			}
		}

		async Task MoveBackwards()
		{
			//from start to end, move backwards
			for (var pos = entryPos + oldLength; pos < fileLength; pos += bufLength)
			{
				var read = await RandomAccess.ReadAsync(handle, bufMem, pos);
				await RandomAccess.WriteAsync(handle, bufMem[..read], pos + diff);
			}

			RandomAccess.SetLength(handle, fileLength + diff);
		}
	}

	public async Task Append(uint level, QAEntry entry)
	{
		var stream = File.Open(path, new FileStreamOptions() { Access = FileAccess.Write, Options = FileOptions.WriteThrough | FileOptions.Asynchronous });
		using var writer = new StreamWriter(stream);
		stream.Seek(0, SeekOrigin.End);

		await writer.WriteAsync(entry.ToString());
	}
}
