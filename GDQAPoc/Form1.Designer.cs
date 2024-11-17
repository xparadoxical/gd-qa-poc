namespace GDQAPoc;

partial class Form1
{
	/// <summary>
	///  Required designer variable.
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	///  Clean up any resources being used.
	/// </summary>
	/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows Form Designer generated code

	/// <summary>
	///  Required method for Designer support - do not modify
	///  the contents of this method with the code editor.
	/// </summary>
	private void InitializeComponent()
	{
		components = new System.ComponentModel.Container();
		textBox1 = new TextBox();
		form1ViewModelBindingSource = new BindingSource(components);
		button1 = new Button();
		textBox2 = new TextBox();
		textBox3 = new TextBox();
		textBox4 = new TextBox();
		textBox5 = new TextBox();
		panel1 = new Panel();
		checkBox14 = new CheckBox();
		checkBox13 = new CheckBox();
		checkBox12 = new CheckBox();
		checkBox11 = new CheckBox();
		checkBox10 = new CheckBox();
		panel2 = new Panel();
		splitContainer1 = new SplitContainer();
		loadButton = new Button();
		panel3 = new Panel();
		checkBox23 = new CheckBox();
		checkBox18 = new CheckBox();
		label3 = new Label();
		checkBox22 = new CheckBox();
		label2 = new Label();
		checkBox21 = new CheckBox();
		checkBox19 = new CheckBox();
		checkBox20 = new CheckBox();
		checkBox17 = new CheckBox();
		label1 = new Label();
		checkBox16 = new CheckBox();
		checkBox15 = new CheckBox();
		button2 = new Button();
		((System.ComponentModel.ISupportInitialize)form1ViewModelBindingSource).BeginInit();
		panel1.SuspendLayout();
		panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
		splitContainer1.Panel1.SuspendLayout();
		splitContainer1.Panel2.SuspendLayout();
		splitContainer1.SuspendLayout();
		panel3.SuspendLayout();
		SuspendLayout();
		// 
		// textBox1
		// 
		textBox1.DataBindings.Add(new Binding("Text", form1ViewModelBindingSource, "Id", true, DataSourceUpdateMode.OnPropertyChanged));
		textBox1.Location = new Point(6, 3);
		textBox1.Name = "textBox1";
		textBox1.Size = new Size(125, 27);
		textBox1.TabIndex = 0;
		// 
		// form1ViewModelBindingSource
		// 
		form1ViewModelBindingSource.DataSource = typeof(Form1ViewModel);
		// 
		// button1
		// 
		button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		button1.DataBindings.Add(new Binding("Command", form1ViewModelBindingSource, "SaveNoIssuesCommand", true));
		button1.Location = new Point(192, 3);
		button1.Name = "button1";
		button1.Size = new Size(94, 29);
		button1.TabIndex = 1;
		button1.Text = "No issues";
		button1.UseVisualStyleBackColor = true;
		// 
		// textBox2
		// 
		textBox2.DataBindings.Add(new Binding("Text", form1ViewModelBindingSource, "CoinGuide1", true, DataSourceUpdateMode.OnPropertyChanged));
		textBox2.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		textBox2.Location = new Point(3, 3);
		textBox2.Name = "textBox2";
		textBox2.PlaceholderText = "Coin 1 guide";
		textBox2.Size = new Size(283, 27);
		textBox2.TabIndex = 0;
		// 
		// textBox3
		// 
		textBox3.DataBindings.Add(new Binding("Text", form1ViewModelBindingSource, "CoinGuide2", true, DataSourceUpdateMode.OnPropertyChanged));
		textBox3.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		textBox3.Location = new Point(3, 36);
		textBox3.Name = "textBox3";
		textBox3.PlaceholderText = "Coin 2 guide";
		textBox3.Size = new Size(283, 27);
		textBox3.TabIndex = 0;
		// 
		// textBox4
		// 
		textBox4.DataBindings.Add(new Binding("Text", form1ViewModelBindingSource, "CoinGuide3", true, DataSourceUpdateMode.OnPropertyChanged));
		textBox4.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		textBox4.Location = new Point(3, 69);
		textBox4.Name = "textBox4";
		textBox4.PlaceholderText = "Coin 3 guide";
		textBox4.Size = new Size(283, 27);
		textBox4.TabIndex = 0;
		// 
		// textBox5
		// 
		textBox5.DataBindings.Add(new Binding("Text", form1ViewModelBindingSource, "Remarks", true));
		textBox5.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		textBox5.Location = new Point(6, 36);
		textBox5.Name = "textBox5";
		textBox5.PlaceholderText = "Remarks";
		textBox5.Size = new Size(283, 27);
		textBox5.TabIndex = 4;
		// 
		// panel1
		// 
		panel1.Controls.Add(checkBox14);
		panel1.Controls.Add(checkBox13);
		panel1.Controls.Add(checkBox12);
		panel1.Controls.Add(checkBox11);
		panel1.Controls.Add(checkBox10);
		panel1.Location = new Point(3, 110);
		panel1.Name = "panel1";
		panel1.Size = new Size(138, 155);
		panel1.TabIndex = 6;
		// 
		// checkBox14
		// 
		checkBox14.AutoSize = true;
		checkBox14.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.Memory", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox14.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox14.Location = new Point(3, 123);
		checkBox14.Name = "checkBox14";
		checkBox14.Size = new Size(86, 24);
		checkBox14.TabIndex = 4;
		checkBox14.Text = "Memory";
		checkBox14.UseVisualStyleBackColor = true;
		// 
		// checkBox13
		// 
		checkBox13.AutoSize = true;
		checkBox13.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.BadMusicSync", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox13.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox13.Location = new Point(3, 93);
		checkBox13.Name = "checkBox13";
		checkBox13.Size = new Size(131, 24);
		checkBox13.TabIndex = 3;
		checkBox13.Text = "Bad music sync";
		checkBox13.UseVisualStyleBackColor = true;
		// 
		// checkBox12
		// 
		checkBox12.AutoSize = true;
		checkBox12.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.Overdecorated", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox12.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox12.Location = new Point(3, 63);
		checkBox12.Name = "checkBox12";
		checkBox12.Size = new Size(130, 24);
		checkBox12.TabIndex = 2;
		checkBox12.Text = "Overdecorated";
		checkBox12.UseVisualStyleBackColor = true;
		// 
		// checkBox11
		// 
		checkBox11.AutoSize = true;
		checkBox11.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.Unreadable", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox11.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox11.Location = new Point(3, 33);
		checkBox11.Name = "checkBox11";
		checkBox11.Size = new Size(108, 24);
		checkBox11.TabIndex = 1;
		checkBox11.Text = "Unreadable";
		checkBox11.UseVisualStyleBackColor = true;
		// 
		// checkBox10
		// 
		checkBox10.AutoSize = true;
		checkBox10.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.BadGameplay", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox10.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox10.Location = new Point(3, 3);
		checkBox10.Name = "checkBox10";
		checkBox10.Size = new Size(127, 24);
		checkBox10.TabIndex = 0;
		checkBox10.Text = "Bad gameplay";
		checkBox10.UseVisualStyleBackColor = true;
		// 
		// panel2
		// 
		panel2.Controls.Add(textBox4);
		panel2.Controls.Add(textBox2);
		panel2.Controls.Add(textBox3);
		panel2.Location = new Point(3, 3);
		panel2.Name = "panel2";
		panel2.Size = new Size(289, 101);
		panel2.TabIndex = 7;
		// 
		// splitContainer1
		// 
		splitContainer1.Dock = DockStyle.Fill;
		splitContainer1.FixedPanel = FixedPanel.Panel1;
		splitContainer1.Location = new Point(0, 0);
		splitContainer1.Name = "splitContainer1";
		splitContainer1.Orientation = Orientation.Horizontal;
		// 
		// splitContainer1.Panel1
		// 
		splitContainer1.Panel1.Controls.Add(loadButton);
		splitContainer1.Panel1.Controls.Add(textBox1);
		splitContainer1.Panel1.Controls.Add(button1);
		splitContainer1.Panel1.Controls.Add(textBox5);
		// 
		// splitContainer1.Panel2
		// 
		splitContainer1.Panel2.Controls.Add(panel3);
		splitContainer1.Panel2.Controls.Add(button2);
		splitContainer1.Panel2.Controls.Add(panel2);
		splitContainer1.Panel2.Controls.Add(panel1);
		splitContainer1.Size = new Size(296, 385);
		splitContainer1.SplitterDistance = 72;
		splitContainer1.TabIndex = 8;
		// 
		// loadButton
		// 
		loadButton.BackgroundImageLayout = ImageLayout.Stretch;
		loadButton.DataBindings.Add(new Binding("Command", form1ViewModelBindingSource, "LoadExistingEntryCommand", true));
		loadButton.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "EntryExists", true, DataSourceUpdateMode.OnPropertyChanged));
		loadButton.Location = new Point(137, 3);
		loadButton.Name = "loadButton";
		loadButton.Size = new Size(29, 29);
		loadButton.TabIndex = 5;
		loadButton.UseVisualStyleBackColor = true;
		loadButton.EnabledChanged += loadButton_EnabledChanged;
		// 
		// panel3
		// 
		panel3.Controls.Add(checkBox23);
		panel3.Controls.Add(checkBox18);
		panel3.Controls.Add(label3);
		panel3.Controls.Add(checkBox22);
		panel3.Controls.Add(label2);
		panel3.Controls.Add(checkBox21);
		panel3.Controls.Add(checkBox19);
		panel3.Controls.Add(checkBox20);
		panel3.Controls.Add(checkBox17);
		panel3.Controls.Add(label1);
		panel3.Controls.Add(checkBox16);
		panel3.Controls.Add(checkBox15);
		panel3.Location = new Point(147, 110);
		panel3.Name = "panel3";
		panel3.Size = new Size(145, 155);
		panel3.TabIndex = 9;
		// 
		// checkBox23
		// 
		checkBox23.AutoSize = true;
		checkBox23.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.InsaneCoin3", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox23.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox23.Location = new Point(98, 130);
		checkBox23.Name = "checkBox23";
		checkBox23.Size = new Size(39, 24);
		checkBox23.TabIndex = 6;
		checkBox23.Text = "3";
		checkBox23.UseVisualStyleBackColor = true;
		// 
		// checkBox18
		// 
		checkBox18.AutoSize = true;
		checkBox18.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.FreeCoin3", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox18.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox18.Location = new Point(98, 80);
		checkBox18.Name = "checkBox18";
		checkBox18.Size = new Size(39, 24);
		checkBox18.TabIndex = 6;
		checkBox18.Text = "3";
		checkBox18.UseVisualStyleBackColor = true;
		// 
		// label3
		// 
		label3.AutoSize = true;
		label3.Location = new Point(3, 107);
		label3.Name = "label3";
		label3.Size = new Size(89, 20);
		label3.TabIndex = 3;
		label3.Text = "Insane coins";
		// 
		// checkBox22
		// 
		checkBox22.AutoSize = true;
		checkBox22.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.InsaneCoin1", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox22.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox22.Location = new Point(8, 130);
		checkBox22.Name = "checkBox22";
		checkBox22.Size = new Size(39, 24);
		checkBox22.TabIndex = 4;
		checkBox22.Text = "1";
		checkBox22.UseVisualStyleBackColor = true;
		// 
		// label2
		// 
		label2.AutoSize = true;
		label2.Location = new Point(3, 57);
		label2.Name = "label2";
		label2.Size = new Size(75, 20);
		label2.TabIndex = 3;
		label2.Text = "Free coins";
		// 
		// checkBox21
		// 
		checkBox21.AutoSize = true;
		checkBox21.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.InsaneCoin2", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox21.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox21.Location = new Point(53, 130);
		checkBox21.Name = "checkBox21";
		checkBox21.Size = new Size(39, 24);
		checkBox21.TabIndex = 5;
		checkBox21.Text = "2";
		checkBox21.UseVisualStyleBackColor = true;
		// 
		// checkBox19
		// 
		checkBox19.AutoSize = true;
		checkBox19.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.FreeCoin1", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox19.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox19.Location = new Point(8, 80);
		checkBox19.Name = "checkBox19";
		checkBox19.Size = new Size(39, 24);
		checkBox19.TabIndex = 4;
		checkBox19.Text = "1";
		checkBox19.UseVisualStyleBackColor = true;
		// 
		// checkBox20
		// 
		checkBox20.AutoSize = true;
		checkBox20.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.FreeCoin2", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox20.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox20.Location = new Point(53, 80);
		checkBox20.Name = "checkBox20";
		checkBox20.Size = new Size(39, 24);
		checkBox20.TabIndex = 5;
		checkBox20.Text = "2";
		checkBox20.UseVisualStyleBackColor = true;
		// 
		// checkBox17
		// 
		checkBox17.AutoSize = true;
		checkBox17.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.NoCoin3Indication", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox17.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox17.Location = new Point(98, 30);
		checkBox17.Name = "checkBox17";
		checkBox17.Size = new Size(39, 24);
		checkBox17.TabIndex = 2;
		checkBox17.Text = "3";
		checkBox17.UseVisualStyleBackColor = true;
		// 
		// label1
		// 
		label1.AutoSize = true;
		label1.Location = new Point(3, 7);
		label1.Name = "label1";
		label1.Size = new Size(131, 20);
		label1.TabIndex = 0;
		label1.Text = "No coin indication";
		// 
		// checkBox16
		// 
		checkBox16.AutoSize = true;
		checkBox16.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.NoCoin1Indication", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox16.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox16.Location = new Point(8, 30);
		checkBox16.Name = "checkBox16";
		checkBox16.Size = new Size(39, 24);
		checkBox16.TabIndex = 0;
		checkBox16.Text = "1";
		checkBox16.UseVisualStyleBackColor = true;
		// 
		// checkBox15
		// 
		checkBox15.AutoSize = true;
		checkBox15.DataBindings.Add(new Binding("Checked", form1ViewModelBindingSource, "Issues.NoCoin2Indication", true, DataSourceUpdateMode.OnPropertyChanged));
		checkBox15.DataBindings.Add(new Binding("Enabled", form1ViewModelBindingSource, "IsIdValid", true));
		checkBox15.Location = new Point(53, 30);
		checkBox15.Name = "checkBox15";
		checkBox15.Size = new Size(39, 24);
		checkBox15.TabIndex = 1;
		checkBox15.Text = "2";
		checkBox15.UseVisualStyleBackColor = true;
		// 
		// button2
		// 
		button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
		button2.DataBindings.Add(new Binding("Command", form1ViewModelBindingSource, "SaveWithIssuesCommand", true));
		button2.Location = new Point(199, 277);
		button2.Name = "button2";
		button2.Size = new Size(94, 29);
		button2.TabIndex = 8;
		button2.Text = "Save";
		button2.UseVisualStyleBackColor = true;
		// 
		// Form1
		// 
		AutoScaleDimensions = new SizeF(8F, 20F);
		AutoScaleMode = AutoScaleMode.Font;
		ClientSize = new Size(296, 385);
		Controls.Add(splitContainer1);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		MaximizeBox = false;
		MinimizeBox = false;
		Name = "Form1";
		Text = "GD QA PoC";
		((System.ComponentModel.ISupportInitialize)form1ViewModelBindingSource).EndInit();
		panel1.ResumeLayout(false);
		panel1.PerformLayout();
		panel2.ResumeLayout(false);
		panel2.PerformLayout();
		splitContainer1.Panel1.ResumeLayout(false);
		splitContainer1.Panel1.PerformLayout();
		splitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
		splitContainer1.ResumeLayout(false);
		panel3.ResumeLayout(false);
		panel3.PerformLayout();
		ResumeLayout(false);
	}

	#endregion

	private TextBox textBox1;
	private Button button1;
	private TextBox textBox4;
	private TextBox textBox3;
	private TextBox textBox2;
	private TextBox textBox5;
	private Panel panel1;
	private CheckBox checkBox14;
	private CheckBox checkBox13;
	private CheckBox checkBox12;
	private CheckBox checkBox11;
	private CheckBox checkBox10;
	private Panel panel2;
	private SplitContainer splitContainer1;
	private Button button2;
	private Panel panel3;
	private Label label1;
	private CheckBox checkBox23;
	private CheckBox checkBox18;
	private Label label3;
	private CheckBox checkBox22;
	private Label label2;
	private CheckBox checkBox21;
	private CheckBox checkBox19;
	private CheckBox checkBox20;
	private CheckBox checkBox17;
	private CheckBox checkBox16;
	private CheckBox checkBox15;
	private BindingSource form1ViewModelBindingSource;
	private Button loadButton;
}
