﻿// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Metalama.Documentation.SampleCode.AspectFramework.TypeFabric_
{
    // A trivial aspect to demonstrate the type fabric.
    public class LogAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            Console.WriteLine( $"Executing {meta.Target.Method}." );

            return meta.Proceed();
        }
    }
}