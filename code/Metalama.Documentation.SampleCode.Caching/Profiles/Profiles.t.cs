using Metalama.Patterns.Caching;
using Metalama.Patterns.Caching.Aspects;
using Metalama.Patterns.Caching.Aspects.Helpers;
using System;
using System.Reflection;
namespace Doc.Profiles
{
  public sealed class ProductCatalogue
  {
    public int OperationCount { get; private set; }
    [Cache(ProfileName = "Hot")]
    public decimal GetPrice(string productId)
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetPrice_Source((string)args[0]);
      }
      return _cachingService!.GetFromCacheOrExecute<decimal>(_cacheRegistration_GetPrice!, this, new object[] { productId }, Invoke);
    }
    private decimal GetPrice_Source(string productId)
    {
      Console.WriteLine("Getting the price from database.");
      this.OperationCount++;
      return 100 + this.OperationCount;
    }
    [Cache]
    public string[] GetProducts()
    {
      object? Invoke(object? instance, object? [] args)
      {
        return ((ProductCatalogue)instance).GetProducts_Source();
      }
      return _cachingService!.GetFromCacheOrExecute<string[]>(_cacheRegistration_GetProducts!, this, new object[] { }, Invoke);
    }
    private string[] GetProducts_Source()
    {
      Console.WriteLine("Getting the product list from database.");
      this.OperationCount++;
      return new[]
      {
        "corn"
      };
    }
    private static readonly CachedMethodMetadata _cacheRegistration_GetPrice;
    private static readonly CachedMethodMetadata _cacheRegistration_GetProducts;
    private ICachingService _cachingService;
    static ProductCatalogue()
    {
      ProductCatalogue._cacheRegistration_GetProducts = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetProducts", BindingFlags.Public | BindingFlags.Instance, null, Type.EmptyTypes, null)!, "ProductCatalogue.GetProducts()"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = (string? )null, SlidingExpiration = null }, true);
      ProductCatalogue._cacheRegistration_GetPrice = CachedMethodMetadata.Register(RunTimeHelpers.ThrowIfMissing(typeof(ProductCatalogue).GetMethod("GetPrice", BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(string) }, null)!, "ProductCatalogue.GetPrice(string)"), new CachedMethodConfiguration() { AbsoluteExpiration = null, AutoReload = null, IgnoreThisParameter = null, Priority = null, ProfileName = "Hot", SlidingExpiration = null }, false);
    }
    public ProductCatalogue(ICachingService? cachingService = default)
    {
      this._cachingService = cachingService ?? throw new System.ArgumentNullException(nameof(cachingService));
    }
  }
}