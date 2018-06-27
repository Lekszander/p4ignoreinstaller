# p4ignoreinstaller
Deploys a p4 ignore file to the owner's workstation and sets the appropriate environment variable.

## Features
* **Settings**  
The only setting available as of now is the ability to modify the contents of the ignore file that will be deployed.
* **Select Install Location**  
The Perforce ignore file doesn't work like a .gitignore file. It can be located anywhere in your file system, as long as the P4IGNORE environment variable points to it.
* **Deploy**  
As the name implies, this sets the environment variable to point to your file.

## Requirements
* Operating system: Windows Vista or newer.

## Dependencies
* AvalonEdit 5.0.4 (https://www.nuget.org/packages/AvalonEdit/)  
Install via: **Install-Package AvalonEdit -Version 5.0.4**
* WindowsAPICodePack-Shell (https://www.nuget.org/packages/WindowsAPICodePack-Shell/)  
Install via: **Install-Package WindowsAPICodePack-Shell**
