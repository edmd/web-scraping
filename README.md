1. Download the project 
2. Restore the nuget packages (if your IDE doesn't do this automatically)
3. Ensure .Net Core 3.0 is installed (https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.0.100-windows-x64-installer)
4. Compile the project in your preferred IDE, or use the msbuild from VS Command Prompt
5. Run the test suite (optional)
6. The Console project can be run from "web-scraping\web-scraping.UI\bin\Debug\netcoreapp3.0\web-scraping.UI.exe"

https://www.nuget.org/packages/Newtonsoft.Json
Standard Json manipulation library

https://www.nuget.org/packages/HtmlAgilityPack
Defacto .Net web scraping library

https://www.nuget.org/packages/EasyConsoleCore
I have written Console UI's before, it's long overdue to use a libary for this type of plumbing

https://www.nuget.org/packages/NUnit
https://www.nuget.org/packages/NUnit3TestAdapter
https://www.nuget.org/packages/Moq
Used for Unit testing

Please refer to the "TODO" comments in code for further optimisations.
I spent a large portion of time trying various XPath expressions to get this working
