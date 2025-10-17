using System.Reflection;
using Microsoft.Data.SqlClient;

namespace cc.isr.WinForms.ExceptionMessageBox.Helpers;
public class SqlExceptionCreator
{
    private static T Construct<T>( params object[] p )
    {
        ConstructorInfo[] constructors = typeof( T ).GetConstructors( BindingFlags.NonPublic | BindingFlags.Instance );
        return ( T ) constructors.First( ctor => ctor.GetParameters().Length == p.Length ).Invoke( p );
    }

    internal static SqlException NewSqlException( int number = 1 )
    {
        SqlErrorCollection collection = Construct<SqlErrorCollection>();
        SqlError error = Construct<SqlError>( number, ( byte ) 2, ( byte ) 3, "server name", "error message", "proc", 100 );

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        _ = typeof( SqlErrorCollection )
            .GetMethod( "Add", BindingFlags.NonPublic | BindingFlags.Instance )
            .Invoke( collection, [error] );
#pragma warning restore CS8602 // Dereference of a possibly null reference.


#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8603 // Possible null reference return.
        return typeof( SqlException )
            .GetMethod( "CreateException", BindingFlags.NonPublic | BindingFlags.Static,
                null,
                CallingConventions.ExplicitThis,
                [typeof( SqlErrorCollection ), typeof( string )],
                [] )
            .Invoke( null, [collection, "7.0.0"] ) as SqlException;
#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}
