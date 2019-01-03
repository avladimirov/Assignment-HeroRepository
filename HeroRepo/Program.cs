using HeroRepo.Console;
using HeroRepo.Core;
using HeroRepo.Core.Interfaces;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HeroRepo
{
  class Program
  {
    [STAThread()]
    static void Main(string[] args)
    {
      HeroRepository.INIT_MAX_HEROES = 100000;
      System.Console.WriteLine("Output: ");
      Stopwatch _stopWatch = null;

      using (var _repo = new HeroFeedbackRepository(new HeroRepository()))
      {
        var allLines = System.Console.In.ReadToEnd();
        _stopWatch = Stopwatch.StartNew();

        var lines = allLines.Split(Environment.NewLine);
        foreach (var line in lines)
        {
          Execute(_repo, line);
        }
      }

      _stopWatch.Stop();
      System.Console.WriteLine($"Time taken: {_stopWatch.Elapsed}");

      System.Console.In.Close();
      Debugger.Break();
    }

    static void Execute(IHeroRepository repo, string line)
    {
      var args = line.Split(" ");
      var cmd = args[0];

      switch (cmd)
      {
        case "add":
          repo.Add(new Hero
          {
            Name = args[1],
            Type = args[2],
            Attack = UInt32.Parse(args[3])
          });
          break;
        case "find":
        case "remove":
        case "power":
          repo.Remove(args[1]);
          break;
        case "end":
          break;
      }
    }

    //static void TestCase()
    //{
    //  _repo.Add(new Hero
    //  {
    //    Name = "Hulk",
    //    Type = "Boss",
    //    Attack = 100
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "Spiderman",
    //    Type = "Marvel",
    //    Attack = 250
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "Batman",
    //    Type = "Marvel",
    //    Attack = 200
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "Spiderman",
    //    Type = "MutatedHuman",
    //    Attack = 180
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "TheJoker",
    //    Type = "Boss",
    //    Attack = 500
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "MJ",
    //    Type = "MutatedHuman",
    //    Attack = 200
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "Venom",
    //    Type = "Marvel",
    //    Attack = 300
    //  });
    //  _repo.Add(new Hero
    //  {
    //    Name = "Spiderman",
    //    Type = "MutatedHuman",
    //    Attack = 180
    //  });

    //  _repo.Power(3);
    //  _repo.Find("Marvel");
    //  _repo.Find("Boss");
    //  _repo.Remove("BlackWidow");
    //  _repo.Remove("TheJoker");
    //  _repo.Power(3);
    //  _repo.Find("BlackWidow");
    //  _repo.Find("Boss");
    //}
  }
}
