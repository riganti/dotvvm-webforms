# ASP.NET Web Forms Adapters for DotVVM

This repository contains a set of controls that help with using [DotVVM](https://github.com/riganti/dotvvm) and ASP.NET Web Forms together in one ASP.NET application.

This integration can help with modernization of legacy ASP.NET web apps as it allows to build new parts of the old application using modern and cleaner development methods (MVVM pattern) while utilizing the same business layer and allowing smooth transition between old and new parts of the application thanks to single sign on.

> The development of this library is still in progress. The integration will work with DotVVM 2.0. When the library is more stable, it will be published on Nuget - currently you need to build the source code. 

Any feedback is welcome - we're on [Gitter](https://gitter.im/riganti/dotvvm).


## Controls

* `<dotvvm:RouteLink runat="server">` is ASP.NET Web Forms control that renders hyperlinks for DotVVM routes.

* `<webforms:RouteLink>` is DotVVM control that renders hyperlinks for ASP.NET Web Forms routes.

## Usage

1. Open the csproj file and add change the `<ProjectTypeGuids>` element to the following code:

```
<ProjectTypeGuids>{94EE71E2-EE2A-480B-8704-AF46D2E58D94};{349c5851-65df-11da-9384-00065b846f21};{fae04ec0-301f-11d3-bf4b-00c04f79efbc}</ProjectTypeGuids>
```

2. Install `Dotvvm.Adapters.WebForms` and `Microsoft.Owin.Host.SystemWeb` packages in the project.

3. Add `Startup.cs` and `DotvvmStartup.cs` classes (see [sample](src/DotVVM.Adapters.WebForms/TestSamples/) app).

4. Add the following registration to `web.config`:

```
  <system.web>
    <pages>
      <controls>
        <add tagPrefix="dotvvm" namespace="DotVVM.Adapters.WebForms.Controls.WebForms" assembly="DotVVM.Adapters.WebForms"/>
      </controls>
    </pages>
  </system.web>
```

5. Use the controls to link between ASP.NET Web Forms and DotVVM routes (see [sample](src/DotVVM.Adapters.WebForms/TestSamples/Links/) app).
