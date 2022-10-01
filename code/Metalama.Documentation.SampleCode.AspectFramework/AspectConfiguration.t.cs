using Metalama.Framework.Fabrics;
using System;
using System.Linq;

namespace Doc.AspectConfiguration
{
    // The project fabric configures the project at compile time.

#pragma warning disable CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052

    public class Fabric : ProjectFabric
    {
        public override void AmendProject(IProjectAmender amender) => throw new System.NotSupportedException("Compile-time-only code cannot be called at run-time.");

    }

#pragma warning restore CS0067, CS8618, CS0162, CS0169, CS0414, CA1822, CA1823, IDE0051, IDE0052


    // Some target code.
    public class SomeClass
    {
        public void SomeMethod()
        {
            Console.WriteLine($"MyCategory: Executing SomeClass.SomeMethod().");
            return;
        }
    }
}