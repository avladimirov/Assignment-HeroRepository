using HeroRepo.Console;
using HeroRepo.Core;
using HeroRepo.Core.Interfaces;
using System;

namespace HeroRepo
{
  class Program
  {
    static IHeroRepository _repo;

    static void Main(string[] args)
    {
      HeroRepository.INIT_MAX_HEROES = 100000;
      _repo = new HeroFeedbackRepository(new HeroRepository());

      TestCase();
      System.Console.ReadLine();
    }

    static void TestCase()
    {
      _repo.Add(new Hero
      {
        Name = "Hulk", Type = "Boss", Attack = 100
      });
      _repo.Add(new Hero
      {
        Name = "Spiderman",
        Type = "Marvel",
        Attack = 250
      });
      _repo.Add(new Hero
      {
        Name = "Batman",
        Type = "Marvel",
        Attack = 200
      });
      _repo.Add(new Hero
      {
        Name = "Spiderman",
        Type = "MutatedHuman",
        Attack = 180
      });
      _repo.Add(new Hero
      {
        Name = "TheJoker",
        Type = "Boss",
        Attack = 500
      });
      _repo.Add(new Hero
      {
        Name = "MJ",
        Type = "MutatedHuman",
        Attack = 200
      });
      _repo.Add(new Hero
      {
        Name = "Venom",
        Type = "Marvel",
        Attack = 300
      });
      _repo.Add(new Hero
      {
        Name = "Spiderman",
        Type = "MutatedHuman",
        Attack = 180
      });

      _repo.Power(3);
      _repo.Find("Marvel");
      _repo.Find("Boss");
      _repo.Remove("BlackWidow");
      _repo.Remove("TheJoker");
      _repo.Power(3);
      _repo.Find("BlackWidow");
      _repo.Find("Boss");
    }
  }
}
