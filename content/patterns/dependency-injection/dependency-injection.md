---
uid: dependency-injection-aspect
---

# Injecting dependencies

Dependency injection is one of the most prevalent patterns in .NET. Prior to .NET Core, the community developed several frameworks with varying features and coding patterns. Since then, `Microsoft.Extensions.DependencyInjection` has emerged as the default framework.

Unlike its predecessors, `Microsoft.Extensions.DependencyInjection` does not rely on custom attributes on fields and properties. Instead, it requires you to add the dependencies to the class constructor and store them as fields. This requirement can lead to some boilerplate code, especially if there are numerous dependencies in a complex class hierarchy. Moreover, it can be tedious to migrate your code from an attribute-based framework to a constructor-based one.

To alleviate these minor inconveniences, you can employ the <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute?text=[Dependency]> aspect of `Metalama.Extensions.DependencyInjection`.

The advantages of this aspect include:

* Reduction of boilerplate code,
* Simplified migration from attribute-based frameworks to constructor-based ones,
* Compatibility with multiple dependency injection frameworks (see <xref:dependency-injection>).

The <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute?text=[Dependency]> aspect provides two properties:

* <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute.IsLazy> generates code that resolves the dependency lazily, upon the first access.
* <xref:Metalama.Extensions.DependencyInjection.DependencyAttribute.IsRequired> determines whether the code can execute if the property is missing.


## Example: Injecting Dependencies

The following example demonstrates the code generation pattern for three types of dependency: required, optional, and lazy.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.DependencyInjection/DependencyInjectionAspect.cs]

