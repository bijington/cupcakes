# Part 0 - Overview

Let's start by getting a basic overview of what .NET MAUI and why we would consider using it

## What is .NET MAUI?

.NET Multi-platform App UI (MAUI) is a cross platform framework for building modern applications.
It is possible to support running on multiple idioms:

* Mobile - Android and iOS
* Desktop - macOS* and Windows
* Fridges/TVs - through the Tizen platform which Samsung provides support for

![alt text](maui-prism.png)

It is the evolution of `Xamarin.Forms`. It is now versioned and released alongside .NET so we will receive a new major version each year with every even major version number providing long term support.
This evolution brings some much needed benefits from finally being a first class citizen of the .NET ecosystem.

*It is worth noting that the macOS support is provided through MacCatalyst which is Apples mechanism for running iPad applications on a Mac.

----

We mentioned how .NET MAUI supports the multiple platforms so let’s take a quick look at how this is achieved.

![alt text](maui-compilation.png)

Taking the example of compiling our application we can make the following statement:

**Our code** is compiled against **.NET MAUI**, **.NET for Android** and the **Base Class Library**. It then runs on the **Mono Runtime** which provides a full implementation of the **Base Class Library** on the **Android** platform.

----

Mention different architectures

----

## Creating our first .NET MAUI project together

Rather than talking too much about .NET MAUI I think it will be best to create a project and dive into what the default template offers us.

### Using Visual Studio

### Using Visual Studio Code

### Using Rider

### Using the .NET CLI

Open a terminal application of your choice, `cd` to the location that you wish to store your project and run the following command:

```dotnetcli
dotnet new maui --name Cupcakes
```

Note that this option won't create an SLN file which is mentioned below, you can just open the csproj file in the IDE of your choosing instead.

----

## Open Solution in your chosen IDE

1. Open **Cupcakes.sln**

This solution contains 1 project:

* Cupcakes - The main .NET MAUI project that targets Android, iOS, macOS, and Windows. Yes that is right, by default it doesn't target Tizen that is down to it requiring additional setup in order to support it, you should see the following entries in the *Cupcakes.csproj* file confirming this:

```xml
<!-- Uncomment to also build the tizen app. You will need to install tizen by following this: https://github.com/Samsung/Tizen.NET -->
<!-- <TargetFrameworks>$(TargetFrameworks);net8.0-tizen</TargetFrameworks> -->
```

The structure of the project should look as follows:

* <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/solid/desktop.svg" width="20" height="20"> Cupcakes
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/folder.svg" width="20" height="20"> Dependencies
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/folder.svg" width="20" height="20"> Properties
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/folder.svg" width="20" height="20"> Platforms
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/folder.svg" width="20" height="20"> Resources
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/file.svg" width="15" height="15"> App.xaml
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/file.svg" width="15" height="15"> AppShell.xaml
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/file.svg" width="15" height="15"> MainPage.xaml
  * <img src="https://raw.githubusercontent.com/FortAwesome/Font-Awesome/6.x/svgs/regular/file.svg" width="15" height="15"> MauiProgram.cs

----

## Understanding the .NET MAUI single project

.NET MAUI single project takes the platform-specific development experiences you typically encounter while developing apps and abstracts them into a single shared project that can target Android, iOS, macOS, and Windows.

.NET MAUI single project provides a simplified and consistent cross-platform development experience, regardless of the platforms being targeted. .NET MAUI single project provides the following features:

* A single shared project that can target Android, iOS, macOS, and Windows.
* A simplified debug target selection for running your .NET MAUI apps.
* Shared resource files within the single project.
* Access to platform-specific APIs and tools when required.
* A single cross-platform app entry point.

.NET MAUI single project is enabled using multi-targeting and the use of SDK-style projects.

### Resource files

Resource management for cross-platform app development has traditionally been problematic. Each platform has its own approach to managing resources, that must be implemented on each platform. For example, each platform has differing image requirements that typically involves creating multiple versions of each image at different resolutions. Therefore, a single image typically has to be duplicated multiple times per platform, at different resolutions, with the resulting images having to use different filename and folder conventions on each platform.

.NET MAUI single project enables resource files to be stored in a single location while being consumed on each platform. This includes fonts, images, the app icon, the splash screen, and raw assets.

> IMPORTANT:
> Each image resource file is used as a source image, from which images of the required resolutions are generated for each platform at build time.

Resource files should be placed in the _Resources_ folder of your .NET MAUI app project, or child folders of the _Resources_ folder, and must have their build action set correctly. The following table shows the build actions for each resource file type:

| Resource | Build action |
| -------- | ------------ |
| App icon | MauiIcon |
| Fonts | MauiFont |
| Images | MauiImage |
| Splash screen | MauiSplashScreen |
| Raw assets | MauiAsset |

<!--| CSS files | MauiCss | -->

> NOTE:
> XAML files are also stored in your .NET MAUI app project, and are automatically assigned the **MauiXaml** build action when created by project and item templates. However, XAML files will not typically be located in the _Resources_ folder of the app project.

When a resource file is added to a .NET MAUI app project, a corresponding entry for the resource is created in the project (.csproj) file. After adding a resource file, its build action can be set in the **Properties** window.

Child folders of the _Resources_ folder can be designated for each resource type by editing the project file for your app:

