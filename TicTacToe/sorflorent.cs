using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace sorflorent
{
    public class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> game = new Dictionary<string, string>()
        {
            {"A1", ""}, {"A2", ""}, {"A3", ""},
            {"B1", ""}, {"B2", ""}, {"B3", ""},
            {"C1", ""}, {"C2", ""}, {"C3", ""}
        };

            List<string> locations = game.Keys.ToList();
            // Display the number of command line arguments.
            Console.WriteLine("Jeu du Tic Tac Toe");
            string choice = "";
            string choiceIA = "";

            Random random = new Random();

            do
            {
                while (locations.Any())
                {
                    Console.WriteLine("[Joueur] Choisir un emplacement (exemple A1, A3, C3 ... max -> A à C, 1 à 3) : ");
                    choice = Console.ReadLine();

                    if (locations.Contains(choice))
                    {
                        break;
                    }
                }
                game[choice] = "X";
                locations.Remove(choice);

                
                if (locations.Any())
                {
                    Console.WriteLine("[IA] Je réfléchis...");
                    Thread.Sleep(1000);
                    int index;
                    index = random.Next(locations.Count());
                    choiceIA = locations[index];
                    Console.WriteLine("[IA] Je joue en {0}", choiceIA);
                    game[choiceIA] = "O";
                    locations.Remove(locations[index]);
                }
                else if(EndGame(game, choice) == EndGame(game, choiceIA))
                {
                    Console.WriteLine("Draw.");
                    Display(game);
                    break;
                }
                Display(game);
            }
            while (!EndGame(game, choice) && !EndGame(game, choiceIA));
        }

        static void Display(Dictionary<string, string> game)
        {
            List<string> lines = new List<string>() { "A", "B", "C" };
            List<string> columns = new List<string>() { "1", "2", "3" };
            string sep = "  +---+---+---+";
            string coord;

            Console.WriteLine("    1   2   3    ");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(sep);
                Console.Write(lines[i]);
                Console.Write(" |");
                for (int j = 0; j < 3; j++)
                {
                    coord = String.Concat(lines[i], columns[j]);
                    if (game[coord] != "")
                    {
                        Console.Write(" {0} ", game[coord]);
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                    Console.Write("|");
                }
                Console.Write("\n");
            }
            Console.Write(sep + "\n");
        }

        static bool EndGame(Dictionary<string, string> game, string choice)
        {
            string symbol = game[choice];
            List<string> lines = new List<string>() { "A", "B", "C" };
            List<string> columns = new List<string>() { "1", "2", "3" };

            char l = choice[0];
            char c = choice[1];

            List<string> line = new List<string>() { l + "1", l + "2", l + "3" };
            List<string> col = new List<string>() { "A" + c, "B" + c, "C" + c };
            List<string> diag1 = new List<string>() { "A1", "B2", "C3" };
            List<string> diag2 = new List<string>() { "A3", "B2", "C1" };

            if (Count3Marks(game, symbol, line) ||
                Count3Marks(game, symbol, col) ||
                Count3Marks(game, symbol, diag1) ||
                Count3Marks(game, symbol, diag2))
            {
                if (symbol == "X") {
                    Console.WriteLine("You win.");
                }
                else
                {
                    Console.WriteLine("You lose.");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool Count3Marks(Dictionary<string, string> game, string symbol, List<string> l)
        {
            int marksCount = 0;
            
            foreach (string coord in l)
            {
                if (game[coord] == symbol)
                {
                    marksCount ++;
                }
                if (marksCount == 3)
                {
                    return true;
                }
            }
            return false;
        }
    }
}