using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GDQAPoc;
public partial class Form1ViewModel : ObservableObject
{
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveNoIssues) + "Command", nameof(SaveWithIssues) + "Command")]
	private string _id = "";

	[ObservableProperty]
	private string _remarks = "";

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private string _coinGuide1 = "";
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private string _coinGuide2 = "";
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private string _coinGuide3 = "";

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _badGameplay;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _unreadable;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _overdecorated;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _badMusicSync;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _memory;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _noCoinIndication1;
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _noCoinIndication2;
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _noCoinIndication3;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _freeCoin1;
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _freeCoin2;
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _freeCoin3;

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _insaneCoin1;
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _insaneCoin2;
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private bool _insaneCoin3;

	[ObservableProperty]
	private bool _exists;

	private readonly QAFile _file = new(Environment.GetEnvironmentVariable("USERPROFILE") + @"\Documents\gd qa.md");

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
		var exists = await _file.Exists(id);

		if (exists)
		{
			if (MessageBox.Show($"Overwrite {id}?",
					"Confirm overwrite",
					MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
				is not DialogResult.OK)
				return;
		}

		var entry = new QAEntry(id, Remarks,
			new Issue?[]
			{
				BadGameplay ? new Issue.BadGameplay() : null,
				Unreadable ? new Issue.Unreadable() : null,
				Overdecorated ? new Issue.Overdecorated() : null,
				BadMusicSync ? new Issue.BadSync() : null,
				Memory ? new Issue.Memory() : null,
				NoCoinIndication1 || NoCoinIndication2 || NoCoinIndication3
					? new Issue.NoCoinIndication(NoCoinIndication1, NoCoinIndication2, NoCoinIndication3) : null,
				FreeCoin1 || FreeCoin2 || FreeCoin3
					? new Issue.FreeCoins(FreeCoin1, FreeCoin2, FreeCoin3) : null,
				InsaneCoin1 || InsaneCoin2 || InsaneCoin3
					? new Issue.InsaneCoins(InsaneCoin1, InsaneCoin2, InsaneCoin3) : null
			}.WhereNotNull().ToArray(),
			new CoinGuides(CoinGuide1, CoinGuide2, CoinGuide3));

		if (exists)
			await _file.Overwrite(id, entry);
		else
			await _file.Append(id, entry);
	}

	private bool ValidateId() => uint.TryParse(Id, out _);

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
			(CoinGuide1.Any() || CoinGuide2.Any() || CoinGuide3.Any()
			|| BadGameplay
			|| Unreadable
			|| Overdecorated
			|| BadMusicSync
			|| Memory
			|| NoCoinIndication1 || NoCoinIndication2 || NoCoinIndication3
			|| FreeCoin1 || FreeCoin2 || FreeCoin3
			|| InsaneCoin1 || InsaneCoin2 || InsaneCoin3);
	}

	[RelayCommand(CanExecute = nameof(CanSaveWithIssues))]
	public async Task SaveWithIssues() => await Save(true);
}
