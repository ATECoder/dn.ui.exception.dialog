using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

internal sealed partial class AdvancedInformationForm
{
    /// <summary>
    /// Disposes of the resources (other than memory) used by the <see cref="System.Windows.Forms.Form" />.
    /// </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="disposing">    <see langword="true" /> to release both managed and unmanaged
    ///                             resources; <see langword="false" /> to release only unmanaged
    ///                             resources. </param>
    protected override void Dispose( bool disposing )
    {
        if ( disposing )
            this.components?.Dispose();
        base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    private IContainer components;
    private Label TreeLabel;
    private Label DetailsTextBoxLabel;
    private ImageList ButtonsImageList;
    private TreeView Tree;
    private TextBox DetailsTextBox;
    private Button CloseButton;
    private ToolStrip ToolStrip1;
    private ToolStripButton CopyToolStripButton;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdvancedInformationForm));
            this.TreeLabel = new System.Windows.Forms.Label();
            this.Tree = new System.Windows.Forms.TreeView();
            this.DetailsTextBoxLabel = new System.Windows.Forms.Label();
            this.DetailsTextBox = new System.Windows.Forms.TextBox();
            this.ButtonsImageList = new System.Windows.Forms.ImageList(this.components);
            this.CloseButton = new System.Windows.Forms.Button();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.CopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TreeLabel
            // 
            resources.ApplyResources(this.TreeLabel, "TreeLabel");
            this.TreeLabel.Name = "TreeLabel";
            // 
            // Tree
            // 
            resources.ApplyResources(this.Tree, "Tree");
            this.Tree.Name = "Tree";
            this.Tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Tree_AfterSelect);
            // 
            // DetailsTextBoxLabel
            // 
            resources.ApplyResources(this.DetailsTextBoxLabel, "DetailsTextBoxLabel");
            this.DetailsTextBoxLabel.Name = "DetailsTextBoxLabel";
            // 
            // DetailsTextBox
            // 
            resources.ApplyResources(this.DetailsTextBox, "DetailsTextBox");
            this.DetailsTextBox.Name = "DetailsTextBox";
            this.DetailsTextBox.ReadOnly = true;
            // 
            // ButtonsImageList
            // 
            this.ButtonsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ButtonsImageList.ImageStream")));
            this.ButtonsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ButtonsImageList.Images.SetKeyName(0, "");
            this.ButtonsImageList.Images.SetKeyName(1, "SaveTextAsEmail.ico");
            this.ButtonsImageList.Images.SetKeyName(2, "");
            this.ButtonsImageList.Images.SetKeyName(3, "");
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Name = "CloseButton";
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.AllowMerge = false;
            resources.ApplyResources(this.ToolStrip1, "ToolStrip1");
            this.ToolStrip1.CanOverflow = false;
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CopyToolStripButton});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip1.TabStop = true;
            // 
            // CopyToolStripButton
            // 
            this.CopyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.CopyToolStripButton, "CopyToolStripButton");
            this.CopyToolStripButton.Name = "CopyToolStripButton";
            this.CopyToolStripButton.Click += new System.EventHandler(this.CopyToolStripButton_Click);
            // 
            // AdvancedInformationForm
            // 
            this.AcceptButton = this.CloseButton;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.CloseButton;
            this.Controls.Add(this.ToolStrip1);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.DetailsTextBox);
            this.Controls.Add(this.DetailsTextBoxLabel);
            this.Controls.Add(this.Tree);
            this.Controls.Add(this.TreeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedInformationForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
}
