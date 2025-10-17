using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

internal sealed partial class ExceptionMessageBoxForm
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

    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private TableLayoutPanel FormLayoutPanel;
    private Panel IconPanel;
    private TableLayoutPanel MessageLayoutPanel;
    private LinkLabel TopMessageLabel;
    private Label AdditionalInfoLabel;
    private TableLayoutPanel AdditionalLayoutPanel;
    private WrappingCheckBox DoNotShowCheckBox;
    private GroupBox SeparatorGroupBox;
    private ImageList IconsImageList;
    private Button Button1;
    private Button Button2;
    private Button Button3;
    private Button Button4;
    private Button Button5;
    private TableLayoutPanel ButtonsLayoutPanel;
    private ToolStrip ToolStrip1;
    private ToolStripDropDownButton HelpToolStripButton;
    private ToolStripButton CopyToolStripButton;
    private ToolStripButton AdvancedToolStripButton;
    private ToolStripButton HelpSingleToolStripButton;

    /// <summary>
    /// Required method for Designer support - do not modify the contents of this method with the
    /// code editor.
    /// </summary>
    /// <remarks>   2025-06-23. </remarks>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExceptionMessageBoxForm));
            this.FormLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.IconPanel = new System.Windows.Forms.Panel();
            this.MessageLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TopMessageLabel = new System.Windows.Forms.LinkLabel();
            this.AdditionalInfoLabel = new System.Windows.Forms.Label();
            this.AdditionalLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.DoNotShowCheckBox = new cc.isr.WinForms.Dialogs.WrappingCheckBox();
            this.SeparatorGroupBox = new System.Windows.Forms.GroupBox();
            this.ButtonsLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.HelpToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.HelpSingleToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.CopyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.AdvancedToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.Button1 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button5 = new System.Windows.Forms.Button();
            this.IconsImageList = new System.Windows.Forms.ImageList(this.components);
            this.FormLayoutPanel.SuspendLayout();
            this.MessageLayoutPanel.SuspendLayout();
            this.ButtonsLayoutPanel.SuspendLayout();
            this.ToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FormLayoutPanel
            // 
            resources.ApplyResources(this.FormLayoutPanel, "FormLayoutPanel");
            this.FormLayoutPanel.Controls.Add(this.IconPanel, 0, 0);
            this.FormLayoutPanel.Controls.Add(this.MessageLayoutPanel, 1, 0);
            this.FormLayoutPanel.Controls.Add(this.DoNotShowCheckBox, 1, 1);
            this.FormLayoutPanel.Controls.Add(this.SeparatorGroupBox, 0, 2);
            this.FormLayoutPanel.Controls.Add(this.ButtonsLayoutPanel, 0, 3);
            this.FormLayoutPanel.Name = "FormLayoutPanel";
            this.FormLayoutPanel.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // IconPanel
            // 
            resources.ApplyResources(this.IconPanel, "IconPanel");
            this.IconPanel.Name = "IconPanel";
            this.IconPanel.Click += new System.EventHandler(this.HideBorderLines);
            this.IconPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.IconPanel_Paint);
            // 
            // MessageLayoutPanel
            // 
            resources.ApplyResources(this.MessageLayoutPanel, "MessageLayoutPanel");
            this.MessageLayoutPanel.Controls.Add(this.TopMessageLabel, 0, 0);
            this.MessageLayoutPanel.Controls.Add(this.AdditionalInfoLabel, 0, 1);
            this.MessageLayoutPanel.Controls.Add(this.AdditionalLayoutPanel, 0, 2);
            this.MessageLayoutPanel.Name = "MessageLayoutPanel";
            this.MessageLayoutPanel.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // TopMessageLabel
            // 
            resources.ApplyResources(this.TopMessageLabel, "TopMessageLabel");
            this.TopMessageLabel.Name = "TopMessageLabel";
            this.TopMessageLabel.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // AdditionalInfoLabel
            // 
            resources.ApplyResources(this.AdditionalInfoLabel, "AdditionalInfoLabel");
            this.AdditionalInfoLabel.Name = "AdditionalInfoLabel";
            this.AdditionalInfoLabel.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // AdditionalLayoutPanel
            // 
            resources.ApplyResources(this.AdditionalLayoutPanel, "AdditionalLayoutPanel");
            this.AdditionalLayoutPanel.Name = "AdditionalLayoutPanel";
            this.AdditionalLayoutPanel.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // DoNotShowCheckBox
            // 
            resources.ApplyResources(this.DoNotShowCheckBox, "DoNotShowCheckBox");
            this.DoNotShowCheckBox.Name = "DoNotShowCheckBox";
            this.DoNotShowCheckBox.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // SeparatorGroupBox
            // 
            resources.ApplyResources(this.SeparatorGroupBox, "SeparatorGroupBox");
            this.FormLayoutPanel.SetColumnSpan(this.SeparatorGroupBox, 2);
            this.SeparatorGroupBox.Name = "SeparatorGroupBox";
            this.SeparatorGroupBox.TabStop = false;
            // 
            // ButtonsLayoutPanel
            // 
            resources.ApplyResources(this.ButtonsLayoutPanel, "ButtonsLayoutPanel");
            this.FormLayoutPanel.SetColumnSpan(this.ButtonsLayoutPanel, 2);
            this.ButtonsLayoutPanel.Controls.Add(this.ToolStrip1, 0, 0);
            this.ButtonsLayoutPanel.Controls.Add(this.Button1, 1, 0);
            this.ButtonsLayoutPanel.Controls.Add(this.Button2, 2, 0);
            this.ButtonsLayoutPanel.Controls.Add(this.Button3, 3, 0);
            this.ButtonsLayoutPanel.Controls.Add(this.Button4, 4, 0);
            this.ButtonsLayoutPanel.Controls.Add(this.Button5, 5, 0);
            this.ButtonsLayoutPanel.Name = "ButtonsLayoutPanel";
            this.ButtonsLayoutPanel.Click += new System.EventHandler(this.HideBorderLines);
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.AllowMerge = false;
            this.ToolStrip1.CanOverflow = false;
            resources.ApplyResources(this.ToolStrip1, "ToolStrip1");
            this.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpToolStripButton,
            this.HelpSingleToolStripButton,
            this.CopyToolStripButton,
            this.AdvancedToolStripButton});
            this.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip1.TabStop = true;
            // 
            // HelpToolStripButton
            // 
            resources.ApplyResources(this.HelpToolStripButton, "HelpToolStripButton");
            this.HelpToolStripButton.Name = "HelpToolStripButton";
            this.HelpToolStripButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // HelpSingleToolStripButton
            // 
            resources.ApplyResources(this.HelpSingleToolStripButton, "HelpSingleToolStripButton");
            this.HelpSingleToolStripButton.Name = "HelpSingleToolStripButton";
            this.HelpSingleToolStripButton.Click += new System.EventHandler(this.HelpButton_Click);
            // 
            // CopyToolStripButton
            // 
            resources.ApplyResources(this.CopyToolStripButton, "CopyToolStripButton");
            this.CopyToolStripButton.Name = "CopyToolStripButton";
            this.CopyToolStripButton.Click += new System.EventHandler(this.CopyToolStripButton_Click);
            // 
            // AdvancedToolStripButton
            // 
            resources.ApplyResources(this.AdvancedToolStripButton, "AdvancedToolStripButton");
            this.AdvancedToolStripButton.Name = "AdvancedToolStripButton";
            this.AdvancedToolStripButton.Click += new System.EventHandler(this.ShowDetailsItem_Click);
            // 
            // Button1
            // 
            resources.ApplyResources(this.Button1, "Button1");
            this.Button1.Name = "Button1";
            this.Button1.Click += new System.EventHandler(this.Button_Click);
            // 
            // Button2
            // 
            resources.ApplyResources(this.Button2, "Button2");
            this.Button2.Name = "Button2";
            this.Button2.Click += new System.EventHandler(this.Button_Click);
            // 
            // Button3
            // 
            resources.ApplyResources(this.Button3, "Button3");
            this.Button3.Name = "Button3";
            this.Button3.Click += new System.EventHandler(this.Button_Click);
            // 
            // Button4
            // 
            resources.ApplyResources(this.Button4, "Button4");
            this.Button4.Name = "Button4";
            this.Button4.Tag = "z";
            this.Button4.Click += new System.EventHandler(this.Button_Click);
            // 
            // Button5
            // 
            resources.ApplyResources(this.Button5, "Button5");
            this.Button5.Name = "Button5";
            this.Button5.Click += new System.EventHandler(this.Button_Click);
            // 
            // IconsImageList
            // 
            this.IconsImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IconsImageList.ImageStream")));
            this.IconsImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.IconsImageList.Images.SetKeyName(0, "indentarrow.bmp");
            this.IconsImageList.Images.SetKeyName(1, "indentarrow_right.bmp");
            // 
            // ExceptionMessageBoxForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Alert;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FormLayoutPanel);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ExceptionMessageBoxForm";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form_Load);
            this.Click += new System.EventHandler(this.Form_Click);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.Form_HelpRequested);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.FormLayoutPanel.ResumeLayout(false);
            this.FormLayoutPanel.PerformLayout();
            this.MessageLayoutPanel.ResumeLayout(false);
            this.MessageLayoutPanel.PerformLayout();
            this.ButtonsLayoutPanel.ResumeLayout(false);
            this.ButtonsLayoutPanel.PerformLayout();
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
}
