// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using Pre.LinqBasics.Core;
using Pre.LinqBasics.Core.Entities;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

Console.WriteLine("Basic linq usage");

List<string> names = new List<string> {"Mike","Jack","Walter","Farilde","Krista","William","Teddy","Charles","Bronco" };
List<int> numbers = new List<int> { 2, 4, 5, 9, 89, 12, 2, 5, 78, 3, 4, 5, 5, 56, 9, 87, 21 };
var games = GetGames();
foreach(var game in games)
{
    Console.WriteLine(game.Title);
}
//LINQ SYNTAX
//Standard query syntax <=> query methods
var results = from game in games
              where game.Title.Contains("The")
              select game;

//First => FirstOrDefault

//Single => singleOrDefault

// Filtering => Where, OfType

// Sorting => orderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse

// Grouping => GroupBy, ToLookUp

// Projection => Select, SelectMany

//Aggregation => Aggregate, Average, Count , Max, Min, Sum,

// Quantifiers => All, Any, Contains


// Elements => ElementAt, ElementAtOrDefault,
//          Last, LastOrDefault

// Set => Distinct, Except

// Partitioning => Skip, SkipWhile, Take, TakeWhile

// Conversion => AsEnumerable, AsQueryable, Cast, ToArray, ToList

// Concatenation => Concat

//Deferred execution

//immediate execution

//Anonymous classes

//Projection to data model class

//Helper methods
IEnumerable < Game > GetGames()
{
    var gameRepository = new GameRepository();
    var games = gameRepository.GetGames();
    Console.Write("Loading:");
    while (!games.IsCompleted)
    {
        Console.Write("*");
        Thread.Sleep(100);
    }
    Console.WriteLine(" Games loaded");
    return games.Result;
}
void PrintLines(string title)
{
    Console.WriteLine("-----------------");
    Console.WriteLine(title);
    Console.WriteLine("-----------------");
}
void PrintGames(IEnumerable<Game> games)
{
    foreach(var game in games)
    {
        PrintGame(game);
    }
}
void PrintGame(Game game)
{
    Console.WriteLine(game.Title);
    Console.WriteLine(game.Description);
    Console.WriteLine(game.Type);
}
