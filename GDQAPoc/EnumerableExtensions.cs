namespace GDQAPoc;
public static class EnumerableExtensions
{
	public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> e)
		=> e.Where(x => x is not null).Select(x => x!);
}
