
using DefaultNamespace;

/// <summary>
/// Стратегия для анимации. Вызывается каждый Update.
/// </summary>
public interface IAnimationStrategy
{
    void DoUpdate(MatchElementView matchElement);
}
