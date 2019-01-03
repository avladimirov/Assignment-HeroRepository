using System;

namespace HeroRepo.Core
{
  public class Hero
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public uint Attack { get; set; }

    public Hero() { }
    public Hero(string name)
    {
      Name = name;
    }

    public override string ToString()
    {
      return $"{Name}[{Type}]({Attack})";
    }

    public string ToInputString()
    {
      return $"{Name} {Type} {Attack}";
    }
  }
}
