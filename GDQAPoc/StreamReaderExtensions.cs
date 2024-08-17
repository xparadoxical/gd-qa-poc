using System.Buffers;
using System.Runtime.CompilerServices;

namespace GDQAPoc;
public static class StreamReaderExtensions
{
	//https://stackoverflow.com/a/59442732/6416482
	public static long GetPosition(this StreamReader reader)
	{
		var buffer = GetCharBuffer(reader);
		var charPos = GetCharPos(reader);
		var charLen = GetCharLen(reader);

		return reader.BaseStream.Position - reader.CurrentEncoding.GetByteCount(buffer, charPos, charLen - charPos);

		[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_charBuffer")]
		static extern ref char[] GetCharBuffer(StreamReader sr);
		[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_charPos")]
		static extern ref int GetCharPos(StreamReader sr);
		[UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_charLen")]
		static extern ref int GetCharLen(StreamReader sr);
	}

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

	public static IAsyncEnumerable<char> ReadUntil(this StreamReader reader, char c)
		=> ReadUntil(reader, c.ToString());

	public static IAsyncEnumerable<char> ReadUntil(this StreamReader reader, string s)
		=> ReadUntilCore(reader, s).SelectMany(segm => segm.ToArray().ToAsyncEnumerable());

	private static async IAsyncEnumerable<ArraySegment<char>> ReadUntilCore(StreamReader reader, string s)
	{
		const int bufLength = 4096;
		var arr = ArrayPool<char>.Shared.Rent(bufLength);
		var memory = arr.AsMemory(0, bufLength);

		int correct = 0;

		while (true)
		{
			var read = await reader.ReadAsync(memory);
			if (read == 0)
				goto stop;

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
					goto stop;
				}
			}

			yield return new(arr);
		}

	stop:
		ArrayPool<char>.Shared.Return(arr);
		yield break;
	}
}
