# ASP.NET Web Forms to DotVVM - Converter

This repository contains the source code of the [ASP.NET Web Forms to DotVVM website](https://www.dotvvm.com/webforms). 

It contains:

* The landing page which describes the differences between the **ASPX** and **DotHTML** markup.
* The [ASP.NET Web Forms to DotVVM Converter](https://www.dotvvm.com/webforms/converter). 

## Extending the converter

The converter can be extended with additional rules that help to transform common ASPX constructs.

The process uses the following pipeline:

* The input ASPX file is parsed and exposed as a list of tokens.
* The `Matchers` folder contains classes that iterate the list of tokens and produce `SuggestionInstance` objects representing the type of transform that can be performed.
* Each suggestion instance can provide a list of `Fixes` (transforms made to the token list). When the suggestion is applied, the fixes are invoked (in the order in which they were added to the suggestion instance).
* After applying the suggestion, the process is repeated. It may need several executions until the token list gets stabilized.

Since the token list is linear, in order to read the element hierarchy (find the matching end tag for the begin tag), there is the `ControlInnerElementsReader`.

You can also extend the `BindingParser` class to support transforms in the data-binding expressions.

There are also tests for each transform.
