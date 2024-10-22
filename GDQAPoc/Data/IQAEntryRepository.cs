namespace GDQAPoc.Data;
public interface IQAEntryRepository
{
	Task<bool> Exists(uint level);
	Task<QAEntry?> TryRead(uint level);

	Task Add(QAEntry entry);
	Task Overwrite(QAEntry entry);
}
