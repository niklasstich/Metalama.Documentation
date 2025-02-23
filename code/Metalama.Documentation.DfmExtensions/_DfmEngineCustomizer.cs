﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using JetBrains.Annotations;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using Microsoft.DocAsCode.Dfm;
using Microsoft.DocAsCode.MarkdownLite;
using System.Diagnostics;

namespace Metalama.Documentation.DfmExtensions;

[Export( typeof(IDfmEngineCustomizer) )]
[UsedImplicitly]
public class DfmEngineCustomizer : IDfmEngineCustomizer
{
    public void Customize( DfmEngineBuilder builder, IReadOnlyDictionary<string, object> parameters )
    {
        var includePosition = builder.BlockRules.Select( ( r, i ) => (r, i) ).Single( x => x.r.Name == "DfmIncludeBlock" ).i;

        builder.BlockRules = builder.BlockRules.InsertRange(
            includePosition,
            new IMarkdownRule[]
            {
                new AspectTestTokenRule(),
                new SingleFileTokenRule(),
                new ProjectButtonsTokenRule(),
                new CompareFileTokenRule(),
                new MultipleFilesTokenRule(),
                new VimeoTokenRule()
            } );
    }
}