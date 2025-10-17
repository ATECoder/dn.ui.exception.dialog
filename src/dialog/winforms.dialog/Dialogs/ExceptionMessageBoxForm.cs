using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace cc.isr.WinForms.Dialogs;

/// <summary>
/// Form for viewing the exception message box. This class cannot be inherited.
/// </summary>
/// <remarks>   2025-06-19. </remarks>
internal sealed partial class ExceptionMessageBoxForm : Form
{
    #region " construction and cleanup "

    /// <summary>   Default constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    public ExceptionMessageBoxForm()
    {
        this.InitializeComponent();
        this.HelpToolStripButton.DropDown = this._helpMenuDropdown;
        this.ToolStrip1.Renderer = new MyRenderer();
        this.Icon = this.FormIcon;
    }

    #endregion

    #region " properties "

    /// <summary>   Gets or sets the message. </summary>
    /// <value> The message. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public Exception? Message { get; set; }

    /// <summary>   Gets or sets the caption. </summary>
    /// <value> The caption. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public string Caption
    {
        get => this.Text;
        set => this.Text = value;
    }

    /// <summary>   Gets or sets the symbol. </summary>
    /// <value> The symbol. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public ExceptionMessageBoxSymbol Symbol { get; set; } = ExceptionMessageBoxSymbol.Warning;

    /// <summary>   Gets or sets the buttons. </summary>
    /// <value> The buttons. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public ExceptionMessageBoxButtons Buttons { get; set; }

    /// <summary>   Gets or sets options for controlling the operation. </summary>
    /// <value> The options. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public ExceptionMessageBoxOptions Options { get; set; }

    /// <summary>   Gets the custom dialog result. </summary>
    /// <value> The custom dialog result. </value>
    public ExceptionMessageBoxDialogResult CustomDialogResult { get; private set; }

    /// <summary>   Gets or sets the default button. </summary>
    /// <value> The default button. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public ExceptionMessageBoxDefaultButton DefaultButton { get; set; }

    private string[] _buttonTextArray = new string[5];
    private int _buttonCount;
    /// <summary>   Sets button text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="value">    The value. </param>
    public void SetButtonText( string[] value )
    {
        this._buttonTextArray = value;
        if ( string.IsNullOrWhiteSpace( this._buttonTextArray[0] ) )
            this._buttonCount = 0;
        else if ( string.IsNullOrWhiteSpace( this._buttonTextArray[1] ) )
            this._buttonCount = 1;
        else if ( string.IsNullOrWhiteSpace( this._buttonTextArray[2] ) )
            this._buttonCount = 2;
        else if ( string.IsNullOrWhiteSpace( this._buttonTextArray[3] ) )
            this._buttonCount = 3;
        else if ( string.IsNullOrWhiteSpace( this._buttonTextArray[4] ) )
            this._buttonCount = 4;
        else
            this._buttonCount = 5;
    }

    /// <summary>   Gets or sets the number of message levels. </summary>
    /// <value> The number of message levels. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public int MessageLevelCount { get; set; } = -1;

    /// <summary>   Gets or sets a value indicating whether the help button is shown. </summary>
    /// <value> True if show help button, false if not. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public bool ShowHelpButton { get; set; } = true;

    /// <summary>   Gets or sets the custom symbol. </summary>
    /// <value> The custom symbol. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public Bitmap? CustomSymbol { get; set; }

    /// <summary>   Gets or sets the form icon. </summary>
    /// <value> The form icon. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    public Icon? FormIcon { get; set; }

    /// <summary>   Gets or sets a value indicating whether the check box is shown. </summary>
    /// <value> True if show check box, false if not. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public bool ShowCheckBox { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this object is check box checked.
    /// </summary>
    /// <value> True if this object is check box checked, false if not. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public bool IsCheckBoxChecked
    {
        get => this.DoNotShowCheckBox.Checked;
        set => this.DoNotShowCheckBox.Checked = value;
    }

    /// <summary>   Gets or sets the check box text. </summary>
    /// <value> The check box text. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Visible )]
    public string CheckBoxText
    {
        get => this.DoNotShowCheckBox.Text;
        set => this.DoNotShowCheckBox.Text = value;
    }

    /// <summary>   Gets or sets a value indicating whether the do beep. </summary>
    /// <value> True if do beep, false if not. </value>
    [DesignerSerializationVisibility( DesignerSerializationVisibility.Content )]
    internal bool DoBeep { get; set; } = true;

    #endregion

    #region " show "

