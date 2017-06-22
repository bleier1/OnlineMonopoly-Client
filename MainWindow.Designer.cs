// The designer file of the MainWindow window.
using System.Windows.Forms;

namespace MonopolyClient
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.chatLog = new System.Windows.Forms.TextBox();
            this.chatInput = new System.Windows.Forms.TextBox();
            this.rollButton = new System.Windows.Forms.Button();
            this.firstDieBox = new System.Windows.Forms.TextBox();
            this.secondDieBox = new System.Windows.Forms.TextBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.positionLabel = new System.Windows.Forms.Label();
            this.fundsLabel = new System.Windows.Forms.Label();
            this.propertiesButton = new System.Windows.Forms.Button();
            this.mortgageButton = new System.Windows.Forms.Button();
            this.tradeButton = new System.Windows.Forms.Button();
            this.buildingsButton = new System.Windows.Forms.Button();
            this.bankruptcyButton = new System.Windows.Forms.Button();
            this.boardPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.boardPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // chatLog
            // 
            this.chatLog.BackColor = System.Drawing.SystemColors.Window;
            this.chatLog.Location = new System.Drawing.Point(13, 13);
            this.chatLog.Multiline = true;
            this.chatLog.Name = "chatLog";
            this.chatLog.ReadOnly = true;
            this.chatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.chatLog.Size = new System.Drawing.Size(334, 289);
            this.chatLog.TabIndex = 0;
            // 
            // chatInput
            // 
            this.chatInput.Location = new System.Drawing.Point(13, 299);
            this.chatInput.Name = "chatInput";
            this.chatInput.Size = new System.Drawing.Size(334, 20);
            this.chatInput.TabIndex = 1;
            this.chatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chatInput_KeyDown);
            // 
            // rollButton
            // 
            this.rollButton.Enabled = false;
            this.rollButton.Location = new System.Drawing.Point(13, 325);
            this.rollButton.Name = "rollButton";
            this.rollButton.Size = new System.Drawing.Size(55, 23);
            this.rollButton.TabIndex = 2;
            this.rollButton.Text = "Roll";
            this.rollButton.UseVisualStyleBackColor = true;
            this.rollButton.Click += new System.EventHandler(this.rollButton_Click);
            // 
            // firstDieBox
            // 
            this.firstDieBox.Location = new System.Drawing.Point(370, 13);
            this.firstDieBox.Name = "firstDieBox";
            this.firstDieBox.ReadOnly = true;
            this.firstDieBox.Size = new System.Drawing.Size(31, 20);
            this.firstDieBox.TabIndex = 3;
            // 
            // secondDieBox
            // 
            this.secondDieBox.Location = new System.Drawing.Point(407, 13);
            this.secondDieBox.Name = "secondDieBox";
            this.secondDieBox.ReadOnly = true;
            this.secondDieBox.Size = new System.Drawing.Size(31, 20);
            this.secondDieBox.TabIndex = 4;
            // 
            // doneButton
            // 
            this.doneButton.Enabled = false;
            this.doneButton.Location = new System.Drawing.Point(13, 354);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(55, 23);
            this.doneButton.TabIndex = 5;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(259, 425);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(70, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start Game";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new System.Drawing.Point(441, 344);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Size = new System.Drawing.Size(27, 104);
            this.positionLabel.TabIndex = 7;
            // 
            // fundsLabel
            // 
            this.fundsLabel.AutoSize = true;
            this.fundsLabel.Location = new System.Drawing.Point(12, 435);
            this.fundsLabel.Name = "fundsLabel";
            this.fundsLabel.Size = new System.Drawing.Size(72, 13);
            this.fundsLabel.TabIndex = 8;
            this.fundsLabel.Text = "Funds: $1500";
            // 
            // propertiesButton
            // 
            this.propertiesButton.Enabled = false;
            this.propertiesButton.Location = new System.Drawing.Point(74, 325);
            this.propertiesButton.Name = "propertiesButton";
            this.propertiesButton.Size = new System.Drawing.Size(73, 23);
            this.propertiesButton.TabIndex = 9;
            this.propertiesButton.Text = "Properties";
            this.propertiesButton.UseVisualStyleBackColor = true;
            this.propertiesButton.Click += new System.EventHandler(this.propertiesButton_Click);
            // 
            // mortgageButton
            // 
            this.mortgageButton.Enabled = false;
            this.mortgageButton.Location = new System.Drawing.Point(153, 325);
            this.mortgageButton.Name = "mortgageButton";
            this.mortgageButton.Size = new System.Drawing.Size(60, 23);
            this.mortgageButton.TabIndex = 10;
            this.mortgageButton.Text = "Mortgage";
            this.mortgageButton.UseVisualStyleBackColor = true;
            this.mortgageButton.Click += new System.EventHandler(this.mortgageButton_Click);
            // 
            // tradeButton
            // 
            this.tradeButton.Enabled = false;
            this.tradeButton.Location = new System.Drawing.Point(219, 325);
            this.tradeButton.Name = "tradeButton";
            this.tradeButton.Size = new System.Drawing.Size(46, 23);
            this.tradeButton.TabIndex = 11;
            this.tradeButton.Text = "Trade";
            this.tradeButton.UseVisualStyleBackColor = true;
            this.tradeButton.Click += new System.EventHandler(this.tradeButton_Click);
            // 
            // buildingsButton
            // 
            this.buildingsButton.Enabled = false;
            this.buildingsButton.Location = new System.Drawing.Point(271, 325);
            this.buildingsButton.Name = "buildingsButton";
            this.buildingsButton.Size = new System.Drawing.Size(58, 23);
            this.buildingsButton.TabIndex = 12;
            this.buildingsButton.Text = "Buildings";
            this.buildingsButton.UseVisualStyleBackColor = true;
            this.buildingsButton.Click += new System.EventHandler(this.buildingsButton_Click);
            // 
            // bankruptcyButton
            // 
            this.bankruptcyButton.Enabled = false;
            this.bankruptcyButton.Location = new System.Drawing.Point(13, 383);
            this.bankruptcyButton.Name = "bankruptcyButton";
            this.bankruptcyButton.Size = new System.Drawing.Size(71, 23);
            this.bankruptcyButton.TabIndex = 13;
            this.bankruptcyButton.Text = "Bankruptcy";
            this.bankruptcyButton.UseVisualStyleBackColor = true;
            this.bankruptcyButton.Click += new System.EventHandler(this.bankruptcyButton_Click);
            // 
            // boardPicture
            // 
            this.boardPicture.Image = ((System.Drawing.Image)(resources.GetObject("boardPicture.Image")));
            this.boardPicture.Location = new System.Drawing.Point(444, 13);
            this.boardPicture.Name = "boardPicture";
            this.boardPicture.Size = new System.Drawing.Size(392, 319);
            this.boardPicture.TabIndex = 14;
            this.boardPicture.TabStop = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 472);
            this.Controls.Add(this.boardPicture);
            this.Controls.Add(this.bankruptcyButton);
            this.Controls.Add(this.buildingsButton);
            this.Controls.Add(this.tradeButton);
            this.Controls.Add(this.mortgageButton);
            this.Controls.Add(this.propertiesButton);
            this.Controls.Add(this.fundsLabel);
            this.Controls.Add(this.positionLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.secondDieBox);
            this.Controls.Add(this.firstDieBox);
            this.Controls.Add(this.rollButton);
            this.Controls.Add(this.chatInput);
            this.Controls.Add(this.chatLog);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "Monopoly";
            ((System.ComponentModel.ISupportInitialize)(this.boardPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatLog;
        private System.Windows.Forms.TextBox chatInput;
        private Button rollButton;
        private TextBox firstDieBox;
        private TextBox secondDieBox;
        private Button doneButton;
        private Button startButton;
        private Label positionLabel;
        private Label fundsLabel;
        private Button propertiesButton;
        private Button mortgageButton;
        private Button tradeButton;
        private Button buildingsButton;
        private Button bankruptcyButton;
        private PictureBox boardPicture;
    }
}