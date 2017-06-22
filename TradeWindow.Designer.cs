// The designer file for the TradeWindow window.
namespace MonopolyClient
{
    partial class TradeWindow
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
            this.tradeWithLabel = new System.Windows.Forms.Label();
            this.namesBox = new System.Windows.Forms.ComboBox();
            this.yoursLabel = new System.Windows.Forms.Label();
            this.theirsLabel = new System.Windows.Forms.Label();
            this.firstMoneyLabel = new System.Windows.Forms.Label();
            this.secondMoneyLabel = new System.Windows.Forms.Label();
            this.yourMoneyBox = new System.Windows.Forms.TextBox();
            this.theirMoneyBox = new System.Windows.Forms.TextBox();
            this.offerButton = new System.Windows.Forms.Button();
            this.yourPropertiesBox = new System.Windows.Forms.ListBox();
            this.theirPropertiesBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // tradeWithLabel
            // 
            this.tradeWithLabel.AutoSize = true;
            this.tradeWithLabel.Location = new System.Drawing.Point(59, 9);
            this.tradeWithLabel.Name = "tradeWithLabel";
            this.tradeWithLabel.Size = new System.Drawing.Size(60, 13);
            this.tradeWithLabel.TabIndex = 0;
            this.tradeWithLabel.Text = "Trade with:";
            // 
            // namesBox
            // 
            this.namesBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.namesBox.FormattingEnabled = true;
            this.namesBox.Location = new System.Drawing.Point(125, 6);
            this.namesBox.Name = "namesBox";
            this.namesBox.Size = new System.Drawing.Size(88, 21);
            this.namesBox.TabIndex = 1;
            this.namesBox.SelectedIndexChanged += new System.EventHandler(this.namesBox_SelectedIndexChanged);
            // 
            // yoursLabel
            // 
            this.yoursLabel.AutoSize = true;
            this.yoursLabel.Location = new System.Drawing.Point(12, 52);
            this.yoursLabel.Name = "yoursLabel";
            this.yoursLabel.Size = new System.Drawing.Size(37, 13);
            this.yoursLabel.TabIndex = 2;
            this.yoursLabel.Text = "Yours:";
            // 
            // theirsLabel
            // 
            this.theirsLabel.AutoSize = true;
            this.theirsLabel.Location = new System.Drawing.Point(155, 52);
            this.theirsLabel.Name = "theirsLabel";
            this.theirsLabel.Size = new System.Drawing.Size(39, 13);
            this.theirsLabel.TabIndex = 3;
            this.theirsLabel.Text = "Theirs:";
            // 
            // firstMoneyLabel
            // 
            this.firstMoneyLabel.AutoSize = true;
            this.firstMoneyLabel.Location = new System.Drawing.Point(59, 52);
            this.firstMoneyLabel.Name = "firstMoneyLabel";
            this.firstMoneyLabel.Size = new System.Drawing.Size(13, 13);
            this.firstMoneyLabel.TabIndex = 4;
            this.firstMoneyLabel.Text = "$";
            // 
            // secondMoneyLabel
            // 
            this.secondMoneyLabel.AutoSize = true;
            this.secondMoneyLabel.Location = new System.Drawing.Point(215, 52);
            this.secondMoneyLabel.Name = "secondMoneyLabel";
            this.secondMoneyLabel.Size = new System.Drawing.Size(13, 13);
            this.secondMoneyLabel.TabIndex = 5;
            this.secondMoneyLabel.Text = "$";
            // 
            // yourMoneyBox
            // 
            this.yourMoneyBox.Location = new System.Drawing.Point(78, 49);
            this.yourMoneyBox.Name = "yourMoneyBox";
            this.yourMoneyBox.Size = new System.Drawing.Size(38, 20);
            this.yourMoneyBox.TabIndex = 6;
            // 
            // theirMoneyBox
            // 
            this.theirMoneyBox.Location = new System.Drawing.Point(234, 49);
            this.theirMoneyBox.Name = "theirMoneyBox";
            this.theirMoneyBox.Size = new System.Drawing.Size(38, 20);
            this.theirMoneyBox.TabIndex = 7;
            // 
            // offerButton
            // 
            this.offerButton.Location = new System.Drawing.Point(116, 187);
            this.offerButton.Name = "offerButton";
            this.offerButton.Size = new System.Drawing.Size(52, 23);
            this.offerButton.TabIndex = 8;
            this.offerButton.Text = "Offer";
            this.offerButton.UseVisualStyleBackColor = true;
            this.offerButton.Click += new System.EventHandler(this.offerButton_Click);
            // 
            // yourPropertiesBox
            // 
            this.yourPropertiesBox.FormattingEnabled = true;
            this.yourPropertiesBox.Location = new System.Drawing.Point(15, 75);
            this.yourPropertiesBox.Name = "yourPropertiesBox";
            this.yourPropertiesBox.Size = new System.Drawing.Size(120, 95);
            this.yourPropertiesBox.TabIndex = 9;
            // 
            // theirPropertiesBox
            // 
            this.theirPropertiesBox.FormattingEnabled = true;
            this.theirPropertiesBox.Location = new System.Drawing.Point(158, 75);
            this.theirPropertiesBox.Name = "theirPropertiesBox";
            this.theirPropertiesBox.Size = new System.Drawing.Size(120, 95);
            this.theirPropertiesBox.TabIndex = 10;
            // 
            // TradeWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 222);
            this.Controls.Add(this.theirPropertiesBox);
            this.Controls.Add(this.yourPropertiesBox);
            this.Controls.Add(this.offerButton);
            this.Controls.Add(this.theirMoneyBox);
            this.Controls.Add(this.yourMoneyBox);
            this.Controls.Add(this.secondMoneyLabel);
            this.Controls.Add(this.firstMoneyLabel);
            this.Controls.Add(this.theirsLabel);
            this.Controls.Add(this.yoursLabel);
            this.Controls.Add(this.namesBox);
            this.Controls.Add(this.tradeWithLabel);
            this.Name = "TradeWindow";
            this.Text = "Trade";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tradeWithLabel;
        private System.Windows.Forms.ComboBox namesBox;
        private System.Windows.Forms.Label yoursLabel;
        private System.Windows.Forms.Label theirsLabel;
        private System.Windows.Forms.Label firstMoneyLabel;
        private System.Windows.Forms.Label secondMoneyLabel;
        private System.Windows.Forms.TextBox yourMoneyBox;
        private System.Windows.Forms.TextBox theirMoneyBox;
        private System.Windows.Forms.Button offerButton;
        private System.Windows.Forms.ListBox yourPropertiesBox;
        private System.Windows.Forms.ListBox theirPropertiesBox;
    }
}