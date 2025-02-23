---
uid: compile-time-testing
level: 400
---

# Testing compile-time code

When building complex aspects, it's advisable to shift the intricate compile-time logic, for instance, code that queries the code model, to compile-time classes that are not aspects. Unlike aspects, these compile-time classes can be subjected to unit tests.

## Benefits

Unit-testing compile-time classes offers the following advantages:

* It's generally simpler to achieve comprehensive test coverage with unit tests than with aspect tests (see <xref:aspect-testing>),
* Debugging unit tests is easier than debugging aspect tests.

## Creating unit tests for your compile-time code

### Step 1. Disable pruning of compile-time code

In the project defining the compile-time code, set the `MetalamaRemoveCompileTimeOnlyCode` property to `False`:

```xml
<PropertyGroup>
    <MetalamaRemoveCompileTimeOnlyCode>False</MetalamaRemoveCompileTimeOnlyCode>
</PropertyGroup>
```

Failing to follow this step will result in an exception whenever any compile-time code is called from a unit test.

### Step 2. Create an Xunit test project

Proceed to create an Xunit test project as you usually would.

It's strongly recommended to target .NET 6.0 as temporary files cannot be automatically cleaned up with lower .NET versions.

Disable Metalama for the test project by defining the following property:

```xml
<PropertyGroup>
   <MetalamaEnabled>false</MetalamaEnabled>
</PropertyGroup>

### Step 3. Reference the Metalama.Testing.UnitTesting package

```xml
<ItemGroup>
    <PackageReference Include="Metalama.Testing.UnitTesting" Version="CHANGE ME" />
</ItemGroup>
```

### Step 4. Create a test class derived from UnitTestClass

Create a new test class that derives from <xref:Metalama.Testing.UnitTesting.UnitTestClass>.

```cs
 public class MyTests : UnitTestClass { }

```

### Step 5. Create test methods

Each test method _must_ call the <xref:Metalama.Testing.UnitTesting.UnitTestClass.CreateTestContext> and _must_ dispose of the context at the end of the test method.

Your test would typically call the <xref:Metalama.Testing.UnitTesting.TestContext.CreateCompilation*?text=context.CreateCompilation> method to obtain an <xref:Metalama.Framework.Code.ICompilation>.

```cs

public class MyTests : UnitTestClass
{
    [Fact]
    public void SimpleTest()
    {
        // Create a test context and dispose of it at the end of the test.
        using var testContext = this.CreateTestContext();

        // Create a compilation
        var code = @"
class C
{
    void M1 () {}

    void M2()
    {
        var x = 0;
        x++;
    }
}

";

        var compilation = testContext.CreateCompilation( code );

        var type = compilation.Types.OfName( "C" ).Single();

        var m1 = type.Methods.OfName( "M1" ).Single();

        // Perform any assertion. Typically, your compile-time code would be called here.
        Assert.Equal( 0, m1.Parameters.Count );
    }
}
```

> [!NOTE]
> Some APIs require the execution context to be set and assigned to your compilation. Currently, there's no public API to modify the execution context.

