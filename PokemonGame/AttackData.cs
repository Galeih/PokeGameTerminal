using PokemonGame.Attack.Physical;
using PokemonGame.Attack.Special;
using PokemonGame.Attack.Support;

namespace PokemonGame;

public static class AttackData
{
    public static List<AttackLogic> GetAllAttacks()
    {
        var allAttacks = new List<AttackLogic>();

        // Ajout des attaques physiques
        allAttacks.AddRange(Attack.Physical.Normal.GetAttacks());
        // Ajouter les autres types comme Feu, Eau...
        // allAttacks.AddRange(Feu.GetAttacks());

        // Ajout des attaques spéciales
        // allAttacks.AddRange(SpecialNormal.GetAttacks());
        // Ajouter les autres types...

        // Ajout des attaques de soutien
        // allAttacks.AddRange(SupportNormal.GetAttacks());
        // Ajouter les autres types...

        return allAttacks;
    }
}
