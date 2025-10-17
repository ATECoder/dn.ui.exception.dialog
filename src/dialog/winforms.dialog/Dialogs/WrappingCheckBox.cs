using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   A wrapping check box. </summary>
/// <remarks>   2025-06-19. </remarks>
internal sealed class WrappingCheckBox : CheckBox
{
    /// <summary>   Default constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    public WrappingCheckBox() => this.AutoSize = true;

    /// <summary>
    /// Raises the <see cref="System.Windows.Forms.Control.TextChanged" /> event.
    /// </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="e">    An <see cref="System.EventArgs" /> that contains the event data. </param>
    protected override void OnTextChanged( EventArgs e )
    {
        base.OnTextChanged( e );
        this.CacheTextSize();
    }

    /// <summary>
    /// Raises the <see cref="System.Windows.Forms.Control.FontChanged" /> event.
    /// </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="e">    An <see cref="System.EventArgs" /> that contains the event data. </param>
    protected override void OnFontChanged( EventArgs e )
    {
        base.OnFontChanged( e );
        this.CacheTextSize();
    }

    private Size _cachedSizeOfOneLineOfText = Size.Empty;

    private void CacheTextSize()
    {
        this._preferredSizeHash.Clear();
        if ( string.IsNullOrEmpty( this.Text ) )
            this._cachedSizeOfOneLineOfText = Size.Empty;
        else
            this._cachedSizeOfOneLineOfText = TextRenderer.MeasureText( this.Text, this.Font, new Size( int.MaxValue, int.MaxValue ), TextFormatFlags.WordBreak );
    }

    private readonly Hashtable _preferredSizeHash = new( 3 );
    /// <summary>
    /// Retrieves the size of a rectangular area into which a control can be fitted.
    /// </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="proposedSize"> The custom-sized area for a control. </param>
    /// <returns>
    /// An ordered pair of type <see cref="System.Drawing.Size" /> representing the width and
    /// height of a rectangle.
    /// </returns>
    public override Size GetPreferredSize( Size proposedSize )
    {
        Size preferredSize = base.GetPreferredSize( proposedSize );
        if ( preferredSize.Width > proposedSize.Width )
        {
            int num;
            if ( !string.IsNullOrEmpty( this.Text ) )
            {
                num = proposedSize.Width;
                if ( !num.Equals( int.MaxValue ) )
                    goto label_4;
            }
            num = proposedSize.Height;
            if ( num.Equals( int.MaxValue ) )
                goto label_7;
            label_4:
            Size size1 = preferredSize - this._cachedSizeOfOneLineOfText;
            Size size2 = proposedSize - size1 - new Size( 3, 0 );
            if ( !this._preferredSizeHash.ContainsKey( size2 ) )
            {
                preferredSize = size1 + TextRenderer.MeasureText( this.Text, this.Font, size2, TextFormatFlags.WordBreak );
                this._preferredSizeHash[size2] = preferredSize;
            }
            else
            {
                if ( this._preferredSizeHash[size2] is Size cachedPreferredSize )
                    preferredSize = cachedPreferredSize;
            }
        }
    label_7:
        return preferredSize;
    }
}
