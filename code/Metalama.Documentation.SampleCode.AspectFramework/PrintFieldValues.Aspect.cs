﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Metalama.Framework.Aspects;
using System;

namespace Doc.PrintFieldValues
{
    internal class PrintFieldValuesAttribute : OverrideMethodAspect
    {
        public override dynamic? OverrideMethod()
        {
            foreach ( var fieldOrProperty in meta.Target.Type.FieldsAndProperties )
            {
                if ( !fieldOrProperty.IsImplicitlyDeclared && fieldOrProperty.IsAutoPropertyOrField == true )
                {
                    var value = fieldOrProperty.Invokers.Final.GetValue( fieldOrProperty.IsStatic ? null : meta.This );
                    Console.WriteLine( $"{fieldOrProperty.Name}={value}" );
                }
            }

            return meta.Proceed();
        }
    }
}