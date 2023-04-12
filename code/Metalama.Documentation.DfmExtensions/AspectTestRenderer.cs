﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.DocAsCode.MarkdownLite;
using System;
using System.IO;
using System.Text;

namespace Metalama.Documentation.DfmExtensions;

internal class AspectTestRenderer : BaseRenderer<AspectTestToken>
{
    public override string Name => nameof(AspectTestRenderer);

    protected override StringBuffer RenderCore(
        AspectTestToken token,
        MarkdownBlockContext context )
    {
        var id = Path.GetFileNameWithoutExtension( token.Src ).ToLowerInvariant();

        var tabGroup = new AspectTestTabGroup( id );

        void AddCodeTab( string tabId, string suffix, SandboxFileKind kind, string? name = null )
        {
            var tabPath = suffix == "" ? token.Src : Path.ChangeExtension( token.Src, suffix + ".cs" );

            if ( File.Exists( tabPath ) )
            {
                tabGroup.Tabs.Add( new CodeTab( tabId, tabPath, name ?? suffix, kind ) );
            }
        }

        void AddOtherTab( string extension, Func<string, BaseTab> createTab )
        {
            var tabPath = Path.ChangeExtension( token.Src, extension );

            if ( File.Exists( tabPath ) )
            {
                tabGroup.Tabs.Add( createTab( tabPath ) );
            }
        }

        AddCodeTab( "aspect", "Aspect", SandboxFileKind.AspectCode );
        AddCodeTab( "fabric", "Fabric", SandboxFileKind.AspectCode );
        AddCodeTab( "target", "", SandboxFileKind.TargetCode, "Target" );
        AddCodeTab( "dependency", "Dependency", SandboxFileKind.Incompatible );
        AddOtherTab( "t.cs", p => new TransformedTestCodeTab( p ) );
        AddOtherTab( ".t.txt", p => new ProgramOutputTab( p ) );

        var stringBuilder = new StringBuilder();
        tabGroup.Render( stringBuilder, token );

        return stringBuilder.ToString();
    }
}