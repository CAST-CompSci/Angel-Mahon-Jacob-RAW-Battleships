
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_of_increace
{
    class Program
    {
        static void Winner()
        {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();
            for (var a = 0; a < 900; a++)
            {
                Console.Write(":-) ");
            }
            System.Threading.Thread.Sleep(100);
            string winner = "Congratulations, you have won this game of Battleships!";

            Console.SetWindowSize(120, 30);
            Console.SetCursorPosition(Console.WindowWidth / 2 - winner.Length / 2, Console.WindowHeight / 2);
            for (int i = 0; i <= winner.Length - 1; i++)
            {
                Console.Write(winner[i]);
                System.Threading.Thread.Sleep(25);
            }
            Console.ReadKey();
            System.Environment.Exit(1);
        }

        static void Looser()
        {
            Console.Clear();
            Console.WriteLine("You ran out of hits, :-(. Better luck next time.");
            Console.ReadKey();
            for (var a = 0; a < 1000; a++)
            {
                Console.Write(":-( ");
            }
            System.Threading.Thread.Sleep(10);
            System.Environment.Exit(1);
        }
        static int safeIntInput(String message)
        {
            int Number = 0;
            bool isRunning = true;
            while (isRunning == true)
            {
                Console.WriteLine("");
                Console.Write(message);
                string stringToTest = Console.ReadLine();
                bool res = int.TryParse(stringToTest, out Number);
                if (res == false)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Invalid Input, please try again: ");
                }
                else
                {
                    isRunning = false;
                }
            }
            return Number;
        }

        static void Destroy(int size, string[,] hitMatrix, string[,] selectMatrix, int crosses, int numberOfBoats, int hitsUsed)
        {
            int[] currentSelection = new int[2];
            currentSelection[0] = 0;
            currentSelection[1] = 0;
            string selectionType = "X";
            
            while (true)
            {
                selectMatrix[currentSelection[0], currentSelection[1]] = selectionType;

                BuildAll(size, hitMatrix, selectMatrix, crosses, numberOfBoats, hitsUsed);
                ConsoleKeyInfo keyinfo;
                keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (currentSelection[1] >= 1)
                    {
                        currentSelection[1] = currentSelection[1] - 1;
                        selectionType = "X";
                    }
                }
                else if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (currentSelection[1] < size - 1)
                    {
                        currentSelection[1] = currentSelection[1] + 1;
                        selectionType = "X";
                    }
                }
                else if (keyinfo.Key == ConsoleKey.LeftArrow)
                {
                    if (currentSelection[0] > 0)
                    {
                        currentSelection[0] = currentSelection[0] - 1;
                        selectionType = "X";
                    }
                }
                else if (keyinfo.Key == ConsoleKey.RightArrow)
                {
                    if (currentSelection[0] < size - 1)
                    {
                        currentSelection[0] = currentSelection[0] + 1;
                        selectionType = "X";
                    }
                }
                else if (keyinfo.Key == ConsoleKey.Enter)
                {
                    
                    int length = 5;
                    selectionType = "E";
                    if (hitMatrix[currentSelection[0], currentSelection[1]] == " ")
                    {
                        hitMatrix[currentSelection[0], currentSelection[1]] = "/";
                        hitsUsed++;
                    }
                    else if (hitMatrix[currentSelection[0], currentSelection[1]] == "X")
                    {
                        hitMatrix[currentSelection[0], currentSelection[1]] = "!";
                    }
                    selectionType = "X";
                }
                else if (keyinfo.Key == ConsoleKey.Backspace)
                {
                    selectionType = "D";
                }

                for (int i = 0; i <= size - 1; i++)
                {
                    for (int j = 0; j <= size - 1; j++)
                    {
                        selectMatrix[i, j] = "O";
                    }
                }

            }

        }

        static void BuildAll(int size, string[,] hitMatrix, string[,] selectMatrix, int crosses, int numberOfboats, int hitsUsed)
        {
            int hits = 0;
            ConsoleColor[,] textColorMatrix = new ConsoleColor[size, size];
            ConsoleColor[,] backColorMatrix = new ConsoleColor[size, size];
            string[,] displayMatrix = new string[size, size];
            for (int i = 0; i <= size - 1; i++)
            {
                for (int j = 0; j <= size - 1; j++)
                {
                    textColorMatrix[i, j] = ConsoleColor.White;
                    backColorMatrix[i, j] = ConsoleColor.Black;
                    displayMatrix[i, j] = " ";
                }
            }
            for (int i = 0; i <= size - 1; i++)
            {
                for (int j = 0; j <= size - 1; j++)
                {
                    if (hitMatrix[i, j] == "/")
                    {
                        textColorMatrix[i, j] = ConsoleColor.White;
                        backColorMatrix[i, j] = ConsoleColor.Black;
                        displayMatrix[i, j] = "●";
                    }
                    else if (hitMatrix[i, j] == "!")
                    {
                        textColorMatrix[i, j] = ConsoleColor.Red;
                        backColorMatrix[i, j] = ConsoleColor.Black;
                        displayMatrix[i, j] = "●";
                        hits++;

                    }
                    if (selectMatrix[i, j] == "X")
                    {
                        textColorMatrix[i, j] = ConsoleColor.Blue;
                        backColorMatrix[i, j] = ConsoleColor.White;
                    }
                    else if (selectMatrix[i, j] == "E")
                    {
                        textColorMatrix[i, j] = ConsoleColor.White;
                        backColorMatrix[i, j] = ConsoleColor.DarkBlue;
                        hitMatrix[i, j] = "#";
                    }
                    else if (selectMatrix[i, j] == "D")
                    {
                        textColorMatrix[i, j] = ConsoleColor.White;
                        backColorMatrix[i, j] = ConsoleColor.DarkRed;
                        hitMatrix[i, j] = " ";
                    }
                }
            }

            if (size == 0)
            {
                Console.WriteLine("Excuse me?");
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Could you please leave?");
                System.Threading.Thread.Sleep(5000);
                size = 88888;
            }
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorLeft = 0;
            Console.CursorTop = 0;


            char letter = 'A';
            Console.Write("    ");
            for (var y = 0; y < size; y++)
            {
                Console.Write(letter);
                Console.Write("   ");
                letter++;
            }
            Console.WriteLine("");
            Console.Write("  ┏");
            for (int a = 0; a < (size - 1); a++)
            {
                Console.Write("━━━");
                Console.Write("┳");
            }
            Console.Write("━━━┓");

            for (int b = 0; b < (size); b++)
            {
                Console.WriteLine("");

                if (b < 9)
                {
                    Console.Write(b + 1);
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(b + 1);
                }

                for (int c = 0; c < size; c++)
                {
                    Console.Write("┃");
                    Console.Write(" ");
                    Console.BackgroundColor = backColorMatrix[c, b];
                    Console.ForegroundColor = textColorMatrix[c, b];
                    Console.Write(hitMatrix[c, b]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" ");
                }
                if (b < (size - 1))
                {
                    Console.Write("┃");
                    Console.WriteLine("");
                    Console.Write("  ┣");
                    for (int a = 0; a < (size - 1); a++)
                    {
                        Console.Write("━━━");
                        Console.Write("╋");
                    }
                    Console.Write("━━━┫");
                }
                else
                {
                    Console.Write("┃");
                }
            }
            Console.WriteLine("");


            Console.Write("  ┗");
            for (int a = 0; a < (size - 1); a++)
            {
                Console.Write("━━━");
                Console.Write("┻");
            }
            Console.Write("━━━┛");

            int total = 0;
            for (var a = 2; a <= numberOfboats + 1; a++)
            {
                total = total + a;
           
            }
            int remainingHits = 0;
            remainingHits = remainingHits + (((size*size) - total)/2) - hitsUsed;
            Console.WriteLine("");
            Console.WriteLine("Game stats. Hits: " + hits + "/" + total);
            Console.WriteLine("Remaining misses: " + remainingHits);
            if (hits == total)
            {
                Winner();
            }
            if (remainingHits == 0)
            {
                Looser();
            }
        }

        static bool addShip(int size, string[,] hitMatrix, string[,] selectMatrix, int length, int[] currentSelection, int direction, int crosses)
        {
            string selectionType = "X";

            bool isPlacable = true;
            if (direction == 1)
            {
                isPlacable = true;
                for (var a = 0; a < length; a++)
                {
                    if ((currentSelection[1] - a) < 0)
                    {
                        a = length;
                        isPlacable = false;
                    }
                    else if (hitMatrix[currentSelection[0], currentSelection[1] - a] == "X")
                    {
                        a = length;
                        isPlacable = false;
                    }

                }
                if (isPlacable)
                {
                    for (var a = 0; a < length; a++)
                    {

                        hitMatrix[currentSelection[0], currentSelection[1] - a] = "X";
                        

                    }
                    hitMatrix[currentSelection[0], currentSelection[1]] = "X";
                }
            }
            else if (direction == 2)
            {
                isPlacable = true;
                for (var a = 0; a < length; a++)
                {
                    if ((currentSelection[1] + a) > size - 1)
                    {
                        a = length;
                        isPlacable = false;
                    }
                    else if (hitMatrix[currentSelection[0], currentSelection[1] + a] == "X")
                    {
                        a = length;
                        isPlacable = false;
                    }

                }
                if (isPlacable)
                {
                    for (var a = 0; a < length; a++)
                    {
                        {
                            hitMatrix[currentSelection[0], currentSelection[1] + a] = "X";
  
                        }
                    }
                    hitMatrix[currentSelection[0], currentSelection[1]] = "X";
                }
            }
            else if (direction == 3)
            {

                isPlacable = true;
                for (var a = 0; a < length; a++)
                {
                    if ((currentSelection[0] - a) < 0)
                    {
                        a = length;
                        isPlacable = false;
                    }
                    else if (hitMatrix[currentSelection[0] - a, currentSelection[1]] == "X")
                    {
                        a = length;
                        isPlacable = false;
                    }

                }
                if (isPlacable)
                {
                    for (var a = 0; a < length; a++)
                    {
                        {
                            hitMatrix[currentSelection[0] - a, currentSelection[1]] = "X";
                     
                        }
                    }
                    
                    hitMatrix[currentSelection[0], currentSelection[1]] = "X";
                }
        




            }
            else if (direction == 4)
            {
                isPlacable = true;
                for (var a = 0; a < length; a++)
                {
                    if ((currentSelection[0] + a) > size)
                    {
                        a = length;
                        isPlacable = false;
                    }
                    else if (hitMatrix[currentSelection[0] + a, currentSelection[1]] == "X")
                    {
                        a = length;
                        isPlacable = false;
                    }

                }
                if (isPlacable)
                {
                    for (var a = 0; a < length; a++)
                    {
                        {
                            hitMatrix[currentSelection[0] + a, currentSelection[1]] = "X";
 

                        }
                    }
 
                    hitMatrix[currentSelection[0], currentSelection[1]] = "X";
                }
            }
            for (int i = 0; i <= size - 1; i++)
            {
                for (int j = 0; j <= size - 1; j++)
                {
                    selectMatrix[i, j] = "O";
                }
            }
 
            return isPlacable;
        }
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome to battleships!");

            string h = "hello";

            int size = 1;
            int crosses = 0;
            int hitsUsed = 0;
            bool isRunning = true;
            while (isRunning)
            {
                size = safeIntInput("Please enter grid size (2 to 26): ");
                if (size >= 2)
                {
                    if (size <= 26)
                    {
                        isRunning = false;
                    }
                    else
                    {
                        Console.WriteLine("Grid must be less than 27");
                    }
                }
                else
                {
                    Console.WriteLine("Grid must be bigger than 1");
                }
            }

            int numberofboats = safeIntInput("Please enter desired number of boats: ");
            string[,] hitMatrix = new string[size, size];
            string[,] selectMatrix = new string[size, size];
  
            for (int i = 0; i <= size - 1; i++)
            {
                for (int j = 0; j <= size - 1; j++)
                {
                    selectMatrix[i, j] = "O";
                    hitMatrix[i, j] = " ";
                }
            }

            int length = 2;

            for (int a = 0; a < numberofboats; a++)
            {
                Random generator = new Random();
                int rand = generator.Next(1, size);

                Random generator2 = new Random();
                int rand2 = generator.Next(1, size);

                int[] currentSelection = new int[2];
                currentSelection[0] = rand;
                currentSelection[1] = rand2;

                Random generator3 = new Random();
                int rand3 = generator.Next(1, 4);
  

                if (addShip(size, hitMatrix, selectMatrix, length, currentSelection, rand3, crosses))
                {
                    Console.CursorVisible = false;
                    length++;
                    Console.CursorLeft = 0;
                    Console.Write("[");
                    Console.CursorLeft = numberofboats + 1;
                    Console.Write("]");
                    Console.CursorLeft = 1;

                    for (var b = 0; b < numberofboats; b++)
                    {
                        Console.CursorLeft = b + 1;
                        if (b <= a)
                        {
                            Console.Write("=");
                        }
                    }

                    Console.CursorLeft = 50;

                }
                else
                {
                    a--;
                }



            }
            Console.Clear();
            Destroy(size, hitMatrix, selectMatrix, crosses, numberofboats, hitsUsed);
            Console.Read();
        }


    }
}
