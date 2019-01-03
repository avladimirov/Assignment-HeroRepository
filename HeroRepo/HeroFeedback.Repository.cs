using HeroRepo.Core;
using HeroRepo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeroRepo.Console
{
  class HeroFeedbackRepository : IHeroRepository, IDisposable
  {
    private IHeroRepository _baseRepo;
    private StringBuilder _strBuilder = new StringBuilder(Int16.MaxValue);
    public HeroFeedbackRepository(IHeroRepository repo)
    {
      _baseRepo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    bool IHeroRepository.Add(Hero hero)
    {
      var result = _baseRepo.Add(hero);
      var msg = result ? $"SUCCESS: {hero.Name} added!" : $"FAIL: {hero.Name} already exists!";

      _strBuilder.AppendLine(msg);
      return result;
    }

    public bool Remove(string heroName)
    {
      var result = _baseRepo.Remove(heroName);
      var msg = result ? $"SUCCESS: {heroName} removed!" : $"FAIL: {heroName} could not be found!";

      _strBuilder.AppendLine(msg);
      return result;
    }

    IEnumerable<Hero> IHeroRepository.Find(string type)
    {
      var result = _baseRepo.Find(type);
      var msg = $"RESULT: {String.Join(", ", result.Select(h => h))}";

      _strBuilder.AppendLine(msg);
      return result;
    }

    IEnumerable<Hero> IHeroRepository.Power(int top)
    {
      var result = _baseRepo.Power(top);
      var msg = $"RESULT: {String.Join(", ", result.Select(h => h))}";

      _strBuilder.AppendLine(msg);
      return result;
    }

    public void Dispose()
    {
      System.Console.Write(_strBuilder.ToString());
    }
  }
}
