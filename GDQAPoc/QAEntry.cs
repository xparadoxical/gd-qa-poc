using CommunityToolkit.HighPerformance;

namespace GDQAPoc;

public sealed record QAEntry(uint Level, string Remarks, Issue[] Issues, CoinGuides Coins)
{
	public override string ToString() => $"| {Level} | {string.Join(", ", (object?[])Issues)} | {Remarks} | {Coins} |";
}

public record struct CoinGuides(string? Coin1, string? Coin2, string? Coin3)
{
	public static CoinGuides Parse(ReadOnlySpan<char> input)
	{
		var ret = new CoinGuides(null, null, null);
		foreach (var guide in input.Tokenize(';'))
		{
			var colon = guide.IndexOf(':');
			var id = guide[..colon];
			var text = new string(guide[(colon + 2)..]);

			switch (id)
			{
				case "c1": ret = ret with { Coin1 = text }; break;
				case "c2": ret = ret with { Coin2 = text }; break;
				case "c3": ret = ret with { Coin3 = text }; break;
			}
		}

		return ret;
	}

	public override string ToString()
		=> string.Join("; ",
			new string?[]
			{
				Coin1 is not null ? $"c1: {Coin1}" : null,
				Coin2 is not null ? $"c1: {Coin2}" : null,
				Coin3 is not null ? $"c1: {Coin3}" : null
			}.WhereNotNull());
}
