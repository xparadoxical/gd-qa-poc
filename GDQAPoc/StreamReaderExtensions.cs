﻿namespace GDQAPoc;
public static class StreamReaderExtensions
{
	public static Task<bool> SkipUntil(this StreamReader reader, char c)
		=> SkipUntil(reader, c.ToString());

	public static async Task<bool> SkipUntil(this StreamReader reader, string s)
	{
		var arr = new char[1];
		var memory = arr.AsMemory();

		int correct = 0;

		while (true)
		{
			var read = await reader.ReadAsync(memory);
			if (read == 0)
				return false;

			if (arr[0] == s[correct])
				correct++;
			else
				correct = 0;

			if (correct == s.Length)
				return true;
		}
	}

	public static IAsyncEnumerable<ArraySegment<char>> ReadUntil(this StreamReader reader, char c)
		=> ReadUntil(reader, c.ToString());

	public static async IAsyncEnumerable<ArraySegment<char>> ReadUntil(this StreamReader reader, string s)
	{
		var arr = new char[4096];
		var memory = arr.AsMemory();

		int correct = 0;

		while (true)
		{
			var read = await reader.ReadAsync(memory);
			if (read == 0)
				yield break;

			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] == s[correct])
					correct++;
				else
					correct = 0;

				if (correct == s.Length)
				{
					yield return new(arr, 0, i + 1);

					// abcdefghijlmnoPQRSTuvwxyz
					// 0                        ^read (Position)
					// 0                 ^i
					//                    ^Position -= read - i - 1
					reader.BaseStream.Seek(-(read - i - 1), SeekOrigin.Current);
					yield break;
				}
			}

			yield return new(arr);
		}
	}
}