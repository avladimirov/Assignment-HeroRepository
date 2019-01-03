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
      HeroDictionaryRepository.INIT_MAX_HEROES = 100000;
      System.Console.WriteLine("Output: ");
      Stopwatch _stopWatch = null;

      using (var _repo = new HeroFeedbackRepository(new HeroJaggedRepository()))
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
      System.Console.WriteLine($"Time taken: {_stopWatch.ElapsedMilliseconds}ms | Target: 600ms");

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
        case "remove":
          repo.Remove(args[1]);
          break;
        case "find":
          repo.Find(args[1]);
          break;
        case "power":
          repo.Power(int.Parse(args[1]));
          break;
        case "end":
          break;
      }
    }

    static void Generator(int linesCount)
    {

    }
  }
}
