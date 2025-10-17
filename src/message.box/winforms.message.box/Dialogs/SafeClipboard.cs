using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary> Safe clipboard set data object. </summary>
/// <remarks>
/// David, 2015-03-24
/// http://StackOverflow.com/questions/899350/how-to-copy-the-contents-of-a-string-to-the-clipboard-in-c.
/// </remarks>
/// <remarks> Constructor. </remarks>
/// <remarks> David, 202-09-12. </remarks>
/// <param name="format"> Describes the <see cref="DataFormats">format</see> of the specified data. </param>
/// <param name="data">   The data. </param>
internal sealed class SafeClipboardSetDataObject( string format, object data ) : SingleThreadApartmentBase()

{
    /// <summary> Describes the format to use. </summary>
    private readonly string _format = format;

    /// <summary> The data. </summary>
    private readonly object _data = data;

    /// <summary> Implemented in this class to do actual work. </summary>
    /// <remarks> David, 202-09-12. </remarks>
    protected override void Work()
    {
        object obj = new DataObject( this._format, this._data );
        Clipboard.SetDataObject( obj, true );
    }

    /// <summary> Copies text to the clipboard. </summary>
    /// <remarks> David, 202-09-12. </remarks>
    /// <param name="text"> The text. </param>
    public static void SetDataObject( string text )
    {
        SafeClipboardSetDataObject scr = new( DataFormats.Text, text );
        scr.Go();
    }
}
