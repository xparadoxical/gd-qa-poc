namespace GDQAPoc.Data;
public interface IQAEntryRepository
{
	Task<bool> Exists(uint level, CancellationToken ct = default);
	Task<QAEntry?> TryRead(uint level, CancellationToken ct = default);

	Task Add(QAEntry entry, CancellationToken ct = default);
	Task Overwrite(QAEntry entry, CancellationToken ct = default);
}
