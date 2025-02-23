using System;
using System.Collections.Concurrent;
namespace Doc.AuxiliaryTemplate_TemplateInvocation
{
  public class SelfCachedClass
  {
    [Cache]
    public int Add(int a, int b)
    {
      {
        var cacheKey = $"Add({string.Join(", ", new object[] { a, b })})";
        if (!this._cache.TryGetValue(cacheKey, out var returnValue))
        {
          returnValue = a + b;
          this._cache.TryAdd(cacheKey, returnValue);
        }
        return (int)returnValue;
      }
      return default;
    }
    [CacheAndRetry(IncludeRetry = true)]
    public int Rmove(int a, int b)
    {
      for (var i = 0;; i++)
      {
        try
        {
          {
            var cacheKey = $"Rmove({string.Join(", ", new object[] { a, b })})";
            if (!this._cache.TryGetValue(cacheKey, out var returnValue))
            {
              returnValue = a - b;
              this._cache.TryAdd(cacheKey, returnValue);
            }
            return (int)returnValue;
          }
        }
        catch (Exception ex)when (i < 10)
        {
          Console.WriteLine(ex.ToString());
          continue;
        }
      }
      return default;
    }
    private ConcurrentDictionary<string, object?> _cache = new();
  }
}