    private void AddAdditionalInfoMessage( int messageCount, string strText, Exception ex )
    {
        int count = this.AdditionalLayoutPanel.Controls.Count;
        this.AdditionalLayoutPanel.SuspendLayout();
        try
        {
            Label label = new()
            {
                Name = "picIndentArrow" + count.ToString( CultureInfo.CurrentCulture ),
                TabIndex = count++,
                TabStop = false,
                Visible = true,
                ImageList = this.IconsImageList,
                ImageIndex = (this.Options & ExceptionMessageBoxOptions.RtlReading) == ExceptionMessageBoxOptions.None ? 0 : 1,
                Size = new Size( 16 /*0x10*/, 16 /*0x10*/),
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Margin = new Padding( 0 ),
                AutoSize = false,
                AccessibleRole = AccessibleRole.Graphic
            };
            label.Click += new EventHandler( this.HideBorderLines );
            this.AdditionalLayoutPanel.Controls.Add( label, messageCount, messageCount );
            LinkLabel linkLabel = new()
            {
                Name = "txtMessage" + count.ToString( CultureInfo.CurrentCulture ),
                AutoSize = true,
                TabIndex = count,
                TabStop = true,
                Text = strText,
                LinkArea = new LinkArea( 0, 0 ),
                AccessibleName = strText,
                Visible = true,
                Margin = new Padding( 0, 0, 0, 8 ),
                MaximumSize = new Size( this.AdditionalLayoutPanel.GetPreferredSize( Size.Empty ).Width - ((messageCount + 1) * 20), 0 )
            };
            linkLabel.Click += new EventHandler( this.HideBorderLines );
            linkLabel.UseMnemonic = false;
            linkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            linkLabel.Tag = ex;
            this.AdditionalLayoutPanel.Controls.Add( linkLabel, messageCount + 1, messageCount );
            this.AdditionalLayoutPanel.SetColumnSpan( linkLabel, this.AdditionalLayoutPanel.ColumnStyles.Count - (messageCount + 1) );
        }
        finally
        {
            this.AdditionalLayoutPanel.ResumeLayout();
        }
    }

