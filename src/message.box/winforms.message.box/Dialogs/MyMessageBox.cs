using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;
/// <summary>
/// Extends the <see cref="ExceptionMessageBox">Exception message dialog</see>.
/// </summary>
/// <remarks>   David, 2014-02-14. </remarks>
public class MyMessageBox
{
    #region " construction "

    /// <summary>   Gets the exception message box. </summary>
    /// <value> The exception message box. </value>
    private MessageBox ExceptionMessageBox { get; }

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    public MyMessageBox( Exception exception ) : base() => this.ExceptionMessageBox = new MessageBox( exception );

    /// <summary>   Constructor. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    public MyMessageBox( string text, string caption ) : base() => this.ExceptionMessageBox = new MessageBox( text, caption );

    #endregion

    #region " static methods "

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialog( Exception exception )
    {
        return MessageBox.ShowDialog( exception ).ToDialogResult();
    }

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialog( string text, string caption )
    {
        return MessageBox.ShowDialog( text, caption ).ToDialogResult();
    }

    /// <summary>
    /// Synchronously Invokes the exception display on the apartment thread to permit using the
    /// clipboard.
    /// </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="exception">        The exception. </param>
    /// <param name="icon">             The icon. </param>
    /// <param name="dialogResults">    The dialog results. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialog( Exception exception, MyMessageBoxIcon icon, MyDialogResult[] dialogResults )
    {
        return MessageBox.ShowDialog( exception, icon.ToSymbol(), dialogResults.ToDialogResults() ).ToDialogResult();
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
    /// <param name="icon">             The icon. </param>
    /// <param name="dialogResults">    The dialog results. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialog( string text, string caption, MyMessageBoxIcon icon, MyDialogResult[] dialogResults )
    {
        return MessageBox.ShowDialog( text, caption, icon.ToSymbol(), dialogResults.ToDialogResults() ).ToDialogResult();
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="icon">         The icon. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialogAbortIgnore( Exception exception, MyMessageBoxIcon icon )
    {
        return ShowDialog( exception, icon, [MyDialogResult.Abort, MyDialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialogAbortIgnore( Exception exception )
    {
        return ShowDialog( exception, MyMessageBoxIcon.Error, [MyDialogResult.Abort, MyDialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public static MyDialogResult ShowDialogAbortIgnore( string text, string caption, MyMessageBoxIcon icon )
    {
        return ShowDialog( text, caption, icon, [MyDialogResult.Abort, MyDialogResult.Ignore] );
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="icon">         The icon. </param>
    /// <returns>
    /// Either <see cref="MyDialogResult.Ignore">ignore</see> or
    /// <see cref="MyDialogResult.Ok">Okay</see>.
    /// </returns>
    public static MyDialogResult ShowDialogIgnoreExit( Exception exception, MyMessageBoxIcon icon )
    {
        return MessageBox.ShowDialogIgnoreExit( exception, icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>
    /// Either <see cref="MyDialogResult.Ignore">ignore</see> or
    /// <see cref="MyDialogResult.Ok">Okay</see>.
    /// </returns>
    public static MyDialogResult ShowDialogIgnoreExit( string text, string caption, MyMessageBoxIcon icon )
    {
        return MessageBox.ShowDialogIgnoreExit( text, caption, icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <param name="icon">         The icon. </param>
    /// <returns>   <see cref="MyDialogResult.Ok">Okay</see>. </returns>
    public static MyDialogResult ShowDialogExit( Exception exception, MyMessageBoxIcon icon )
    {
        return MessageBox.ShowDialogExit( exception, icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   <see cref="MyDialogResult.Ok">Okay</see>. </returns>
    public static MyDialogResult ShowDialogExit( string text, string caption, MyMessageBoxIcon icon )
    {
        return MessageBox.ShowDialogExit( text, caption, icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Shows the okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogOkay( string text, string caption, MyMessageBoxIcon icon )
    {
        return MessageBox.ShowDialogOkay( text, caption, icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Shows the okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogOkay( string text, string caption )
    {
        return MessageBox.ShowDialogOkay( text, caption ).ToDialogResult();
    }

    /// <summary>   Shows the okay/cancel dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogOkayCancel( string text, string caption, MyMessageBoxIcon icon )
    {
        return ShowDialog( text, caption, icon, [MyDialogResult.Ok, MyDialogResult.Cancel] );
    }

    /// <summary>   Shows the okay/cancel dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogOkayCancel( string text, string caption )
    {
        return ShowDialogOkayCancel( text, caption, MyMessageBoxIcon.Information );
    }

    /// <summary>   Shows the Cancel/Okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogCancelOkay( string text, string caption, MyMessageBoxIcon icon )
    {
        return ShowDialog( text, caption, icon, [MyDialogResult.Cancel, MyDialogResult.Ok] );
    }

    /// <summary>   Shows the Cancel/Okay dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogCancelOkay( string text, string caption )
    {
        return ShowDialogCancelOkay( text, caption, MyMessageBoxIcon.Information );
    }

    /// <summary>   Shows the yes/no dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogYesNo( string text, string caption, MyMessageBoxIcon icon )
    {
        return ShowDialog( text, caption, icon, [MyDialogResult.Yes, MyDialogResult.No] );
    }

    /// <summary>   Shows the yes/no dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogYesNo( string text, string caption )
    {
        return ShowDialogYesNo( text, caption, MyMessageBoxIcon.Question );
    }

    /// <summary>   Shows the No/Yes dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <param name="icon">     The icon. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogNoYes( string text, string caption, MyMessageBoxIcon icon )
    {
        return ShowDialog( text, caption, icon, [MyDialogResult.No, MyDialogResult.Yes] );
    }

    /// <summary>   Shows the No/Yes dialog. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="text">     The text. </param>
    /// <param name="caption">  The caption. </param>
    /// <returns>   A DialogResult. </returns>
    public static MyDialogResult ShowDialogNoYes( string text, string caption )
    {
        return ShowDialogNoYes( text, caption, MyMessageBoxIcon.Question );
    }

    #endregion

    #region " show dialog "

    /// <summary>   Shows the exception display. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    /// <example>   Example 1: Simple Message <code>
    ///             Dim box As MyMessageBox = New MyMessageBox(message)
    ///             me.ShowDialog(owner)      </code></example>
    /// <example>   Example 2: Message box with exception message. <code>
    ///             Dim box As MyMessageBox = New MyMessageBox(exception)
    ///             me.ShowDialog(owner)      </code></example>
    public MyDialogResult ShowDialog()
    {
        return this.ExceptionMessageBox.ShowDialog().ToDialogResult();
    }

    #endregion

    #region " show dialog - predefined buttons "

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="icon"> The icon. </param>
    /// <returns>   <see cref="MyDialogResult">Dialog result</see>. </returns>
    public MyDialogResult ShowDialogAbortIgnore( MyMessageBoxIcon icon )
    {
        return this.ExceptionMessageBox.ShowDialogAbortIgnore( icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="icon"> The icon. </param>
    /// <returns>
    /// Either <see cref="MyDialogResult.Ignore">ignore</see> or
    /// <see cref="MyDialogResult.Ok">Okay</see>.
    /// </returns>
    public MyDialogResult ShowDialogIgnoreExit( MyMessageBoxIcon icon )
    {
        return this.ExceptionMessageBox.ShowDialogIgnoreExit( icon.ToSymbol() ).ToDialogResult();
    }

    /// <summary>   Displays the message box. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="icon"> The icon. </param>
    /// <returns>   <see cref="MyDialogResult.Ok">Okay</see>. </returns>
    public MyDialogResult ShowDialogExit( MyMessageBoxIcon icon )
    {
        return this.ExceptionMessageBox.ShowDialogExit( icon.ToSymbol() ).ToDialogResult();
    }

    #endregion

}
/// <summary>   Values that represent dialog results. </summary>
/// <remarks>   David, 202-09-12. </remarks>
public enum MyDialogResult
{
    /// <summary> An enum constant representing the none option. </summary>
    None = System.Windows.Forms.DialogResult.None,

    /// <summary> An enum constant representing the ok option. </summary>
    Ok = System.Windows.Forms.DialogResult.OK,

    /// <summary> An enum constant representing the cancel option. </summary>
    Cancel = System.Windows.Forms.DialogResult.Cancel,

    /// <summary> An enum constant representing the abort option. </summary>
    Abort = System.Windows.Forms.DialogResult.Abort,

    /// <summary> An enum constant representing the retry option. </summary>
    Retry = System.Windows.Forms.DialogResult.Retry,

    /// <summary> An enum constant representing the ignore option. </summary>
    Ignore = System.Windows.Forms.DialogResult.Ignore,

    /// <summary> An enum constant representing the yes option. </summary>
    Yes = System.Windows.Forms.DialogResult.Yes,

    /// <summary> An enum constant representing the no option. </summary>
    No = System.Windows.Forms.DialogResult.No
}
/// <summary>   Values that represent my message box icons. </summary>
/// <remarks>   David, 202-09-12. </remarks>
public enum MyMessageBoxIcon
{
    /// <summary> An enum constant representing the none option. </summary>
    None = System.Windows.Forms.MessageBoxIcon.None,

    /// <summary> An enum constant representing the asterisk option. </summary>
    Asterisk = System.Windows.Forms.MessageBoxIcon.Asterisk,

    /// <summary> An enum constant representing the error] option. </summary>
    Error = System.Windows.Forms.MessageBoxIcon.Error,

    /// <summary> An enum constant representing the exclamation option. </summary>
    Exclamation = System.Windows.Forms.MessageBoxIcon.Exclamation,

    /// <summary> An enum constant representing the hand option. </summary>
    /// <summary>   . </summary>
#pragma warning disable CA1069
    Hand = System.Windows.Forms.MessageBoxIcon.Hand,
#pragma warning restore CA1069

    /// <summary> An enum constant representing the information option. </summary>
    /// <summary>   An enum constant representing the information option. </summary>
#pragma warning disable CA1069
    Information = System.Windows.Forms.MessageBoxIcon.Information,
#pragma warning restore CA1069

    /// <summary> An enum constant representing the question option. </summary>
    Question = System.Windows.Forms.MessageBoxIcon.Question,

    /// <summary> An enum constant representing the stop] option. </summary>
    /// <summary>   An enum constant representing the stop option. </summary>
#pragma warning disable CA1069
    Stop = System.Windows.Forms.MessageBoxIcon.Stop,
#pragma warning restore CA1069

    /// <summary> An enum constant representing the warning option. </summary>
    /// <summary>   An enum constant representing the warning option. </summary>
#pragma warning disable CA1069
    Warning = System.Windows.Forms.MessageBoxIcon.Warning
#pragma warning restore CA1069
}
/// <summary>   A dialog extension methods. </summary>
/// <remarks>   David, 2021-03-11. </remarks>
internal static class DialogMethods
{
    /// <summary>   Converts a value to a dialog result. </summary>
    /// <remarks>   David, 2021-03-11. </remarks>
    /// <param name="value">    The value. </param>
    /// <returns>   Value as a DialogResult. </returns>
    internal static DialogResult ToDialogResult( this MyDialogResult value )
    {
        return ( DialogResult ) ( int ) value;
    }

    /// <summary>
    /// A MyDialogResult[] extension method that converts the values to a dialog results.
    /// </summary>
    /// <remarks>   David, 2021-03-11. </remarks>
    /// <param name="values">   The values to act on. </param>
    /// <returns>   Values as a DialogResult[]. </returns>
    internal static DialogResult[] ToDialogResults( this MyDialogResult[] values )
    {
        List<DialogResult> results = [];
        foreach ( MyDialogResult dr in values )
        {
            results.Add( dr.ToDialogResult() );
        }
        return [.. results];
    }

    /// <summary>   Converts a value to a dialog result. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="value">    The value. </param>
    /// <returns>   Value as a DialogResult. </returns>
    internal static MyDialogResult ToDialogResult( this DialogResult value )
    {
        return ( MyDialogResult ) ( int ) value;
    }

    /// <summary>   Converts a value to a message box icon. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="value">    The value. </param>
    /// <returns>   Value as a MessageBoxIcon. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Code Quality", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private static MyMessageBoxIcon ToMessageBoxIcon( this MessageBoxIcon value )
    {
        return ( MyMessageBoxIcon ) ( int ) value;
    }

    /// <summary>   The lazy my icon symbols. </summary>
    private static readonly Lazy<Dictionary<MyMessageBoxIcon, ExceptionMessageBoxSymbol>> _lazyMyIconSymbols = new( BuildBoxMyIconSymbolHash );

    /// <summary>   Builds message box icon symbol hash. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <returns>
    /// A <see cref="Dictionary{MyMessageBoxIcon, ExceptionMessageBoxSymbol}">dictionary</see>.
    /// </returns>
    private static Dictionary<MyMessageBoxIcon, ExceptionMessageBoxSymbol> BuildBoxMyIconSymbolHash()
    {
        Dictionary<MyMessageBoxIcon, ExceptionMessageBoxSymbol> dix2 = [];
        Dictionary<MyMessageBoxIcon, ExceptionMessageBoxSymbol> dix3 = dix2;
        // same as information: dix3.Add(MessageBoxIcon.Asterisk, ExceptionMessageBoxSymbol.Asterisk)
        // same as warning: dix3.Add(MessageBoxIcon.Exclamation, ExceptionMessageBoxSymbol.Exclamation)
        dix3.Add( MyMessageBoxIcon.Error, ExceptionMessageBoxSymbol.Error );
        // same as error: dix3.Add(MessageBoxIcon.Hand, ExceptionMessageBoxSymbol.Hand)
        dix3.Add( MyMessageBoxIcon.Information, ExceptionMessageBoxSymbol.Information );
        dix3.Add( MyMessageBoxIcon.None, ExceptionMessageBoxSymbol.None );
        dix3.Add( MyMessageBoxIcon.Question, ExceptionMessageBoxSymbol.Question );
        // same as error: dix3.Add(MessageBoxIcon.Stop, ExceptionMessageBoxSymbol.Stop)
        dix3.Add( MyMessageBoxIcon.Warning, ExceptionMessageBoxSymbol.Warning );
        return dix2;
    }

    /// <summary>   my icon symbols. </summary>
    private static readonly Dictionary<MyMessageBoxIcon, ExceptionMessageBoxSymbol> _myIconSymbols = _lazyMyIconSymbols.Value;

    /// <summary>   Converts a value to a symbol. </summary>
    /// <remarks>   David, 202-09-12. </remarks>
    /// <param name="value">    The value. </param>
    /// <returns>   Symbol. </returns>
    public static ExceptionMessageBoxSymbol ToSymbol( this MyMessageBoxIcon value )
    {
        return _myIconSymbols.TryGetValue( value, out ExceptionMessageBoxSymbol symbol )
            ? symbol
            : ExceptionMessageBoxSymbol.Information;
    }

}
