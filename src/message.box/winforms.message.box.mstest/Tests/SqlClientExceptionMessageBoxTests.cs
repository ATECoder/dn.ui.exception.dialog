using System.Data;
using System.Diagnostics;
using System.Reflection;
using cc.isr.WinForms.Dialogs;
using Microsoft.Data.SqlClient;

namespace cc.isr.WinForms.ExceptionMessageBox.Tests;

/// <summary> SQL Server Exception Message Box tests. </summary>
/// <remarks>
/// <para>
/// David, 2018-04-13 </para>
/// </remarks>
[TestClass]
public class SqlClientExceptionMessageBoxTests
{
    #region " construction and cleanup "

    /// <summary> Initializes the test class before running the first test. </summary>
    /// <remarks>
    /// Use <see cref="InitializeTestClass(TestContext)"/> to run code before running the first test in the class.
    /// </remarks>
    /// <param name="testContext"> Gets or sets the test context which provides information about
    /// and functionality for the current test run. </param>
    [ClassInitialize()]
    public static void InitializeTestClass( TestContext testContext )
    {
        string methodFullName = $"{testContext.FullyQualifiedTestClassName}.{System.Reflection.MethodBase.GetCurrentMethod()?.Name}";
        try
        {
            Trace.WriteLine( "Initializing", methodFullName );
        }
        catch ( Exception ex )
        {
            Trace.WriteLine( $"Failed initializing the test class: {ex}", methodFullName );

            // cleanup to meet strong guarantees

            try
            {
                CleanupTestClass();
            }
            finally
            {
            }

            throw;
        }
    }

    /// <summary> Cleans up the test class after all tests in the class have run. </summary>
    /// <remarks> Use <see cref="CleanupTestClass"/> to run code after all tests in the class have run. </remarks>
    [ClassCleanup]
    public static void CleanupTestClass()
    {
    }

    /// <summary> Initializes the test class instance before each test runs. </summary>
    [TestInitialize()]
    public void InitializeBeforeEachTest()
    {
        Console.Out.WriteLine( $"{this.TestContext?.FullyQualifiedTestClassName}: {DateTime.Now} {System.TimeZoneInfo.Local}" );
        Console.WriteLine( $"\tTesting {typeof( cc.isr.WinForms.Dialogs.ExceptionMessageBox ).Assembly.FullName}" );

        // assign the relevant exception message builder.
        cc.isr.WinForms.Dialogs.ExceptionMessageBuilder.MessageBuilder = new SqliteExceptionMessageBuilder();
    }

    /// <summary> Cleans up the test class instance after each test has run. </summary>
    /// <remarks> David, 2020-09-18. </remarks>
    [TestCleanup()]
    public void CleanupAfterEachTest()
    {
    }

    /// <summary>
    /// Gets or sets the test context which provides information about and functionality for the
    /// current test run.
    /// </summary>
    /// <value> The test context. </value>
    public TestContext? TestContext { get; set; }

    #endregion

    #region " exception makers "

    /// <summary>
    /// Helper method to simulate the creation of a SqlException.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="number">The error number.</param>
    /// <param name="state">The error state.</param>
    /// <param name="server">The server name.</param>
    /// <param name="procedure">The procedure name.</param>
    /// <param name="lineNumber">The line number.</param>
    /// <returns>A simulated SqlException.</returns>
    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private static Microsoft.Data.SqlClient.SqlException CreateSqlException(
        string message, int number, byte state, string server, string procedure, int lineNumber )
    {
        // Use reflection to create an instance of SqlErrorCollection since it does not have a public constructor.
        Microsoft.Data.SqlClient.SqlErrorCollection errorCollection = ( Microsoft.Data.SqlClient.SqlErrorCollection ) Activator.CreateInstance(
            typeof( Microsoft.Data.SqlClient.SqlErrorCollection ),
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            null,
            null )!;

        // Create a SqlError instance using reflection since it does not have a public constructor.
        Microsoft.Data.SqlClient.SqlError error = ( Microsoft.Data.SqlClient.SqlError ) Activator.CreateInstance(
            typeof( Microsoft.Data.SqlClient.SqlError ),
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [number, state, ( byte ) 11, server, message, procedure, lineNumber],
            null )!;

        // Use reflection to add the error to the errorCollection.
        MethodInfo? addMethod = typeof( Microsoft.Data.SqlClient.SqlErrorCollection ).GetMethod(
            "Add",
            BindingFlags.NonPublic | BindingFlags.Instance );

        _ = addMethod?.Invoke( errorCollection, [error] );

        // Create the SqlException instance using reflection.
        Microsoft.Data.SqlClient.SqlException sqlException = ( Microsoft.Data.SqlClient.SqlException ) Activator.CreateInstance(
            typeof( Microsoft.Data.SqlClient.SqlException ),
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,
            [message, errorCollection, server, Guid.NewGuid()],
            null )!;

        return sqlException;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage( "Style", "IDE0051:Remove unused private members", Justification = "<Pending>" )]
    private static Microsoft.Data.SqlClient.SqlException CreateSqlException( string message )
    {
        ConstructorInfo? collectionConstructor = typeof( SqlErrorCollection ).GetConstructor( BindingFlags.NonPublic | BindingFlags.Instance, //visibility
            null, //binder
            [],
            null );

        SqlErrorCollection errorCollection = ( SqlErrorCollection ) collectionConstructor!.Invoke( null );

        ConstructorInfo? constructor = typeof( SqlException ).GetConstructor( BindingFlags.NonPublic | BindingFlags.Instance, //visibility
            null, //binder
            [typeof( string ), typeof( SqlErrorCollection ), typeof( Exception ), typeof( Guid )],
            null ); //param modifiers

        return ( SqlException ) constructor!.Invoke( [message, errorCollection, new DataException(), Guid.NewGuid()] );
    }