    /// <summary>   Shows the given owner. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="owner">    The owner. </param>
    public void Show( Control owner )
    {
        if ( owner is null )
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = true;
        }
        else
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.Parent = owner;
            this.CenterToParent();
        }
        this.Show();
    }

    /// <summary>   Shows the error. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="str">      The string. </param>
    /// <param name="exError">  The exception error. </param>
    internal void ShowError( string str, Exception? exError )
    {
        _ = new ExceptionMessageBox( new InvalidOperationException( str, exError )
        {
            Source = this.Text
        } )
        {
            Options = this.Options
        }.Show( this.Parent );
    }

    private readonly ArrayList _helpUrlArray = new( 5 );

    private int _helpUrlCount;

    private bool _isButtonPressed;

    private readonly ToolStripDropDown _helpMenuDropdown = new();

    /// <summary>   Prepare to show. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <exception cref="Exception">            Thrown when an exception error condition occurs. </exception>
    /// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
    internal void PrepareToShow()
    {
        if ( this.Message is null )
            throw new ArgumentException( $"{nameof( this.Message )} is null" );
        if ( this._buttonTextArray is null || this._buttonTextArray.Length < 5 )
            throw new ArgumentException( ExceptionMessageBoxErrorSR.CantComplete, nameof( this._buttonTextArray ) );
        if ( this._isButtonPressed )
            throw new ArgumentException( ExceptionMessageBoxErrorSR.CantReuseObject, nameof( this._isButtonPressed ) );
        if ( this.Caption is null || this.Caption.Length == 0 )
            this.Caption = this.Message.Source ?? string.Empty;
        this.SuspendLayout();
        this.ToolStrip1.Visible = this.ShowHelpButton;
        this.MessageLayoutPanel.MaximumSize = new Size( this.MessageLayoutPanel.MaximumSize.Width, Screen.FromControl( this.MessageLayoutPanel ).WorkingArea.Height * 3 / 4 );
        this.InitializeCheckbox();
        this.InitializeMessage();
        this.InitializeSymbol();
        this.InitializeButtons();
        if ( !ExceptionMessageBuilder.MessageBuilder.HasTechnicalDetails( this.Message ) )
        {
            this.AdvancedToolStripButton.Visible = false;
            this.AdvancedToolStripButton.Enabled = false;
        }
        if ( this.ShowHelpButton )
        {
            if ( this._helpUrlCount == 0 )
            {
                this.HelpToolStripButton.Visible = false;
                this.HelpSingleToolStripButton.Visible = false;
                this.HelpToolStripButton.Enabled = false;
                this._helpMenuDropdown.Items.Clear();
            }
            else if ( this._helpMenuDropdown.Items.Count == 1 && this._helpUrlCount == 1 )
            {
                this._helpMenuDropdown.Items.Clear();
                this.HelpToolStripButton.Visible = false;
                this.HelpSingleToolStripButton.Visible = true;
            }
        }
        if ( (this.Options & ExceptionMessageBoxOptions.RtlReading) != ExceptionMessageBoxOptions.None )
        {
            this.RightToLeft = RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.DoNotShowCheckBox.Padding = new Padding( this.DoNotShowCheckBox.Padding.Right, this.DoNotShowCheckBox.Padding.Top, this.DoNotShowCheckBox.Padding.Left, this.DoNotShowCheckBox.Padding.Bottom );
        }
        this.ResumeLayout();
    }

    private void InitializeButtons()
    {
        Button[] buttonArray = [this.Button1, this.Button2, this.Button3, this.Button4, this.Button5];
        switch ( this.Buttons )
        {
            case ExceptionMessageBoxButtons.OK:
                this._buttonTextArray[0] = NewMessageBoxSR.OKButton;
                this.Button1.DialogResult = DialogResult.OK;
                this.AcceptButton = this.Button1;
                this.CancelButton = this.Button1;
                this._buttonCount = 1;
                break;
            case ExceptionMessageBoxButtons.OKCancel:
                this._buttonTextArray[0] = NewMessageBoxSR.OKButton;
                this._buttonTextArray[1] = NewMessageBoxSR.CancelButton;
                this.Button1.DialogResult = DialogResult.OK;
                this.Button2.DialogResult = DialogResult.Cancel;
                this.AcceptButton = this.Button1;
                this.CancelButton = this.Button2;
                this._buttonCount = 2;
                break;
            case ExceptionMessageBoxButtons.YesNoCancel:
                this._buttonTextArray[0] = NewMessageBoxSR.YesButton;
                this._buttonTextArray[1] = NewMessageBoxSR.NoButton;
                this._buttonTextArray[2] = NewMessageBoxSR.CancelButton;
                this.Button1.DialogResult = DialogResult.Yes;
                this.Button2.DialogResult = DialogResult.No;
                this.Button3.DialogResult = DialogResult.Cancel;
                this._buttonCount = 3;
                this.CancelButton = this.Button3;
                break;
            case ExceptionMessageBoxButtons.YesNo:
                this._buttonTextArray[0] = NewMessageBoxSR.YesButton;
                this._buttonTextArray[1] = NewMessageBoxSR.NoButton;
                this.Button1.DialogResult = DialogResult.Yes;
                this.Button2.DialogResult = DialogResult.No;
                this._buttonCount = 2;
                this.ControlBox = false;
                break;
            case ExceptionMessageBoxButtons.AbortRetryIgnore:
                this._buttonTextArray[0] = NewMessageBoxSR.AbortButton;
                this._buttonTextArray[1] = NewMessageBoxSR.RetryButton;
                this._buttonTextArray[2] = NewMessageBoxSR.IgnoreButton;
                this.Button1.DialogResult = DialogResult.Abort;
                this.Button2.DialogResult = DialogResult.Retry;
                this.Button3.DialogResult = DialogResult.Ignore;
                this._buttonCount = 3;
                this.ControlBox = false;
                break;
            case ExceptionMessageBoxButtons.RetryCancel:
                this._buttonTextArray[0] = NewMessageBoxSR.RetryButton;
                this._buttonTextArray[1] = NewMessageBoxSR.CancelButton;
                this.Button1.DialogResult = DialogResult.Retry;
                this.Button2.DialogResult = DialogResult.Cancel;
                this.CancelButton = this.Button2;
                this._buttonCount = 2;
                break;
            case ExceptionMessageBoxButtons.Custom:
                this.ControlBox = false;
                break;
            default:
                break;
        }
        int width = this.ButtonsLayoutPanel.GetPreferredSize( Size.Empty ).Width;
        for ( int index = 0; index < this._buttonCount; ++index )
        {
            Button button = buttonArray[index];
            button.Text = this._buttonTextArray[index];
            button.Visible = true;
        }
        this.AdjustDialogWidth( this.ButtonsLayoutPanel.GetPreferredSize( Size.Empty ).Width - width, true );
        this.AcceptButton = this.DefaultButton < ( ExceptionMessageBoxDefaultButton ) this._buttonCount ? ( IButtonControl ) buttonArray[( int ) this.DefaultButton] : throw new InvalidEnumArgumentException( "DefaultButton", ( int ) this.DefaultButton, typeof( ExceptionMessageBoxDefaultButton ) );
    }

    private Icon? _symbolIcon;

    private void InitializeSymbol()
    {
        if ( this.CustomSymbol is not null )
        {
            int offset = this.CustomSymbol.Width + 2 - this.IconPanel.Width;
            this.IconPanel.Width += offset;
            this.AdjustDialogWidth( offset, false );
            this.IconPanel.Height = this.CustomSymbol.Height + 2;
            this.IconPanel.MinimumSize = new Size( 0, this.CustomSymbol.Height + 2 );
        }
        else
        {
            switch ( this.Symbol )
            {
                case ExceptionMessageBoxSymbol.None:
                    this.IconPanel.Visible = false;
                    break;
                case ExceptionMessageBoxSymbol.Warning:
                case ExceptionMessageBoxSymbol.Exclamation:
                    this._symbolIcon = SystemIcons.Warning;
                    break;
                case ExceptionMessageBoxSymbol.Information:
                case ExceptionMessageBoxSymbol.Asterisk:
                    this._symbolIcon = SystemIcons.Information;
                    break;
                case ExceptionMessageBoxSymbol.Error:
                case ExceptionMessageBoxSymbol.Hand:
                    this._symbolIcon = SystemIcons.Error;
                    break;
                case ExceptionMessageBoxSymbol.Question:
                    this._symbolIcon = SystemIcons.Question;
                    break;
                default:
                    break;
            }
        }
    }

    private void AdjustDialogWidth( int offset, bool isAdjustingForButtons )
    {
        if ( offset <= 0 )
            return;
        this.TopMessageLabel.MaximumSize = new Size( this.TopMessageLabel.MaximumSize.Width + offset, this.TopMessageLabel.MaximumSize.Height );
        this.TopMessageLabel.MinimumSize = this.TopMessageLabel.MaximumSize;
        this.AdditionalLayoutPanel.MaximumSize = new Size( this.AdditionalLayoutPanel.MaximumSize.Width + offset, this.AdditionalLayoutPanel.MaximumSize.Height );
        foreach ( Label control in ( ArrangedElementCollection ) this.AdditionalLayoutPanel.Controls )
        {
            if ( control.ImageIndex < 0 )
            {
                Label label = control;
                Size maximumSize = control.MaximumSize;
                int width = maximumSize.Width + offset;
                maximumSize = control.MaximumSize;
                int height = maximumSize.Height;
                Size size = new( width, height );
                label.MaximumSize = size;
            }
        }
        if ( isAdjustingForButtons )
            return;
        this.ButtonsLayoutPanel.MinimumSize = new Size( this.ButtonsLayoutPanel.MinimumSize.Width + offset, this.ButtonsLayoutPanel.MinimumSize.Height );
    }

    private void InitializeCheckbox()
    {
        if ( !this.ShowCheckBox )
        {
            this.DoNotShowCheckBox.Visible = false;
            this.DoNotShowCheckBox.Enabled = false;
        }
        else
        {
            if ( this.DoNotShowCheckBox.Text.Length != 0 )
                return;
            this.DoNotShowCheckBox.Text = NewMessageBoxSR.DefaultCheckboxText;
        }
    }

    private void ShowBorderLines( int index )
    {
        int num = 1;
        if ( index == 0 )
            this.TopMessageLabel.BackColor = ControlPaint.Light( SystemColors.ControlDark, 0.5f );
        else
            this.TopMessageLabel.BackColor = SystemColors.Control;
        if ( this.AdditionalLayoutPanel is null )
            return;
        foreach ( Label control in ( ArrangedElementCollection ) this.AdditionalLayoutPanel.Controls )
        {
            if ( control.ImageIndex < 0 )
            {
                if ( num == index )
                    control.BackColor = ControlPaint.Light( SystemColors.ControlDark, 0.5f );
                else
                    control.BackColor = SystemColors.Control;
                ++num;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    private void InitializeMessage()
    {
        int num1 = 0;
        int num2 = 0;
        this._helpMenuDropdown.Items.Clear();
        for ( Exception? innerException = this.Message?.InnerException; innerException is not null; innerException = innerException.InnerException )
            ++num2;
        if ( this.MessageLevelCount > 0 && num2 > this.MessageLevelCount - 1 )
            num2 = this.MessageLevelCount - 1;
        if ( num2 > 0 )
        {
            for ( int index = 0; index < num2; ++index )
            {
                this.AdditionalLayoutPanel.ColumnStyles.Insert( 0, new ColumnStyle( SizeType.Absolute, 20f ) );
                _ = this.AdditionalLayoutPanel.RowStyles.Add( new RowStyle( SizeType.AutoSize ) );
            }
            this.AdditionalLayoutPanel.ColumnCount = num2 + 1;
            this.AdditionalLayoutPanel.RowCount = num2;
        }
        this.AdditionalInfoLabel.Visible = this.AdditionalLayoutPanel.Visible = num2 > 0;
        for ( Exception? ex = this.Message; ex is not null && (this.MessageLevelCount < 0 || num1 < this.MessageLevelCount); ++num1 )
        {
            StringBuilder stringBuilder = new();
            string str1 = ex.Message is null || ex.Message.Length == 0 ? ExceptionMessageBoxErrorSR.CantComplete : ex.Message;
            if ( this.ShowHelpButton )
            {
                string str2 = ExceptionMessageBuilder.MessageBuilder.BuildHelpURL( ex );
                int num3 = str2.Length > 0 ? 1 : 0;
                _ = this._helpUrlArray.Add( str2 );

                string messagePart = str1.Length <= 50
                    ? str1
                    : string.Format( NewMessageBoxSR.Culture, NewMessageBoxSR.AddEllipsis, str1[..50] );
                ToolStripItem toolStripItem;
                if ( num3 != 0 )
                {
                    toolStripItem = this._helpMenuDropdown.Items.Add( string.Format( NewMessageBoxSR.Culture, NewMessageBoxSR.HelpMenuText, messagePart ),
                        null, new EventHandler( this.HelpItem_Click ) );
                    ++this._helpUrlCount;
                }
                else
                {
                    toolStripItem = this._helpMenuDropdown.Items.Add( string.Format( NewMessageBoxSR.Culture, NewMessageBoxSR.NoHelpMenuText, messagePart ),
                        null, new EventHandler( this.HelpItem_Click ) );
                    toolStripItem.Enabled = false;
                }
                toolStripItem.Tag = num1;
                toolStripItem.MouseEnter += new EventHandler( this.OnHelpButtonMouseEnter );
                toolStripItem.MouseLeave += new EventHandler( this.OnHelpButtonMouseLeave );
            }
            _ = stringBuilder.Remove( 0, stringBuilder.Length );
            _ = stringBuilder.Append( str1 );
            _ = ExceptionMessageBuilder.MessageBuilder.BuildMessage( stringBuilder, ex, this.Caption );
            if ( num1 == 0 )
            {
                this.TopMessageLabel.Text = stringBuilder.ToString();
                this.TopMessageLabel.LinkArea = new LinkArea( 0, 0 );
                this.TopMessageLabel.Tag = ex;
            }
            else
                this.AddAdditionalInfoMessage( num1 - 1, stringBuilder.ToString(), ex );
            ex = ex.InnerException;
        }
        Point location = this.Location;
        if ( location.Y + this.GetPreferredSize( Size.Empty ).Height <= Screen.PrimaryScreen!.WorkingArea.Bottom )
            return;
        location = this.Location;
        this.Location = new Point( location.X, Screen.PrimaryScreen!.WorkingArea.Bottom - this.Size.Height - 10 );
    }

    #endregion

    #region " operations "

    /// <summary>   Builds message text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="isInternal">   True if is internal, false if not. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    internal string BuildMessageText( bool isInternal )
    {
        if ( this.Message is null )
            return string.Empty;
        StringBuilder stringBuilder1 = new();
        _ = stringBuilder1.Append( Environment.NewLine );
        _ = stringBuilder1.Append( "------------------------------" );
        _ = stringBuilder1.Append( Environment.NewLine );
        string str = stringBuilder1.ToString();
        bool flag = this.Message.InnerException is not null;
        int num = 1;
        StringBuilder stringBuilder2 = new();
        if ( isInternal )
        {
            _ = stringBuilder2.Append( NewMessageBoxSR.MessageTitle );
            _ = stringBuilder2.Append( this.Caption );
        }
        for ( Exception? ex = this.Message; ex is not null; ex = ex.InnerException )
        {
            if ( isInternal || num > 1 )
                _ = stringBuilder2.Append( str );
            if ( flag && num == 2 )
            {
                _ = stringBuilder2.Append( NewMessageBoxSR.AdditionalInfo );
                _ = stringBuilder2.Append( Environment.NewLine );
            }
            if ( isInternal || num > 1 )
                _ = stringBuilder2.Append( Environment.NewLine );
            if ( ex.Message is null || ex.Message.Length == 0 )
                _ = stringBuilder2.Append( ExceptionMessageBoxErrorSR.CantComplete );
            else
                _ = stringBuilder2.Append( ex.Message );
            if ( ex.Source is not null && ex.Source.Length > 0 && (num != 1 || this.Caption != ex.Source) )
            {
                _ = stringBuilder2.Append( ' ' );
                _ = stringBuilder2.Append( ExceptionMessageBuilder.MessageBuilder.BuildExceptionSource( ex ) );
            }
            _ = stringBuilder2.Append( Environment.NewLine );
            string url = ExceptionMessageBuilder.MessageBuilder.BuildHelpURL( ex );
            if ( url.Length > 0 )
            {
                _ = stringBuilder2.Append( Environment.NewLine );

                _ = stringBuilder2.Append( string.Format( NewMessageBoxSR.Culture, NewMessageBoxSR.ClipboardOrEmailHelpLink, url ) );
                _ = stringBuilder2.Append( Environment.NewLine );
            }
            ++num;
        }
        if ( isInternal )
        {
            _ = stringBuilder2.Append( str );
            _ = stringBuilder2.Append( NewMessageBoxSR.Buttons );
            _ = stringBuilder2.Append( Environment.NewLine );
            for ( int index = 0; index < this._buttonCount; ++index )
            {
                _ = stringBuilder2.Append( Environment.NewLine );
                _ = stringBuilder2.Append( this._buttonTextArray![index] );
            }
            _ = stringBuilder2.Append( str );
        }
        return stringBuilder2.ToString();
    }

    /// <summary>   Builds advanced information. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="ex">   The exception. </param>
    /// <param name="type"> The type. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1822:Mark members as static", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    internal string BuildAdvancedInfo( Exception ex, AdvancedInfoType type )
    {
        StringBuilder stringBuilder = new();
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( ex );
#else
        if ( ex is null ) throw new ArgumentNullException( nameof( ex ) );
#endif
        if ( type == AdvancedInfoType.All )
        {
            _ = stringBuilder.Append( "===================================" );
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( Environment.NewLine );
            }
        }
        if ( ex.Message is not null && ex.Message.Length != 0 && (type == AdvancedInfoType.All || type == AdvancedInfoType.Message) )
        {
            _ = stringBuilder.Append( ex.Message );
            if ( !string.IsNullOrWhiteSpace( ex.Source ) )
            {
                string src = $" ({ex.Source})";
                _ = stringBuilder.Append( src );
            }
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( Environment.NewLine );
            }
        }
        string url = ExceptionMessageBuilder.MessageBuilder.BuildHelpURL( ex );
        if ( url.Length > 0 )
        {
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( "------------------------------" );
                _ = stringBuilder.Append( Environment.NewLine );
            }
            if ( type is AdvancedInfoType.All or AdvancedInfoType.HelpLink )
            {

                _ = stringBuilder.Append( string.Format( NewMessageBoxSR.Culture, NewMessageBoxSR.ClipboardOrEmailHelpLink, url ) );
            }
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( Environment.NewLine );
            }
        }
        string str = ExceptionMessageBuilder.MessageBuilder.BuildAdvancedInfoProperties( ex );
        if ( str is not null && str.Length > 0 )
        {
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( "------------------------------" );
                _ = stringBuilder.Append( Environment.NewLine );
            }
            if ( type is AdvancedInfoType.All or AdvancedInfoType.Data )
                _ = stringBuilder.Append( str );
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( Environment.NewLine );
            }
        }
        if ( ex.StackTrace is not null && ex.StackTrace.Length > 0 )
        {
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( "------------------------------" );
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( NewMessageBoxSR.CodeLocation );
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( Environment.NewLine );
            }
            if ( type is AdvancedInfoType.All or AdvancedInfoType.StackTrace )
                _ = stringBuilder.Append( ex.StackTrace );
            if ( type == AdvancedInfoType.All )
            {
                _ = stringBuilder.Append( Environment.NewLine );
                _ = stringBuilder.Append( Environment.NewLine );
            }
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Event queue for all listeners interested in OnCopyToClipboardInternal events.
    /// </summary>
    internal event CopyToClipboardEventHandler? OnCopyToClipboardInternal;

    private void CopyToClipboard()
    {
        string str = this.BuildMessageText( true );
        if ( this.OnCopyToClipboardInternal is not null )
        {
            CopyToClipboardEventArgs e = new( str );
            this.OnCopyToClipboardInternal( this, e );
            if ( e.EventHandled )
                return;
        }
        try
        {
            Clipboard.SetDataObject( str, true );
        }
        catch ( Exception )
        {
            try
            {
                Clipboard.SetDataObject( str, true );
            }
            catch ( Exception ex2 )
            {
                this.ShowError( ExceptionMessageBoxErrorSR.CopyToClipboardError, ex2 );
            }
        }
    }

    private void GetHelp( int index )
    {
        if ( this._helpUrlCount == 1 )
        {
            index = 0;
            while ( index < this._helpUrlArray.Count && string.IsNullOrWhiteSpace( this._helpUrlArray[index] as string ) )
                ++index;
        }
        if ( index >= this._helpUrlArray.Count || string.IsNullOrWhiteSpace( this._helpUrlArray[index] as string ) )
            return;
        string? helpUrl = this._helpUrlArray[index] as string;
        try
        {
            DialogResult dialogResult;
            using ( PrivacyConfirmationForm privacyConfirmation = new( this.Text, helpUrl ) )
            {
                if ( this.Parent is null )
                {
                    privacyConfirmation.ShowInTaskbar = true;
                    privacyConfirmation.StartPosition = FormStartPosition.CenterScreen;
                    dialogResult = privacyConfirmation.ShowDialog( this );
                }
                else
                {
                    privacyConfirmation.StartPosition = FormStartPosition.CenterParent;
                    dialogResult = privacyConfirmation.ShowDialog( this.Parent );
                }
            }
            if ( dialogResult == DialogResult.No )
                return;
        }
        catch ( Exception )
        {

            this.ShowError( ExceptionMessageBoxForm.CantStartHelpLink( helpUrl ), null );
            return;
        }
        try
        {
            _ = new Process() { StartInfo = { FileName = helpUrl } }.Start();
        }
        catch ( Exception ex )
        {

            this.ShowError( ExceptionMessageBoxForm.CantStartHelpLink( helpUrl ), ex );
        }
    }

    /// <summary>   Can't start help link. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="helpUrl">  URL of the help. </param>
    /// <returns>   A string. </returns>
    public static string CantStartHelpLink( string? helpUrl )
    {
        string formatString = ExceptionMessageBoxErrorSR.CantStartHelpLink;
        return string.Format( ExceptionMessageBoxErrorSR.Culture, formatString, helpUrl );
    }

    private void ShowDetails()
    {
        try
        {
            using AdvancedInformationForm advancedInformation = new();
            advancedInformation.MessageBoxForm = this;
            if ( (this.Options & ExceptionMessageBoxOptions.RtlReading) != ExceptionMessageBoxOptions.None )
            {
                advancedInformation.RightToLeft = RightToLeft.Yes;
                advancedInformation.RightToLeftLayout = true;
            }
            if ( this.Parent is null )
            {
                advancedInformation.ShowInTaskbar = true;
                advancedInformation.StartPosition = FormStartPosition.CenterScreen;
                int num = ( int ) advancedInformation.ShowDialog( this );
            }
            else
            {
                advancedInformation.StartPosition = FormStartPosition.CenterParent;
                int num = ( int ) advancedInformation.ShowDialog( this.Parent );
            }
        }
        catch ( Exception ex )
        {
            this.ShowError( ExceptionMessageBoxErrorSR.CantShowTechnicalDetailsError, ex );
        }
    }

    private void Beep()
    {
        switch ( this.Symbol )
        {
            case ExceptionMessageBoxSymbol.None:
            case ExceptionMessageBoxSymbol.Warning:
                _ = NativeMethods.MessageBeep( ExceptionMessageBoxForm.BeepType.Asterisk );
                break;
            case ExceptionMessageBoxSymbol.Information:
                _ = NativeMethods.MessageBeep( ExceptionMessageBoxForm.BeepType.Exclamation );
                break;
            case ExceptionMessageBoxSymbol.Error:
                _ = NativeMethods.MessageBeep( ExceptionMessageBoxForm.BeepType.Hand );
                break;
            case ExceptionMessageBoxSymbol.Exclamation:
                break;
            case ExceptionMessageBoxSymbol.Asterisk:
                break;
            case ExceptionMessageBoxSymbol.Question:
                break;
            case ExceptionMessageBoxSymbol.Hand:
                break;
            default:
                break;
        }
    }

    #endregion

    #region " event handlers "

    /// <summary>   Processes Windows messages. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="m">    [in,out] The Windows <see cref="System.Windows.Forms.Message" /> to
    ///                     process. </param>
    protected override void WndProc( ref System.Windows.Forms.Message m )
    {
        if ( m.Msg == 7690 )
            m.Result = new( 10007 );
        else
            base.WndProc( ref m );
    }

    private void Form_Load( object sender, EventArgs e )
    {
        if ( this.Parent is null )
            this.StartPosition = FormStartPosition.CenterScreen;
        else
        {
            this.StartPosition = FormStartPosition.CenterParent;
            this.CenterToParent();
        }
        if ( this.DoBeep )
            this.Beep();

        switch ( this.DefaultButton )
        {
            case ExceptionMessageBoxDefaultButton.Button1:
                _ = this.Button1.Focus();
                this.ActiveControl = this.Button1;
                break;
            case ExceptionMessageBoxDefaultButton.Button2:
                _ = this.Button2.Focus();
                this.ActiveControl = this.Button2;
                break;
            case ExceptionMessageBoxDefaultButton.Button3:
                _ = this.Button3.Focus();
                this.ActiveControl = this.Button3;
                break;
            case ExceptionMessageBoxDefaultButton.Button4:
                _ = this.Button4.Focus();
                this.ActiveControl = this.Button4;
                break;
            case ExceptionMessageBoxDefaultButton.Button5:
                _ = this.Button5.Focus();
                this.ActiveControl = this.Button5;
                break;
            default:
                break;
        }
    }

    private void Form_HelpRequested( object? sender, HelpEventArgs helpEvent )
    {
        if ( !this.ShowHelpButton )
            return;
        if ( this._helpMenuDropdown.Items.Count == 0 && this._helpUrlCount == 1 )
            this.GetHelp( 0 );
        helpEvent.Handled = true;
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

    private void Form_Click( object sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
    }

    private void Form_Closing( object sender, CancelEventArgs e )
    {
        switch ( this.Buttons )
        {
            case ExceptionMessageBoxButtons.OK:
                this.DialogResult = DialogResult.OK;
                break;
            case ExceptionMessageBoxButtons.YesNo:
            case ExceptionMessageBoxButtons.AbortRetryIgnore:
            case ExceptionMessageBoxButtons.Custom:
                if ( this._isButtonPressed )
                    break;
                e.Cancel = true;
                _ = NativeMethods.MessageBeep( ExceptionMessageBoxForm.BeepType.Hand );
                break;
            case ExceptionMessageBoxButtons.OKCancel:
                break;
            case ExceptionMessageBoxButtons.YesNoCancel:
                break;
            case ExceptionMessageBoxButtons.RetryCancel:
                break;
            default:
                break;
        }
    }

    private void Button_Click( object sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
        this.CustomDialogResult = this.Buttons != ExceptionMessageBoxButtons.Custom
            ? ExceptionMessageBoxDialogResult.None
            : (sender != this.Button1
                ? (sender != this.Button2
                    ? (sender != this.Button3
                        ? (sender != this.Button4
                            ? ExceptionMessageBoxDialogResult.Button5
                            : ExceptionMessageBoxDialogResult.Button4)
                        : ExceptionMessageBoxDialogResult.Button3)
                    : ExceptionMessageBoxDialogResult.Button2)
               : ExceptionMessageBoxDialogResult.Button1);
        this._isButtonPressed = true;
        this.Close();
    }

    private void CopyToolStripButton_Click( object sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
        this.CopyToClipboard();
    }

    private void HideBorderLines( object? sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
    }

    private void IconPanel_Paint( object sender, PaintEventArgs e )
    {
        if ( this.CustomSymbol is not null )
        {
            e.Graphics.DrawImageUnscaled( this.CustomSymbol, new Point( 0, 0 ) );
        }
        else
        {
            if ( this._symbolIcon is null )
                return;
            e.Graphics.DrawIcon( this._symbolIcon, 0, 0 );
        }
    }

    private void HelpItem_Click( object? sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
        if ( sender is ToolStripItem item && item.Tag is int tag )
            this.GetHelp( tag & 4095 );
    }

    private void OnHelpButtonMouseEnter( object? sender, EventArgs e )
    {
        if ( sender is ToolStripItem item && item.Tag is int tag )
            this.ShowBorderLines( tag );
    }

    private void OnHelpButtonMouseLeave( object? sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
    }

    private void HelpButton_Click( object? sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
        if ( this._helpMenuDropdown.Items.Count != 0 || this._helpUrlCount != 1 )
            return;
        this.GetHelp( 0 );
    }

    private void ShowDetailsItem_Click( object sender, EventArgs e )
    {
        this.ShowBorderLines( -1 );
        this.ShowDetails();
    }

    #endregion

    #region " nested types "

    /// <summary>   Values that represent beep types. </summary>
    /// <remarks>   2025-06-19. </remarks>
    internal enum BeepType
    {
        /// <summary>   0xFFFFFFFF. </summary>
        Standard = -1,
        /// <summary>   An enum constant representing the default option. </summary>
        Default = 0,
        /// <summary>   0x00000010. </summary>
        Hand = 16,
        /// <summary>   0x00000020. </summary>
        Question = 32,
        /// <summary>   0x00000030. </summary>
        Exclamation = 48,
        /// <summary>   0x00000040. </summary>
        Asterisk = 64,
    }

    #endregion
}
