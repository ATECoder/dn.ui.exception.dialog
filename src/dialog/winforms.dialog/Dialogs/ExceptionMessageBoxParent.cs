using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   An exception message box parent. This class cannot be inherited. </summary>
/// <remarks>   2025-06-19. </remarks>
/// <remarks>   Constructor. </remarks>
/// <remarks>   2025-06-19. </remarks>
/// <param name="hwnd"> The handle to the window. </param>
[Guid( "E1F61A91-BC94-4478-8AFC-A634B91C4CC3" )]
internal sealed class ExceptionMessageBoxParent( IntPtr hwnd ) : IWin32Window
{
    /// <summary>   Gets the handle to the window represented by the implementer. </summary>
    /// <value> A handle to the window represented by the implementer. </value>
    public IntPtr Handle { get; } = hwnd;
}
