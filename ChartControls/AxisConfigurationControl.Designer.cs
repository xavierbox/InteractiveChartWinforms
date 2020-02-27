namespace ChartControls
{
  partial class AxisConfigurationControl
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
            this.reverseCheckbox = new System.Windows.Forms.CheckBox();
            this.titleBox = new System.Windows.Forms.TextBox();
            this.logaritmicCheck = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.arrowStyleCombobox = new System.Windows.Forms.ComboBox();
            this.gridCheckBox = new System.Windows.Forms.CheckBox();
            this.fontSelectorButton = new System.Windows.Forms.Button();
            this.zradioButton = new System.Windows.Forms.RadioButton();
            this.yradioButton = new System.Windows.Forms.RadioButton();
            this.xradioButton = new System.Windows.Forms.RadioButton();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // reverseCheckbox
            // 
            this.reverseCheckbox.AutoSize = true;
            this.reverseCheckbox.Location = new System.Drawing.Point(11, 73);
            this.reverseCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.reverseCheckbox.Name = "reverseCheckbox";
            this.reverseCheckbox.Size = new System.Drawing.Size(66, 17);
            this.reverseCheckbox.TabIndex = 72;
            this.reverseCheckbox.Text = "Reverse";
            this.reverseCheckbox.UseVisualStyleBackColor = true;
            // 
            // titleBox
            // 
            this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox.Location = new System.Drawing.Point(8, 43);
            this.titleBox.Margin = new System.Windows.Forms.Padding(2);
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(470, 20);
            this.titleBox.TabIndex = 67;
            // 
            // logaritmicCheck
            // 
            this.logaritmicCheck.AutoSize = true;
            this.logaritmicCheck.Location = new System.Drawing.Point(130, 73);
            this.logaritmicCheck.Margin = new System.Windows.Forms.Padding(2);
            this.logaritmicCheck.Name = "logaritmicCheck";
            this.logaritmicCheck.Size = new System.Drawing.Size(74, 17);
            this.logaritmicCheck.TabIndex = 84;
            this.logaritmicCheck.Text = "Logaritmic";
            this.logaritmicCheck.UseVisualStyleBackColor = true;
            this.logaritmicCheck.Visible = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.arrowStyleCombobox);
            this.groupBox4.Controls.Add(this.gridCheckBox);
            this.groupBox4.Controls.Add(this.fontSelectorButton);
            this.groupBox4.Controls.Add(this.titleBox);
            this.groupBox4.Controls.Add(this.zradioButton);
            this.groupBox4.Controls.Add(this.logaritmicCheck);
            this.groupBox4.Controls.Add(this.reverseCheckbox);
            this.groupBox4.Controls.Add(this.yradioButton);
            this.groupBox4.Controls.Add(this.xradioButton);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(511, 194);
            this.groupBox4.TabIndex = 87;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Axis";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 92;
            this.label1.Text = "Arrow style";
            // 
            // arrowStyleCombobox
            // 
            this.arrowStyleCombobox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.arrowStyleCombobox.FormattingEnabled = true;
            this.arrowStyleCombobox.Location = new System.Drawing.Point(8, 122);
            this.arrowStyleCombobox.Margin = new System.Windows.Forms.Padding(2);
            this.arrowStyleCombobox.Name = "arrowStyleCombobox";
            this.arrowStyleCombobox.Size = new System.Drawing.Size(496, 21);
            this.arrowStyleCombobox.TabIndex = 76;
            // 
            // gridCheckBox
            // 
            this.gridCheckBox.AutoSize = true;
            this.gridCheckBox.Location = new System.Drawing.Point(81, 73);
            this.gridCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.gridCheckBox.Name = "gridCheckBox";
            this.gridCheckBox.Size = new System.Drawing.Size(45, 17);
            this.gridCheckBox.TabIndex = 91;
            this.gridCheckBox.Text = "Grid";
            this.gridCheckBox.UseVisualStyleBackColor = true;
            // 
            // fontSelectorButton
            // 
            this.fontSelectorButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fontSelectorButton.BackColor = System.Drawing.Color.White;
            this.fontSelectorButton.Font = new System.Drawing.Font("Mistral", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontSelectorButton.Location = new System.Drawing.Point(482, 42);
            this.fontSelectorButton.Margin = new System.Windows.Forms.Padding(2);
            this.fontSelectorButton.Name = "fontSelectorButton";
            this.fontSelectorButton.Size = new System.Drawing.Size(22, 22);
            this.fontSelectorButton.TabIndex = 70;
            this.fontSelectorButton.Text = "F";
            this.fontSelectorButton.UseVisualStyleBackColor = false;
            // 
            // zradioButton
            // 
            this.zradioButton.AutoSize = true;
            this.zradioButton.Location = new System.Drawing.Point(95, 21);
            this.zradioButton.Name = "zradioButton";
            this.zradioButton.Size = new System.Drawing.Size(32, 17);
            this.zradioButton.TabIndex = 90;
            this.zradioButton.Text = "Z";
            this.zradioButton.UseVisualStyleBackColor = true;
            // 
            // yradioButton
            // 
            this.yradioButton.AutoSize = true;
            this.yradioButton.Location = new System.Drawing.Point(57, 21);
            this.yradioButton.Name = "yradioButton";
            this.yradioButton.Size = new System.Drawing.Size(32, 17);
            this.yradioButton.TabIndex = 89;
            this.yradioButton.Text = "Y";
            this.yradioButton.UseVisualStyleBackColor = true;
            // 
            // xradioButton
            // 
            this.xradioButton.AutoSize = true;
            this.xradioButton.Checked = true;
            this.xradioButton.Location = new System.Drawing.Point(10, 21);
            this.xradioButton.Name = "xradioButton";
            this.xradioButton.Size = new System.Drawing.Size(32, 17);
            this.xradioButton.TabIndex = 88;
            this.xradioButton.TabStop = true;
            this.xradioButton.Text = "X";
            this.xradioButton.UseVisualStyleBackColor = true;
            // 
            // AxisConfigurationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox4);
            this.Name = "AxisConfigurationControl";
            this.Size = new System.Drawing.Size(519, 200);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.CheckBox reverseCheckbox;
    private System.Windows.Forms.TextBox titleBox;
    private System.Windows.Forms.CheckBox logaritmicCheck;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Button fontSelectorButton;
        private System.Windows.Forms.ComboBox arrowStyleCombobox;
        private System.Windows.Forms.RadioButton zradioButton;
        private System.Windows.Forms.RadioButton yradioButton;
        private System.Windows.Forms.RadioButton xradioButton;
        private System.Windows.Forms.CheckBox gridCheckBox;
        private System.Windows.Forms.Label label1;
    }
}
