using System.Drawing;
using System.Windows.Forms;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   my renderer. </summary>
/// <remarks>   2025-06-19. </remarks>
internal sealed class MyRenderer : ToolStripSystemRenderer
{
    /// <summary>
    /// Raises the <see cref="System.Windows.Forms.ToolStripRenderer.RenderToolStripBackground" />
    /// event.
    /// </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="e">    A <see cref="System.Windows.Forms.ToolStripRenderEventArgs" /> that
    ///                     contains the event data. </param>
    protected override void OnRenderToolStripBackground( ToolStripRenderEventArgs e )
    {
        e.Graphics.Clear( SystemColors.Control );
    }

    /// <summary>
    /// Raises the <see cref="System.Windows.Forms.ToolStripRenderer.RenderToolStripBorder" />
    /// event.
    /// </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="e">    A <see cref="System.Windows.Forms.ToolStripRenderEventArgs" /> that
    ///                     contains the event data. </param>
    protected override void OnRenderToolStripBorder( ToolStripRenderEventArgs e )
    {
    }
}
