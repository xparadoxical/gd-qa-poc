using System.Media;

using CommunityToolkit.Mvvm.Messaging;

namespace GDQAPoc;

public partial class Form1 : Form, IRecipient<DataSavedMessage>
{
	private readonly Color _defaultLoadButtonBackColor;
	private readonly SoundPlayer _chimes;

	public Form1()
	{
		InitializeComponent();
		loadButton.BackgroundImage = new Icon(Resources.imageres_5338, 32, 32).ToBitmap();

		_defaultLoadButtonBackColor = loadButton.BackColor;
		_chimes = new SoundPlayer(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Media", "chimes.wav"));

		form1ViewModelBindingSource.DataSource = new Form1ViewModel();
		WeakReferenceMessenger.Default.Register(this);
	}

	public void Receive(DataSavedMessage message) => _chimes.Play();

	private void loadButton_EnabledChanged(object sender, EventArgs e)
	{
		var button = (Button)sender;
		button.BackColor = button.Enabled ? SystemColors.GradientActiveCaption : _defaultLoadButtonBackColor;
	}
}
