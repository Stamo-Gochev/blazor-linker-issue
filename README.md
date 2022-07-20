# blazor-linker-issue

Link to bug report:
https://github.com/dotnet/aspnetcore/issues/42832

### Describe the bug

Link to a repository that demonstrates the bug:
https://github.com/Stamo-Gochev/blazor-linker-issue

Publishing a blazor wasm app with a custom component rendered in a `RenderFragment` throws a Mono Linker exception.

The setup has the following specifics:
1. The [blazor wasm app](https://github.com/Stamo-Gochev/blazor-linker-issue/tree/master/BlazorLinkerIssue/BlazorLinkerIssue) that is used for testing is the standard .NET 7.0 project template - nothing specific here
2. A [Razor Class Library](https://github.com/Stamo-Gochev/blazor-linker-issue/tree/master/BlazorLinkerIssue/CustomComponents) is added as a dependency to the blazor wasm app
3. A [custom component](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/CustomComponents/Components/Node.razor) from the Razor Class Library is rendered in the [sample page](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/BlazorLinkerIssue/Client/Pages/Index.razor#L9) in the blazor wasm app.
4. The [custom component](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/CustomComponents/Components/Node.razor) renders itself in [another component](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/CustomComponents/Components/NodeFragment.razor#L3-L5) that itself just renders its [ChildContent](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/CustomComponents/Components/NodeFragment.razor#L3-L5) fragment.

The blazor wasm app is runnable without errors, the problem is reproducible when the app is published (when linker trimming is executed).


### Expected Behavior

No exception is thrown.

### Steps To Reproduce

1. Clone https://github.com/Stamo-Gochev/blazor-linker-issue
2. Navigate to https://github.com/Stamo-Gochev/blazor-linker-issue/tree/master/BlazorLinkerIssue/BlazorLinkerIssue/Server
3. Run
```
dotnet publish -c Release BlazorLinkerIssue.Server.csproj
```

> **Note:** A [global.json](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/BlazorLinkerIssue/Server/global.json#L3) file explicitly targets .NET 7.0 Preview 6. If the project is built with .NET 7.0 Preview 5, i.e. if you use:
```
{
    "sdk": {
        "version": "7.0.100-preview.5.22307.18"
    }
}

```
> instead, then the project builds successfully.

---

Here are the logs of the publish output:
- [.NET 7.0 Preview 6 log](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/BlazorLinkerIssue/Server/publish-output-net7-preview6.txt) - the error is thrown
- [.NET 7.0 Preview 5 log](https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/BlazorLinkerIssue/Server/publish-output-net7-preview5.txt) - no error is thrown

### Exceptions (if any)

An error is thrown:
```
MSBuild version 17.3.0-preview-22329-01+77c72dd0f for .NET
  Determining projects to restore...
  All projects are up-to-date for restore.
C:\Program Files\dotnet\sdk\7.0.100-preview.6.22352.1\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(219,5): message NETSDK1057: You are using a preview version of .NET. See: https://aka.ms/dotnet-support-policy [D:\...\blazor-linker-issue\BlazorLinkerIssue\BlazorLinkerIssue\Server\BlazorLinkerIssue.Server.csproj]
C:\Program Files\dotnet\sdk\7.0.100-preview.6.22352.1\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(219,5): message NETSDK1057: You are using a preview version of .NET. See: https://aka.ms/dotnet-support-policy [D:\...\blazor-linker-issue\BlazorLinkerIssue\CustomComponents\CustomComponents.csproj]
C:\Program Files\dotnet\sdk\7.0.100-preview.6.22352.1\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(219,5): message NETSDK1057: You are using a preview version of .NET. See: https://aka.ms/dotnet-support-policy [D:\...\blazor-linker-issue\BlazorLinkerIssue\BlazorLinkerIssue\Shared\BlazorLinkerIssue.Shared.csproj]
  CustomComponents -> D:\...\blazor-linker-issue\BlazorLinkerIssue\CustomComponents\bin\Release\net7.0\CustomComponents.dll
  BlazorLinkerIssue.Shared -> D:\...\blazor-linker-issue\BlazorLinkerIssue\BlazorLinkerIssue\Shared\bin\Release\net7.0\BlazorLinkerIssue.Shared.dll
  BlazorLinkerIssue.Client -> D:\...\blazor-linker-issue\BlazorLinkerIssue\BlazorLinkerIssue\Client\bin\Release\net7.0\BlazorLinkerIssue.Client.dll
  BlazorLinkerIssue.Client (Blazor output) -> D:\...\blazor-linker-issue\BlazorLinkerIssue\BlazorLinkerIssue\Client\bin\Release\net7.0\wwwroot
  BlazorLinkerIssue.Server -> D:\...\blazor-linker-issue\BlazorLinkerIssue\BlazorLinkerIssue\Server\bin\Release\net7.0\BlazorLinkerIssue.Server.dll
  Optimizing assemblies for size may change the behavior of the app. Be sure to test after publishing. See: https://aka.ms/dotnet-illink
  Optimizing assemblies for size. This process might take a while.
  Stack overflow.
     at Mono.Linker.Steps.MarkStep.MarkType(Mono.Cecil.TypeReference, Mono.Linker.DependencyInfo, System.Nullable`1<Mono.Linker.MessageOrigin>)
     at Mono.Linker.Steps.MarkStep.MarkGenericArguments(Mono.Cecil.IGenericInstance)
```

Full log can be fount at:
https://github.com/Stamo-Gochev/blazor-linker-issue/blob/master/BlazorLinkerIssue/BlazorLinkerIssue/Server/publish-output-net7-preview6.txt

### .NET Version

7.0.100-preview.5.22307.18

### Anything else?

_No response_
