using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   Information about the advanced. </summary>
/// <remarks>   2025-06-19. </remarks>
internal sealed partial class AdvancedInformationForm : Form
{
    /// <summary>   Default constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    public AdvancedInformationForm()
    {
        this.InitializeComponent();
        this.Icon = null;
        this.ToolStrip1.Renderer = new MyRenderer();
    }

    /// <summary>   Gets or sets the message box form. </summary>
    /// <value> The message box form. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    internal ExceptionMessageBoxForm? MessageBoxForm { get; set; }

    private void Form_Load( object sender, EventArgs e )
    {
        TreeNode node1 = new( NewMessageBoxSR.AdvInfoAllMessages );
        for ( Exception? ex1 = this.MessageBoxForm?.Message; ex1 is not null; ex1 = ex1.InnerException )
        {
            try
            {
                TreeNode node2 = new( ex1.Message )
                {
                    Tag = this.MessageBoxForm?.BuildAdvancedInfo( ex1, AdvancedInfoType.All )
                };

                _ = node2.Nodes.Add( new TreeNode( NewMessageBoxSR.AdvInfoMessage )
                {
                    Tag = this.MessageBoxForm?.BuildAdvancedInfo( ex1, AdvancedInfoType.Message )
                } );

                _ = node1.Nodes.Add( node2 );

                try
                {
                    string? str = this.MessageBoxForm?.BuildAdvancedInfo( ex1, AdvancedInfoType.HelpLink );
                    if ( str is not null )
                    {
                        if ( str.Length > 0 )
                            _ = node2.Nodes.Add( new TreeNode( NewMessageBoxSR.AdvInfoHelpLink )
                            {
                                Tag = str
                            } );
                    }
                }
                catch ( Exception )
                {
                }
                try
                {
                    string? str = this.MessageBoxForm?.BuildAdvancedInfo( ex1, AdvancedInfoType.Data );
                    if ( str is not null )
                    {
                        if ( str.Length > 0 )
                            _ = node2.Nodes.Add( new TreeNode( NewMessageBoxSR.ADvInfoData )
                            {
                                Tag = str
                            } );
                    }
                }
                catch ( Exception )
                {
                }
                if ( ex1.StackTrace is not null )
                {
                    try
                    {
                        string? str = this.MessageBoxForm?.BuildAdvancedInfo( ex1, AdvancedInfoType.StackTrace );
                        if ( str is not null )
                        {
                            if ( str.Length > 0 )
                                _ = node2.Nodes.Add( new TreeNode( NewMessageBoxSR.CodeLocation[..^1] )
                                {
                                    Tag = str
                                } );
                        }
                    }
                    catch ( Exception )
                    {
                    }
                }
            }
            catch ( Exception )
            {
            }
        }
        if ( node1.Nodes.Count == 0 )
        {
            this.Close();
        }
        else
        {
            StringBuilder stringBuilder = new();
            foreach ( TreeNode node3 in node1.Nodes )
                _ = stringBuilder.Append( node3.Tag?.ToString() );
            node1.Tag = stringBuilder.ToString();
            _ = this.Tree.Nodes.Add( node1 );
            this.Tree.ExpandAll();
            this.Tree.SelectedNode = node1;
        }
    }

    private void Form_KeyDown( object sender, KeyEventArgs e )
    {
        if ( (e.Modifiers != System.Windows.Forms.Keys.Control
            || (e.KeyData & System.Windows.Forms.Keys.C) != System.Windows.Forms.Keys.C)
            && (e.KeyData & System.Windows.Forms.Keys.Insert) != System.Windows.Forms.Keys.Insert )
            return;
        this.CopyToClipboard();
        e.Handled = true;
    }

    private void CopyToolStripButton_Click( object sender, EventArgs e )
    {
        this.CopyToClipboard();
    }

    private void Tree_AfterSelect( object sender, TreeViewEventArgs e )
    {
        this.DetailsTextBox.Text = e?.Node?.Tag as string;
    }

    private void CopyToClipboard()
    {
        try
        {
            Clipboard.SetDataObject( this.DetailsTextBox.SelectionLength == 0
                ? this.DetailsTextBox.Text
                : this.DetailsTextBox.SelectedText, true );
        }
        catch ( Exception ex )
        {
            this.MessageBoxForm?.ShowError( ExceptionMessageBoxErrorSR.CopyToClipboardError, ex );
        }
    }

}
