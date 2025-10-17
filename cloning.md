# Cloning

Support libraries for Windows Forms.

* [Source Code](#Source-Code)
* [Repositories](#Repositories)
* [Packages](#Packages)
* [Facilitated By](#Facilitated-By)

<a name="Source-Code"></a>
## Source Code
Clone the repository along with its requisite repositories to their respective relative path.

### Repositories
The repositories listed in [external repositories] are required:
* [IDE Repository] - ISR framework windows forms libraries
```
git clone git@bitbucket.org:davidhary/vs.ide.git
```

Clone the repositories into the following folders (parents of the .git folder):
```
%vslib%\core\ide
```
where %vslib% is the root folder of the .NET libraries, e.g., %my%\lib\vs 
and %my% is the root folder of the .NET solutions

### Global Configuration Files
ISR libraries use a global editor configuration file and a global test run settings file. 
These files can be found in the [IDE Repository].

Restoring Editor Configuration:
```
xcopy /Y %my%\.editorconfig %my%\.editorconfig.bak
xcopy /Y %vslib%\core\ide\code\.editorconfig %my%\.editorconfig
```

Restoring Run Settings:
```
xcopy /Y %userprofile%\.runsettings %userprofile%\.runsettings.bak
xcopy /Y %vslib%\core\ide\code\.runsettings %userprofile%\.runsettings
```
where %userprofile% is the root user folder.

### Packages
Presently, packages are consumed from a _source feed_ residing in a local folder, e.g., _%my%\nuget\packages_. 
The packages are 'packed', using the _Pack_ command from each packable project,
into the _%my%\nuget_ folder as specified in the project file and then
added to the source feed. Alternatively, the packages can be downloaded from the 
private [MEGA packages folder].

[MEGA packages folder]: https://mega.nz/folder/KEcVxC5a#GYnmvMcwP4yI4tsocD31Pg
[IDE Repository]: https://www.bitbucket.org/davidhary/vs.ide.git
[SQL Exception Message]: https://msdn.microsoft.com/en-us/library/ms365274.aspx

[external repositories]: ExternalReposCommits.csv

