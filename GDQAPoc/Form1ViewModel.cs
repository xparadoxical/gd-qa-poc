using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using GDQAPoc.Data;
using GDQAPoc.Data.Searching;

namespace GDQAPoc;
public partial class Form1ViewModel : ObservableObject, IRecipient<EntryFoundMessage>
{
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveNoIssuesCommand), nameof(SaveWithIssuesCommand))]
	private string _id = "";

	[ObservableProperty]
	private string _remarks = "";

	[ObservableProperty]
	private IssueCollection _issues;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssuesCommand))]
	private string _coinGuide1 = "";
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssuesCommand))]
	private string _coinGuide2 = "";
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssuesCommand))]
	private string _coinGuide3 = "";

	private readonly IQAEntryRepository _qa = Ioc.Default.GetRequiredService<IQAEntryRepository>();

	public Form1ViewModel()
	{
		Issues = new();
		Issues.PropertyChanged += (_, _) => SaveWithIssuesCommand.NotifyCanExecuteChanged();
		WeakReferenceMessenger.Default.Register(this);
		Ioc.Default.GetRequiredService<DebouncedEntryFinder>(); //ensure initialized
	}

	//async partial void OnIdChanged(string value)
	//{
	//	//verify id, maybe load level data on textbox unfocus
	//	if (uint.TryParse(value, out var id) is false)
	//		return;

	//	if (await _file.TryRead(id) is null)
	//		return;
	//}

	public async Task Save(bool all)
	{
		var id = uint.Parse(Id);
		var exists = await _qa.Exists(id);

		if (exists)
		{
			if (MessageBox.Show($"Overwrite {id}?",
					"Confirm overwrite",
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
				is not DialogResult.Yes)
				return;
		}

		var entry = new QAEntry(id, Remarks, Issues, new CoinGuides(CoinGuide1, CoinGuide2, CoinGuide3));

		if (exists)
			await _qa.Overwrite(entry);
		else
			await _qa.Add(entry);

		WeakReferenceMessenger.Default.Send<DataSavedMessage>();
		Id = "";
		Issues = new();
		Remarks = CoinGuide1 = CoinGuide2 = CoinGuide3 = "";
	}

	[ObservableProperty]
	private bool _isIdValid;

	private bool ValidateId()
		=> IsIdValid = QAEntry.TryParseId(Id, out _);

	[RelayCommand(CanExecute = nameof(ValidateId))]
	public async Task SaveNoIssues()
	{
		if (CanSaveWithIssues())
		{
			if (MessageBox.Show("Do you want to ignore your written issues?",
					"Confirm ignore",
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
				is not DialogResult.OK)
				return;
		}

		await Save(false);
	}

	public bool CanSaveWithIssues()
	{
		return ValidateId() &&
			//remarks are ignored
			(CoinGuide1.Length != 0 || CoinGuide2.Length != 0 || CoinGuide3.Length != 0 || !Issues.IsEmpty);
	}

	[RelayCommand(CanExecute = nameof(CanSaveWithIssues))]
	public async Task SaveWithIssues() => await Save(true);

	partial void OnIdChanged(string value)
	{
		EntryExists = false;
		_entry = null;
		WeakReferenceMessenger.Default.Send(new IdChangedMessage(value));
	}

	[ObservableProperty]
	private bool _entryExists;

	private QAEntry? _entry;

	public void Receive(EntryFoundMessage message)
	{
		EntryExists = true;
		_entry = message.Entry;
	}

	[RelayCommand]
	public void LoadExistingEntry()
	{
		Issues = _entry!.Issues;
		(CoinGuide1, CoinGuide2, CoinGuide3) = _entry.Coins;
		Remarks = _entry.Remarks;
	}
}
