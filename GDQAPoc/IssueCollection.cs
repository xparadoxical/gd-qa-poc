using CommunityToolkit.Mvvm.ComponentModel;

namespace GDQAPoc;

public sealed partial class IssueCollection : ObservableObject
{
	public IssueCollection() { }

	public IssueCollection(Issue[] issues)
	{
		if (issues.Any(i => i is Issue.BadGameplay))
			BadGameplay = true;
		if (issues.Any(i => i is Issue.Unreadable))
			Unreadable = true;
		if (issues.Any(i => i is Issue.Overdecorated))
			Overdecorated = true;
		if (issues.Any(i => i is Issue.BadSync))
			BadMusicSync = true;
		if (issues.Any(i => i is Issue.Memory))
			Memory = true;
		if (issues.FirstOfTypeOrDefault<Issue.NoCoinIndication>() is { } nci)
		{
			NoCoin1Indication = nci.C1;
			NoCoin2Indication = nci.C2;
			NoCoin3Indication = nci.C3;
		}
		if (issues.FirstOfTypeOrDefault<Issue.FreeCoins>() is { } fc)
		{
			FreeCoin1 = fc.C1;
			FreeCoin2 = fc.C2;
			FreeCoin3 = fc.C3;
		}
		if (issues.FirstOfTypeOrDefault<Issue.InsaneCoins>() is { } ic)
		{
			InsaneCoin1 = ic.C1;
			InsaneCoin2 = ic.C2;
			InsaneCoin3 = ic.C3;
		}
	}

	[ObservableProperty]
	public bool _badGameplay;

	[ObservableProperty]
	public bool _unreadable;

	[ObservableProperty]
	public bool _overdecorated;

	[ObservableProperty]
	public bool _badMusicSync;

	[ObservableProperty]
	public bool _memory;

	[ObservableProperty]
	public bool _noCoin1Indication;
	[ObservableProperty]
	public bool _noCoin2Indication;
	[ObservableProperty]
	public bool _noCoin3Indication;

	[ObservableProperty]
	public bool _freeCoin1;
	[ObservableProperty]
	public bool _freeCoin2;
	[ObservableProperty]
	public bool _freeCoin3;

	[ObservableProperty]
	public bool _insaneCoin1;
	[ObservableProperty]
	public bool _insaneCoin2;
	[ObservableProperty]
	public bool _insaneCoin3;

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

	public bool IsEmpty =>
		!(BadGameplay
			|| Unreadable
			|| Overdecorated
			|| BadMusicSync
			|| Memory
			|| NoCoin1Indication
			|| NoCoin2Indication
			|| NoCoin3Indication
			|| FreeCoin1
			|| FreeCoin2
			|| FreeCoin3
			|| InsaneCoin1
			|| InsaneCoin2
			|| InsaneCoin3);
}
