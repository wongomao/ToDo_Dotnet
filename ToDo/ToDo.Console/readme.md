This project is meant to be a precursor, building a simple sqlite db with a ToDo table, outputing the contents in a console when run in linux.
Install sqlite tools.
Sqlite3 database - **create in PowerShell** with:
`> sqlite3 todo.sqlite.db`
Then paste in the data from `create_todo.sql` and 'select' the data.
Session:
```
> sqlite3 todo.sqlite.db
SQLite version 3.36.0 2021-06-18 18:36:39
Enter ".help" for usage hints.
sqlite> create table if not exists todo (
   ...>     todo_id INTEGER PRIMARY KEY,
   ...>     description TEXT NOT NULL,
   ...>     done BOOLEAN NOT NULL DEFAULT FALSE
   ...> );
sqlite>
sqlite> insert into todo (description) values ('grocery shopping');
sqlite> insert into todo (description) values ('walk the dog');
sqlite> insert into todo (description) values ('clean the house');
sqlite> insert into todo (description, done) values ('do the laundry', 1);
sqlite> insert into todo (description) values ('take out the trash');
sqlite> select * from todo;
1|grocery shopping|0
2|walk the dog|0
3|clean the house|0
4|do the laundry|1
5|take out the trash|0
sqlite> .exit
>
```
Ensure that the VS project file holds the newly created db file and will _copy always_ to output directory.
Add nuget.config file : `> dotnet new nugetconfig`
Add nuget sqlite package : `dotnet add package Microsoft.Data.Sqlite --version 8.0.0`
Build and run the project :
```
> dotnet restore
  Determining projects to restore...
  Restored C:\Users\nparker\source\repos\ToDo_Dotnet\ToDo\ToDo.Console\ToDo.Console.csproj (in 266 ms).
> dotnet build
MSBuild version 17.8.3+195e7f5a3 for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
  ToDo.Console -> ...\ToDo.Console\bin\Debug\net8.0\ToDo.Console.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:05.73
> dotnet run
Connection String: Data Source=todo.sqlite.db
ID: 1, Description: grocery shopping, Done: No
ID: 2, Description: walk the dog, Done: No
ID: 3, Description: clean the house, Done: No
ID: 4, Description: do the laundry, Done: Yes
ID: 5, Description: take out the trash, Done: No
>
```
Note the contents of the build directory :
```
> Get-ChildItem -Path .\bin\Debug\net8.0\

    Directory: ...\bin\Debug\net8.0

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
d----          12/24/2023  7:42 AM                runtimes
-a---          10/31/2023  4:15 PM         173088 Microsoft.Data.Sqlite.dll
-a---           8/23/2023 12:41 PM           5120 SQLitePCLRaw.batteries_v2.dll
-a---           8/23/2023 12:38 PM          50688 SQLitePCLRaw.core.dll
-a---           8/23/2023 12:38 PM          35840 SQLitePCLRaw.provider.e_sqlite3.dll
-a---          12/24/2023  7:43 AM           8180 ToDo.Console.deps.json
-a---          12/24/2023  7:43 AM           5632 ToDo.Console.dll
-a---          12/24/2023  7:43 AM         140800 ToDo.Console.exe
-a---          12/24/2023  7:43 AM          12076 ToDo.Console.pdb
-a---          12/24/2023  7:43 AM            268 ToDo.Console.runtimeconfig.json
-a---          12/24/2023  7:23 AM           8192 todo.sqlite.db

>
```
Now we **publish** the project and copy it to the Ubuntu/linux server using **scp** :
```
PS > dotnet publish -r linux-x64 --self-contained false
MSBuild version 17.8.3+195e7f5a3 for .NET
  Determining projects to restore...
  Restored ...\ToDo.Console\ToDo.Console.csproj (in 268 ms).
  ToDo.Console -> ...\ToDo.Console\bin\Release\net8.0\linux-x64\ToDo.Console
  .dll
  ToDo.Console -> ...\ToDo.Console\bin\Release\net8.0\linux-x64\publish\
PS > Get-ChildItem -Path .\bin\Release\net8.0\linux-x64\

    Directory: ...\ToDo.Console\bin\Release\net8.0\linux-x64

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
d----          12/24/2023  7:58 AM                publish
-a---           8/23/2023 12:33 PM        1249880 libe_sqlite3.so
-a---          10/31/2023  4:15 PM         173088 Microsoft.Data.Sqlite.dll
-a---           8/23/2023 12:41 PM           5120 SQLitePCLRaw.batteries_v2.dll
-a---           8/23/2023 12:38 PM          50688 SQLitePCLRaw.core.dll
-a---           8/23/2023 12:38 PM          35840 SQLitePCLRaw.provider.e_sqlite3.dll
-a---          12/24/2023  7:58 AM          72520 ToDo.Console
-a---          12/24/2023  7:58 AM           4668 ToDo.Console.deps.json
-a---          12/24/2023  7:58 AM           5120 ToDo.Console.dll
-a---          12/24/2023  7:58 AM          11972 ToDo.Console.pdb
-a---          12/24/2023  7:58 AM            340 ToDo.Console.runtimeconfig.json
-a---          12/24/2023  7:23 AM           8192 todo.sqlite.db

PS > Get-ChildItem -Path .\bin\Release\net8.0\linux-x64\publish\

    Directory: ...\ToDo.Console\bin\Release\net8.0\linux-x64\publish

Mode                 LastWriteTime         Length Name
----                 -------------         ------ ----
-a---           8/23/2023 12:33 PM        1249880 libe_sqlite3.so
-a---          10/31/2023  4:15 PM         173088 Microsoft.Data.Sqlite.dll
-a---           8/23/2023 12:41 PM           5120 SQLitePCLRaw.batteries_v2.dll
-a---           8/23/2023 12:38 PM          50688 SQLitePCLRaw.core.dll
-a---           8/23/2023 12:38 PM          35840 SQLitePCLRaw.provider.e_sqlite3.dll
-a---          12/24/2023  7:58 AM          72520 ToDo.Console
-a---          12/24/2023  7:58 AM           4668 ToDo.Console.deps.json
-a---          12/24/2023  7:58 AM           5120 ToDo.Console.dll
-a---          12/24/2023  7:58 AM          11972 ToDo.Console.pdb
-a---          12/24/2023  7:58 AM            340 ToDo.Console.runtimeconfig.json
-a---          12/24/2023  7:23 AM           8192 todo.sqlite.db

PS > scp .\bin\Release\net8.0\linux-x64\publish\* norman@192.168.1.114:/home/norman/dv/todo_console
The authenticity of host '192.168.1.114 (192.168.1.114)' can't be established.
ED25519 key fingerprint is SHA256:QXqu3FKxN8X2tMAQ7NB1gEbv7f0G4p3QbhB75UYn/9I.
This key is not known by any other names
Are you sure you want to continue connecting (yes/no/[fingerprint])?
Warning: Permanently added '192.168.1.114' (ED25519) to the list of known hosts.
norman@192.168.1.114's password:
Microsoft.Data.Sqlite.dll                                                             100%  169KB 169.0KB/s   00:00
SQLitePCLRaw.batteries_v2.dll                                                         100% 5120     5.0KB/s   00:00
SQLitePCLRaw.core.dll                                                                 100%   50KB   3.1MB/s   00:00
SQLitePCLRaw.provider.e_sqlite3.dll                                                   100%   35KB  35.0KB/s   00:00
ToDo.Console                                                                          100%   71KB  70.8KB/s   00:00
ToDo.Console.deps.json                                                                100% 4668     4.6KB/s   00:00
ToDo.Console.dll                                                                      100% 5120   320.0KB/s   00:00
ToDo.Console.pdb                                                                      100%   12KB  11.7KB/s   00:00
ToDo.Console.runtimeconfig.json                                                       100%  340     0.3KB/s   00:00
libe_sqlite3.so                                                                       100% 1221KB  79.4MB/s   00:00
todo.sqlite.db                                                                        100% 8192     8.0KB/s   00:00
PS >
```
Now we can see and run the ToDo.Console app on the linux server :
```
$ ls
libe_sqlite3.so                SQLitePCLRaw.core.dll                ToDo.Console.deps.json  ToDo.Console.runtimeconfig.json
Microsoft.Data.Sqlite.dll      SQLitePCLRaw.provider.e_sqlite3.dll  ToDo.Console.dll        todo.sqlite.db
SQLitePCLRaw.batteries_v2.dll  ToDo.Console                         ToDo.Console.pdb
$ dotnet ToDo.Console.dll
Connection String: Data Source=todo.sqlite.db
ID: 1, Description: grocery shopping, Done: No
ID: 2, Description: walk the dog, Done: No
ID: 3, Description: clean the house, Done: No
ID: 4, Description: do the laundry, Done: Yes
ID: 5, Description: take out the trash, Done: No
$
```