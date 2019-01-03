using HeroRepo.Console;
using HeroRepo.Core;
using HeroRepo.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HeroRepo
{
  class Program
  {
    [STAThread()]
    static void Main(string[] args)
    {
      //Generator();

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

    static void Generator()
    {
      var types_count = 50;
      var heroes_count = 1000;
      var lines_count = 10000;

      var types = new List<string>(types_count);
      var heroes = new List<Hero>(heroes_count);
      var lines = new List<string>(lines_count);

      for (int i = 0; i < types_count; i++)
      {
        types.Add(RandomString(5, 15));
      }

      for (int i = 0; i < heroes_count; i++)
      {
        heroes.Add(new Hero
        {
          Name = RandomString(5, 15),
          Attack = (uint)random.Next(100, 1000),
          Type = types[random.Next(0, types_count - 1)],
        });
        Guid.NewGuid().ToString("n").Substring(0, 8);
      }

      // lines genertion
      for (int j = 0; j < heroes_count; j++)
      {
        lines.Add($"add {heroes[j].ToInputString()}");
      }

      for (int i = 0; i < lines_count - heroes_count; i++)
      {
        var r = random.Next(1, 4);
        switch (r)
        {
          case 1:
            lines.Add($"find {types[random.Next(0, types_count - 1)]}");
            break;
          case 2:
            lines.Add($"power {random.Next(1, heroes_count / 2)}");
            break;
          case 3:
            lines.Add($"remove {heroes[random.Next(1, heroes_count)].Name}");
            break;
        }
      }

      lines.Add("end");
      File.WriteAllLines("generated.input.txt", lines);
    }

    private static Random random = new Random();
    public static string RandomString(int min, int max)
    {
      var lenght = random.Next(min, max);
      const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
      return new string(Enumerable.Repeat(chars, lenght)
        .Select(s => s[random.Next(s.Length)]).ToArray());
    }
  }
}
