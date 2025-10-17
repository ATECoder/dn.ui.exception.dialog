using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

internal sealed partial class PrivacyConfirmationForm
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
        base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    private DataGridView InfoGrid;
    private Label HelpLabel;
    private CheckBox AlwaysSendCheckBox;
    private Button YesButton;
    private Button NoButton;

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrivacyConfirmationForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.HelpLabel = new System.Windows.Forms.Label();
            this.InfoGrid = new System.Windows.Forms.DataGridView();
            this.AlwaysSendCheckBox = new System.Windows.Forms.CheckBox();
            this.YesButton = new System.Windows.Forms.Button();
            this.NoButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.InfoGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // HelpLabel
            // 
            resources.ApplyResources(this.HelpLabel, "HelpLabel");
            this.HelpLabel.Name = "HelpLabel";
            // 
            // InfoGrid
            // 
            resources.ApplyResources(this.InfoGrid, "InfoGrid");
            this.InfoGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.InfoGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.InfoGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.InfoGrid.EnableHeadersVisualStyles = false;
            this.InfoGrid.Name = "InfoGrid";
            this.InfoGrid.ReadOnly = true;
            this.InfoGrid.RowHeadersVisible = false;
            // 
            // AlwaysSendCheckBox
            // 
            resources.ApplyResources(this.AlwaysSendCheckBox, "AlwaysSendCheckBox");
            this.AlwaysSendCheckBox.Name = "AlwaysSendCheckBox";
            // 
            // YesButton
            // 
            resources.ApplyResources(this.YesButton, "YesButton");
            this.YesButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.YesButton.Name = "YesButton";
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            // 
            // NoButton
            // 
            resources.ApplyResources(this.NoButton, "NoButton");
            this.NoButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.NoButton.Name = "NoButton";
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            // 
            // PrivacyConfirmationForm
            // 
            this.AcceptButton = this.YesButton;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Dialog;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.NoButton;
            this.ControlBox = false;
            this.Controls.Add(this.NoButton);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.AlwaysSendCheckBox);
            this.Controls.Add(this.InfoGrid);
            this.Controls.Add(this.HelpLabel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrivacyConfirmationForm";
            this.ShowInTaskbar = false;
            this.Closed += new System.EventHandler(this.Form_Closed);
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.InfoGrid)).EndInit();
            this.ResumeLayout(false);

    }


    #endregion
}
