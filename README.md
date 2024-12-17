# Pokémon Battle Game

## Description

Le projet **Pokémon Battle Game** est un jeu où le joueur peut sélectionner un Pokémon, utiliser ses sorts, et affronter un adversaire en utilisant une interface graphique. Le jeu est développé avec **C#** et **WPF**, et les données sont stockées dans une base de données SQL Server.

Le projet permet de :
- Créer un compte utilisateur et se connecter.
- Afficher les sorts disponibles.
- Sélectionner deux Pokémon.
- Êtes-vous prêt pour la phase de combat?

## Fonctionnalités

1. **Gestion des comptes utilisateurs :**
   - Création d'un compte avec un nom d'utilisateur et un mot de passe.
   - Vérification des identifiants lors de la connexion.
   
2. **Sélection des Pokémon et leurs sorts :**
   - Affichage des Pokémon disponibles dans un menu déroulant (`ComboBox`).
   - Affichage des sorts disponibles avec leurs caractéristiques.

3. **Système de combat :**
   - Affrontement entre deux Pokémon (le joueur et l'adversaire).
   - Choix d'un sort pour attaquer l'adversaire.
   - Mise à jour des barres de vie et affichage de l'état du combat.

## Prérequis

Avant de commencer, vous aurez besoin de :

- **.NET 5.0 ou version supérieure** pour compiler et exécuter le projet.
- **SQL Server** pour la gestion de la base de données.
- **Visual Studio** pour développer et exécuter l'application WPF.

## Installation

### Étape 1 : Cloner le dépôt

Clonez ce dépôt sur votre machine locale en utilisant Git :

```bash
git clone https://github.com/votre-utilisateur/PokemonBattleGame.git
