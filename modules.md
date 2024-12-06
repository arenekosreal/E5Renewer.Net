# Guide to create new module for E5Renewer

We have a module mechanism to extend this program by adding new user secret formats, new graph api implementations, etc.
If you want to create new module, here is the right place.

> [!NOTE]
> All modules will be injected into DI services as a transient service.
> So please do not storage status in your module, as it will be initialized each time other service requires it.

## Choose module type

We supports those modules types now:
- `E5Renewer.Models.ModulesCheckers.IModulesChecker` is for checking modules, such as if it is outdated.
- `E5Renewer.Models.Secrets.IUserSecretLoader` is for adding new user secret format.
- `E5Renewer.Models.GraphAPIs.IGraphAPICaller` is for adding new way to call `E5Renewer.Models.GraphAPIs.IAPIFunction`.
- `E5Renewer.Models.GraphAPIs.IAPIFunction` is for adding new way to call msgraph apis.

If nothing meets your needs, you can implement `E5Renewer.Models.Modules.IModule` directly, 
but it may not have too much meaning because it is just been added into DI services.

## Create a new C# project

If you want to create a new module in this repository, you need to create a new C# project.
It must have references to `E5Renewer.Models.Modules` project, 
because we need `E5Renewer.Models.Modules.ModuleAttribute` for marking your module.

Then you need to add reference to project in the solution based on your module's type.
For example, you need to add `E5Renewer.Models.ModulesCheckers` project if you want to implement `IModulesChecker`.

> [!TIPS]
> If you need to access Asp.Net Core items, like `ILogger`, you can add a
> ```xml
> <ItemGroup>
>   <FrameworkReference Include="Microsoft.AspNetCore.App" />
> </ItemGroup>
> ```
> in your `.csproj` file.

## Write the code

Implement the interface you chose before, fill everything required. 
And at last, do not forget to mark it with `[Module]` attribute.

## Leave a metainfo about your module

It is an assembly level attribute named `E5Renewer.Models.Modules.ModulesInAssemblyAttribute`.
You need to add your class with it, for example, `[ModulesInAssembly(typeof(MyAwesomeModule))]`.

> [!TIP]
> If you added project `E5Renewer.Models.SourceGenerators.ModulesInAssemblyGenerator`,
> we can use it to generate the metainfo for you automatically.
> But this requires you edit `.csproj` file after adding it, 
> adding `OutputItemType="Analyzer" ReferenceOutputAssembly="false"` so it can be used normally.

## Let program know your module

If your module is developped to be added into this repository, you can modify `E5Renewer` project's `.csproj` file,
add a reference to your project, and we will do other things left.

If your module is independent, you need to build it, place it under a folder named by your project in `modules` folder,
which should be placed beside the program:
```
--- E5Renewer.dll
--- modules
------ MyModule
--------- MyModule.dll
````
Please note that you must match the filesystem layer, or your modules may not be loaded correctly.

> [!CAUTION]
> Due to that loading assembly from filesystem path does not satisfy AOT's requirements, 
> you should create module project with our solution as much as possible.
