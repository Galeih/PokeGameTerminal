# Pokémon Game Terminal [WORK IN PROGRESS]

Un jeu Pokémon en ligne de commande développé en C# avec un gameplay interactif, des combats Pokémon, une gestion d'expérience, et une intégration PokeAPI.

## 🕹️ Fonctionnalités

- **Combat Pokémon** : Affrontez des Pokémon sauvages et capturez-les dans des combats au tour par tour dynamiques. Le système de combat prend en compte les statistiques, les types de chaque Pokémon.

- **Système de type** : Implémentation complète du système de forces et faiblesses des types Pokémon, ajoutant une couche stratégique aux combats. Les dégâts sont calculés en fonction des relations entre les types.

- **Gestion de l'équipe** : Interface intuitive pour consulter et gérer votre équipe de Pokémon. Visualisez les statistiques détaillées, l'expérience, et l'état de santé de chaque membre de votre équipe.

- **Système d'expérience** : Système de progression élaboré où les Pokémon gagnent de l'expérience basée sur le niveau relatif des adversaires. Les statistiques évoluent de manière équilibrée à chaque niveau.

- **Shop interactif** : Boutique complète proposant divers objets essentiels comme des potions, des Pokéballs et des objets de soin. Gérez votre argent et vos ressources stratégiquement.

- **Données dynamiques via PokeAPI** : Stats réelles, attaques apprises par niveau, et lieux de rencontre pour les starters et les Pokémon sauvages via l'API officielle, avec cache local et fallback hors-ligne.

- **Interface stylisée** : Expérience utilisateur immersive grâce à Spectre.Console, offrant des menus colorés, des animations et des éléments visuels attrayants dans la console.

## 🛠️ Technologies utilisées

- **C#** : Langage principal du projet
- **.NET Core** : Framework moderne et cross-platform
- **Spectre.Console** : Bibliothèque pour des interfaces console élégantes
- **Git** : Gestion de version
- **GitHub** : Hébergement et collaboration

## 📦 Installation

1. **Clonez le repository** :
```bash
git clone https://github.com/Galeih/PokeGameTerminal.git
cd PokeGameTerminal
```

2. **Configuration requise** :
   - .NET SDK 8.0 ou supérieur
   - Un terminal compatible avec Unicode

3. **Exécution** :
```bash
dotnet restore
dotnet run
```

## 🎮 Comment jouer

1. **Démarrage**
   - Lancez le jeu
   - Créez votre profil de dresseur
   - Sélectionnez votre Pokémon starter

2. **Menu principal**
   - Exploration et combats
   - Gestion d'équipe
   - Soin de votre équipe
   - Boutique d'objets

3. **Combats**
   - Capturez de nouveaux Pokémon

## 📧 Contact

- **Créateur** : Galeih
- **GitHub** : [Profil GitHub](https://github.com/Galeih)
