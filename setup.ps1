# Change this to your preferred project name
$projectName = "CondoApp"

Write-Host "Creating solution..."
dotnet new sln -n $projectName

Write-Host "Creating Core project (Entities, Logic)..."
dotnet new classlib -n "$projectName.Core"
New-Item -ItemType Directory -Path "$projectName.Core\Entities" -Force
New-Item -ItemType Directory -Path "$projectName.Core\DTOs" -Force
New-Item -ItemType Directory -Path "$projectName.Core\Services" -Force

Write-Host "Creating Data project (DbContext, Repositories)..."
dotnet new classlib -n "$projectName.Data"
New-Item -ItemType Directory -Path "$projectName.Data\Repositories" -Force
New-Item -ItemType Directory -Path "$projectName.Data\Configurations" -Force

Write-Host "Creating API project..."
dotnet new webapi -n "$projectName.WebApi"
New-Item -ItemType Directory -Path "$projectName.WebApi\Controllers" -Force

Write-Host "Adding projects to solution..."
dotnet sln add "$projectName.Core\$projectName.Core.csproj"
dotnet sln add "$projectName.Data\$projectName.Data.csproj"
dotnet sln add "$projectName.WebApi\$projectName.WebApi.csproj"

Write-Host "Adding project references..."
dotnet add "$projectName.Data\$projectName.Data.csproj" reference "$projectName.Core\$projectName.Core.csproj"
dotnet add "$projectName.WebApi\$projectName.WebApi.csproj" reference "$projectName.Core\$projectName.Core.csproj"
dotnet add "$projectName.WebApi\$projectName.WebApi.csproj" reference "$projectName.Data\$projectName.Data.csproj"

Write-Host "✅ Structure created:"
Write-Host ""
Write-Host "$projectName"
Write-Host "├── $projectName.sln"
Write-Host "├── $projectName.Core      ← business logic, DTOs, entities"
Write-Host "├── $projectName.Data      ← EF, DbContext, repositories"
Write-Host "└── $projectName.WebApi    ← ASP.NET Core API"
