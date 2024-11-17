using System.Buffers;
using System.Text;

using CommunityToolkit.HighPerformance;

using Microsoft.Extensions.Options;

namespace GDQAPoc.Data;

public sealed class QAFile(IOptionsMonitor<Config> config) : IQAEntryRepository
{
	public async Task<QAEntry?> TryRead(uint level, CancellationToken ct = default)
	{
		ct.ThrowIfCancellationRequested();

		var stream = File.Open(config.CurrentValue.FilePath, new FileStreamOptions() { Options = FileOptions.SequentialScan | FileOptions.Asynchronous });
		using var reader = new StreamReader(stream);

		//find id
		if (await reader.SkipUntil($"| {level} |", ct) is false)
			return null;

		var data = await reader.ReadUntil('\n', ct).ToArrayAsync(ct);
		var ret = SyncRest(data); //hack c# 13
		ct.ThrowIfCancellationRequested();
		return ret;

		QAEntry SyncRest(ReadOnlySpan<char> data)
		{
			data = ReadIssues(data, out var issues);
			var remarks = new string(data.ConsumeUntil('|').Trim("| "));
			var coins = CoinGuides.Parse(data.ConsumeUntil('|').Trim("| "));
			return new(level, remarks, new(issues), coins);

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

	public async Task<bool> Exists(uint level, CancellationToken ct = default)
	{
		ct.ThrowIfCancellationRequested();

		var stream = File.Open(config.CurrentValue.FilePath, new FileStreamOptions() { Options = FileOptions.SequentialScan | FileOptions.Asynchronous });
		using var reader = new StreamReader(stream);

		return await reader.SkipUntil($"| {level} |", ct);
	}

	public async Task Overwrite(QAEntry entry, CancellationToken ct = default)
	{
		ct.ThrowIfCancellationRequested();

		var handle = File.OpenHandle(config.CurrentValue.FilePath, access: FileAccess.ReadWrite, options: FileOptions.Asynchronous);
		using var stream = new FileStream(handle, FileAccess.Read);
		var reader = new StreamReader(stream, Encoding.UTF8, leaveOpen: true);

		var toFind = $"| {entry.Level} |";
		await reader.SkipUntil(toFind, ct);
		var entryPos = reader.GetPosition() - toFind.Length;

		await reader.SkipUntil(Environment.NewLine, ct);
		var oldLength = reader.GetPosition() - entryPos;

		var newEntry = Encoding.UTF8.GetBytes(entry.ToString() + Environment.NewLine);
		var diff = newEntry.Length - oldLength;

		var fileLength = RandomAccess.GetLength(handle);
		const int bufLength = 64 * 1024;
		var buf = ArrayPool<byte>.Shared.Rent(bufLength);
		var bufMem = buf.AsMemory(..bufLength);

		//no cancelling until the last write finishes
		if (diff > 0)
			await MoveForwards();
		else if (diff < 0)
			await MoveBackwards();

		await RandomAccess.WriteAsync(handle, newEntry.AsMemory().AsBytes(), entryPos, CancellationToken.None);

		ArrayPool<byte>.Shared.Return(buf);
		ct.ThrowIfCancellationRequested();
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
				var read = await RandomAccess.ReadAsync(handle, bufMem[..(int)(last ? lastChunkSize : bufLength)], pos, CancellationToken.None);
				await RandomAccess.WriteAsync(handle, bufMem[..read], pos + diff, CancellationToken.None);
				last = false;
			}
		}

		async Task MoveBackwards()
		{
			//from start to end, move backwards
			for (var pos = entryPos + oldLength; pos < fileLength; pos += bufLength)
			{
				var read = await RandomAccess.ReadAsync(handle, bufMem, pos, CancellationToken.None);
				await RandomAccess.WriteAsync(handle, bufMem[..read], pos + diff, CancellationToken.None);
			}

			RandomAccess.SetLength(handle, fileLength + diff);
		}
	}

	public async Task Add(QAEntry entry, CancellationToken ct = default)
	{
		ct.ThrowIfCancellationRequested();

		var stream = File.Open(config.CurrentValue.FilePath, new FileStreamOptions() { Access = FileAccess.Write, Options = FileOptions.WriteThrough | FileOptions.Asynchronous });
		using var writer = new StreamWriter(stream);
		stream.Seek(0, SeekOrigin.End);

		await writer.WriteAsync((entry.ToString() + Environment.NewLine).AsMemory(), ct);
	}
}
