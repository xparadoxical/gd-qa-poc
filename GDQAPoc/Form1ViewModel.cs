using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GDQAPoc;
public partial class Form1ViewModel : ObservableObject
{
	[ObservableProperty]
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



	partial void OnIdChanged(string value)
	{
		//verify id, maybe load level data on textbox unfocus
	}

	public async Task Save()
	{

	}

	[RelayCommand]
	public async Task SaveNoIssues()
	{
		if (AnyIssuesFilled())
		{
			if (MessageBox.Show("Do you want to ignore your written issues?",
				"Confirm ignore",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
				is not DialogResult.OK)
				return;
		}


	}

	public bool AnyIssuesFilled()
	{
		return true;
	}

	[RelayCommand(CanExecute = nameof(AnyIssuesFilled))]
	public async Task SaveWithIssues()
	{

	}
}
