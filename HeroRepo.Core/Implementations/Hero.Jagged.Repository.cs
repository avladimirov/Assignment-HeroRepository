using HeroRepo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HeroRepo.Core
{
  public class HeroJaggedRepository : IHeroRepository
  {
    public static int INIT_MAX_HEROES = 10000;

    private IComparer<Hero> _comparer = new HeroComparerer();
    public Dictionary<uint, SortedSet<Hero>> Heroes { get; }

    public HeroJaggedRepository()
    {
      Heroes = new Dictionary<uint, SortedSet<Hero>>(INIT_MAX_HEROES);
      for (uint i = 100; i <= 1000; i++)
      {
        Heroes.Add(i, new SortedSet<Hero>(_comparer));
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Add(Hero hero)
    {
      foreach (var atkSet in Heroes.Values)
      {
        if (atkSet.Contains(hero)) return false;
      }

      var destinationSet = Heroes[hero.Attack];
      destinationSet.Add(hero);

      return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(string heroName)
    {
      var tmp = new Hero(heroName);
      foreach (var atkSet in Heroes.Values)
      {
        atkSet.TryGetValue(tmp, out Hero result);
        if(result != null)
        {
          atkSet.Remove(result);
          return true;
        }
      }

      return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Hero> Find(string type)
    {
      return Search(type : type, top: 10).ToList();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Hero> Power(int top)
    {
      return Search(top: top).ToList();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IEnumerable<Hero> Search(string type = null, int top = 10)
    {
      int count = 0;
      SortedSet<Hero> result = new SortedSet<Hero>(_comparer);
      for (uint i = 1000; i >= 100; i--)
      {
        var atkSet = Heroes[i];

        // filter by type
        if (!String.IsNullOrEmpty(type))
        {
          atkSet.Where(h => h.Type == type);
        }

        if (atkSet.Any())
        {
          result.UnionWith(atkSet);
          count += atkSet.Count;
        }

        if (count >= top) break;
      }

      return result;
    }
  }

  internal class HeroComparerer : StringComparer, IComparer<Hero>
  {
    public int Compare(Hero x, Hero y)
    {
      return InvariantCulture.Compare(x.Name, y.Name);
    }

    public override int Compare(string x, string y)
    {
      return InvariantCulture.Compare(x, y);
    }

    public override bool Equals(string x, string y)
    {
      return InvariantCulture.Equals(x, y);
    }

    public override int GetHashCode(string obj)
    {
      return InvariantCulture.GetHashCode(obj);
    }
  }
}
