using System;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   A bit-field of flags for specifying exception message box options. </summary>
/// <remarks>   2025-06-19. </remarks>
[Flags]
public enum ExceptionMessageBoxOptions
{
    /// <summary>   A binary constant representing the none flag. </summary>
    None = 0,
    /// <summary>   A binary constant representing the right align flag. </summary>
    RightAlign = 1,
    /// <summary>   A binary constant representing the RTL reading flag. </summary>
    RtlReading = 2,
}
