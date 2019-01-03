using HeroRepo.Core;
using HeroRepo.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace HeroRepo.Console
{
  class HeroFeedbackRepository : IHeroRepository
  {
    private IHeroRepository _baseRepo;

    public HeroFeedbackRepository(IHeroRepository repo)
    {
      _baseRepo = repo ?? throw new ArgumentNullException(nameof(repo));
    }

    bool IHeroRepository.Add(Hero hero)
    {
      return _baseRepo.Add(hero);
    }

    public bool Remove(string heroName)
    {
      return _baseRepo.Remove(heroName);
    }

    IEnumerable<Hero> IHeroRepository.Find(string type)
    {
      return _baseRepo.Find(type);
    }

    IEnumerable<Hero> IHeroRepository.Power(uint top)
    {
      return _baseRepo.Power(top);
    }




  }
}
