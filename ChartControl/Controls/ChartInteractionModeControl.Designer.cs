namespace ChartControl
{
  partial class ChartInteractionModeControl
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
 this.components = new System.ComponentModel.Container();
 System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartInteractionModeControl));
 this.groupBox1 = new System.Windows.Forms.GroupBox();
 this.clearButton = new System.Windows.Forms.Button();
 this.imageList1 = new System.Windows.Forms.ImageList(this.components);
 this.zoomButton = new System.Windows.Forms.Button();
 this.editButton = new System.Windows.Forms.Button();
 this.groupBox1.SuspendLayout();
 this.SuspendLayout();
 // 
 // groupBox1
 // 
 this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
  | System.Windows.Forms.AnchorStyles.Right)));
 this.groupBox1.Controls.Add(this.clearButton);
 this.groupBox1.Controls.Add(this.zoomButton);
 this.groupBox1.Controls.Add(this.editButton);
 this.groupBox1.Location = new System.Drawing.Point(0, 0);
 this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
 this.groupBox1.MaximumSize = new System.Drawing.Size(80, 40);
 this.groupBox1.MinimumSize = new System.Drawing.Size(0, 40);
 this.groupBox1.Name = "groupBox1";
 this.groupBox1.Size = new System.Drawing.Size(80, 40);
 this.groupBox1.TabIndex = 5;
 this.groupBox1.TabStop = false;
 this.groupBox1.Text = " ";
 // 
 // clearButton
 // 
 this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
  | System.Windows.Forms.AnchorStyles.Right)));
 this.clearButton.ImageKey = "error.png";
 this.clearButton.ImageList = this.imageList1;
 this.clearButton.Location = new System.Drawing.Point(52, 13);
 this.clearButton.Name = "clearButton";
 this.clearButton.Size = new System.Drawing.Size(20, 20);
 this.clearButton.TabIndex = 2;
 this.clearButton.Text = " ";
 this.clearButton.UseVisualStyleBackColor = true;
 // 
 // imageList1
 // 
 this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
 this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
 this.imageList1.Images.SetKeyName(0, "1462_Pencil_48x48.png");
 this.imageList1.Images.SetKeyName(1, "base_charts.png");
 this.imageList1.Images.SetKeyName(2, "Function_small.png");
 this.imageList1.Images.SetKeyName(3, "Notepad_32x32.png");
 this.imageList1.Images.SetKeyName(4, "search_zoom.png");
 this.imageList1.Images.SetKeyName(5, "error.png");
 this.imageList1.Images.SetKeyName(6, "batfile.png");
 // 
 // zoomButton
 // 
 this.zoomButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
  | System.Windows.Forms.AnchorStyles.Right)));
 this.zoomButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
 this.zoomButton.ImageKey = "search_zoom.png";
 this.zoomButton.ImageList = this.imageList1;
 this.zoomButton.Location = new System.Drawing.Point(8, 13);
 this.zoomButton.Name = "zoomButton";
 this.zoomButton.Size = new System.Drawing.Size(20, 20);
 this.zoomButton.TabIndex = 0;
 this.zoomButton.Text = " ";
 this.zoomButton.UseVisualStyleBackColor = true;
 // 
 // editButton
 // 
 this.editButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
  | System.Windows.Forms.AnchorStyles.Right)));
 this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
 this.editButton.ImageKey = "1462_Pencil_48x48.png";
 this.editButton.ImageList = this.imageList1;
 this.editButton.Location = new System.Drawing.Point(30, 13);
 this.editButton.Name = "editButton";
 this.editButton.Size = new System.Drawing.Size(20, 20);
 this.editButton.TabIndex = 1;
 this.editButton.Text = " ";
 this.editButton.UseVisualStyleBackColor = true;
 // 
 // ChartInteractionModeControl
 // 
 this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 this.Controls.Add(this.groupBox1);
 this.Name = "ChartInteractionModeControl";
 this.Size = new System.Drawing.Size(78, 45);
 this.groupBox1.ResumeLayout(false);
 this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button clearButton;
    private System.Windows.Forms.Button zoomButton;
    private System.Windows.Forms.Button editButton;
    private System.Windows.Forms.ImageList imageList1;
  }
}
