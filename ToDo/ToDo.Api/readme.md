This project is the API portion, the backend raw data, meant to be run in linux.
I copied the database `todo.sqlite.db` from the console project.
Publish Configuration
    - Target location: bin\Release\net8.0\publish
    - Configuration: Release
    - Target Framework: net8.0
    - Target Runtime: linux-x64

Publish via Visual Studio to folder.
After *publish*, copy (scp) the files to the linux server:
```
PS > scp .\bin\Release\net8.0\publish\* norman@192.168.1.114:/home/norman/dv/todo_api
Microsoft.Data.Sqlite.dll                                                             100%  169KB  10.3MB/s   00:00
SQLitePCLRaw.batteries_v2.dll                                                         100% 5120     5.0KB/s   00:00
SQLitePCLRaw.core.dll                                                                 100%   50KB  49.5KB/s   00:00
SQLitePCLRaw.provider.e_sqlite3.dll                                                   100%   35KB   2.2MB/s   00:00
ToDo.Api                                                                              100%   71KB  70.8KB/s   00:00
ToDo.Api.deps.json                                                                    100% 4908     4.8KB/s   00:00
ToDo.Api.dll                                                                          100%   13KB  12.5KB/s   00:00
ToDo.Api.pdb                                                                          100%   23KB   1.4MB/s   00:00
ToDo.Api.runtimeconfig.json                                                           100%  595     0.6KB/s   00:00
ToDo.Shared.dll                                                                       100% 5120     5.0KB/s   00:00
ToDo.Shared.pdb                                                                       100%   11KB  11.5KB/s   00:00
appsettings.Development.json                                                          100%  127     0.1KB/s   00:00
appsettings.json                                                                      100%  233     0.2KB/s   00:00
libe_sqlite3.so                                                                       100% 1221KB  76.3MB/s   00:00
nuget.config                                                                          100%  288     0.3KB/s   00:00
todo.sqlite.db                                                                        100% 8192     8.0KB/s   00:00
```

Run it on linux:
`$ dotnet ./ToDo.Api.dll`
Note: If there's no endpoint configuration, then Kestrel binds to http://localhost:5000. So that run command will only work for localhost 'curl' commands. In order to run it for other endpoints there are options.
An easy one is to run it like:
`$ dotnet ./ToDo.Api.dll --urls http://0.0.0.0:5000`
See [Kestrel configuration](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-8.0)