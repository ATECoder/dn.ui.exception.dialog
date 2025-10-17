using System.Runtime.InteropServices;
namespace cc.isr.WinForms.Dialogs;

/// <summary>
/// This class just wraps some Win32 stuff that is used for implementing a single instance
/// application.
/// </summary>
/// <remarks>   2023-05-24. </remarks>
internal sealed partial class NativeMethods
{
#if NET5_0_OR_GREATER
    /// <summary>   Message beep. </summary>
    /// <remarks>   2025-06-23. </remarks>
    /// <param name="beepType"> Type of the beep. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    [LibraryImport( "user32" )]
    [return: MarshalAs( UnmanagedType.Bool )]
    public static partial bool MessageBeep( uint beepType );
#else
    [return: MarshalAs( UnmanagedType.Bool )]
    [DllImport( "user32.dll", CharSet = CharSet.Auto )]
    public static extern bool MessageBeep( uint beepType );
#endif

    /// <summary>   Plays a waveform sound. The waveform sound for each sound type is identified by an entry in the registry.</summary>
    /// <remarks>   2025-06-23. </remarks>
    /// <param name="beepType"> The waveform sound type. </param>
    /// <returns>   True if it succeeds, false if it fails. </returns>
    public static bool MessageBeep( ExceptionMessageBoxForm.BeepType beepType )
    {
        // send our Win32 message to make the currently running instance
        // jump on top of all the other windows
        return NativeMethods.MessageBeep( ( uint ) beepType );
    }
}
