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
//projectie = old school way
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
var dlcGameTitles = games
    .Where(g => g.Type.Equals("DLC"))
    .Select(g => g.Title);
//simple select projection to anonymous class
var titleAndDate = games
                    .Where(g => g.Type.Equals("DLC"))
                    .Select(g => new {g.Title,g.Published_date });

var gameModels = games
    .Where(g => g.Type.Equals("DLC"))
    .Select(g => new GameModel 
    {
        Title = g.Title,
        Type = g.Type,
        Id = g.Id
    });
//selectmany
PrintLines("SelectMany = Flattening");
var platforms = games.SelectMany(g => g.PlatformsList);
foreach(var platform in platforms.Distinct())
{
    Console.Write($"{platform} ");
}
//Aggregation => Aggregate, Average, Count , Max, Min, Sum,
PrintLines("Aggregate: Average");
var averageUsers = games.Average(g => g.Users);
Console.WriteLine($"AverageUSers:{averageUsers.ToString("#.##")}");
PrintLines("Aggregate: Sum");
var sumOfUsers = games.Sum(g => g.Users);
Console.WriteLine($"SumOfUSers:{sumOfUsers}");
PrintLines("Aggregate: Max");
var maxOfUsers = games.Max(g => g.Users);
Console.WriteLine($"MaxOfUSers:{maxOfUsers}");
PrintLines("Aggregate: Min");
var minOfUsers = games.Min(g => g.Users);
Console.WriteLine($"MinOfUSers:{minOfUsers}");
PrintLines("Aggregate: Count");
var countOfUsers = games.Count();
Console.WriteLine($"NumOfUsers:{countOfUsers}");

//example of aggregate sum
//var result =games
//    .Select(g => g.Users)
//    .Aggregate((user1, user2) => { return user1 + user2; });

// boolean Quantifiers => All, Any, Contains
PrintLines("Quantifiers: All");
if(games.All(g => g.Published_date.Year > 1975))
{
    Console.WriteLine("No old games in database");
}
else
{
    Console.WriteLine("Old boomer games present");
}
PrintLines("Quantifiers: All");
if (games.All(g => g.Published_date.Year > 1975))
{
    Console.WriteLine("No old games in database");
}
else
{
    Console.WriteLine("Old boomer games present");
}
PrintLines("Quantifiers: Any");
if (games.Any(g => g.Published_date.Year > 2020))
{
    Console.WriteLine("No old games in database");
}
else
{
    Console.WriteLine("Old boomer games present");
}
PrintLines("Quantifiers: Contains");
//pointless code only as example
var firstGame = games.First();
if (games.Contains(firstGame))
{
    Console.WriteLine("Game in list");
}
else
{
    Console.WriteLine("Game not in list");
}

// Elements => ElementAt, ElementAtOrDefault,
//          Last, LastOrDefault
PrintLines("IndexSearch:elementAt");
var gameAtPosition9 = games.ElementAtOrDefault(9);
PrintGame(gameAtPosition9);
PrintLines("IndexSearch:Last game of 2020");
var lastGameInListFrom2020 = games.Last(g => g.Published_date.Year.Equals(2020));
PrintGame(lastGameInListFrom2020);

// Set => Distinct, Except
PrintLines("Working Set:Distinct");
var uniquePlatforms = games.Select(g => g.Platforms.Distinct());
Console.WriteLine($"{string.Join(",",uniquePlatforms)}");
// Partitioning => Skip, SkipWhile, Take, TakeWhile
PrintLines("Partitioning:Skip");
var skipFirstTenGames = games.Skip(10).Select(g => g.Title);
Console.WriteLine($"{string.Join(",",skipFirstTenGames)}");
PrintLines("Partitioning:Take");
var takeFirstTenGames = games.Take(10).Select(g => g.Title);
Console.WriteLine($"{string.Join(",", takeFirstTenGames)}");
PrintLines("Partitioning:Skip + take = slice");
var sliceTenGames = games.Skip(10).Take(10).Select(g => g.Title);
Console.WriteLine($"{string.Join(",", skipFirstTenGames)}");

//deferred execution
var evenNumbersEnumerable = numbers.Where(n => n%2 == 0);
//immediate execution
var evenNumbersList = numbers.Where(n => n%2 == 0).ToList();
//add an even number to numbers list
numbers.Add(240);
//foreach also immediate execution
foreach(var evenNumber in evenNumbersEnumerable)
{
    Console.Write($"{evenNumber} ");
}
//Deferred execution


// Conversion => AsEnumerable, AsQueryable, Cast, ToArray, ToList
PrintLines("Conversion: Cast");
var castedElements = games.Select(g => g.Users).Cast<double>();

// Concatenation => Concat
//concat the games list with a list containing the first and the last game
//pointless code for example purposes!
games.Concat(new List<Game> { games.First(), games.Last() });

//Anonymous classes

//Projection to data model class

//Helper methods
IEnumerable< Game > GetGames()
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
