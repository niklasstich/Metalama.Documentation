---
uid: auxiliary-templates
level: 300
---

# Calling auxiliary templates

Auxiliary templates are templates designed to be called from other templates. When an auxiliary template is called from a template, the code generated by the auxiliary template is expanded at the point where it is called.

There are two primary reasons why you may want to use auxiliary templates:

* Code reuse: Moving repetitive code logic to an auxiliary template can reduce duplication. This aligns with the primary goal of Metalama, which is to streamline code writing.
* Abstraction: Since template methods can be `virtual`, you can allow the users of your aspects to modify the templates.

There are two ways to call a template: the standard way, just like you would call any C# method, and the dynamic way, which addresses more advanced scenarios. Both approaches will be covered in the subsequent sections.

## Creating auxiliary templates

To create an auxiliary template, follow these steps:

1. Just like a normal template, create a method and annotate it with the <xref:Metalama.Framework.Aspects.TemplateAttribute?text=[Template]> custom attribute.

2. If you are creating this method outside of an aspect or fabric type, ensure that this class implements the <xref:Metalama.Framework.Aspects.ITemplateProvider> empty interface.

    > [!NOTE]
    > This rule applies even if you want to create a helper class that contains only `static` methods. In this case, you cannot mark the class as `static`, but you can add a unique `private` constructor to prevent instantiation of the class.

3. Most of the time, you will want auxiliary templates to be `void`, as explained below.

A template can invoke another template just like any other method. You can pass values to its compile-time and run-time [parameters](xref:template-parameters).

> [!WARNING]
> An important limitation to bear in mind is that templates can be invoked only as _statements_ and not as part of an _expression_. We will revisit this restriction later in this article.

### Example: simple auxiliary templates

The following example is a simple caching aspect. The aspect is intended to be used in different projects, and in some projects, we want to log a message in case of cache hit or miss. Therefore, we moved the logging logic to `virtual` auxiliary template methods, with an empty implementation by default. In `CacheAndLog`, we override the logging logic.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate.cs]

## Return statements in auxiliary templates

The behavior of `return` statements in auxiliary templates can sometimes be confusing compared to normal templates. Their nominal processing by the T# compiler is identical (indeed the T# compiler does not differentiate auxiliary templates from normal templates as their difference is only in usage): `return` statements in any template result in `return` statements in the output.

In a normal non-void C# method, all execution branches must end with a `return <expression>` statement. However, because auxiliary templates often generate snippets instead of complete method bodies, you don't always want every branch of the auxiliary template to end with a `return` statement.

To work around this situation, you can make the subtemplate `void` and call the <xref:Metalama.Framework.Aspects.meta.Return*?text=meta.Return> method, which will generate a `return <expression>` statement while making the C# compiler satisfied with your template.

> [!NOTE]
> There is no way to explicitly interrupt the template processing other than playing with compile-time `if`, `else` and `switch` statements and ensuring that the control flow continues to the natural end of the template method.

### Example: meta.Return

The following example is a variation of our previous caching example, but we abstract the entire caching logic instead of just the logging part. The aspect has two auxiliary templates: `GetFromCache` and `AddToCache`. The first template is problematic because the cache hit branch must have a `return` statement while the cache miss branch must continue the execution. Therefore we designed `GetFromCache` as a `void` template and used <xref:Metalama.Framework.Aspects.meta.Return*?text=meta.Return> to generate the `return` statement.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate_Return.cs]

## Dynamic invocation of generic templates

Auxiliary templates can be beneficial when you need to call a generic API from a `foreach` loop and the type parameter must be bound to a type that depends on the iterator variable.

For instance, suppose you want to generate a field-by-field implementation of the `Equals` method and you want to invoke the `EqualityComparer<T>.Default.Equals` method for each field or property of the target type. C# does not allow you to write `EqualityComparer<field.Type>.Default.Equals`, although this is what you would conceptually need.

In this situation, you can use an auxiliary template with a [compile-time type parameter](xref:template-parameters).

To invoke the template, use the <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*?text=meta.InvokeTemplate> and specify the `args` parameter. For instance:

```cs
meta.InvokeTemplate( nameof(CompareFieldOrProperty), args:
     new { TFieldOrProperty = fieldOrProperty.Type, fieldOrProperty, other = (IExpression) other! } );
```

This is illustrated by the following example:

### Example: invoke generic template

The following aspect implements the `Equals` method by comparing all fields or automatic properties. For the sake of the exercise, we want to call the `EqualityComparer<T>.Default.Equals` method with the proper value of `T` for each field or property. This is achieved by use of an auxiliary template and the <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*?text=meta.InvokeTemplate> method.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate_StructurallyComparable.cs]

## Delegate-like invocation

Calls to auxiliary templates can be encapsulated into an object of type <xref:Metalama.Framework.Aspects.TemplateInvocation>, similar to the encapsulation of a method call into a delegate. The <xref:Metalama.Framework.Aspects.TemplateInvocation> can be passed as an argument to another auxiliary template and invoked by the <xref:Metalama.Framework.Aspects.meta.InvokeTemplate*?text=meta.InvokeTemplate> method.

This technique is helpful when an aspect allows customizations of the generated code but when the customized template must call a given logic. For instance, a caching aspect may allow the customization to inject some `try..catch`, and therefore requires a mechanism for the customization to call the desired logic inside the `try..catch`.

### Example: delegate-like invocation

The following code shows a base caching aspect named `CacheAttribute` that allows customizations to wrap the entire caching logic into arbitrary logic by overriding the `AroundCaching` template. This template must by contract invoke the <xref:Metalama.Framework.Aspects.TemplateInvocation> it receives. The `CacheAndRetryAttribute` uses this mechanism to inject retry-on-exception logic.

[!metalama-test ~/code/Metalama.Documentation.SampleCode.AspectFramework/AuxiliaryTemplate_TemplateInvocation.cs]

This example is contrived in two regards. First, it would make sense in this case to use two aspects. Second, the use of a `protected` method invoked by `AroundCaching` would be preferable in this case. The use of <xref:Metalama.Framework.Aspects.TemplateInvocation> makes sense when the template to call is not a part of the same class -- for instance if the caching aspect accepts options that can be set from a fabric, and that would allow users to supply a different implementation of this logic without overriding the caching attribute itself.