    #endregion

    public static SqlException? MakeSqlException()
    {
        SqlException? exception = null;
        try
        {
            SqlConnection conn = new( @"Data Source=.;Database=GUARANTEED_TO_FAIL;Connection Timeout=1" );
            conn.Open();
        }
        catch ( SqlException ex )
        {
            exception = ex;
        }
        return exception;
    }

    /// <summary> Shows the dialog abort ignore. </summary>
    /// <remarks> David, 2020-09-18. </remarks>
    /// <exception cref="DivideByZeroException"> Thrown when an attempt is made to divide a number by
    /// <summary> Shows the dialog abort ignore. </summary>
    /// <remarks> David, 2020-09-18. </remarks>
    /// <exception cref="DivideByZeroException"> Thrown when an attempt is made to divide a number by
    /// zero. </exception>
    public static void ShowDialogException()
    {
        threadException = null;
        try
        {
            // nop: Microsoft.Data.SqlClient.SqlException ex = CreateSqlException("SQL Server test exception message", 11, 12, "ServerName", "ProcedureName", 1234 );
            // nop: Microsoft.Data.SqlClient.SqlException ex = SqlExceptionHelper.Generate( SqlExceptionNumber.EncryptionNotSupported );
            // nop: Microsoft.Data.SqlClient.SqlException ex = CreateSqlException( "SQL Server test exception message" );
            // nop: Microsoft.Data.SqlClient.SqlException ex = SqlExceptionCreator.NewSqlException();
            // this works: Microsoft.Data.SqlClient.SqlError sqlError = SqlExceptionHelper.GenerateSqlError( ( int ) SqlExceptionNumber.EncryptionNotSupported );

            // SqlException does not have a public constructor. Instead, we simulate it using a helper method or mock.
            Microsoft.Data.SqlClient.SqlException? ex = SqlClientExceptionMessageBoxTests.MakeSqlException();
            Assert.IsNotNull( ex, "SqlException should not be null." );
            MyDialogResult expected = MyDialogResult.Ok;
            MyDialogResult actual;
            ex.Data.Add( "@isr", "Testing exception message" );
            actual = MyMessageBox.ShowDialog( ex );
            Assert.AreEqual( expected, actual );
        }
        catch ( Exception ex )
        {
            threadException = ex;
        }
    }

    /// <summary>
    /// Send the given keys to the active application and then wait for the message to be processed.
    /// </summary>
    /// <remarks> David, 202-09-12. </remarks>
    /// <param name="keys"> The keys. </param>
    public static void SendWait( string keys )
    {
        System.Windows.Forms.SendKeys.SendWait( keys );
    }

    /// <summary>   (Unit Test Method) exception message should show. </summary>
    /// <remarks>   David, 2020-09-18. </remarks>
    [TestMethod()]
    public void ExceptionMessageShouldShow()
    {
        Thread oThread = new( new ThreadStart( ShowDialogException ) );
        oThread.Start();
        // a long delay was required...
        System.Threading.Tasks.Task.Delay( 2000, this.TestContext?.CancellationTokenSource.Token ?? System.Threading.CancellationToken.None ).Wait( this.TestContext?.CancellationTokenSource.Token ?? System.Threading.CancellationToken.None );
        SendWait( "{ENTER}" );
        // This tabbed into another application.  cc.isr.WinForms.SendWait("%{TAB}{ENTER}")
        oThread.Join();
        if ( threadException != null )
            Assert.Fail( $"Thread exception:\n\t{threadException}" );
    }

    private static Exception? threadException;
    /// <summary> Shows the dialog abort ignore. </summary>
    /// <remarks> David, 2020-09-18. </remarks>
    /// <exception cref="DivideByZeroException"> Thrown when an attempt is made to divide a number by
    /// zero. </exception>
    public static void ShowDialogAbortIgnore()
    {
        threadException = null;
        try
        {
            // SqlException does not have a public constructor. Instead, we simulate it using a helper method or mock.
            Microsoft.Data.SqlClient.SqlException? ex = SqlClientExceptionMessageBoxTests.MakeSqlException();
            Assert.IsNotNull( ex, "SqlException should not be null." );
            ex.Data.Add( "@isr1", "Testing exception message" );
            MyDialogResult result = MyMessageBox.ShowDialogAbortIgnore( ex, MyMessageBoxIcon.Error );
            Assert.IsTrue( result is MyDialogResult.Abort or MyDialogResult.Ignore, $"{result} expected {MyDialogResult.Abort} or {MyDialogResult.Ignore}" );
        }
        catch ( Exception ex )
        {
            threadException = ex;
        }
    }

    /// <summary>   (Unit Test Method) exception message should abort or ignore. </summary>
    /// <remarks>   David, 2020-09-18. </remarks>
    [TestMethod()]
    public void ExceptionMessageShouldAbortOrIgnore()
    {
        Thread oThread = new( new ThreadStart( ShowDialogAbortIgnore ) );
        oThread.Start();
        // a long delay was required...
        System.Threading.Tasks.Task.Delay( 2000, this.TestContext?.CancellationTokenSource.Token ?? System.Threading.CancellationToken.None ).Wait( this.TestContext?.CancellationTokenSource.Token ?? System.Threading.CancellationToken.None );
        SendWait( "{ENTER}" );
        // This tabbed into another application.  cc.isr.WinForms.SendWait("%{TAB}{ENTER}")
        oThread.Join();
        if ( threadException != null )
            Assert.Fail( $"Thread exception:\n\t{threadException}" );
    }
}
