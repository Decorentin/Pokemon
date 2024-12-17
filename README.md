Pokémon Battle Game
Description
Le projet Pokémon Battle Game est un jeu où le joueur peut sélectionner un Pokémon, utiliser ses sorts, et affronter un adversaire en utilisant une interface graphique. Le jeu est développé avec C# et WPF, et les données sont stockées dans une base de données SQL Server.

Le projet permet de :

Créer un compte utilisateur et se connecter.
Sélectionner un Pokémon et utiliser ses sorts dans un combat.
Afficher les Pokémon disponibles et les sorts associés dans l'interface utilisateur.
Fonctionnalités
Gestion des comptes utilisateurs :

Création d'un compte avec un nom d'utilisateur et un mot de passe.
Vérification des identifiants lors de la connexion.
Sélection des Pokémon et leurs sorts :

Affichage des Pokémon disponibles dans un menu déroulant (ComboBox).
Affichage des sorts disponibles pour chaque Pokémon sélectionné.
Système de combat :

Affrontement entre deux Pokémon (le joueur et l'adversaire).
Choix d'un sort pour attaquer l'adversaire.
Mise à jour des barres de vie et affichage de l'état du combat.
Prérequis
Avant de commencer, vous aurez besoin de :

.NET 5.0 ou version supérieure pour compiler et exécuter le projet.
SQL Server pour la gestion de la base de données.
Visual Studio pour développer et exécuter l'application WPF.
Installation
Étape 1 : Cloner le dépôt
Clonez ce dépôt sur votre machine locale en utilisant Git :

bash
Copier le code
git clone https://github.com/votre-utilisateur/PokemonBattleGame.git
Étape 2 : Ouvrir le projet dans Visual Studio
Ouvrez Visual Studio.
Cliquez sur File > Open > Project/Solution et sélectionnez le fichier .sln du projet.
Étape 3 : Configurer la base de données
Créez une base de données SQL Server nommée ExerciceMonster.
Utilisez le script SQL suivant pour créer les tables nécessaires :
sql
Copier le code
CREATE TABLE Pokemons (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50)
);

CREATE TABLE Spells (
    ID INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(50),
    Damage INT,
    Description NVARCHAR(250),
    PokemonID INT FOREIGN KEY REFERENCES Pokemons(ID)
);

CREATE TABLE Login (
    ID INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) UNIQUE,
    PasswordHash NVARCHAR(255)
);
Assurez-vous de mettre à jour la chaîne de connexion dans le fichier LoginWindow.xaml.cs avec votre propre instance de SQL Server :
csharp
Copier le code
private string connectionString = @"Data Source=ACEREVO\SQLEXPRESS01;Initial Catalog=ExerciceMonster;Integrated Security=True";
Étape 4 : Lancer l'application
Appuyez sur F5 pour démarrer l'application dans Visual Studio.
L'application affichera une fenêtre de connexion. Si vous n'avez pas de compte, cliquez sur le bouton d'inscription pour en créer un.
Structure du projet
Model :
Pokemon.cs : Représente un Pokémon.
Spell.cs : Représente un sort.
View :
LoginWindow.xaml et LoginWindow.xaml.cs : Fenêtre de connexion.
RegisterWindow.xaml et RegisterWindow.xaml.cs : Fenêtre d'inscription.
MonsterSelectionWindow.xaml et MonsterSelectionWindow.xaml.cs : Fenêtre de sélection des Pokémon.
SpellsWindow.xaml et SpellsWindow.xaml.cs : Fenêtre affichant les sorts des Pokémon sélectionnés.
Database :
ExerciceMonster : Base de données SQL Server avec les tables pour les utilisateurs, Pokémon et sorts.
