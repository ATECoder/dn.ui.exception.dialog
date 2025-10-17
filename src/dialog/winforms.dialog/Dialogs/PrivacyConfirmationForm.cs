using Microsoft.Win32;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   A privacy confirmation form. </summary>
/// <remarks>   2025-06-19. </remarks>
internal sealed partial class PrivacyConfirmationForm : Form
{
    #region " construction and cleanup "

    private readonly DataTable _table = new( "table" );
    private readonly string? _url;
    private RegistryKey? _key;

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="title">    The title. </param>
    /// <param name="url">      URL of the resource. </param>
    public PrivacyConfirmationForm( string title, string? url )
    {
        this.InitializeComponent();
        this.Text = title;
        this._url = url;
        this._table.Locale = CultureInfo.CurrentCulture;
    }

    #endregion

    #region " event handlers "

    private void Form_Load( object sender, EventArgs e )
    {
        this._table.Columns.Add( new DataColumn( NewMessageBoxSR.PrivacyItemName, typeof( string ) ) );
        this._table.Columns.Add( new DataColumn( NewMessageBoxSR.PrivacyItemValue, typeof( string ) ) );
        this.InfoGrid.DataSource = this._table;
        this.InfoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        // this.grid.PreferredColumnWidth = (this.grid.Width - 4) / 2;
        this.ParseUrl();
    }

    private void Form_Closed( object sender, EventArgs e )
    {
        try
        {
            if ( this._key is null || !this.AlwaysSendCheckBox.Checked )
                return;
            this._key.SetValue( "ShowPrivacyDialog", 0 );
        }
        catch ( Exception )
        {
        }
    }

    private void NoButton_Click( object sender, EventArgs e )
    {
        this.Close();
    }

    private void YesButton_Click( object sender, EventArgs e )
    {
        this.Close();
    }

    #endregion

    #region " show form "

    /// <summary>   Shows the form as a modal dialog box with the specified owner. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="owner">    Any object that implements <see cref="System.Windows.Forms.IWin32Window" />
    ///                         that represents the top-level window that will own the modal dialog box. </param>
    /// <returns>   One of the <see cref="System.Windows.Forms.DialogResult" /> values. </returns>
    public new DialogResult ShowDialog( IWin32Window owner )
    {
        return this._url is null || string.IsNullOrWhiteSpace( this._url ) || this._url.IndexOf( '?' ) < 0
            ? DialogResult.Yes
            : base.ShowDialog( owner );
    }

    /// <summary>   Gets a value indicating whether we can show this dialog. </summary>
    /// <value> True if we can show this dialog, false if not. </value>
    public bool CanShowDialog
    {
        get
        {
            try
            {
                this._key ??= Registry.CurrentUser.CreateSubKey( $"Software\\Microsoft\\Microsoft SQL Server\\{"150"}\\Tools\\Client\\ExceptionMessageBox" );
                return ( int ) this._key.GetValue( "ShowPrivacyDialog", 1 ) == 1;
            }
            catch ( Exception )
            {
                return true;
            }
        }
    }

    private void ParseUrl()
    {
        object[] objArray = new object[2];
        int startIndex = (this._url is null || string.IsNullOrWhiteSpace( this._url )) ? 0 : this._url.IndexOf( '?', 0 );
        while ( startIndex > 0 )
        {
            int num1 = this._url!.IndexOf( '=', startIndex );
            int num2 = this._url.IndexOf( '&', startIndex + 1 );
            string stringToUnEscape1;
            string stringToUnEscape2;
            if ( num2 < 0 )
            {
                if ( num1 < 0 )
                {
                    stringToUnEscape1 = this._url[(startIndex + 1)..];
                    stringToUnEscape2 = string.Empty;
                }
                else
                {
                    stringToUnEscape1 = this._url.Substring( startIndex + 1, num1 - startIndex - 1 );
                    stringToUnEscape2 = this._url[(num1 + 1)..];
                }
            }
            else if ( num1 < 0 || num1 > num2 )
            {
                stringToUnEscape1 = this._url.Substring( startIndex + 1, num2 - startIndex - 1 );
                stringToUnEscape2 = string.Empty;
            }
            else
            {
                stringToUnEscape1 = this._url.Substring( startIndex + 1, num1 - startIndex - 1 );
                stringToUnEscape2 = this._url.Substring( num1 + 1, num2 - num1 - 1 );
            }
            startIndex = num2;
            objArray[0] = Uri.UnescapeDataString( stringToUnEscape1 );
            string title = ( string ) objArray[0];
            if ( string.Equals( title, "ProdName", StringComparison.Ordinal ) )
                objArray[0] = NewMessageBoxSR.ProductName;
            else if ( string.Equals( title, "ProdVer", StringComparison.Ordinal ) )
                objArray[0] = NewMessageBoxSR.ProductVersion;
            else if ( string.Equals( title, "EvtSrc", StringComparison.Ordinal ) )
                objArray[0] = NewMessageBoxSR.MessageSource;
            else if ( string.Equals( title, "EvtID", StringComparison.Ordinal ) )
                objArray[0] = NewMessageBoxSR.MessageID;
            objArray[1] = Uri.UnescapeDataString( stringToUnEscape2 );
            _ = this._table.Rows.Add( objArray );
        }
    }

    #endregion
}
