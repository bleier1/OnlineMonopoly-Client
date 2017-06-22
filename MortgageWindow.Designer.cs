// The designer file of the MortgageWindow window.
namespace MonopolyClient
{
    partial class MortgageWindow
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
            this.unmortgagedProperties = new System.Windows.Forms.ListBox();
            this.mortgagedProperties = new System.Windows.Forms.ListBox();
            this.mortgageButton = new System.Windows.Forms.Button();
            this.unmortgageButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // unmortgagedProperties
            // 
            this.unmortgagedProperties.FormattingEnabled = true;
            this.unmortgagedProperties.Location = new System.Drawing.Point(12, 12);
            this.unmortgagedProperties.Name = "unmortgagedProperties";
            this.unmortgagedProperties.Size = new System.Drawing.Size(132, 121);
            this.unmortgagedProperties.TabIndex = 0;
            // 
            // mortgagedProperties
            // 
            this.mortgagedProperties.FormattingEnabled = true;
            this.mortgagedProperties.Location = new System.Drawing.Point(176, 12);
            this.mortgagedProperties.Name = "mortgagedProperties";
            this.mortgagedProperties.Size = new System.Drawing.Size(132, 121);
            this.mortgagedProperties.TabIndex = 1;
            // 
            // mortgageButton
            // 
            this.mortgageButton.Location = new System.Drawing.Point(40, 144);
            this.mortgageButton.Name = "mortgageButton";
            this.mortgageButton.Size = new System.Drawing.Size(75, 23);
            this.mortgageButton.TabIndex = 2;
            this.mortgageButton.Text = "Mortgage";
            this.mortgageButton.UseVisualStyleBackColor = true;
            this.mortgageButton.Click += new System.EventHandler(this.mortgageButton_Click);
            // 
            // unmortgageButton
            // 
            this.unmortgageButton.Location = new System.Drawing.Point(205, 144);
            this.unmortgageButton.Name = "unmortgageButton";
            this.unmortgageButton.Size = new System.Drawing.Size(75, 23);
            this.unmortgageButton.TabIndex = 3;
            this.unmortgageButton.Text = "Unmortgage";
            this.unmortgageButton.UseVisualStyleBackColor = true;
            this.unmortgageButton.Click += new System.EventHandler(this.unmortgageButton_Click);
            // 
            // MortgageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 179);
            this.Controls.Add(this.unmortgageButton);
            this.Controls.Add(this.mortgageButton);
            this.Controls.Add(this.mortgagedProperties);
            this.Controls.Add(this.unmortgagedProperties);
            this.Name = "MortgageWindow";
            this.Text = "Property Mortgaging";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox unmortgagedProperties;
        private System.Windows.Forms.ListBox mortgagedProperties;
        private System.Windows.Forms.Button mortgageButton;
        private System.Windows.Forms.Button unmortgageButton;
    }
}