using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace cc.isr.WinForms.Dialogs;
/// <summary>
/// Encapsulates the <see cref="ExceptionMessageBox"/> exception
/// message box.
/// </summary>
/// <remarks>   David, 2019-01-17. </remarks>
public class MessageBox : ExceptionMessageBox
{
    /// <summary>   Default constructor. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    public MessageBox() => this.OnCopyToClipboard += this.HandleCopyToClipboard;

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    public MessageBox( string text, string caption ) : base( text, caption ) => this.OnCopyToClipboard += this.HandleCopyToClipboard;

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    public MessageBox( Exception exception ) : base( exception ) => this.OnCopyToClipboard += this.HandleCopyToClipboard;

    /// <summary>   Handles the copy to clipboard. </summary>
    /// <remarks>   David, 2021-03-11. </remarks>
    /// <param name="sender">   Source of the event. </param>
    /// <param name="e">        Copy to clipboard event information. </param>
    private void HandleCopyToClipboard( object? sender, CopyToClipboardEventArgs e )
    {
        SafeClipboardSetDataObject.SetDataObject( e.ClipboardText );
        e.EventHandled = true;
    }

    /// <summary>   Try show. </summary>
    /// <remarks>   2025-06-20. </remarks>
    /// <param name="owner">    The owner. </param>
    /// <returns>   A DialogResult. </returns>
    private DialogResult TryShow( IWin32Window? owner )
    {
        try
        {
            return this.Show( owner );
        }
        catch ( Exception ex )
        {
            // If the message box fails to show, we can still log the exception.
            // This is useful for debugging purposes.
            System.Diagnostics.Debug.WriteLine( $"Failed to show message box:\n\t{ex}" );
            return DialogResult.Abort;
        }
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public DialogResult ShowDialog()
    {
        DialogResult r = DialogResult.Abort;
        System.Threading.Thread oThread;
        oThread = new System.Threading.Thread( new System.Threading.ThreadStart( () => r = this.TryShow( default ) ) );
        oThread.SetApartmentState( System.Threading.ApartmentState.STA );
        oThread.Start();
        oThread.Join();
        while ( oThread.IsAlive )
        {
            System.Threading.Thread.Sleep( 1000 );
        }

        return r;
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <exception cref="ArgumentNullException">        Thrown when one or more required arguments
    ///                                                 are null. </exception>
    /// <exception cref="ArgumentOutOfRangeException">  Thrown when one or more arguments are outside
    ///                                                 the required range. </exception>
    /// <exception cref="InvalidOperationException">    Thrown when the requested operation is
    ///                                                 invalid. </exception>
    /// <param name="buttonText">       The buttons text. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="defaultButton">    The default button. </param>
    /// <param name="dialogResults">    The dialog results corresponding to the
    ///                                 <paramref name="buttonText">buttons</paramref>. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    /// <example>
    /// Example 1: Simple Message
    /// <code>
    /// Dim box As ExceptionMessageBox = New ExceptionMessageBox(exception)
    /// box.InvokeShow(owner, New String(){"A","B"},ExceptionMessageBoxSymbol.Asterisk,
    /// ExceptionMessageBoxDefaultButton.Button1)
    /// </code>
    /// </example>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0010:Add missing cases", Justification = "<Pending>" )]
    public DialogResult ShowDialog( string[] buttonText, ExceptionMessageBoxSymbol symbol, ExceptionMessageBoxDefaultButton defaultButton, DialogResult[] dialogResults )
    {
        if ( buttonText is null )
        {
            throw new ArgumentNullException( nameof( buttonText ) );
        }
        else if ( buttonText.Length is 0 or > 5 )
        {
            throw new ArgumentOutOfRangeException( nameof( buttonText ), "Must have between 1 and 5 values" );
        }
        else if ( dialogResults is null )
        {
            throw new ArgumentNullException( nameof( dialogResults ) );
        }
        else if ( dialogResults.Length is 0 or > 5 )
        {
            throw new ArgumentOutOfRangeException( nameof( dialogResults ), "Must have between 1 and 5 values" );
        }
        else if ( dialogResults.Length != buttonText.Length )
        {
            throw new ArgumentOutOfRangeException( nameof( dialogResults ), "Must have the same count as button text" );
        }

        // Set the names of the custom buttons.
        switch ( buttonText.Length )
        {
            case 1:
                {
                    this.SetButtonText( buttonText[0] );
                    break;
                }

            case 2:
                {
                    this.SetButtonText( buttonText[0], buttonText[1] );
                    break;
                }

            case 3:
                {
                    this.SetButtonText( buttonText[0], buttonText[1], buttonText[2] );
                    break;
                }

            case 4:
                {
                    this.SetButtonText( buttonText[0], buttonText[1], buttonText[2], buttonText[3] );
                    break;
                }

            case 5:
                {
                    this.SetButtonText( buttonText[0], buttonText[1], buttonText[2], buttonText[4] );
                    break;
                }
        }

        this.DefaultButton = defaultButton;
        this.Symbol = symbol;
        this.Buttons = ExceptionMessageBoxButtons.Custom;
        _ = this.ShowDialog();
        switch ( this.CustomDialogResult )
        {
            case ExceptionMessageBoxDialogResult.Button1:
                {
                    return dialogResults[0];
                }

            case ExceptionMessageBoxDialogResult.Button2:
                {
                    return dialogResults[1];
                }

            case ExceptionMessageBoxDialogResult.Button3:
                {
                    return dialogResults[2];
                }

            case ExceptionMessageBoxDialogResult.Button4:
                {
                    return dialogResults[3];
                }

            case ExceptionMessageBoxDialogResult.Button5:
                {
                    return dialogResults[4];
                }

            default:
                {
                    throw new InvalidOperationException(
                        $"Failed displaying the message box with {nameof( cc.isr.WinForms.Dialogs.ExceptionMessageBox.CustomDialogResult )}={this.CustomDialogResult}" );
                }
        }
    }


    #region " static methods "

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialog( Exception exception )
    {
        MessageBox box = new( exception );
        return box.ShowDialog();
    }

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialog( string text, string caption )
    {
        MessageBox box = new( text, caption );
        return box.ShowDialog();
    }

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="exception">        The exception. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="dialogResults">    The dialog results. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialog( Exception exception, ExceptionMessageBoxSymbol symbol, DialogResult[] dialogResults )
    {
#if NET5_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( dialogResults, nameof( dialogResults ) );
#else
        if ( dialogResults is null ) throw new ArgumentNullException( nameof( dialogResults ) );
#endif

        MessageBox box = new( exception );
        List<string> buttonText = [];
        foreach ( DialogResult d in dialogResults )
        {
            buttonText.Add( d.ToString() );
        }

        return box.ShowDialog( [.. buttonText], symbol, ExceptionMessageBoxDefaultButton.Button1, dialogResults );
    }

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="text">             The exception. </param>
    /// <param name="caption">          The caption. </param>
    /// <param name="symbol">           The symbol. </param>
    /// <param name="dialogResults">    The dialog results. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialog( string text, string caption, ExceptionMessageBoxSymbol symbol, DialogResult[] dialogResults )
    {
#if NET5_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( dialogResults, nameof( dialogResults ) );
#else
        if ( dialogResults is null ) throw new ArgumentNullException( nameof( dialogResults ) );
#endif

        MessageBox box = new( text, caption );
        List<string> buttonText = [];
        foreach ( DialogResult d in dialogResults )
        {
            buttonText.Add( d.ToString() );
        }

        return box.ShowDialog( [.. buttonText], symbol, ExceptionMessageBoxDefaultButton.Button1, dialogResults );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="symbol">       The symbol. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialogAbortIgnore( Exception exception, ExceptionMessageBoxSymbol symbol )
    {
        return ShowDialog( exception, symbol, [DialogResult.Abort, DialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialogAbortIgnore( Exception exception )
    {
        return ShowDialog( exception, ExceptionMessageBoxSymbol.Error, [DialogResult.Abort, DialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public static DialogResult ShowDialogAbortIgnore( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        return ShowDialog( text, caption, symbol, [DialogResult.Abort, DialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="symbol">       The symbol. </param>
    /// <returns>
    /// Either <see cref="DialogResult.Ignore">ignore</see> or
    /// <see cref="DialogResult.OK">Okay</see>.
    /// </returns>
    public static DialogResult ShowDialogIgnoreExit( Exception exception, ExceptionMessageBoxSymbol symbol )
    {
        MessageBox box = new( exception );
        return box.ShowDialogIgnoreExit( symbol );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>
    /// Either <see cref="DialogResult.Ignore">ignore</see> or
    /// <see cref="DialogResult.OK">Okay</see>.
    /// </returns>
    public static DialogResult ShowDialogIgnoreExit( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        MessageBox box = new( text, caption );
        return box.ShowDialogIgnoreExit( symbol );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="symbol">       The symbol. </param>
    /// <returns>   <see cref="DialogResult.OK">Okay</see>. </returns>
    public static DialogResult ShowDialogExit( Exception exception, ExceptionMessageBoxSymbol symbol )
    {
        MessageBox box = new( exception );
        return box.ShowDialogExit( symbol );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   <see cref="DialogResult.OK">Okay</see>. </returns>
    public static DialogResult ShowDialogExit( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        MessageBox box = new( text, caption );
        return box.ShowDialogExit( symbol );
    }

    /// <summary>   Shows the okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogOkay( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        MessageBox box = new( text, caption );
        return box.ShowDialogExit( symbol );
    }

    /// <summary>   Shows the okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogOkay( string text, string caption )
    {
        return ShowDialogOkay( text, caption, ExceptionMessageBoxSymbol.Information );
    }

    /// <summary>   Shows the okay/cancel dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogOkayCancel( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        return ShowDialog( text, caption, symbol, [DialogResult.OK, DialogResult.Cancel] );
    }

    /// <summary>   Shows the okay/cancel dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogOkayCancel( string text, string caption )
    {
        return ShowDialogOkayCancel( text, caption, ExceptionMessageBoxSymbol.Information );
    }

    /// <summary>   Shows the Cancel/Okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogCancelOkay( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        return ShowDialog( text, caption, symbol, [DialogResult.Cancel, DialogResult.OK] );
    }

    /// <summary>   Shows the Cancel/Okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogCancelOkay( string text, string caption )
    {
        return ShowDialogCancelOkay( text, caption, ExceptionMessageBoxSymbol.Information );
    }

    /// <summary>   Shows the yes/no dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogYesNo( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        return ShowDialog( text, caption, symbol, [DialogResult.Yes, DialogResult.No] );
    }

    /// <summary>   Shows the yes/no dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogYesNo( string text, string caption )
    {
        return ShowDialogYesNo( text, caption, ExceptionMessageBoxSymbol.Question );
    }

    /// <summary>   Shows the No/Yes dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogNoYes( string text, string caption, ExceptionMessageBoxSymbol symbol )
    {
        return ShowDialog( text, caption, symbol, [DialogResult.No, DialogResult.Yes] );
    }

    /// <summary>   Shows the No/Yes dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static DialogResult ShowDialogNoYes( string text, string caption )
    {
        return ShowDialogNoYes( text, caption, ExceptionMessageBoxSymbol.Question );
    }

    #endregion

    #region " show dialog - predefined buttons "

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   <see cref="DialogResult">Dialog result</see>. </returns>
    public DialogResult ShowDialogAbortIgnore( ExceptionMessageBoxSymbol symbol )
    {
        return this.ShowDialog( ["Abort", "Ignore"], symbol, ExceptionMessageBoxDefaultButton.Button1,
                                [DialogResult.Abort, DialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>
    /// Either <see cref="DialogResult.Ignore">ignore</see> or
    /// <see cref="DialogResult.OK">Okay</see>.
    /// </returns>
    public DialogResult ShowDialogIgnoreExit( ExceptionMessageBoxSymbol symbol )
    {
        return this.ShowDialog( ["Ignore", "Exit"], symbol, ExceptionMessageBoxDefaultButton.Button1,
                                [DialogResult.Ignore, DialogResult.OK] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="symbol">   The symbol. </param>
    /// <returns>   <see cref="DialogResult.OK">Okay</see>. </returns>
    public DialogResult ShowDialogExit( ExceptionMessageBoxSymbol symbol )
    {
        return this.ShowDialog( ["Exit"], symbol, ExceptionMessageBoxDefaultButton.Button1,
                                [DialogResult.OK] );
    }

    #endregion

}
