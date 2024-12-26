using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokemonGame;

public class Quest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public int Reward { get; set; }

    public Quest(string name, string description, int reward)
    {
        Name = name;
        Description = description;
        Reward = reward;
        IsCompleted = false;
    }

    public void Complete()
    {
        IsCompleted = true;
        Console.WriteLine($"Quête terminée : {Name} ! Vous gagnez {Reward} pièces !");
    }
}