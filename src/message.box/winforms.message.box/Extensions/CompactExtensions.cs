using System;
using System.Drawing;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs.CompactExtensions;

/// <summary> Includes extensions for <see cref="string">String</see> Compacting. </summary>
/// <remarks> (c) 2009 Integrated Scientific Resources, Inc. All rights reserved. <para>
/// Licensed under The MIT License.</para><para>
/// David, 2009-04-09, 1.1.3386 </para></remarks>
internal static class CompactMethods
{
    /// <summary>
    /// Returns a substring of the input string taking not of start index and length exceptions.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <param name="value">      The input string to sub string. </param>
    /// <param name="startIndex"> The zero based starting index. </param>
    /// <param name="length">     The total length. </param>
    /// <returns>
    /// A substring of the input string taking not of start index and length exceptions.
    /// </returns>
    public static string SafeSubstring( this string value, int startIndex, int length )
    {
        if ( string.IsNullOrWhiteSpace( value ) || length <= 0 )
        {
            return string.Empty;
        }

        int inputLength = value.Length;
        if ( startIndex > inputLength )
        {
            return string.Empty;
        }
        else
        {
            if ( startIndex + length > inputLength )
            {
                length = inputLength - startIndex;
            }

            return value.Substring( startIndex, length );
        }
    }

    /// <summary> Adds an ellipsis to the text. </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <param name="value">     Specifies the string to compact. </param>
    /// <param name="format">    Specifies the desired <see cref="TextFormatFlags">formatting</see> </param>
    /// <param name="trimCount"> Number of characters to trim. </param>
    /// <returns> A <see cref="string" />. </returns>
    private static string AddEllipsis( string value, TextFormatFlags format, int trimCount )
    {
        if ( string.IsNullOrWhiteSpace( value ) || value.Length < trimCount )
        {
            return value;
        }
        else if ( value.Length == trimCount )
        {
            return string.Empty;
        }
        else if ( 0 != (format & TextFormatFlags.PathEllipsis) )
        {
            // center ellipsis
            int length = value.Length;
            value = string.Join( "...", value.SafeSubstring( 0, length / 2 ), value.SafeSubstring( length / 2, length - (length / 2) ) );
            bool trimRight = true;
            while ( value.Length > length - trimCount )
            {
                value = trimRight ? value.SafeSubstring( 0, value.Length - 1 ) : value.SafeSubstring( 1, value.Length - 1 );
                trimRight = !trimRight;
            }

            return value;
        }
        else
        {
            return 0 != (format & TextFormatFlags.EndEllipsis)
                ? $"{value.SafeSubstring( 0, value.Length - trimCount )}..."
                : $"...{value.SafeSubstring( trimCount, value.Length - trimCount )}";
        }
    }

    /// <summary>
    /// Compacts the string to permit display within the given width. For example, using
    /// <see cref="TextFormatFlags.PathEllipsis">Path Ellipsis</see>
    /// as the <paramref name="format">format instruction</paramref>
    /// the string <c>c:\program files\test app\RunMe.exe' might turn into 'c:\program files\...\
    /// RunMe.exe'</c> depending on the font and width.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <exception cref="ArgumentNullException">       Thrown when one or more required arguments
    /// are null. </exception>
    /// <exception cref="ArgumentOutOfRangeException"> Thrown when one or more arguments are outside
    /// the required range. </exception>
    /// <param name="value">  Specifies the string to compact. </param>
    /// <param name="width">  Specifies the width. </param>
    /// <param name="font">   Specifies the <see cref="Font">font</see> </param>
    /// <param name="format"> Specifies the desired <see cref="TextFormatFlags">formatting</see> </param>
    /// <returns> Compacted string. </returns>
    public static string Compact( this string value, int width, Font font, TextFormatFlags format )
    {
        if ( string.IsNullOrWhiteSpace( value ) )
        {
            return string.Empty;
        }

#if NET5_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( font, nameof( font ) );
#else
        if ( font is null )
        {
            throw new ArgumentNullException( nameof( font ) );
        }
#endif

        if ( width <= 0 )
        {
            throw new ArgumentOutOfRangeException( nameof( width ), width, "Must be non-zero" );
        }

        string result = value;
        int i = 0;
        while ( TextRenderer.MeasureText( result, font, new Size( width, font.Height ), TextFormatFlags.Default ).Width > width )
        {
            i += 1;
            result = AddEllipsis( value, format, i );
        }

        return result;
    }

