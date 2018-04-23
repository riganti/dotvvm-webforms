# ASP.NET Web Forms Adapters for DotVVM

This repository contains a set of controls that help with using [DotVVM](https://github.com/riganti/dotvvm) and ASP.NET Web Forms together in one ASP.NET application.

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
