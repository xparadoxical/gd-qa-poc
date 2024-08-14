namespace GDQAPoc;

public sealed class IssueCollection// : IEnumerable<Issue>
{
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool BadGameplay { get; set; }

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool Unreadable;

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool Overdecorated;

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool BadMusicSync;

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool Memory;

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool NoCoin1Indication;
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool NoCoin2Indication;
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool NoCoin3Indication;

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool FreeCoin1;
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool FreeCoin2;
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool FreeCoin3;

	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool InsaneCoin1;
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool InsaneCoin2;
	//[ObservableProperty, NotifyCanExecuteChangedFor(nameof(SaveWithIssues) + "Command")]
	public bool InsaneCoin3;

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
