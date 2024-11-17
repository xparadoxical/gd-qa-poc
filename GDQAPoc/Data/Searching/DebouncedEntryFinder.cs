using CommunityToolkit.Mvvm.Messaging;

namespace GDQAPoc.Data.Searching;
public sealed class DebouncedEntryFinder : IRecipient<IdChangedMessage>
{
	private readonly IQAEntryRepository _repo;
	private CancellationTokenSource? _cts;

	public DebouncedEntryFinder(IQAEntryRepository repo)
	{
		_repo = repo;
		WeakReferenceMessenger.Default.Register(this);
	}

	public void Receive(IdChangedMessage message)
	{
		const int debounceMs = 1250;

		_cts?.Cancel();
		_cts?.Dispose();
		_cts = null;

		if (!QAEntry.TryParseId(message.Id, out var id))
			return;

		var originalScheduler = TaskScheduler.FromCurrentSynchronizationContext(); //might use the UI sync context
		_cts = new();
		var ct = _cts.Token;

		Task.Delay(debounceMs, ct)
			.ContinueWith(async _ =>
			{
				if (await _repo.TryRead(id, ct) is not null and var entry)
				{
					WeakReferenceMessenger.Default.Send(new EntryFoundMessage(entry));//run this on ui thread :(
					return;
				}

				ct.ThrowIfCancellationRequested();
			}, ct, TaskContinuationOptions.NotOnCanceled, originalScheduler);
	}
}
