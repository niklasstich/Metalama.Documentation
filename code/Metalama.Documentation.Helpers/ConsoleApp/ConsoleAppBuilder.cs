﻿// Copyright (c) SharpCrafters s.r.o. See the LICENSE.md file in the root directory of this repository root for details.

using Microsoft.Extensions.DependencyInjection;

namespace Metalama.Documentation.Helpers.ConsoleApp;

public class ConsoleAppBuilder
{
    private readonly ServiceCollection _serviceCollection = new();

    public IServiceCollection Services => this._serviceCollection;

    public ConsoleApp Build( IReadOnlyList<string>? arguments = null )
    {
        this._serviceCollection.AddSingleton<IConsoleHost>( _ => new ConsoleHost( arguments ) );

        return new ConsoleApp( this._serviceCollection.BuildServiceProvider() );
    }
}