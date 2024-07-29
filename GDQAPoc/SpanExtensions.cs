using CommunityToolkit.HighPerformance;

namespace GDQAPoc;
public static class SpanExtensions
{
	/// <summary>Consumes a part of the <paramref name="span"/>, modifying it.</summary>
	/// <returns>The consumed part of <paramref name="span"/>.</returns>
	public static ReadOnlySpan<T> ConsumeUntil<T>(this scoped ref ReadOnlySpan<T> span, T terminator)
	{
		var length = span.IndexOf(terminator) + 1;
		var consumed = span[..length];

		span = span[length..];
		return consumed;
	}
}
