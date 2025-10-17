using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.Data.SqlClient;
using cc.isr.WinForms.Dialogs;

namespace cc.isr.WinForms.ExceptionMessageBox.Tests;
/// <summary>   An SQL Server exception message builder. </summary>
/// <remarks>   2025-06-23. </remarks>
public class SqlClientExceptionMessageBuilder() : IExceptionMessageBuilder
{
    /// <summary>   Builds a message. </summary>
    /// <remarks>   2025-06-23. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="stringBuilder">            The string builder. </param>
    /// <param name="exception">                The exception. </param>
    /// <param name="handledExceptionSource">   The handled exception source. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    public virtual string BuildMessage( StringBuilder stringBuilder, Exception exception, string handledExceptionSource )
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( exception );
        ArgumentNullException.ThrowIfNull( stringBuilder );
#else
        if ( exception is null ) throw new ArgumentNullException( nameof( exception ) );
        if ( stringBuilder is null ) throw new ArgumentNullException( nameof( stringBuilder ) );
#endif
        int number = 0;
        if ( exception is SqlException exp )
            number = exp.Number;
        if ( exception.Source is not null && exception.Source.Length > 0 && (stringBuilder.Length == 0 || handledExceptionSource != exception.Source) )
        {
            _ = stringBuilder.Append( ' ' );
            string source = !(exception.GetType() == typeof( SqlException )) ? exception.Source : ExceptionMessageBuilderResources.SqlServerSource;
            if ( number > 0 )
                _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.ErrorSourceNumber, source, number ) );
            else
                _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.ErrorSource, source ) );
        }
        else if ( number > 0 )
        {
            _ = stringBuilder.Append( ' ' );
            _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.ErrorNumber, number ) );
        }
        return stringBuilder.ToString();
    }

    /// <summary>   Builds technical details. </summary>
    /// <remarks>   2025-06-23. </remarks>
    /// <param name="exception">    The exception. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1822: Mark members as static", Justification = "<Pending>" )]
    public virtual string BuildTechnicalDetails( Exception exception )
    {
        StringBuilder stringBuilder1 = new();
        if ( exception.GetType() == typeof( SqlException ) )
        {
            SqlException sqlException = ( SqlException ) exception;
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( "---------------" );
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( ExceptionMessageBuilderResources.SqlServerInfo );
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( Environment.NewLine );
            if ( sqlException.Server is not null && sqlException.Server.Length > 0 )
            {
                _ = stringBuilder1.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlServerName, sqlException.Server ) );
                _ = stringBuilder1.Append( Environment.NewLine );
            }

            _ = stringBuilder1.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlError, sqlException.Number.ToString( CultureInfo.CurrentCulture ) ) );
            _ = stringBuilder1.Append( Environment.NewLine );
            StringBuilder stringBuilder2 = stringBuilder1;
            byte state = sqlException.Class;

            string str1 = string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlSeverity, state.ToString( CultureInfo.CurrentCulture ) );
            _ = stringBuilder2.Append( str1 );
            _ = stringBuilder1.Append( Environment.NewLine );
            StringBuilder stringBuilder3 = stringBuilder1;
            state = sqlException.State;

            string str2 = string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlState, state.ToString( CultureInfo.CurrentCulture ) );
            _ = stringBuilder3.Append( str2 );
            _ = stringBuilder1.Append( Environment.NewLine );
            if ( sqlException.Procedure is not null && sqlException.Procedure.Length > 0 )
            {
                _ = stringBuilder1.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlProcedure, sqlException.Procedure ) );
                _ = stringBuilder1.Append( Environment.NewLine );
            }
            if ( sqlException.LineNumber != 0 )
            {
                _ = stringBuilder1.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlLineNumber, sqlException.LineNumber.ToString( CultureInfo.CurrentCulture ) ) );
                _ = stringBuilder1.Append( Environment.NewLine );
            }
        }
        if ( exception.StackTrace is not null && exception.StackTrace.Length > 0 )
        {
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( "---------------" );
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( ExceptionMessageBuilderResources.CodeLocation );
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( Environment.NewLine );
            _ = stringBuilder1.Append( exception.StackTrace );
            _ = stringBuilder1.Append( Environment.NewLine );
        }
        return stringBuilder1.ToString();
    }

    /// <summary>   Query if 'exception' has technical details. </summary>
    /// <remarks>   2025-06-23. </remarks>
    /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
    ///                                             null. </exception>
    /// <param name="exception">    The exception. </param>
    /// <returns>   True if technical details, false if not. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1309:Use ordinal StringComparison", Justification = "<Pending>" )]
    public bool HasTechnicalDetails( Exception exception )
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( exception );
#else
        if ( exception is null ) throw new ArgumentNullException( nameof( exception ) );
