using System;

namespace cc.isr.WinForms.Dialogs;

/// <summary>
/// Additional information for copy to clipboard events. This class cannot be inherited.
/// </summary>
/// <remarks>   2025-06-19. </remarks>
public sealed class CopyToClipboardEventArgs : EventArgs
{
    /// <summary>   Constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="clipboardText">    The clipboard text. </param>
    public CopyToClipboardEventArgs( string clipboardText )
    {
        this.ClipboardText = clipboardText;
        this.EventHandled = false;
    }

    /// <summary>   Default constructor. </summary>
    /// <remarks>   2025-06-19. </remarks>
    public CopyToClipboardEventArgs()
    {
        this.ClipboardText = string.Empty;
        this.EventHandled = false;
    }

    /// <summary>   Gets the clipboard text. </summary>
    /// <value> The clipboard text. </value>
    public string ClipboardText { get; }

    /// <summary>   Gets or sets a value indicating whether the event handled. </summary>
    /// <value> True if event handled, false if not. </value>
    public bool EventHandled { get; set; }
}