    /// <summary>
    /// Compacts the string to permit display within the given width. For example, the string
    /// <c>'c:\program files\test app\RunMe.exe' might turn into 'c:\program files\...\RunMe.exe'</c>
    /// depending on the font and width.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <param name="value"> Specifies the string to compact. </param>
    /// <param name="width"> Specifies the width. </param>
    /// <param name="font">  Specifies the <see cref="Font">font</see> </param>
    /// <returns> Compacted string. </returns>
    public static string Compact( this string value, int width, Font font )
    {
        return value.Compact( width, font, TextFormatFlags.PathEllipsis );
    }

    /// <summary>
    /// Compacts the string to permit display within the given control. For example, using
    /// <see cref="TextFormatFlags.PathEllipsis">Path Ellipsis</see>
    /// as the <paramref name="format">format instruction</paramref>
    /// the string <c>'c:\program files\test app\RunMe.exe' might turn into 'c:\program files\...\
    /// RunMe.exe'</c> depending on the font and width.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
    /// <param name="value">   Specifies the text. </param>
    /// <param name="control"> Specifies the control within which to format the text. </param>
    /// <param name="format">  Specifies the desired <see cref="TextFormatFlags">formatting</see> </param>
    /// <returns> Compacted string. </returns>
    public static string Compact( this string value, Control control, TextFormatFlags format )
    {
        return string.IsNullOrWhiteSpace( value )
            ? string.Empty
            : control is null
            ? throw new ArgumentNullException( nameof( control ) )
            : control.AutoSize || control.Width == 0 ? value : value.Compact( control.Width, control.Font, format );
    }

    /// <summary>
    /// Compacts the string to permit display within the given control. For example, the string
    /// <c>'c:\program files\test app\RunMe.exe' might turn into 'c:\program files\...\RunMe.exe'</c>
    /// depending on the font and width.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
    /// <param name="value">   Specifies the string to compact. </param>
    /// <param name="control"> Specifies the control receiving the string. </param>
    /// <returns> Compacted string. </returns>
    public static string Compact( this string value, Control control )
    {
        return string.IsNullOrWhiteSpace( value )
            ? string.Empty
            : control is null ? throw new ArgumentNullException( nameof( control ) ) : value.Compact( control, TextFormatFlags.PathEllipsis );
    }

    /// <summary>
    /// Compacts the string to permit display within the given width. For example, the string
    /// <c>'c:\program files\test app\RunMe.exe' might turn into 'c:\program files\...\RunMe.exe'</c>
    /// depending on the font and width.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
    /// <param name="value">   Specifies the string to compact. </param>
    /// <param name="control"> Specifies the control within which to format the text. </param>
    /// <param name="format">  Specifies the desired <see cref="TextFormatFlags">formatting</see> </param>
    /// <returns> Compacted string. </returns>
    public static string Compact( this string value, ToolStripItem control, TextFormatFlags format )
    {
        return string.IsNullOrWhiteSpace( value )
            ? string.Empty
            : control is null
            ? throw new ArgumentNullException( nameof( control ) )
            : control.AutoSize || control.Width == 0 ? value : value.Compact( control.Width, control.Font, format );
    }

    /// <summary>
    /// Compacts the string to permit display within the given control. For example, the string
    /// <c>'c:\program files\test app\RunMe.exe' might turn into 'c:\program files\...\RunMe.exe'</c>
    /// depending on the font and width.
    /// </summary>
    /// <remarks> David, 2020-09-16. </remarks>
    /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
    /// <param name="value">   Specifies the string to compact. </param>
    /// <param name="control"> Specifies the control within which to format the text. </param>
    /// <returns> Compacted string. </returns>
    public static string Compact( this string value, ToolStripItem control )
    {
        return string.IsNullOrWhiteSpace( value )
            ? string.Empty
            : control is null ? throw new ArgumentNullException( nameof( control ) ) : value.Compact( control, TextFormatFlags.PathEllipsis );
    }
}
