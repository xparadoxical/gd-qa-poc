namespace GDQAPoc;

public record Issue(string Content)
{
	public sealed record BadGameplay() : Issue("bgp");
	public sealed record Unreadable() : Issue("unrd");
	public sealed record Overdecorated() : Issue("ovdeco");
	public sealed record BadSync() : Issue("sync");
	public sealed record Memory() : Issue("mem");

	public record CoinIssue(string Id, bool C1, bool C2, bool C3)
		: Issue(C1 || C2 || C3 ? $"{Id} {string.Join(' ', new[] { (1, C1), (2, C2), (3, C3) }
																	.Where(c => c.Item2)
																	.Select(c => c.Item1))}" : "");

	public sealed record NoCoinIndication(bool C1, bool C2, bool C3) : CoinIssue("nci", C1, C2, C3);
	public sealed record FreeCoins(bool C1, bool C2, bool C3) : CoinIssue("fc", C1, C2, C3);
	public sealed record InsaneCoins(bool C1, bool C2, bool C3) : CoinIssue("ic", C1, C2, C3);
}