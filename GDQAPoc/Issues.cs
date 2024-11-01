using System.Diagnostics;

namespace GDQAPoc;

public record Issue(string Tag)
{
	public static Issue Parse(ReadOnlySpan<char> input)
	{
		var space = input.IndexOf(' ');
		var tag = input[..space];

		var simpleIssue = tag switch
		{
			"bgp" => new BadGameplay(),
			"unrd" => new Unreadable(),
			"ovdeco" => new Overdecorated(),
			"sync" => new BadSync(),
			"mem" => new Memory(),
			_ => default(Issue)
		};
		if (simpleIssue is not null)
			return simpleIssue;

		var c1 = input.Contains('1');
		var c2 = input.Contains('2');
		var c3 = input.Contains('3');
		return tag switch
		{
			"nci" => new NoCoinIndication(c1, c2, c3),
			"fc" => new FreeCoins(c1, c2, c3),
			"ic" => new InsaneCoins(c1, c2, c3),
			_ => throw new UnreachableException()
		};
	}

	public sealed override string ToString() => Tag;

	public sealed record BadGameplay() : Issue("bgp");
	public sealed record Unreadable() : Issue("unrd");
	public sealed record Overdecorated() : Issue("ovdeco");
	public sealed record BadSync() : Issue("sync");
	public sealed record Memory() : Issue("mem");

	public record CoinIssue(string Tag, bool C1, bool C2, bool C3)
		: Issue(C1 || C2 || C3 ? $"{Tag} {string.Join(' ', new[] { (1, C1), (2, C2), (3, C3) }
																	.Where(c => c.Item2)
																	.Select(c => c.Item1))}" : "");

	public sealed record NoCoinIndication(bool C1, bool C2, bool C3) : CoinIssue("nci", C1, C2, C3);
	public sealed record FreeCoins(bool C1, bool C2, bool C3) : CoinIssue("fc", C1, C2, C3);
	public sealed record InsaneCoins(bool C1, bool C2, bool C3) : CoinIssue("ic", C1, C2, C3);
}