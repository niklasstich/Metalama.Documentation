﻿// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using System;
using System.Threading;

namespace Metalama.Documentation.SampleCode.AspectFramework.Retry
{
    internal class RetryAttribute : OverrideMethodAspect
    {
        public int MaxAttempts { get; set; } = 5;

        public override dynamic? OverrideMethod()
        {
            for ( var i = 0;; i++ )
            {
                try
                {
                    return meta.Proceed();
                }
                catch ( Exception e ) when ( i < this.MaxAttempts )
                {
                    Console.WriteLine( $"{e.Message}. Retrying in 100 ms." );
                    Thread.Sleep( 100 );
                }
            }
        }
    }
}