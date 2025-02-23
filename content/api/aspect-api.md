---
uid: aspect-api
---

# Aspect API documentation

These namespaces are used while writing your own aspects or fabrics.

| Namespace                             | Description                                                                                                                                                     |
|---------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <xref:Metalama.Framework.Aspects>     | The main namespace for creating your own aspects.                                                                                                        |
| <xref:Metalama.Framework.Code>        | Contains the object representation of source code, including interfaces that represent types, methods, fields, etc.                                                |
| <xref:Metalama.Framework.CodeFixes>   | Allows your aspects to suggest code fixes, accessible at design time from the IDE.                                                            |
| <xref:Metalama.Framework.Diagnostics> | Enables your aspects to report or suppress errors, warnings, or information.                                                                           |
| <xref:Metalama.Framework.Eligibility> | Allows your aspects to declare to which declarations they can be validly applied.                                                                    |
| <xref:Metalama.Framework.Fabrics>    | Provides the ability to add aspects or validators to entire projects and namespaces, and configures aspects.                               |
| <xref:Metalama.Framework.Metrics>        | Allows you to read predefined code metrics and to implement your own. Metrics are useful in validators and LinqPad queries.                   |
| <xref:Metalama.Framework.Project>        | Exposes the object model of the project being processed, as well as the service provider.                                                       |
| <xref:Metalama.Framework.RunTime>     | Contains the classes used at runtime. All other namespaces are used only at compilation time.                                         |
| <xref:Metalama.Framework.Validation>  | Enables you to build aspects that can validate user code against your own rules. You can validate both the target of the aspect and _references_ to that target. |


