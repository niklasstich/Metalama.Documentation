﻿// Copyright (c) SharpCrafters s.r.o. All rights reserved.
// This project is not open source. Please see the LICENSE.md file in the repository root for details.

using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Diagnostics;
using System;
using System.Linq;

namespace Metalama.Documentation.SampleCode.AspectFramework.ImportService
{
    internal class ImportAspect : OverrideFieldOrPropertyAspect
    {
        private static readonly DiagnosticDefinition<INamedType> _serviceProviderFieldMissing = new(
            "MY001",
            Severity.Error,
            "The 'ImportServiceAspect' aspects requires the type '{0}' to have a field named '_serviceProvider' and " +
            " of type 'IServiceProvider'." );

        private static readonly DiagnosticDefinition<(IField, IType)> _serviceProviderFieldTypeMismatch = new(
            "MY002",
            Severity.Error,
            "The type of field '{0}' must be 'IServiceProvider', but it is '{1}." );

        private static readonly SuppressionDefinition _suppressFieldIsNeverUsed = new( "CS0169" );

        public override void BuildAspect( IAspectBuilder<IFieldOrProperty> builder )
        {
            // Get the field _serviceProvider and check its type.
            var serviceProviderField = builder.Target.DeclaringType.Fields.OfName( "_serviceProvider" ).SingleOrDefault();

            if ( serviceProviderField == null )
            {
                _serviceProviderFieldMissing.WithArguments( builder.Target.DeclaringType ).ReportTo( builder.Diagnostics );

                return;
            }
            else if ( !serviceProviderField.Type.Is( typeof(IServiceProvider) ) )
            {
                _serviceProviderFieldTypeMismatch.WithArguments( (serviceProviderField, serviceProviderField.Type) ).ReportTo( builder.Diagnostics );

                return;
            }

            // Provide the advice.
            base.BuildAspect( builder );

            // Suppress the diagnostic.
            builder.Diagnostics.Suppress( serviceProviderField, _suppressFieldIsNeverUsed );
        }

        public override dynamic? OverrideProperty
        {
            get => meta.This._serviceProvider.GetService( meta.Target.FieldOrProperty.Type.ToType() );

            set => throw new NotSupportedException();
        }
    }
}