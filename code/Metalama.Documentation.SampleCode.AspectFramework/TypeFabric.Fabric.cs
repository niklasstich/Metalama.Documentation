// This is public domain Metalama sample code.

using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;
using System.Linq;

namespace Doc.TypeFabric_
{
    internal partial class MyClass
    {
        private class Fabric : TypeFabric
        {
            public override void AmendType( ITypeAmender amender )
            {
                // Adds logging aspect to all public methods.
                amender.Outbound.SelectMany( t => t.Methods.Where( m => m.Accessibility == Accessibility.Public ) )
                    .AddAspect<LogAttribute>();
            }
        }
    }
}