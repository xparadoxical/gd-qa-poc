namespace GDQAPoc;

public sealed class IssueCollection// : IEnumerable<Issue>
{
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool BadGameplay { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool Unreadable { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool Overdecorated { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool BadMusicSync { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool Memory { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool NoCoin1Indication { get; set; }
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool NoCoin2Indication { get; set; }
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool NoCoin3Indication { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool FreeCoin1 { get; set; }
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool FreeCoin2 { get; set; }
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool FreeCoin3 { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool InsaneCoin1 { get; set; }
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool InsaneCoin2 { get; set; }
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool InsaneCoin3 { get; set; }

	public IEnumerable<Issue> Enumerate()
	{
		if (BadGameplay) yield return new Issue.BadGameplay();
		if (Unreadable) yield return new Issue.Unreadable();
		if (Overdecorated) yield return new Issue.Overdecorated();
		if (BadMusicSync) yield return new Issue.BadSync();
		if (Memory) yield return new Issue.Memory();
		if (NoCoin1Indication || NoCoin2Indication || NoCoin3Indication)
			yield return new Issue.NoCoinIndication(NoCoin1Indication, NoCoin2Indication, NoCoin3Indication);
		if (FreeCoin1 || FreeCoin2 || FreeCoin3)
			yield return new Issue.FreeCoins(FreeCoin1, FreeCoin2, FreeCoin3);
		if (InsaneCoin1 || InsaneCoin2 || InsaneCoin3)
			yield return new Issue.InsaneCoins(InsaneCoin1, InsaneCoin2, InsaneCoin3);
	}

	//IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
