using Microsoft.Win32;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   An exception message box. </summary>
/// <remarks>   2025-06-19. </remarks>
public class ExceptionMessageBox
{
    #region " Construction and cleanup "

    /// <summary>   Default constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    public ExceptionMessageBox()
    {
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="exception">    The exception. </param>
    public ExceptionMessageBox( Exception exception ) => this.Message = exception;

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="text"> The text. </param>
    public ExceptionMessageBox( string text ) => this.Text = text;

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    public ExceptionMessageBox( string text, string caption )
    {
        this.Text = text;
        this.Caption = caption;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="buttons">      The buttons. </param>
    public ExceptionMessageBox( Exception exception, ExceptionMessageBoxButtons buttons )
    {
        this.Message = exception;
        this.Buttons = buttons;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="buttons">  The buttons. </param>
    public ExceptionMessageBox( string text, string caption, ExceptionMessageBoxButtons buttons )
    {
        this.Text = text;
        this.Caption = caption;
        this.Buttons = buttons;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="buttons">      The buttons. </param>
    /// <param name="symbol">       The symbol. </param>
    public ExceptionMessageBox(
      Exception exception,
      ExceptionMessageBoxButtons buttons,
      ExceptionMessageBoxSymbol symbol )
    {
        this.Message = exception;
        this.Buttons = buttons;
        this.Symbol = symbol;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="buttons">  The buttons. </param>
    /// <param name="symbol">   The symbol. </param>
    public ExceptionMessageBox(
      string text,
      string caption,
      ExceptionMessageBoxButtons buttons,
      ExceptionMessageBoxSymbol symbol )
    {
        this.Text = text;
        this.Caption = caption;
        this.Buttons = buttons;
        this.Symbol = symbol;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="exception">        The exception. </param>
    /// <param name="buttons">          The buttons. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="defaultButton">    The default button. </param>
    public ExceptionMessageBox(
      Exception exception,
      ExceptionMessageBoxButtons buttons,
      ExceptionMessageBoxSymbol symbol,
      ExceptionMessageBoxDefaultButton defaultButton )
    {
        this.Message = exception;
        this.Buttons = buttons;
        this.Symbol = symbol;
        this.DefaultButton = defaultButton;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="text">             The text. </param>
    /// <param name="caption">          The caption. </param>
    /// <param name="buttons">          The buttons. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="defaultButton">    The default button. </param>
    public ExceptionMessageBox(
      string text,
      string caption,
      ExceptionMessageBoxButtons buttons,
      ExceptionMessageBoxSymbol symbol,
      ExceptionMessageBoxDefaultButton defaultButton )
    {
        this.Text = text;
        this.Caption = caption;
        this.Buttons = buttons;
        this.Symbol = symbol;
        this.DefaultButton = defaultButton;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="exception">        The exception. </param>
    /// <param name="buttons">          The buttons. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="defaultButton">    The default button. </param>
    /// <param name="options">          The options. </param>
    public ExceptionMessageBox(
      Exception exception,
      ExceptionMessageBoxButtons buttons,
      ExceptionMessageBoxSymbol symbol,
      ExceptionMessageBoxDefaultButton defaultButton,
      ExceptionMessageBoxOptions options )
    {
        this.Message = exception;
        this.Buttons = buttons;
        this.Symbol = symbol;
        this.DefaultButton = defaultButton;
        this.Options = options;
    }

    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="text">             The text. </param>
    /// <param name="caption">          The caption. </param>
    /// <param name="buttons">          The buttons. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="defaultButton">    The default button. </param>
    /// <param name="options">          The options. </param>
    public ExceptionMessageBox(
      string text,
      string caption,
      ExceptionMessageBoxButtons buttons,
      ExceptionMessageBoxSymbol symbol,
      ExceptionMessageBoxDefaultButton defaultButton,
      ExceptionMessageBoxOptions options )
    {
        this.Text = text;
        this.Caption = caption;
        this.Buttons = buttons;
        this.Symbol = symbol;
        this.DefaultButton = defaultButton;
        this.Options = options;
    }

    #endregion

    #region " Properties "

    /// <summary>   Gets or sets the exception to be messaged. </summary>
    /// <value> The message. </value>
    public Exception? Message { get; set; }

    /// <summary>   Gets or sets the caption. </summary>
    /// <value> The caption. </value>
    public string Caption { get; set; } = string.Empty;

    /// <summary>   Gets or sets the text. </summary>
    /// <value> The text. </value>
    public string Text { get; set; } = string.Empty;

    /// <summary>   Gets or sets the help link. </summary>
    /// <value> The help link. </value>
    public string HelpLink { get; set; } = string.Empty;

#pragma warning disable IDE0079 // Remove unnecessary suppression
#pragma warning disable CA1822 // Make static
    /// <summary>   Gets the data. </summary>
    /// <value> The data. </value>
    public IDictionary Data => new InvalidOperationException().Data;
#pragma warning restore CA1822 // Make static
#pragma warning restore IDE0079 // Remove unnecessary suppression

    /// <summary>   Gets or sets the inner exception. </summary>
    /// <value> The inner exception. </value>
    public Exception? InnerException { get; set; }

    /// <summary>   Gets or sets the buttons. </summary>
    /// <value> The buttons. </value>
    public ExceptionMessageBoxButtons Buttons { get; set; }

    /// <summary>   Gets or sets the symbol. </summary>
    /// <value> The symbol. </value>
    public ExceptionMessageBoxSymbol Symbol { get; set; } = ExceptionMessageBoxSymbol.Warning;

    /// <summary>   Gets or sets the custom symbol. </summary>
    /// <value> The custom symbol. </value>
    public Bitmap? CustomSymbol { get; set; }

    /// <summary>   Gets or sets the default button. </summary>
    /// <value> The default button. </value>
    public ExceptionMessageBoxDefaultButton DefaultButton { get; set; }

    /// <summary>   Gets or sets options for controlling the operation. </summary>
    /// <value> The options. </value>
    public ExceptionMessageBoxOptions Options { get; set; }

    /// <summary>   Gets or sets the message level default. </summary>
    /// <value> The message level default. </value>
    public int MessageLevelDefault { get; set; } = -1;

    /// <summary>   Gets or sets a value indicating whether the tool bar is shown. </summary>
    /// <value> True if show tool bar, false if not. </value>
    public bool ShowToolBar { get; set; } = true;

    /// <summary>   Gets or sets a value indicating whether this object use owner font. </summary>
    /// <value> True if use owner font, false if not. </value>
    public bool UseOwnerFont { get; set; }

    private Font _font = SystemFonts.DefaultFont;
    /// <summary>   Gets or sets the font. </summary>
    /// <value> The font. </value>
    public Font Font
    {
        get => this._font;
        set
        {
            this._font = value;
            this.UseOwnerFont = false;
        }
    }

    /// <summary>   Gets or sets a value indicating whether the check box is shown. </summary>
    /// <value> True if show check box, false if not. </value>
    public bool ShowCheckBox { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this object is check box checked.
    /// </summary>
    /// <value> True if this object is check box checked, false if not. </value>
    public bool IsCheckBoxChecked { get; set; }

    /// <summary>   Gets or sets the check box text. </summary>
    /// <value> The check box text. </value>
    public string CheckBoxText { get; set; } = string.Empty;

    /// <summary>   Gets or sets the check box registry key. </summary>
    /// <value> The check box registry key. </value>
    public RegistryKey? CheckBoxRegistryKey { get; set; }

    /// <summary>   Gets or sets the check box registry value. </summary>
    /// <value> The check box registry value. </value>
    public string CheckBoxRegistryValue { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the check box registry means do not show dialog.
    /// </summary>
    /// <value> True if check box registry means do not show dialog, false if not. </value>
    public bool CheckBoxRegistryMeansDoNotShowDialog { get; set; } = true;

    /// <summary>   Gets or sets the default dialog result. </summary>
    /// <value> The default dialog result. </value>
    public DialogResult DefaultDialogResult { get; set; } = DialogResult.OK;

    /// <summary>   Gets the custom dialog result. </summary>
    /// <value> The custom dialog result. </value>
    public ExceptionMessageBoxDialogResult CustomDialogResult { get; private set; }

    /// <summary>   Gets the ok button text. </summary>
    /// <value> The ok button text. </value>
    public static string OKButtonText => NewMessageBoxSR.OKButton;

    /// <summary>   Gets the cancel button text. </summary>
    /// <value> The cancel button text. </value>
    public static string CancelButtonText => NewMessageBoxSR.CancelButton;

    /// <summary>   Gets the yes button text. </summary>
    /// <value> The yes button text. </value>
    public static string YesButtonText => NewMessageBoxSR.YesButton;

    /// <summary>   Gets the no button text. </summary>
    /// <value> The no button text. </value>
    public static string NoButtonText => NewMessageBoxSR.NoButton;

    /// <summary>   Gets the abort button text. </summary>
    /// <value> The abort button text. </value>
    public static string AbortButtonText => NewMessageBoxSR.AbortButton;

    /// <summary>   Gets the retry button text. </summary>
    /// <value> The retry button text. </value>
    public static string RetryButtonText => NewMessageBoxSR.RetryButton;

    /// <summary>   Gets the fail button text. </summary>
    /// <value> The fail button text. </value>
    public static string FailButtonText => NewMessageBoxSR.FailButton;

    /// <summary>   Gets the ignore button text. </summary>
    /// <value> The ignore button text. </value>
    public static string IgnoreButtonText => NewMessageBoxSR.IgnoreButton;

    /// <summary>   Gets or sets a value indicating whether beep is enabled. </summary>
    /// <value> True if beep is enabled, false if not. </value>
    public bool Beep { get; set; } = true;

    #endregion

    #region " show construction "

    private int _buttonCount;
    private readonly string[] _buttonTextArray = new string[5];
    /// <summary>   Sets button text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="button1Text">  The button 1 text. </param>
    /// <param name="button2Text">  The button 2 text. </param>
    /// <param name="button3Text">  The button 3 text. </param>
    /// <param name="button4Text">  The button 4 text. </param>
    /// <param name="button5Text">  The button 5 text. </param>
    public void SetButtonText(
      string button1Text,
      string? button2Text,
      string? button3Text,
      string? button4Text,
      string? button5Text )
    {
        this._buttonTextArray[0] = button1Text ?? "";
        this._buttonTextArray[1] = button2Text ?? "";
        this._buttonTextArray[2] = button3Text ?? "";
        this._buttonTextArray[3] = button4Text ?? "";
        this._buttonTextArray[4] = button5Text ?? "";
        if ( string.IsNullOrWhiteSpace( button1Text ) )
            this._buttonCount = 0;
        else if ( string.IsNullOrWhiteSpace( button2Text ) )
            this._buttonCount = 1;
        else if ( string.IsNullOrWhiteSpace( button3Text ) )
            this._buttonCount = 2;
        else if ( string.IsNullOrWhiteSpace( button4Text ) )
            this._buttonCount = 3;
        else if ( string.IsNullOrWhiteSpace( button5Text ) )
            this._buttonCount = 4;
        else
            this._buttonCount = 5;
    }

    /// <summary>   Sets button text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="button1Text">  The button 1 text. </param>
    /// <param name="button2Text">  The button 2 text. </param>
    /// <param name="button3Text">  The button 3 text. </param>
    /// <param name="button4Text">  The button 4 text. </param>
    public void SetButtonText(
      string button1Text,
      string button2Text,
      string button3Text,
      string button4Text )
    {
        this.SetButtonText( button1Text, button2Text, button3Text, button4Text, null );
    }

    /// <summary>   Sets button text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="button1Text">  The button 1 text. </param>
    /// <param name="button2Text">  The button 2 text. </param>
    /// <param name="button3Text">  The button 3 text. </param>
    public void SetButtonText( string button1Text, string button2Text, string button3Text )
    {
        this.SetButtonText( button1Text, button2Text, button3Text, null, null );
    }

    /// <summary>   Sets button text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="button1Text">  The button 1 text. </param>
    /// <param name="button2Text">  The button 2 text. </param>
    public void SetButtonText( string button1Text, string button2Text )
    {
        this.SetButtonText( button1Text, button2Text, null, null, null );
    }

    /// <summary>   Sets button text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="button1Text">  The button 1 text. </param>
    public void SetButtonText( string button1Text )
    {
        this.SetButtonText( button1Text, null, null, null, null );
    }

    /// <summary>   Shows the given owner. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="hwnd">             The hwnd. </param>
    /// <param name="message">          The message. </param>
    /// <param name="source">           Source for the. </param>
    /// <param name="sourceAppName">    Name of the source application. </param>
    /// <param name="sourceAppVersion"> Source application version. </param>
    /// <param name="sourceModule">     Source module. </param>
    /// <param name="sourceMessageId">  Identifier for the source message. </param>
    /// <param name="sourceLanguage">   Source language. </param>
    /// <returns>   A DialogResult. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0060:Remove unused parameter", Justification = "<Pending>" )]
    public DialogResult Show(
      IntPtr hwnd,
      string message,
      string source,
      string sourceAppName,
      string sourceAppVersion,
      string sourceModule,
      string sourceMessageId,
      string sourceLanguage )
    {
        ExceptionMessageBoxParent owner = new( hwnd );
        this.Message = new InvalidOperationException( message )
        {
            Source = source
        };
        return this.Show( owner );
    }

    /// <summary>   Shows the given owner. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <exception cref="ArgumentNullException">        Thrown when one or more required arguments
    ///                                                 are null. </exception>
    /// <exception cref="InvalidEnumArgumentException"> Thrown when an Invalid Enum Argument error
    ///                                                 condition occurs. </exception>
    /// <exception cref="Exception">                    Thrown when an exception error condition
    ///                                                 occurs. </exception>
    /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
    ///                                                 the required range. </exception>
    /// <param name="owner">    The owner. </param>
    /// <returns>   A DialogResult. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Usage", "CA2208:Instantiate argument exceptions correctly", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "<Pending>" )]
    public DialogResult Show( IWin32Window? owner )
    {
        if ( this.Message is null && string.IsNullOrWhiteSpace( this.Text ) )
            throw new InvalidOperationException( $"{nameof( this.Message )} && {nameof( this.Text )} must not be null or having an empty message text." );
        if ( this.Buttons is < ExceptionMessageBoxButtons.OK or > ExceptionMessageBoxButtons.Custom )
            throw new InvalidEnumArgumentException( "Buttons", ( int ) this.Buttons, typeof( ExceptionMessageBoxButtons ) );
        if ( this.Symbol is < ExceptionMessageBoxSymbol.None or > ExceptionMessageBoxSymbol.Hand )
            throw new InvalidEnumArgumentException( "Symbol", ( int ) this.Symbol, typeof( ExceptionMessageBoxSymbol ) );
        if ( this.DefaultButton is < ExceptionMessageBoxDefaultButton.Button1 or > ExceptionMessageBoxDefaultButton.Button5 )
            throw new InvalidEnumArgumentException( "DefaultButton", ( int ) this.DefaultButton, typeof( ExceptionMessageBoxDefaultButton ) );
        if ( (this.Options & ~(ExceptionMessageBoxOptions.RightAlign | ExceptionMessageBoxOptions.RtlReading)) != ExceptionMessageBoxOptions.None )
            throw new InvalidEnumArgumentException( "Options", ( int ) this.Options, typeof( ExceptionMessageBoxOptions ) );
        if ( this.Buttons == ExceptionMessageBoxButtons.Custom && this._buttonCount == 0 )
            throw new InvalidEnumArgumentException( ExceptionMessageBoxErrorSR.CustomButtonTextError );
        if ( this.MessageLevelDefault is not (-1) and < 1 )
            throw new ArgumentOutOfRangeException( "MessageLevelDefault", this.MessageLevelDefault, ExceptionMessageBoxErrorSR.MessageLevelCountError );
        bool hasError = this.Message is not null;
        if ( this.Message is null )
        {
            this.Message = new InvalidOperationException( this.Text, this.InnerException )
            {
                HelpLink = this.HelpLink
            };
            foreach ( DictionaryEntry dictionaryEntry in this.Data )
                this.Message.Data.Add( dictionaryEntry.Key, dictionaryEntry.Value );
        }
        if ( this.UseOwnerFont )
        {
            try
            {
                switch ( owner )
                {
                    case Form:
                        this.Font = (( Control ) owner).Font;
                        break;
                    case UserControl:
                        this.Font = (( Control ) owner).Font;
                        break;
                    case Control:
                        this.Font = (( Control ) owner).Font;
                        break;
                    default:
                        break;
                }
            }
            catch ( Exception )
            {
            }
        }
        if ( this.ShowCheckBox )
        {
            if ( this.CheckBoxRegistryKey is not null )
            {
                try
                {
                    this.IsCheckBoxChecked = ( int ) this.CheckBoxRegistryKey.GetValue( this.CheckBoxRegistryValue, 0 ) != 0;
                    if ( this.CheckBoxRegistryMeansDoNotShowDialog )
                    {
                        if ( this.IsCheckBoxChecked )
                            return this.DefaultDialogResult;
                    }
                }
                catch ( Exception )
                {
                }
            }
        }
        if ( this.Caption is null || this.Caption.Length == 0 )
            this.Caption = this.Message.Source ?? "";
        if ( (this.Caption is null || this.Caption.Length == 0) && owner is Form )
            this.Caption = (( Control ) owner).Text;
        DialogResult dialogResult;
        using ( ExceptionMessageBoxForm exceptionMessageBoxForm = new() )
        {
            exceptionMessageBoxForm.SetButtonText( this._buttonTextArray );
            exceptionMessageBoxForm.Buttons = this.Buttons;
            exceptionMessageBoxForm.Caption = this.Caption ?? string.Empty;
            exceptionMessageBoxForm.Message = this.Message;
            exceptionMessageBoxForm.Symbol = this.Symbol;
            exceptionMessageBoxForm.DefaultButton = this.DefaultButton;
            exceptionMessageBoxForm.Options = this.Options;
            exceptionMessageBoxForm.DoBeep = this.Beep;
            exceptionMessageBoxForm.CheckBoxText = this.CheckBoxText;
            exceptionMessageBoxForm.IsCheckBoxChecked = this.IsCheckBoxChecked;
            exceptionMessageBoxForm.ShowCheckBox = this.ShowCheckBox;
            exceptionMessageBoxForm.MessageLevelCount = this.MessageLevelDefault;
            exceptionMessageBoxForm.ShowHelpButton = this.ShowToolBar;
            exceptionMessageBoxForm.OnCopyToClipboardInternal += new CopyToClipboardEventHandler( this.OnCopyToClipboardEventInternal );
            if ( this.CustomSymbol is not null )
                exceptionMessageBoxForm.CustomSymbol = this.CustomSymbol;
            if ( this._font is not null )
                exceptionMessageBoxForm.Font = this._font;
            if ( owner is null )
            {
                exceptionMessageBoxForm.StartPosition = FormStartPosition.CenterScreen;
                exceptionMessageBoxForm.ShowInTaskbar = true;
            }
            else
                exceptionMessageBoxForm.StartPosition = FormStartPosition.CenterParent;
            exceptionMessageBoxForm.Shown += ( sender, e ) =>
            {
                if ( hasError )
                {
                    Trace.TraceError( "ExceptionMessageBoxShown@" + this.Message.Message );
                    Trace.Write( this.Message );
                }
                else
                    Trace.TraceInformation( "ExceptionMessageBoxShown@" + this.Message.Message );
            };
            exceptionMessageBoxForm.FormClosed += ( sender, e ) => Trace.TraceInformation( "ExceptionMessageBoxClosed@" + this.Message.Message );
            exceptionMessageBoxForm.PrepareToShow();
            dialogResult = exceptionMessageBoxForm.ShowDialog( owner );
            if ( exceptionMessageBoxForm.ShowCheckBox && this.CheckBoxRegistryKey is not null )
                this.CheckBoxRegistryKey.SetValue( this.CheckBoxRegistryValue, exceptionMessageBoxForm.IsCheckBoxChecked ? 1 : 0 );
            this.IsCheckBoxChecked = exceptionMessageBoxForm.IsCheckBoxChecked;
            this.CustomDialogResult = exceptionMessageBoxForm.CustomDialogResult;
        }
        return dialogResult;
    }

    #endregion

    #region " copy handler "

    private void OnCopyToClipboardEventInternal( object sender, CopyToClipboardEventArgs e )
    {
        if ( this.OnCopyToClipboard is null )
            return;
        this.OnCopyToClipboard( this, e );
    }

    /// <summary>   Event queue for all listeners interested in OnCopyToClipboard events. </summary>
    public event CopyToClipboardEventHandler? OnCopyToClipboard;

    /// <summary>   Gets message text. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <returns>   The message text. </returns>
    public static string GetMessageText( Exception exception )
    {
        using ExceptionMessageBoxForm exceptionMessageBoxForm = new();
        exceptionMessageBoxForm.Message = exception;
        return exceptionMessageBoxForm.BuildMessageText( false );
    }

    #endregion
}
