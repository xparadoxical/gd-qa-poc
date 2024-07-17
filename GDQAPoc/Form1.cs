namespace GDQAPoc;

public partial class Form1 : Form
{
	public Form1()
	{
		InitializeComponent();
		form1ViewModelBindingSource.DataSource = new Form1ViewModel();
	}
}
