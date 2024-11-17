namespace GDQAPoc;

public partial class Form1 : Form
{
	private readonly Color _defaultLoadButtonBackColor;

	public Form1()
	{
		InitializeComponent();
		loadButton.BackgroundImage = new Icon(Resources.imageres_5338, 32, 32).ToBitmap();

		_defaultLoadButtonBackColor = loadButton.BackColor;

		form1ViewModelBindingSource.DataSource = new Form1ViewModel();
	}

	private void loadButton_EnabledChanged(object sender, EventArgs e)
	{
		var button = (Button)sender;
		button.BackColor = button.Enabled ? SystemColors.GradientActiveCaption : _defaultLoadButtonBackColor;
	}
}
