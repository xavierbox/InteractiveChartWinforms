namespace ChartControls
{
  partial class BackgroundColorSelector
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.backgroundSelected = new System.Windows.Forms.ComboBox();
            this.secondaryColorSelector = new System.Windows.Forms.Button();
            this.gradientComboBox = new System.Windows.Forms.ComboBox();
            this.primaryColorSelector = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.backgroundSelected);
            this.groupBox4.Controls.Add(this.secondaryColorSelector);
            this.groupBox4.Controls.Add(this.gradientComboBox);
            this.groupBox4.Controls.Add(this.primaryColorSelector);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(470, 99);
            this.groupBox4.TabIndex = 88;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Background";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(170, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 138;
            this.label2.Text = "Gradient";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 137;
            this.label1.Text = "Primary color";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Mistral", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(444, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(22, 22);
            this.button1.TabIndex = 136;
            this.button1.Text = "C";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // backgroundSelected
            // 
            this.backgroundSelected.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.backgroundSelected.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.backgroundSelected.FormattingEnabled = true;
            this.backgroundSelected.Items.AddRange(new object[] {
            "Main area",
            "Back area"});
            this.backgroundSelected.Location = new System.Drawing.Point(6, 19);
            this.backgroundSelected.Name = "backgroundSelected";
            this.backgroundSelected.Size = new System.Drawing.Size(431, 21);
            this.backgroundSelected.TabIndex = 135;
            // 
            // secondaryColorSelector
            // 
            this.secondaryColorSelector.BackColor = System.Drawing.Color.White;
            this.secondaryColorSelector.Font = new System.Drawing.Font("Mistral", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondaryColorSelector.Location = new System.Drawing.Point(224, 44);
            this.secondaryColorSelector.Margin = new System.Windows.Forms.Padding(2);
            this.secondaryColorSelector.Name = "secondaryColorSelector";
            this.secondaryColorSelector.Size = new System.Drawing.Size(72, 22);
            this.secondaryColorSelector.TabIndex = 134;
            this.secondaryColorSelector.Text = "C";
            this.secondaryColorSelector.UseVisualStyleBackColor = false;
            // 
            // gradientComboBox
            // 
            this.gradientComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gradientComboBox.FormattingEnabled = true;
            this.gradientComboBox.Location = new System.Drawing.Point(302, 45);
            this.gradientComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.gradientComboBox.Name = "gradientComboBox";
            this.gradientComboBox.Size = new System.Drawing.Size(135, 21);
            this.gradientComboBox.TabIndex = 132;
            // 
            // primaryColorSelector
            // 
            this.primaryColorSelector.BackColor = System.Drawing.Color.White;
            this.primaryColorSelector.Font = new System.Drawing.Font("Mistral", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primaryColorSelector.Location = new System.Drawing.Point(80, 44);
            this.primaryColorSelector.Margin = new System.Windows.Forms.Padding(2);
            this.primaryColorSelector.Name = "primaryColorSelector";
            this.primaryColorSelector.Size = new System.Drawing.Size(72, 22);
            this.primaryColorSelector.TabIndex = 133;
            this.primaryColorSelector.Text = "C";
            this.primaryColorSelector.UseVisualStyleBackColor = false;
            // 
            // BackgroundColorSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Name = "BackgroundColorSelector";
            this.Size = new System.Drawing.Size(476, 108);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button secondaryColorSelector;
    private System.Windows.Forms.ComboBox gradientComboBox;
    private System.Windows.Forms.Button primaryColorSelector;
    private System.Windows.Forms.ComboBox backgroundSelected;
    private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}
