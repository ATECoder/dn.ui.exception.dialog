using System.Diagnostics;
using cc.isr.WinForms.Dialogs;

namespace cc.isr.WinForms.ExceptionMessageBox.Tests;

/// <summary> Exception Message Box tests. </summary>
/// <remarks>
/// <para>
/// David, 2018-04-13 </para>
/// </remarks>
[TestClass]
public class ExceptionMessageBoxTests
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
        cc.isr.WinForms.Dialogs.ExceptionMessageBuilder.MessageBuilder = new DefaultExceptionMessageBuilder();
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
    /// <summary>   Shows the dialog abort ignore. </summary>
    /// <remarks>   David, 2020-09-18. </remarks>
    public static void ShowDialogException()
    {
        threadException = null;
        try
        {
            DivideByZeroException ex = new();
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
    /// <summary>   Shows the dialog abort ignore. </summary>
    /// <remarks>   David, 2020-09-18. </remarks>
    public static void ShowDialogAbortIgnore()
    {
        threadException = null;
        try
        {
            DivideByZeroException ex = new();
            ex.Data.Add( "@isr", "Testing exception message" );
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
