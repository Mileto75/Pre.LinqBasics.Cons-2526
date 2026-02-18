// See https://aka.ms/new-console-template for more information
using Microsoft.VisualBasic;
using Pre.LinqBasics.Cons.Models;
using Pre.LinqBasics.Core;
using Pre.LinqBasics.Core.Entities;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

Console.WriteLine("Basic linq usage");

List<string> names = new List<string> {"Mike","Jack","Walter","Farilde","Krista","William","Teddy","Charles","Bronco" };
List<int> numbers = new List<int> { 2, 4, 5, 9, 89, 12, 2, 5, 78, 3, 4, 5, 5, 56, 9, 87, 21 };
var games = GetGames();
//LINQ SYNTAX
//Standard query syntax <=> query methods
var results = from game in games
              where game.Title.Contains("The")
              select game;
PrintLines("query syntax");
PrintGames(results);
PrintLines("Query method");
results = games.Where(g => g.Title.Contains("the"));
PrintGames(results);
PrintLines("Single, First, Default");
//First => FirstOrDefault
var number = numbers.FirstOrDefault(n => n == 666);//
Console.WriteLine(number);
//Single => singleOrDefault
number = numbers.SingleOrDefault(n => n == 666);
//number = numbers.Find(n => n==5); //not Linq
// Filtering => Where, OfType
PrintLines("Filtering: Where, OfType");
var namesResult = names.Where(n => n.ToLower().StartsWith("f"));
PrintNames(namesResult);
var mixedArray = new ArrayList { "test", 56, 23.00, new Game { Title = "Test" } };
var gamesFromMixedarray = mixedArray.OfType<Game>();
PrintGames(gamesFromMixedarray);
// Sorting => orderBy, OrderByDescending, ThenBy, ThenByDescending, Reverse
PrintLines("Sorting:");
var orderedNames = names.OrderBy(n => n).Reverse();//alfabetic sort
var orderedGames = games.OrderBy(g => g.Title).ThenBy(g => g.Published_date).ToList();
PrintNames(orderedNames);
// Grouping => GroupBy, ToLookUp
PrintLines("Grouping:GroupBy,ToLookUp");
var gamesByPublisher = games.GroupBy(g =>g.Type);
foreach(var item in gamesByPublisher)
{
    PrintLines($"{item.Key}");
    foreach(var game in item)
    {
        Console.WriteLine($"{game.Title}");
    }
}
// Projection => Select, SelectMany
PrintLines("Projection:Select, SelectMany");
//projectie
//var gameModelsClassic = new List<GameModel>();
//foreach(var game in games)
//{
//    gameModelsClassic.Add(new GameModel
//    {
//        Title = game.Title,
//        Type = game.Type,
//        Id = game.Id
//    });
//}
//simple selects projection
var dlcGameTitles = games.Where(g => g.Type.Equals("DLC")).Select(g => g.Title);
//simple select projection to anonymous class
var titleAndDate = games.Where(g => g.Type.Equals("DLC")).Select(g => new {g.Title,g.Published_date });

var gameModels = games
    .Where(g => g.Type.Equals("DLC"))
    .Select(g => new GameModel 
    {
        Title = g.Title,
        Type = g.Type,
        Id = g.Id
    });
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
//Helper methods
void PrintNames(IEnumerable<string> names)
{
    foreach(var name in names)
    {
        Console.Write($"{name} " );
    }
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
