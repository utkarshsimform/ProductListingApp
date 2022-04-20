# ProductListingApp

Product Listing App web based solution where we can add products based on the brand and manage them.It has functionality to Create, edit, delete product. Product has details like BrandId, BrandName, ProductName
## Getting started


### Prerequisites  

 - Visual Studio 2019 or later  
 - .Net 5.0
 - MS Sql Sever


### Dependancies
#### Nuget Packages
    - Newtonsoft.Json
    - Microsoft.AspNetCore.Mvc.NewtonsoftJson
    - Microsoft.AspNetCore.OData
    - Microsoft.Extensions.DependencyInjection.Abstractions
    - Microsoft.EntityFrameworkCore'
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.AspNetCore.Http.Abstractions    
    - Microsoft.EntityFrameworkCore.InMemory
    - Microsoft.NET.Test.Sdk
    - Moq
    - MSTest.TestAdapter
    - MSTest.TestFramework
    - NUnit

### Database 
Database Server: MS SQL Server 
Database Name: ProtoType

### Project Architecture
Specify your project architecture here. Suppose you are following Repository pattern then mention your all the projects along with short description here.

 - ProtoTypeAPI (End points to manage product)
 - ProtoType.Common (Contains references of other assemblies that are common among multiple projects) 
 - ProtoType.Repository (Contains database operations)
 - ProtoType.Model (Contains request, response and shared data model classes)
 - ProtoType.Service (Contains business logic)
 - ProtoTypeUI (Web app to interact) 
 - ProtoType.Service.UnitTests (Contains all the test methods)

## Running the tests
In this project we have used NUnit. You can run all the tests from the Test Explorer. If Test Explorer is not visible, choose  **Test**  on the Visual Studio menu, choose  **Windows**, and then choose  **Test Explorer**. All the unit tests will be listed so choose the test you want to run. You can also run alto tests by selecteing "Run All" option.
