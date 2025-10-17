using System;

namespace cc.isr.WinForms.Dialogs;

/// <summary>   Interface for exception message builder. </summary>
/// <remarks>   2025-06-23. </remarks>
public interface IExceptionMessageBuilder
{
    /// <summary>   Builds a message. </summary>
    /// <param name="stringBuilder">            The string builder. </param>
    /// <param name="exception">                The exception. </param>
    /// <param name="handledExceptionSource">   The handled exception source. </param>
    /// <returns>   A string. </returns>
    public string BuildMessage( System.Text.StringBuilder stringBuilder, System.Exception exception, string handledExceptionSource );

    /// <summary>   Builds technical details. </summary>
    /// <param name="exception">    The exception. </param>
    /// <returns>   A string. </returns>
    public string BuildTechnicalDetails( System.Exception exception );

    /// <summary>   Query if 'exception' has technical details. </summary>
    /// <param name="exception">    The exception. </param>
    /// <returns>   True if technical details, false if not. </returns>
    public bool HasTechnicalDetails( System.Exception exception );

    /// <summary>   Builds help URL. </summary>
    /// <param name="ex">   The exception. </param>
    /// <returns>   A string. </returns>
    public string BuildHelpURL( Exception ex );

    /// <summary>   Builds exception source. </summary>
    /// <param name="exception">    The exception. </param>
    /// <returns>   A string. </returns>
    public string BuildExceptionSource( Exception exception );

    /// <summary>   Builds advanced information properties. </summary>
    /// <param name="exception">    The exception. </param>
    /// <returns>   A string. </returns>
    public string BuildAdvancedInfoProperties( Exception exception );

}
