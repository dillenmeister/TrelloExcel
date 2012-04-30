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
			this.AuthorizeButton = this.Factory.CreateRibbonButton();
			this.ExportCardsButton = this.Factory.CreateRibbonButton();
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
			this.group1.Items.Add(this.AuthorizeButton);
			this.group1.Items.Add(this.ExportCardsButton);
			this.group1.Label = "Trello";
			this.group1.Name = "group1";
			// 
			// AuthorizeButton
			// 
			this.AuthorizeButton.Image = global::TrelloExcelAddIn.Properties.Resources._157_GetPermission_48x48_72;
			this.AuthorizeButton.Label = "Login";
			this.AuthorizeButton.Name = "AuthorizeButton";
			this.AuthorizeButton.ShowImage = true;
			this.AuthorizeButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AuthorizeButton_Click);
			// 
			// ExportCardsButton
			// 
			this.ExportCardsButton.Enabled = false;
			this.ExportCardsButton.Image = global::TrelloExcelAddIn.Properties.Resources.s40;
			this.ExportCardsButton.Label = "Export cards";
			this.ExportCardsButton.Name = "ExportCardsButton";
			this.ExportCardsButton.ShowImage = true;
			this.ExportCardsButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddToTrelloButton_Click);
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
		internal Microsoft.Office.Tools.Ribbon.RibbonButton AuthorizeButton;
	}

	partial class ThisRibbonCollection
	{
		internal TrelloRibbon TrelloRibbon
		{
			get { return this.GetRibbon<TrelloRibbon>(); }
		}
	}
}
