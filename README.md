# Create new solution file
```
dotnet new sln -n Bootstrap.Solutions
```	


# create new web api project using controllers
```
dotnet new webapi --use-controllers --name Bootstrap.Web.Api
```



# add class library interactors
```
dotnet new classlib --name Bootstrap.Interactors
```

# add class library for data services
```
dotnet new classlib --name Bootstrap.Data.Services
```	

# add class library for ef core data access
```
dotnet new classlib --name Bootstrap.Data.EntityFramework
```

# add class library for third party services
```
dotnet new classlib --name Bootstrap.ThirdParty.Services
```	

# add readme file to solution
```
echo "# Bootstrap.Solutions" >> README.md
```

# add projects to solution
```
dotnet sln add Bootstrap.Web.Api
dotnet sln add Bootstrap.Interactors
dotnet sln add Bootstrap.Data.Services
dotnet sln add Bootstrap.Data.EntityFramework
dotnet sln add Bootstrap.ThirdParty.Services
dotnet sln add Bootstrap.Solutions.Tests
```

# add project references
```
dotnet add Bootstrap.Web.Api reference Bootstrap.Interactors 
dotnet add Bootstrap.Interactors reference Bootstrap.Data.Services 
dotnet add Bootstrap.Interactors reference Bootstrap.ThirdParty.Services 
dotnet add Bootstrap.Data.Services reference Bootstrap.Data.EntityFramework 

dotnet add Bootstrap.Solutions.Tests reference Bootstrap.Web.Api
```


# add nuget packages
```
dotnet add Bootstrap.Interactors package MediatR --version 12.4.1
dotnet add Bootstrap.Interactors package FluentValidation --version 11.11.0

dotnet add Bootstrap.Web.Api package Swashbuckle.AspNetCore --version 7.2.0


dotnet add Bootstrap.Web.Api package Autofac --version 8.2.0
dotnet add Bootstrap.Web.Api package Autofac.Extensions.DependencyInjection --version 10.0.0
dotnet add Bootstrap.Web.Api package MediatR.Extensions.Microsoft.DependencyInjection --version 12.4.1
```

# remove packages
```
dotnet remove Bootstrap.Web.Api package NSwag.AspNetCore
```

# add test project for solution
```
dotnet new xunit --name Bootstrap.Solutions.Tests
```

# run tests
```
dotnet test
```

# move test project to Tests solution folder
```
mkdir Tests
mv Bootstrap.Solutions.Tests tests
```

# add mvc testing package
```
dotnet add Bootstrap.Solutions.Tests package Microsoft.AspNetCore.Mvc.Testing --version 9.0.2
```

# add api versioning and api explorer package
```
dotnet add Bootstrap.Web.Api package Asp.Versioning.Mvc --version 8.1.0
dotnet add Bootstrap.Web.Api package Asp.Versioning.Mvc.ApiExplorer --version 8.1.0
```