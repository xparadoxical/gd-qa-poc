namespace GDQAPoc;
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
}
