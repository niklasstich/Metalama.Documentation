﻿// This is public domain Metalama sample code.

using Metalama.Documentation.Helpers.ConsoleApp;
using Metalama.Patterns.Caching.Building;
using Microsoft.Extensions.DependencyInjection;

namespace Doc.ObjectDependencies
{
    internal static class Program
    {
        public static void Main()
        {
            var builder = ConsoleApp.CreateBuilder();

            // Add the caching service.
            builder.Services.AddCaching();

            // Add other components as usual, then run the application.
            builder.Services.AddConsoleMain<ConsoleMain>();
            builder.Services.AddSingleton<ProductCatalogue>();

            using var app = builder.Build();
            app.Run();
        }
    }
}