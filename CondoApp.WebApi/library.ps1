# Filename: install-packages.ps1

# Ensure this script is run from the project directory or modify $projectPath
$projectPath = "."

# Add packages
dotnet add "$projectPath" package AutoMapper --version 14.0.0
dotnet add "$projectPath" package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add "$projectPath" package Microsoft.AspNetCore.Authentication.JwtBearer --version 9.0.5
dotnet add "$projectPath" package Microsoft.AspNetCore.OpenApi --version 9.0.5
dotnet add "$projectPath" package Microsoft.EntityFrameworkCore.Design --version 9.0.5
