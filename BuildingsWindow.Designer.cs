// The designer file for the BuildingsWindow.
namespace MonopolyClient
{
    partial class BuildingsWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.propertiesBox = new System.Windows.Forms.ListBox();
            this.buyButton = new System.Windows.Forms.Button();
            this.sellButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // propertiesBox
            // 
            this.propertiesBox.FormattingEnabled = true;
            this.propertiesBox.Location = new System.Drawing.Point(12, 12);
            this.propertiesBox.Name = "propertiesBox";
            this.propertiesBox.Size = new System.Drawing.Size(129, 108);
            this.propertiesBox.TabIndex = 0;
            this.propertiesBox.SelectedIndexChanged += new System.EventHandler(this.propertiesBox_SelectedIndexChanged);
            // 
            // buyButton
            // 
            this.buyButton.Enabled = false;
            this.buyButton.Location = new System.Drawing.Point(168, 33);
            this.buyButton.Name = "buyButton";
            this.buyButton.Size = new System.Drawing.Size(50, 23);
            this.buyButton.TabIndex = 1;
            this.buyButton.Text = "Buy";
            this.buyButton.UseVisualStyleBackColor = true;
            this.buyButton.Click += new System.EventHandler(this.buyButton_Click);
            // 
            // sellButton
            // 
            this.sellButton.Enabled = false;
            this.sellButton.Location = new System.Drawing.Point(168, 72);
            this.sellButton.Name = "sellButton";
            this.sellButton.Size = new System.Drawing.Size(50, 23);
            this.sellButton.TabIndex = 2;
            this.sellButton.Text = "Sell";
            this.sellButton.UseVisualStyleBackColor = true;
            this.sellButton.Click += new System.EventHandler(this.sellButton_Click);
            // 
            // BuildingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(247, 137);
            this.Controls.Add(this.sellButton);
            this.Controls.Add(this.buyButton);
            this.Controls.Add(this.propertiesBox);
            this.Name = "BuildingsWindow";
            this.Text = "Buy or Sell Buildings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox propertiesBox;
        private System.Windows.Forms.Button buyButton;
        private System.Windows.Forms.Button sellButton;
    }
}