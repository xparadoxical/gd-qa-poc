﻿using System.Globalization;

using CommunityToolkit.HighPerformance;

namespace GDQAPoc;

public sealed record QAEntry(uint Level, string Remarks, IssueCollection Issues, CoinGuides Coins)
{
	public override string ToString()
		=> $"| {Level} | {(Issues.IsEmpty ? '-' : string.Join(", ", Issues.Enumerate()))} | {Remarks} | {Coins} |";

	internal static bool TryParseId(string input, out uint id)
		=> uint.TryParse(input, NumberStyles.None, null, out id) && id > 0;
}

public record struct CoinGuides(string Coin1, string Coin2, string Coin3)
{
	public static CoinGuides Parse(ReadOnlySpan<char> input)
	{
		var ret = new CoinGuides("", "", "");
		foreach (var guide in input.Tokenize(';'))
		{
			if (guide.IsEmpty)
				continue;

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
				string.IsNullOrEmpty(Coin1) ? null : $"c1: {Coin1}",
				string.IsNullOrEmpty(Coin2) ? null : $"c2: {Coin2}",
				string.IsNullOrEmpty(Coin3) ? null : $"c3: {Coin3}"
			}.WhereNotNull());
}