```xml
<ItemGroup>
    <!-- Images -->
    <MauiImage Include="Resources\Images\*" />

    <!-- Fonts -->
    <MauiFont Include="Resources\Fonts\*" />

    <!-- Raw Assets (also remove the "Resources\Raw" prefix) -->
    <MauiAsset Include="Resources\Raw\**" LogicalName="%(RecursiveDir)%(Filename)%(Extension)" />
</ItemGroup>
```

The wildcard character (`*`) indicates that all the files within the folder will be treated as being of the specified resource type. In addition, it's possible to include all files from child folders:

```xml
<ItemGroup>
    <!-- Images -->
    <MauiImage Include="Resources\Images\**\*" />
</ItemGroup>
```

In this example, the double wildcard character ('**') specifies that the _Images_ folder can contain child folders. Therefore, `<MauiImage Include="Resources\Images\**\*" />` specifies that any files in the _Resources\Images_ folder, or any child folders of the _Images_ folder, will be used as source images from which images of the required resolution are generated for each platform.

Platform-specific resources will override their shared resource counterparts. For example, if you have an Android-specific image located at _Platforms\Android\Resources\drawable-xhdpi\logo.png_, and you also provide a shared _Resources\Images\logo.svg_ image, the Scalable Vector Graphics (SVG) file will be used to generate the required Android images, except for the XHDPI image that already exists as a platform-specific image.

### App icons

An app icon can be added to your app project by dragging an image into the _Resources\Images_ folder of the project, and setting the build action of the icon to **MauiIcon** in the **Properties** window. This creates a corresponding entry in your project file:

```xml
<MauiIcon Include="Resources\Images\appicon.png" />
```

At build time, the app icon is resized to the correct sizes for the target platform and device. The resized app icons are then added to your app package. App icons are resized to multiple resolutions because they have multiple uses, including being used to represent the app on the device, and in the app store.

### Images

Images can be added to your app project by dragging them to the _Resources\Images_ folder of your project, and setting their build action to **MauiImage** in the **Properties** window. This creates a corresponding entry per image in your project file:

```xml
<MauiImage Include="Resources\Images\logo.jpg" />
```

At build time, images are resized to the correct resolutions for the target platform and device. The resized images are then added to your app package.

![alt text](font-creation.png)

### Fonts

True type format (TTF) and open type font (OTF) fonts can be added to your app project by dragging them into the _Resources\Fonts_ folder of your project, and setting their build action to **MauiFont** in the **Properties** window. This creates a corresponding entry per font in your project file:

```xml
<MauiFont Include="Resources\Fonts\OpenSans-Regular.ttf" />
```

At build time, the fonts are copied to your app package.

<!-- For more information, see [Fonts](~/user-interface/fonts.md). -->

### Splash screen

A slash screen can be added to your app project by dragging an image into the _Resources\Images_ folder of your project, and setting the build action of the image to **MauiSplashScreen** in the **Properties** window. This creates a corresponding entry in your project file:

```xml
<MauiSplashScreen Include="Resources\Images\splashscreen.svg" />
```

At build time, the splash screen image is resized to the correct size for the target platform and device. The resized splash screen is then added to your app package.

### Raw assets

Raw asset files, such as HTML, JSON, and videos, can be added to your app project by dragging them into the _Resources_ folder of your project (or a sub-folder, such as _Resources\Assets_), and setting their build action to `MauiAsset` in the **Properties** window. This creates a corresponding entry per asset in your project file:

```xml
<MauiAsset Include="Resources\Assets\index.html" />
```

Raw assets can then be consumed by controls, as required:

```xaml
<WebView Source="index.html" />
```

At build time, raw assets are copied to your app package.

----

## Understanding .NET MAUI app startup

.NET Multi-platform App UI (.NET MAUI) apps are bootstrapped using the .NET Generic Host model. This enables apps to be initialized from a single location, and provides the ability to configure fonts, services, and third-party libraries.

Each platform entry point calls a `CreateMauiApp` method on the static `MauiProgram` class that creates and returns a `MauiApp`, the entry point for your app.

The `MauiProgram` class must at a minimum provide an app to run:

```csharp
namespace MyMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>();

        return builder.Build();
    }
}  
```

The `App` class derives from the `Application` class:

```csharp
namespace MyMauiApp;

public class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
```

### Register fonts

Fonts can be added to your app and referenced by filename or alias. This is accomplished by invoking the `ConfigureFonts` method on the `MauiAppBuilder` object. Then, on the `IFontCollection` object, call the `AddFont` method to add the required font:

```csharp

namespace MyMauiApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        return builder.Build();
    }
}
```

In the example above, the first argument to the `AddFont` method is the font filename, while the second argument represents an optional alias by which the font can be referenced when consuming it.

Any custom fonts consumed by an app must be included in your .csproj file. This can be accomplished by referencing their filenames, or by using a wildcard:

```xml
<ItemGroup>
   <MauiFont Include="Resources\Fonts\*" />
</ItemGroup>
```

> NOTE:
> Fonts added to the project through the Solution Explorer in Visual Studio will automatically be included in the .csproj file.

The font can then be consumed by referencing its name, without the file extension:

```xaml
<!-- Use font name -->
<Label Text="Hello .NET MAUI"
       FontFamily="OpenSans-Regular" />
```

Alternatively, it can be consumed by referencing its alias:

```xaml
<!-- Use font alias -->
<Label Text="Hello .NET MAUI"
       FontFamily="OpenSansRegular" />
```

Now that you have a basic understanding of the .NET MAUI project, let's start building our app! Head over to [Part 1](../Part%201%20-%20Displaying%20Data/readme.md).
