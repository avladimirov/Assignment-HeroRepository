using System.Collections.Generic;

namespace HeroRepo.Core.Interfaces
{
  public interface IHeroRepository
  {
    bool Add(Hero hero);

    bool Remove(string heroName);

    IEnumerable<Hero> Find(string type);

    IEnumerable<Hero> Power(uint top);
  }
}
