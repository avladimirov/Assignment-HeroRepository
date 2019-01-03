using System;

namespace HeroRepo.Core
{
  public class Hero
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public uint Attack { get; set; }

    public override string ToString()
    {
      return $"{Name}[{Type}]({Attack})";
    }
  }
}
