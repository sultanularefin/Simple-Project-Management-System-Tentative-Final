
## error:: 
```c#

Severity	Code	Description	Project	File	Line	Suppression State
Error		The specified task executable location "C:\Users\Taxi\PPP\2021\Simple-Project-Management-System-Tentative-Final\packages\Microsoft.Net.Compilers.2.1.0\build\..\tools\csc.exe" is invalid.	Smpl_prjct_mng_mnt_tol			


```


## again pasting::
```c#

Severity	Code	Description	Project	File	Line	Suppression State
Error		The specified task executable location "C:\Users\Taxi\PPP\2021\Simple-Project-Management-System-Tentative-Final\packages\Microsoft.Net.Compilers.2.1.0\build\..\tools\csc.exe" is invalid.	Smpl_prjct_mng_mnt_tol			

```

## stack over Flow::

```js

Based on the message, seems the csc.exe is damaged.

Just try below things to narrow down the issue:

Navigate to the Microsoft.Net.Compilers package location, delete the package folder, then rebuild. Generally the package will be restored automatically during the build.

YOUR_PROJECT_DIR\packages\Microsoft.Net.Compilers.2.6.1

Uninstall and Re-install the Microsoft.Net.Compilers package:

Open Visual Studio
Go to Tools -> NuGet Package Manager -> Manage NuGet Packages for Solution
Go to Installed tab and find Microsoft.Net.Compilers package
Uninstall the package from your project
Try to build your project now. (Thus it will use the default compiler which lives in the .NET framework folder: C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe)
If you really need the Microsoft.Net.Compilers package then go ahead and find it in the Browse tab and install the latest stable version.
Build the project and if everything works, make sure you commit changes to your code repository.
You can also try to run below command to uninstall and reinstall the Nuget packages from the Package Manager Console: See Microsoft.Net.Compilers

Uninstall-Package Microsoft.Net.Compilers -Version 2.6.1

Install-Package Microsoft.Net.Compilers -Version 2.6.1
```