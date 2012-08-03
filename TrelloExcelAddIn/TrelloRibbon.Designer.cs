namespace TrelloExcelAddIn
{
	partial class TrelloRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public TrelloRibbon()
			: base(Globals.Factory.GetRibbonFactory())
		{
			InitializeComponent();
		}

		/// <summary> 
		/// Clean up any resources being used.
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.LoginSplitButton = this.Factory.CreateRibbonSplitButton();
            this.LoginExpireOneHourButton = this.Factory.CreateRibbonButton();
            this.LoginExpireOneDayButton = this.Factory.CreateRibbonButton();
            this.LoginExpire30DaysButton = this.Factory.CreateRibbonButton();
            this.LoginNeverExpireButton = this.Factory.CreateRibbonButton();
            this.LoggedInButton = this.Factory.CreateRibbonButton();
            this.ExportCardsButton = this.Factory.CreateRibbonButton();
            this.ImportCardsButton = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.LoginSplitButton);
            this.group1.Items.Add(this.LoggedInButton);
            this.group1.Items.Add(this.ExportCardsButton);
            this.group1.Items.Add(this.ImportCardsButton);
            this.group1.Label = "Trello";
            this.group1.Name = "group1";
            // 
            // LoginSplitButton
            // 
            this.LoginSplitButton.Image = global::TrelloExcelAddIn.Properties.Resources.s40;
            this.LoginSplitButton.Items.Add(this.LoginExpireOneHourButton);
            this.LoginSplitButton.Items.Add(this.LoginExpireOneDayButton);
            this.LoginSplitButton.Items.Add(this.LoginExpire30DaysButton);
            this.LoginSplitButton.Items.Add(this.LoginNeverExpireButton);
            this.LoginSplitButton.Label = "Login";
            this.LoginSplitButton.Name = "LoginSplitButton";
            this.LoginSplitButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoginButton_Click);
            // 
            // LoginExpireOneHourButton
            // 
            this.LoginExpireOneHourButton.Label = "Expire in one hour";
            this.LoginExpireOneHourButton.Name = "LoginExpireOneHourButton";
            this.LoginExpireOneHourButton.ShowImage = true;
            this.LoginExpireOneHourButton.Tag = "";
            this.LoginExpireOneHourButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoginButton_Click);
            // 
            // LoginExpireOneDayButton
            // 
            this.LoginExpireOneDayButton.Label = "Expire in one day";
            this.LoginExpireOneDayButton.Name = "LoginExpireOneDayButton";
            this.LoginExpireOneDayButton.ShowImage = true;
            this.LoginExpireOneDayButton.Tag = "";
            this.LoginExpireOneDayButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoginButton_Click);
            // 
            // LoginExpire30DaysButton
            // 
            this.LoginExpire30DaysButton.Label = "Expire in 30 days";
            this.LoginExpire30DaysButton.Name = "LoginExpire30DaysButton";
            this.LoginExpire30DaysButton.ShowImage = true;
            this.LoginExpire30DaysButton.Tag = "";
            this.LoginExpire30DaysButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoginButton_Click);
            // 
            // LoginNeverExpireButton
            // 
            this.LoginNeverExpireButton.Label = "Never expire";
            this.LoginNeverExpireButton.Name = "LoginNeverExpireButton";
            this.LoginNeverExpireButton.ShowImage = true;
            this.LoginNeverExpireButton.Tag = "";
            this.LoginNeverExpireButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.LoginButton_Click);
            // 
            // LoggedInButton
            // 
            this.LoggedInButton.Enabled = false;
            this.LoggedInButton.Image = global::TrelloExcelAddIn.Properties.Resources.s40;
            this.LoggedInButton.Label = "Not logged in";
            this.LoggedInButton.Name = "LoggedInButton";
            this.LoggedInButton.ShowImage = true;
            this.LoggedInButton.Visible = false;
            // 
            // ExportCardsButton
            // 
            this.ExportCardsButton.Enabled = false;
            this.ExportCardsButton.Label = "Export cards";
            this.ExportCardsButton.Name = "ExportCardsButton";
            this.ExportCardsButton.ShowImage = true;
            this.ExportCardsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddToTrelloButton_Click);
            // 
            // ImportCardsButton
            // 
            this.ImportCardsButton.Enabled = false;
            this.ImportCardsButton.Label = "Import cards";
            this.ImportCardsButton.Name = "ImportCardsButton";
            this.ImportCardsButton.ShowImage = true;
            this.ImportCardsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ImportCardsButton_Click);
            // 
            // TrelloRibbon
            // 
            this.Name = "TrelloRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.TrelloRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

		}

		#endregion

		internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
		internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton ExportCardsButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonSplitButton LoginSplitButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton LoginExpireOneHourButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton LoginExpireOneDayButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton LoginExpire30DaysButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton LoginNeverExpireButton;
		internal Microsoft.Office.Tools.Ribbon.RibbonButton LoggedInButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ImportCardsButton;
	}

	partial class ThisRibbonCollection
	{
		internal TrelloRibbon TrelloRibbon
		{
			get { return this.GetRibbon<TrelloRibbon>(); }
		}
	}
}
