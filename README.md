# ToDo_Dotnet
### Description
This project is an example of a web API and web user interface meant to be published on a Ubuntu/linux server running .NET.

### Database
Each sub-project will use a database. For simplicity, the starting choice is Sqlite due to its small rapid interface.

### Links
[Sqlite](https://sqlite.org/index.html)
[Sqlite nuget](https://www.nuget.org/packages/Microsoft.Data.Sqlite)
[.NET on Ubuntu](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-2204)

### Installing .NET on Ubuntu/Linux
In order to install on Ubuntu 22.04 I had to run the following from [Microsoft Ubuntu Troubleshooting](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu#troubleshooting):
```
sudo apt-get install -y gpg
wget -O - https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor -o microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget https://packages.microsoft.com/config/ubuntu/22.04/prod.list
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list
sudo apt-get update && \
  sudo apt-get install -y aspnetcore-runtime-8.0
```
Note that I only installed the runtime .NET because I do not intend on building anything on the linux machine.
Validating that .NET is installed :
```
$ dotnet --list-runtimes
Microsoft.AspNetCore.App 8.0.0 [/usr/share/dotnet/shared/Microsoft.AspNetCore.App]
Microsoft.NETCore.App 8.0.0 [/usr/share/dotnet/shared/Microsoft.NETCore.App]
```

### Console Project : ToDo.Console
See the readme.md in the project folder for details