using System;
using System.Threading;

namespace cc.isr.WinForms.Dialogs;

/// <summary> Single thread apartment base. </summary>
/// <remarks>
/// David, 2015-03-24
/// http://StackOverflow.com/questions/899350/how-to-copy-the-contents-of-a-string-to-the-clipboard-in-c.
/// </remarks>
internal abstract class SingleThreadApartmentBase : IDisposable
{
    #region " construction "

    /// <summary> Specialized default constructor for use only by derived class. </summary>
    /// <remarks> David, 202-09-12. </remarks>
    protected SingleThreadApartmentBase() : base()
    {
    }

    #region " disposable support "

    /// <summary> Gets the disposed indicator. </summary>
    /// <value> The disposed indicator. </value>
    public bool IsDisposed { get; private set; }

    /// <summary>
    /// Releases the unmanaged resources used by the class.
    /// </summary>
    /// <remarks> David, 2020-09-17. </remarks>
    /// <param name="disposing"> True to release both managed and unmanaged resources; false to
    /// release only unmanaged resources. </param>
    protected virtual void Dispose( bool disposing )
    {
        if ( this.IsDisposed ) return;
        try
        {
            if ( disposing )
            {
                this._complete?.Dispose();
            }
        }
        finally
        {
            this.IsDisposed = true;
        }
    }

    /// <summary> Calls <see cref="Dispose(bool)" /> to cleanup. </summary>
    /// <remarks>
    /// Do not make this method Overridable (virtual) because a derived class should not be able to
    /// override this method.
    /// </remarks>
    public void Dispose()
    {
        this.Dispose( true );
        // Take this object off the finalization(Queue) and prevent finalization code
        // from executing a second time.
        GC.SuppressFinalize( this );
    }

    /// <summary>
    /// This destructor will run only if the Dispose method does not get called. It gives the base
    /// class the opportunity to finalize. Do not provide destructors in types derived from this
    /// class.
    /// </summary>
    /// <remarks> David, 2020-09-17. </remarks>
    ~SingleThreadApartmentBase()
    {
        // Do not re-create Dispose clean-up code here.
        // Calling Dispose(false) is optimal for readability and maintainability.
        this.Dispose( false );
    }

    #endregion

    #endregion

    #region " workers "

    /// <summary> Executes the work as defined by the inheriting class. </summary>
    /// <remarks> David, 202-09-12. </remarks>
    public void Go()
    {
        Thread thread = new( new ThreadStart( this.DoWork ) );
        thread.SetApartmentState( ApartmentState.STA );
        thread.Start();
    }

    /// <summary> The complete reset event. Notifies one or more waiting threads that an event has occurred. </summary>
    /// <remarks> is used to block and release threads manually. It is created in the non-signaled state.
    /// <c>
    /// https://msdn.Microsoft.com/en-us/library/system.threading.manualresetevent%28v=vs.110%29.aspx
    /// </c>
    /// It seems that this does not do anything in this case as the event is not set to block, e.g., Wait One.
    /// </remarks>
    private readonly ManualResetEvent _complete = new( false );

    /// <summary> The thread entry method. </summary>
    /// <remarks> David, 202-09-12. </remarks>
    private void DoWork()
    {
        try
        {
            // sets the state of the event to not signaled causing threads to block
            _ = this._complete.Reset();
            this.Work();
        }
        catch ( Exception ex )
        {
            if ( this.RetryWorkOnFailed )
            {
                try
                {
                    Thread.Sleep( 1000 );
                    this.Work();
                }
                catch
                {
                    // ex from first exception
                    System.Diagnostics.Trace.TraceError( ex.ToString() );
                }
            }
            else
            {
                throw;
            }
        }
        finally
        {
            // sets the state of the event to signaled, allowing waiting events to proceed
            _ = this._complete.Set();
        }
    }

    /// <summary> Gets or sets a value indicating whether to retry work on failed. </summary>
    /// <value> <c>true</c> if retry work on failed; otherwise <c>false</c> </value>
    public bool RetryWorkOnFailed { get; set; }

    /// <summary> Implemented in the inheriting class to do actual work. </summary>
    /// <remarks> David, 202-09-12. </remarks>
    protected abstract void Work();

    #endregion
}
