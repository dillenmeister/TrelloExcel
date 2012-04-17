namespace TrelloExcelAddIn
{
	partial class AddToTrelloControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.AuthenticationGroupBox = new System.Windows.Forms.GroupBox();
			this.AuthenticateButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.AuthenticationUrl = new System.Windows.Forms.LinkLabel();
			this.TokenTextBox = new System.Windows.Forms.TextBox();
			this.AddCardsGroupBox = new System.Windows.Forms.GroupBox();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.AddCardsButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ListComboBox = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.BoardComboBox = new System.Windows.Forms.ComboBox();
			this.AuthenticationGroupBox.SuspendLayout();
			this.AddCardsGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// AuthenticationGroupBox
			// 
			this.AuthenticationGroupBox.Controls.Add(this.AuthenticateButton);
			this.AuthenticationGroupBox.Controls.Add(this.label3);
			this.AuthenticationGroupBox.Controls.Add(this.AuthenticationUrl);
			this.AuthenticationGroupBox.Controls.Add(this.TokenTextBox);
			this.AuthenticationGroupBox.Location = new System.Drawing.Point(8, 3);
			this.AuthenticationGroupBox.Name = "AuthenticationGroupBox";
			this.AuthenticationGroupBox.Size = new System.Drawing.Size(256, 103);
			this.AuthenticationGroupBox.TabIndex = 10;
			this.AuthenticationGroupBox.TabStop = false;
			this.AuthenticationGroupBox.Text = "Authentication";
			// 
			// AuthenticateButton
			// 
			this.AuthenticateButton.Location = new System.Drawing.Point(162, 70);
			this.AuthenticateButton.Name = "AuthenticateButton";
			this.AuthenticateButton.Size = new System.Drawing.Size(75, 23);
			this.AuthenticateButton.TabIndex = 13;
			this.AuthenticateButton.Text = "Authenticate";
			this.AuthenticateButton.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(5, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(62, 13);
			this.label3.TabIndex = 12;
			this.label3.Text = "Enter token";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// AuthenticationUrl
			// 
			this.AuthenticationUrl.AutoSize = true;
			this.AuthenticationUrl.Location = new System.Drawing.Point(5, 23);
			this.AuthenticationUrl.Name = "AuthenticationUrl";
			this.AuthenticationUrl.Size = new System.Drawing.Size(232, 13);
			this.AuthenticationUrl.TabIndex = 11;
			this.AuthenticationUrl.TabStop = true;
			this.AuthenticationUrl.Text = "Click here to generate an authentication token  ";
			// 
			// TokenTextBox
			// 
			this.TokenTextBox.Location = new System.Drawing.Point(73, 44);
			this.TokenTextBox.Name = "TokenTextBox";
			this.TokenTextBox.Size = new System.Drawing.Size(164, 20);
			this.TokenTextBox.TabIndex = 10;
			// 
			// AddCardsGroupBox
			// 
			this.AddCardsGroupBox.Controls.Add(this.StatusLabel);
			this.AddCardsGroupBox.Controls.Add(this.AddCardsButton);
			this.AddCardsGroupBox.Controls.Add(this.label2);
			this.AddCardsGroupBox.Controls.Add(this.ListComboBox);
			this.AddCardsGroupBox.Controls.Add(this.label1);
			this.AddCardsGroupBox.Controls.Add(this.BoardComboBox);
			this.AddCardsGroupBox.Location = new System.Drawing.Point(8, 112);
			this.AddCardsGroupBox.Name = "AddCardsGroupBox";
			this.AddCardsGroupBox.Size = new System.Drawing.Size(256, 115);
			this.AddCardsGroupBox.TabIndex = 11;
			this.AddCardsGroupBox.TabStop = false;
			this.AddCardsGroupBox.Text = "Choose list";
			// 
			// StatusLabel
			// 
			this.StatusLabel.AutoSize = true;
			this.StatusLabel.Location = new System.Drawing.Point(5, 86);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(0, 13);
			this.StatusLabel.TabIndex = 13;
			// 
			// AddCardsButton
			// 
			this.AddCardsButton.Location = new System.Drawing.Point(162, 81);
			this.AddCardsButton.Name = "AddCardsButton";
			this.AddCardsButton.Size = new System.Drawing.Size(75, 23);
			this.AddCardsButton.TabIndex = 12;
			this.AddCardsButton.Text = "Add cards";
			this.AddCardsButton.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 57);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(23, 13);
			this.label2.TabIndex = 11;
			this.label2.Text = "List";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// ListComboBox
			// 
			this.ListComboBox.FormattingEnabled = true;
			this.ListComboBox.Location = new System.Drawing.Point(43, 54);
			this.ListComboBox.Name = "ListComboBox";
			this.ListComboBox.Size = new System.Drawing.Size(194, 21);
			this.ListComboBox.TabIndex = 10;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 9;
			this.label1.Text = "Board";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// BoardComboBox
			// 
			this.BoardComboBox.FormattingEnabled = true;
			this.BoardComboBox.Location = new System.Drawing.Point(43, 24);
			this.BoardComboBox.Name = "BoardComboBox";
			this.BoardComboBox.Size = new System.Drawing.Size(194, 21);
			this.BoardComboBox.TabIndex = 8;
			// 
			// AddToTrelloControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.AddCardsGroupBox);
			this.Controls.Add(this.AuthenticationGroupBox);
			this.Name = "AddToTrelloControl";
			this.Size = new System.Drawing.Size(273, 236);
			this.AuthenticationGroupBox.ResumeLayout(false);
			this.AuthenticationGroupBox.PerformLayout();
			this.AddCardsGroupBox.ResumeLayout(false);
			this.AddCardsGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox AuthenticationGroupBox;
		private System.Windows.Forms.Button AuthenticateButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel AuthenticationUrl;
		private System.Windows.Forms.TextBox TokenTextBox;
		private System.Windows.Forms.GroupBox AddCardsGroupBox;
		private System.Windows.Forms.Button AddCardsButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ListComboBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox BoardComboBox;
		private System.Windows.Forms.Label StatusLabel;

	}
}