#endif
        for ( Exception? exp = exception; exp is not null; exp = exp.InnerException )
        {
            if ( (exp.StackTrace is not null && exp.StackTrace.Length > 0) || exp.GetType() == typeof( SqlException ) )
                return true;
            foreach ( DictionaryEntry dictionaryEntry in exp.Data )
                if ( string.Compare( ( string ) dictionaryEntry.Key, 0, "AdvancedInformation.", 0,
                    "AdvancedInformation.".Length, false, CultureInfo.CurrentCulture ) == 0 )
                    return true;
        }
        return false;
    }

    /// <summary>   Builds help URL. </summary>
    /// <remarks>   2025-06-19. </remarks>
    /// <param name="ex">   The exception. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1309:Use ordinal StringComparison", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1822:Mark members as static", Justification = "<Pending>" )]
    public string BuildHelpURL( Exception ex )
    {
#if NET8_0_OR_GREATER
        ArgumentNullException.ThrowIfNull( ex );
#else
        if ( ex is null ) throw new ArgumentNullException( nameof( ex ) );
#endif
        int num = 0;
        string? strA = null;
        StringBuilder stringBuilder = new( "?" );
        if ( ex == null )
            return string.Empty;
        if ( ex.HelpLink is not null && ex.HelpLink.Length > 0 )
            return string.Compare( ex.HelpLink, 0, "HTTP", 0, "HTTP".Length, true, CultureInfo.CurrentCulture ) == 0 ? ex.HelpLink : string.Empty;
        MethodInfo? method = ex.GetType().GetMethod( "get_Data" );
        if ( !(method == null) )
            if ( !(method.ReturnType != typeof( IDictionary )) )
            {
                try
                {
                    if ( ex.Data == null || ex.Data.Count == 0 )
                        return string.Empty;
                    foreach ( DictionaryEntry dictionaryEntry in ex.Data )
                        if ( dictionaryEntry.Key is not null && !(dictionaryEntry.Key.GetType() != typeof( string )) && dictionaryEntry.Value is not null && !(dictionaryEntry.Value.GetType() != typeof( string )) )
                            if ( string.Compare( ( string ) dictionaryEntry.Key, "HelpLink.BaseHelpUrl", true, CultureInfo.CurrentCulture ) == 0 )
                            {
                                strA = dictionaryEntry.Value.ToString();
                                if ( string.Compare( strA, 0, "HTTP", 0, "HTTP".Length, true, CultureInfo.CurrentCulture ) != 0 )
                                    strA = string.Empty;
                            }
                            else if ( string.Compare( ( string ) dictionaryEntry.Key, 0, "HelpLink.", 0, "HelpLink.".Length, true, CultureInfo.CurrentCulture ) == 0 )
                            {
                                if ( num++ > 0 )
                                    _ = stringBuilder.Append( '&' );
                                // Fix for SYSLIB0013 and IDE0057  
                                _ = stringBuilder.Append( Uri.EscapeDataString( (( string ) dictionaryEntry.Key)["HelpLink.".Length..] ) );
                                // _ = stringBuilder.Append( Uri.EscapeUriString( (( string ) dictionaryEntry.Key).Substring( "HelpLink.".Length ) ) );
                                if ( dictionaryEntry.Value is not null )
                                {
                                    string? candidateUrl = dictionaryEntry.Value.ToString();
                                    if ( !string.IsNullOrWhiteSpace( candidateUrl ) )
                                    {
                                        _ = stringBuilder.Append( '=' );
                                        _ = stringBuilder.Append( Uri.EscapeDataString( candidateUrl ) );
                                        // _ = stringBuilder.Append( Uri.EscapeUriString( dictionaryEntry.Value.ToString() ) );
                                    }
                                }
                            }
                    if ( strA is null && num == 0 )
                        return string.Empty;
                    return num == 0
                        ? strA is null
                          ? string.Empty
                          : strA
                        : strA + stringBuilder.ToString();
                }
                catch ( Exception )
                {
                }
                return string.Empty;
            }
        return ex is SqlException exception
            ? string.Format( CultureInfo.CurrentCulture, "http://www.microsoft.com/products/ee/transform.aspx?ProdName=Microsoft%20SQL%20Server&ProdVer=09.00.0000.00&EvtSrc=MSSQLServer&EvtID={0}",
            Uri.EscapeDataString( exception.Number.ToString( CultureInfo.CurrentCulture ) ) )
            : string.Empty;
    }

    /// <summary>   Builds the exception source. </summary>
    /// <param name="exception">   The exception. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    public string BuildExceptionSource( Exception exception )
    {
        return exception is SqlException exp
            ? string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.ErrorSourceNumber, ExceptionMessageBuilderResources.SqlServerSource, exp.Number )
            : string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.ErrorSource, exception.Source );
    }

    /// <summary>   Builds advanced information properties. </summary>
    /// <remarks>   2025-06-23. </remarks>
    /// <param name="exception">   The exception. </param>
    /// <returns>   A string. </returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1305:Specify culture info", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Globalization", "CA1309:Use ordinal StringComparison", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1822:Mark members as static", Justification = "<Pending>" )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Performance", "CA1863:Use CompositeFormat", Justification = "<Pending>" )]
    public string BuildAdvancedInfoProperties( Exception exception )
    {
        StringBuilder stringBuilder = new();
        bool flag = false;
        if ( exception.GetType() == typeof( SqlException ) )
        {
            SqlException sqlException = ( SqlException ) exception;
            if ( sqlException.Server != null && sqlException.Server.Length > 0 )
            {
                _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlServerName, sqlException.Server ) );
                _ = stringBuilder.Append( Environment.NewLine );
            }

            _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlError, sqlException.Number.ToString( CultureInfo.CurrentCulture ) ) );
            _ = stringBuilder.Append( Environment.NewLine );

            _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlSeverity, sqlException.Class.ToString( CultureInfo.CurrentCulture ) ) );
            _ = stringBuilder.Append( Environment.NewLine );

            _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlState, sqlException.State.ToString( CultureInfo.CurrentCulture ) ) );
            _ = stringBuilder.Append( Environment.NewLine );
            if ( sqlException.Procedure != null && sqlException.Procedure.Length > 0 )
            {
                _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlProcedure, sqlException.Procedure ) );
                _ = stringBuilder.Append( Environment.NewLine );
            }
            if ( sqlException.LineNumber != 0 )
            {
                _ = stringBuilder.Append( string.Format( ExceptionMessageBuilderResources.Culture, ExceptionMessageBuilderResources.SqlLineNumber, sqlException.LineNumber.ToString( CultureInfo.CurrentCulture ) ) );
                _ = stringBuilder.Append( Environment.NewLine );
            }
            flag = true;
        }
        foreach ( DictionaryEntry dictionaryEntry in exception.Data )
        {
            if ( dictionaryEntry.Key is not null && !(dictionaryEntry.Key.GetType() != typeof( string ))
                && dictionaryEntry.Value is not null
                && dictionaryEntry.Value is not null && !string.IsNullOrWhiteSpace( dictionaryEntry.Value.ToString() ) )
            {
                string value = (string.Compare( ( string ) dictionaryEntry.Key, 0, "AdvancedInformation.", 0, "AdvancedInformation.".Length, false, CultureInfo.CurrentCulture ) == 0)
                    ? $"{(( string ) dictionaryEntry.Key)["AdvancedInformation.".Length..]} = {dictionaryEntry.Value}"
                    : $"{dictionaryEntry.Key} = {dictionaryEntry.Value}";

                if ( flag )
                {
                    _ = stringBuilder.Append( Environment.NewLine );
                    flag = false;
                }
                _ = stringBuilder.Append( value );
                _ = stringBuilder.Append( Environment.NewLine );
            }
        }
        return stringBuilder.ToString();
    }
}
