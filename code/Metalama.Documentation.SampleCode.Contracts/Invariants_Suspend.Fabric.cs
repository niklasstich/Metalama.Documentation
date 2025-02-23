﻿// This is public domain Metalama sample code.

using Metalama.Framework.Fabrics;
using Metalama.Patterns.Contracts;

namespace Doc.Invariants_Suspend
{
    public class Fabric : ProjectFabric
    {
        public override void AmendProject( IProjectAmender amender )
        {
            amender.Outbound.SetOptions( new ContractOptions { IsInvariantSuspensionSupported = true } );
        }
    }
}