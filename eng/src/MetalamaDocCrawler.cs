﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using HtmlAgilityPack;
using PostSharp.Engineering.BuildTools.Search.Crawlers;
using System;
using System.Linq;

namespace BuildMetalamaDocumentation;

public class MetalamaDocCrawler : DocFxCrawler
{
    // This method parses the breadcrumb of an article
    // (eg. Metalama > 🏠 > Conceptual documentation > Creating aspects > Ordering aspects
    // for https://doc.metalama.net/conceptual/aspects/ordering)
    // and calculates all properties required to create a respective instance
    // of the BreadcrumbInfo record.
    protected override BreadcrumbInfo GetBreadcrumbData( HtmlNode[] breadcrumbLinks )
    {
        var isDefaultKind = breadcrumbLinks.Length < 5;

        var kind = isDefaultKind
            ? "General Information"
            : NormalizeCategoryName( breadcrumbLinks.Skip( 4 ).First().GetText() );

        var breadcrumbTitlesCountToSkip = 4;
        
        var relevantBreadCrumbTitles = breadcrumbLinks
            .Skip( breadcrumbTitlesCountToSkip )
            .Select( n => n.GetText() )
            .ToArray();

        var breadcrumb = string.Join(
            " > ",
            relevantBreadCrumbTitles );

        var isExamplesKind = kind.Contains( "example", StringComparison.OrdinalIgnoreCase );
        var hasCategory = !isExamplesKind;
        
        var category = !hasCategory || breadcrumbLinks.Length < 6
            ? null
            : NormalizeCategoryName( breadcrumbLinks.Skip( 5 ).First().GetText() );
        
        int kindRank;
        var isApiDoc = false; 
        var isPageIgnored = false;
        
        if ( isDefaultKind )
        {
            kindRank = (int) MetalamaDocFxRank.Common;
        }
        else if ( isExamplesKind )
        {
            kindRank = (int) MetalamaDocFxRank.Examples;
        }
        else if ( kind.Contains( "concept", StringComparison.OrdinalIgnoreCase ) )
        {
            kindRank = (int) MetalamaDocFxRank.Conceptual;
        }
        else if ( kind.Contains( "api", StringComparison.OrdinalIgnoreCase ) )
        {
            // The PostSharp API migration doc goes to another collection,
            // so it doesn't clutter the search results for Metalama.
            isPageIgnored = category?.Contains( "postsharp", StringComparison.OrdinalIgnoreCase ) ?? false;
            isApiDoc = true;
            kindRank = (int) MetalamaDocFxRank.Api;
        }
        else
        {
            kindRank = (int) MetalamaDocFxRank.Unknown;
        }

        return new(
            breadcrumb,
            new[] { kind },
            kindRank,
            category == null ? Array.Empty<string>() : new[] { category },
            relevantBreadCrumbTitles.Length,
            isPageIgnored,
            isApiDoc );
    }
}