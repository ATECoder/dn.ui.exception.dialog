# Changelog
Notable changes to this solution are documented in this file using the 
[Keep a Changelog] style. The dates specified are in coordinated universal time (UTC).

[1.0.9421]: https://github.com/ATECoder/dn.ui.exception.dialog.git

## [1.0.9421] - 2025-10-17
- Update packages:
  - .NET Test SDK to 4.0.1.
  - Microsoft Extensions to 9.0.10.
  - XUnit from 3.1.4 to 3.1.5.
  - Fluent Assertions from 8.6.0 to 8.7.1. 
  - Microsoft.Net.Test.SDK from 17.14.1 to 18.0.0.
  - BenchmarkDotNet from 0.14.0 to 0.15.4.
- Test projects
  - Use [TestMethod( DisplayName = "...']
  - Change [ClassCleanup( ClassCleanupBehavior.EndOfClass )] to [ClassCleanup]
  - Add parallelize to the assembly attributes.
  - change Assert...( ..., format, args ); to Assert...(... string.Format( System.Globalization.CultureInfo.CurrentCulture, format, args ) );
  - Use Assert.HasCount<T> in place of Assert.AreEquals( count, [T].Length ).

## [1.0.9382] - 2025-09-08
- Use language preview in windows and net standard projects so as to enable auto properties.
- Implement code analysis recommendations.
- Unify project file structure.
- Remove legacy leftover Visual Basic project file entries.
- Remove UnitTesting Using statement from Test project files.
- Move assembly information such as CLS Compliance and COM visibility to the project file.
- Remove words that are no longer tagged as misspelled from the exclusion dictionary file.
- Remove references to Std.Net21 project.
- Remove the old exclusion dictionary (exclusion.dix and dix.xml) file.
- MS Test: Add cancellation token to the wait task.
- Remove unused core lib and net standard code.
- Implement code analysis recommendations.

## [1.0.9371] - 2025-08-28
- Update MSTest SDK to 3.10.3
- Use preview in net standard classes.
- use isr.cc as company name in the Serilog settings generator.
- Turn off source version in MS Test, Demo and Console projects.
- Remove incorrect Generate Assembly Version Attribute project settings.
- Use file version rather than product version when building the Product folder name because starting with .NET 8 the product version includes the source code commit information, which is not necessary for defining the product folder for settings and logging.

## [1.0.9350] - 2025-08-07
- Moved from the [Windows Forms Repository].
- Add exception dialog project, which restores the ability to run the Microsoft Exception Message Box.
- Exception Message Box
  - Use the new exception dialog. 
  - Remove EnableUnsafeBinaryFormatterSerialization
- Exception Dialog Fork from Microsoft.ExceptionMessageBox.dll
  - Readme.md: Add example for building a custom exception message.
  - Remove SQL Client Package.
  - Change namespace to cc.isr.WinForms.Dialogs.
  - Remove unused private items.	
  - Rename 'm_' values.
	- Rename event arguments to sender and e. 
    - Replace Hungarian notation with Pascal case descriptive names.
    - Set Localizable = true fior all forms.
    - Add Exception Message Builder
	  - Add interface, default and SQL Server exception message builders.
	  - Add regular exception data to the additional information output.
    - Move Message Beep to a Native Methods class.
    - Remove EnableUnsafeBinaryFormatterSerialization
    - Change form fonts to Segoe UI.
  - Exception Message Tests:
    - Add class initialization.
	- Add SQL Server Exception Message resources.
	- Add Default and SQL Server Message Builders.
	- Add SQL Client and SQL Lite tests.

&copy; 2025 Integrated Scientific Resources, Inc. All rights reserved.

[Keep a Changelog]: https://keepachangelog.com/en/1.0.0/
[vs.ide]: https://bitbucket.org/davidhary/vs.ide.git
[SQL Exception Message]: https://msdn.microsoft.com/en-us/library/ms365274.aspx
[to do]: https://github.com/ATECoder/dn.ui.exception.dialog.git/src/todo.md
[Windows Forms Repository]: https://bitbucket.org/davidhary/dn.win.forms.git
