namespace GDQAPoc;
public static class EnumerableExtensions
{
	public static T? FirstOfTypeOrDefault<T>(this System.Collections.IEnumerable e)
		=> e.Cast<T>().FirstOrDefault();
}
