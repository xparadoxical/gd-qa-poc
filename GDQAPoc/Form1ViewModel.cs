using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GDQAPoc;
public partial class Form1ViewModel : ObservableObject
{
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveNoIssues) + "Command", nameof(SaveWithIssues) + "Command")]
	private string _id = "";

	[ObservableProperty]
	private string _remarks = "";

	public IssueCollection Issues { get; set; } = new();

	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private string _coinGuide1 = "";
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private string _coinGuide2 = "";
	[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	private string _coinGuide3 = "";

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

		var entry = new QAEntry(id, Remarks, Issues, new CoinGuides(CoinGuide1, CoinGuide2, CoinGuide3));

		if (exists)
			await _file.Overwrite(entry);
		else
			await _file.Append(entry);
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
			(CoinGuide1.Length != 0 || CoinGuide2.Length != 0 || CoinGuide3.Length != 0
			|| Issues.BadGameplay
			|| Issues.Unreadable
			|| Issues.Overdecorated
			|| Issues.BadMusicSync
			|| Issues.Memory
			|| Issues.NoCoin1Indication || Issues.NoCoin2Indication || Issues.NoCoin3Indication
			|| Issues.FreeCoin1 || Issues.FreeCoin2 || Issues.FreeCoin3
			|| Issues.InsaneCoin1 || Issues.InsaneCoin2 || Issues.InsaneCoin3);
	}

	[RelayCommand(CanExecute = nameof(CanSaveWithIssues))]
	public async Task SaveWithIssues() => await Save(true);
}
