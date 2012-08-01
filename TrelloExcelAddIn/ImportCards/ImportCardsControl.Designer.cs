namespace TrelloExcelAddIn
{
    partial class ImportCardsControl
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
            this.ChooseListsGroupBox = new System.Windows.Forms.GroupBox();
            this.ImportCardsButton = new System.Windows.Forms.Button();
            this.ListsLabel = new System.Windows.Forms.Label();
            this.ListsBox = new System.Windows.Forms.CheckedListBox();
            this.BoardsLabel = new System.Windows.Forms.Label();
            this.BoardComboBox = new System.Windows.Forms.ComboBox();
            this.ChooseListsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseListsGroupBox
            // 
            this.ChooseListsGroupBox.Controls.Add(this.ImportCardsButton);
            this.ChooseListsGroupBox.Controls.Add(this.ListsLabel);
            this.ChooseListsGroupBox.Controls.Add(this.ListsBox);
            this.ChooseListsGroupBox.Controls.Add(this.BoardsLabel);
            this.ChooseListsGroupBox.Controls.Add(this.BoardComboBox);
            this.ChooseListsGroupBox.Location = new System.Drawing.Point(8, 8);
            this.ChooseListsGroupBox.Name = "ChooseListsGroupBox";
            this.ChooseListsGroupBox.Size = new System.Drawing.Size(256, 206);
            this.ChooseListsGroupBox.TabIndex = 0;
            this.ChooseListsGroupBox.TabStop = false;
            this.ChooseListsGroupBox.Text = "Import cards";
            // 
            // ImportCardsButton
            // 
            this.ImportCardsButton.Enabled = false;
            this.ImportCardsButton.Location = new System.Drawing.Point(138, 178);
            this.ImportCardsButton.Name = "ImportCardsButton";
            this.ImportCardsButton.Size = new System.Drawing.Size(100, 22);
            this.ImportCardsButton.TabIndex = 14;
            this.ImportCardsButton.Text = "Import cards";
            this.ImportCardsButton.UseVisualStyleBackColor = true;
            // 
            // ListsLabel
            // 
            this.ListsLabel.AutoSize = true;
            this.ListsLabel.Location = new System.Drawing.Point(6, 49);
            this.ListsLabel.Name = "ListsLabel";
            this.ListsLabel.Size = new System.Drawing.Size(28, 13);
            this.ListsLabel.TabIndex = 13;
            this.ListsLabel.Text = "Lists";
            this.ListsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // ListsBox
            // 
            this.ListsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ListsBox.CheckOnClick = true;
            this.ListsBox.Enabled = false;
            this.ListsBox.FormattingEnabled = true;
            this.ListsBox.Location = new System.Drawing.Point(44, 49);
            this.ListsBox.Name = "ListsBox";
            this.ListsBox.Size = new System.Drawing.Size(194, 122);
            this.ListsBox.TabIndex = 12;
            // 
            // BoardsLabel
            // 
            this.BoardsLabel.AutoSize = true;
            this.BoardsLabel.Location = new System.Drawing.Point(6, 25);
            this.BoardsLabel.Name = "BoardsLabel";
            this.BoardsLabel.Size = new System.Drawing.Size(35, 13);
            this.BoardsLabel.TabIndex = 11;
            this.BoardsLabel.Text = "Board";
            this.BoardsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // BoardComboBox
            // 
            this.BoardComboBox.Enabled = false;
            this.BoardComboBox.FormattingEnabled = true;
            this.BoardComboBox.Location = new System.Drawing.Point(44, 22);
            this.BoardComboBox.Name = "BoardComboBox";
            this.BoardComboBox.Size = new System.Drawing.Size(194, 21);
            this.BoardComboBox.TabIndex = 10;
            // 
            // ImportCardsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChooseListsGroupBox);
            this.Name = "ImportCardsControl";
            this.Size = new System.Drawing.Size(273, 221);
            this.ChooseListsGroupBox.ResumeLayout(false);
            this.ChooseListsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ChooseListsGroupBox;
        private System.Windows.Forms.Label BoardsLabel;
        private System.Windows.Forms.ComboBox BoardComboBox;
        private System.Windows.Forms.Label ListsLabel;
        private System.Windows.Forms.CheckedListBox ListsBox;
        private System.Windows.Forms.Button ImportCardsButton;
    }
}
