namespace ChartControls
{
  partial class TitlesControl
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.fontSelectorButton = new System.Windows.Forms.Button();
      this.secondaryFontSelector = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.groupBox1.Controls.Add(this.secondaryFontSelector);
      this.groupBox1.Controls.Add(this.textBox1);
      this.groupBox1.Controls.Add(this.textBox2);
      this.groupBox1.Controls.Add(this.fontSelectorButton);
      this.groupBox1.Location = new System.Drawing.Point(3, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(494, 80);
      this.groupBox1.TabIndex = 74;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Titles";
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(6, 19);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(456, 20);
      this.textBox1.TabIndex = 2;
      // 
      // textBox2
      // 
      this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox2.Location = new System.Drawing.Point(6, 45);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(456, 20);
      this.textBox2.TabIndex = 3;
      // 
      // fontSelectorButton
      // 
      this.fontSelectorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.fontSelectorButton.BackColor = System.Drawing.Color.White;
      this.fontSelectorButton.Font = new System.Drawing.Font("Mistral", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.fontSelectorButton.Location = new System.Drawing.Point(467, 17);
      this.fontSelectorButton.Margin = new System.Windows.Forms.Padding(2);
      this.fontSelectorButton.Name = "fontSelectorButton";
      this.fontSelectorButton.Size = new System.Drawing.Size(22, 22);
      this.fontSelectorButton.TabIndex = 71;
      this.fontSelectorButton.Text = "F";
      this.fontSelectorButton.UseVisualStyleBackColor = false;
      // 
      // secondaryFontSelector
      // 
      this.secondaryFontSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.secondaryFontSelector.BackColor = System.Drawing.Color.White;
      this.secondaryFontSelector.Font = new System.Drawing.Font("Mistral", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.secondaryFontSelector.Location = new System.Drawing.Point(467, 43);
      this.secondaryFontSelector.Margin = new System.Windows.Forms.Padding(2);
      this.secondaryFontSelector.Name = "secondaryFontSelector";
      this.secondaryFontSelector.Size = new System.Drawing.Size(22, 22);
      this.secondaryFontSelector.TabIndex = 72;
      this.secondaryFontSelector.Text = "F";
      this.secondaryFontSelector.UseVisualStyleBackColor = false;
      // 
      // TitlesControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.groupBox1);
      this.Name = "TitlesControl";
      this.Size = new System.Drawing.Size(500, 90);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.Button fontSelectorButton;
    private System.Windows.Forms.Button secondaryFontSelector;
  }
}
