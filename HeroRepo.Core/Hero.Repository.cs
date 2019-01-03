using HeroRepo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroRepo.Core
{
  public class HeroRepository : IHeroRepository
  {
    public static int INIT_MAX_HEROES = 1000;

    public Dictionary<string, Hero> Heroes { get; }

    public HeroRepository()
    {
      Heroes = new Dictionary<string, Hero>(INIT_MAX_HEROES);
    }

    public bool Add(Hero hero)
    {
      if (Heroes.ContainsKey(hero.Name)) return false;

      Heroes.Add(hero.Name, hero);
      return true;
    }

    public bool Remove(string heroName)
    {
      return Heroes.Remove(heroName);
    }

    public IEnumerable<Hero> Find(string type)
    {
      return Search(type).ToList();
    }

    public IEnumerable<Hero> Power(int top)
    {
      return Search().Take(top).ToList();
    }

    private IEnumerable<Hero> Search(string type = null)
    {
      var results = Heroes.Values.AsQueryable();
      if (!String.IsNullOrEmpty(type))
      {
        results = results.Where(h => h.Type == type);
      }

      return results.OrderByDescending(h => h.Attack).ThenBy(h => h.Name);
    }
  }
}
