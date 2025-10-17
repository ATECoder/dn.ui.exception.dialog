# About

cc.isr.WinForms.ExceptionDialog is a .Net library fork of the [Microsoft SQL Server Exception Message Box].

## Changes from the [Microsoft SQL Server Exception Message Box]

We forked the [Microsoft SQL Server Exception Message Box] because we are unable to invoke the exception message box due to the obsoletion of the the binary formatter serialization as reported by Microsoft: [Binary Formatter Serialization Obsolete].

The following changes were made as part of the forking process:
1) Compiled under .Net frameworks 4.72, 4.8 and 9.
1) The namespace was changed from `Microsoft.SqlServer.MessageBox` to `cc.isr.WinForms.Dialogs`.
1) The dependence on the SQL Client packages (`Microsoft.Data.SqlClient` or `System.Data.SqlClient`) were removed;
1) An exception message builder interface and class were added to build the exception messages.
1) A default exception message builder class is implemented for simple exceptions.
1) An exception message builder resources embedded resource was added to the test project, which implements building exception messages for SQL Client and SQLite in addition to the default builder.
1) The Segoe UI font replaces the San Serif and Verdana fonts.
1) The Hungarian naming convention was abandoned in favor of descriptive names without prefixes.
1) Our code analysis conventions (see .editorconfig in [IDE Repo]) were applied.
1) The message box message is initialized to `InvalidOperationException` instead of `Exception` or `ApplicationException`.

## How to Use

[Microsoft SQL Server Exception Message Box]

### Building custom messages.

Custom messages can be by implementing the exception message builder interface and assigning an instance of the implementation to the static builder class as demonstrated in the following example.

### Building SQL Exception messages.

Building custom exception messages typically consists of the following elements:

- [Exception Message Embedded resource](#resource)
- [Exception Message Builder Class](#builder)
- [Exception Message Builder Assignment](#assign)

The following example is taken from the exception message builder for the SQL Exception of the `Microsoft.Data.SqlClient` package.

<a name="resource"></a>
### Exception Message Embedded resource

The embedded resource contains the text captions that are used when building the exception message. The designer for the embedded resource is built by Visual Studio by selecting the code generation option, e.g., internal, from the resource viewer (double-click on the .resx file to open the viewer).

The code for the SQL Exception resource is:
```
<?xml version="1.0" encoding="utf-8"?>
<root>
  <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
    <xsd:import namespace="http://www.w3.org/XML/1998/namespace" />
    <xsd:element name="root" msdata:IsDataSet="true">
      <xsd:complexType>
        <xsd:choice maxOccurs="unbounded">
          <xsd:element name="metadata">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" />
              </xsd:sequence>
              <xsd:attribute name="name" use="required" type="xsd:string" />
              <xsd:attribute name="type" type="xsd:string" />
              <xsd:attribute name="mimetype" type="xsd:string" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="assembly">
            <xsd:complexType>
              <xsd:attribute name="alias" type="xsd:string" />
              <xsd:attribute name="name" type="xsd:string" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="data">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
              <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
              <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="resheader">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name="resmimetype">
    <value>text/microsoft-resx</value>
  </resheader>
  <resheader name="version">
    <value>2.0</value>
  </resheader>
  <resheader name="reader">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name="writer">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <data name="SqlSeverity" xml:space="preserve">
    <value>Severity: {0}</value>
  </data>
  <data name="SqlLineNumber" xml:space="preserve">
    <value>Line Number: {0}</value>
  </data>
  <data name="SqlServerSource" xml:space="preserve">
    <value>Microsoft SQL Server</value>
  </data>
  <data name="SqlError" xml:space="preserve">
    <value>Error Number: {0}</value>
  </data>
  <data name="SqlState" xml:space="preserve">
    <value>State: {0}</value>
  </data>
  <data name="CodeLocation" xml:space="preserve">
    <value>Program Location:</value>
  </data>
  <data name="SqlServerName" xml:space="preserve">
    <value>Server Name: {0}</value>
  </data>
  <data name="SqlServerInfo" xml:space="preserve">
    <value>SQL Server Information</value>
  </data>
  <data name="ErrorSourceNumber" xml:space="preserve">
    <value>({0}, Error: {1})</value>
  </data>
  <data name="SqlProcedure" xml:space="preserve">
    <value>Procedure: {0}</value>
  </data>
  <data name="ErrorSource" xml:space="preserve">
    <value>({0})</value>
  </data>
  <data name="ErrorNumber" xml:space="preserve">
    <value>(Error: {0})</value>
  </data>
  <data name="SqliteInfo" xml:space="preserve">
    <value>SQLite Information</value>
  </data>
  <data name="ErrorCode" xml:space="preserve">
    <value>Error Code: {0}</value>
  </data>
  <data name="ExtendedErrorCode" xml:space="preserve">
    <value>Extended Error Code: {0}</value>
  </data>
</root>
```

<a name="builder"></a>
### Exception Message Builder Class

The builder class for the SQL Exception is:

```
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
```

<a name="assign"></a>
### Exception Message Builder Assignment

Finally, the instance of the builder class is assigned to the static builder of the `Exception Dialog`.
```
// assign the relevant exception message builder.
cc.isr.WinForms.Dialogs.ExceptionMessageBuilder.MessageBuilder = new SqliteExceptionMessageBuilder();
```

## Key Features

[Microsoft SQL Server Exception Message Box]

## Main Types

[Microsoft SQL Server Exception Message Box]

## Feedback

cc.isr.WinForms.ExceptionDialog is released as open source under the MIT license.
Bug reports and contributions are welcome at the [Core Framework Repository].

[Core Framework Repository]: https://bitbucket.org/davidhary/dn.core
[Microsoft SQL Server Exception Message Box]: https://learn.microsoft.com/en-us/dotnet/api/microsoft.sqlserver.messagebox.exceptionmessagebox?view=sqlserver-201
[Binary Formatter Serialization Obsolete]: https://learn.microsoft.com/en-us/dotnet/core/compatibility/serialization/5.0/binaryformatter-serialization-obsolete
[IDE Repo]: https://git@bitbucket.org:davidhary/vs.ide.git