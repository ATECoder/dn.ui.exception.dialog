namespace cc.isr.WinForms.Dialogs;

/// <summary>   An exception message builder. </summary>
/// <remarks>   2025-06-23. </remarks>
public static class ExceptionMessageBuilder
{
    /// <summary>   Gets or sets the exception message builder. </summary>
    /// <value> The exception message builder. </value>
    public static IExceptionMessageBuilder MessageBuilder { get; set; } = new DefaultExceptionMessageBuilder();
}
