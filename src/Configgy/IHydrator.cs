namespace Configgy;

/// <summary>
/// Configuration hydration
/// </summary>
public interface IHydrator
{
    IConfig Hydrate(Type config);
}
