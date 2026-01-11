using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;

        int screenwidth = Console.WindowWidth;
        int screenheight = Console.WindowHeight;

        Random randomnummer = new Random();

        int score = 0;

        Pixel hoofd = new Pixel();
        hoofd.xPos = screenwidth / 2;
        hoofd.yPos = screenheight / 2;
        hoofd.schermKleur = ConsoleColor.Red;

        List<int> teljePositie = new List<int>();

        teljePositie.Add(hoofd.xPos);
        teljePositie.Add(hoofd.yPos);

        string obstacle = "*";
        int obstacleXpos = randomnummer.Next(1, screenwidth - 2);
        int obstacleYpos = randomnummer.Next(1, screenheight - 2);

        string movement = "RIGHT";

        while (true)
        {
            Console.Clear();
            bool eaten = false;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(obstacleXpos, obstacleYpos);
            Console.Write(obstacle);


            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(hoofd.xPos, hoofd.yPos);
            Console.Write("■");

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
            }
            for (int i = 0; i < screenwidth; i++)
            {
                Console.SetCursorPosition(i, screenheight - 1);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < screenheight; i++)
            {
                Console.SetCursorPosition(screenwidth - 1, i);
                Console.Write("■");
            }

            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.SetCursorPosition(2, 0); 
            Console.WriteLine("Wynik: " + score);


            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < teljePositie.Count; i += 2)
            {
                Console.SetCursorPosition(teljePositie[i], teljePositie[i + 1]);
                Console.Write("■");
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo info = Console.ReadKey(true);
                switch (info.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (movement != "DOWN") movement = "UP";
                        break;
                    case ConsoleKey.DownArrow:
                        if (movement != "UP") movement = "DOWN";
                        break;
                    case ConsoleKey.LeftArrow:
                        if (movement != "RIGHT") movement = "LEFT";
                        break;
                    case ConsoleKey.RightArrow:
                        if (movement != "LEFT") movement = "RIGHT";
                        break;
                }
            }

            if (movement == "UP") hoofd.yPos--;
            if (movement == "DOWN") hoofd.yPos++;
            if (movement == "LEFT") hoofd.xPos--;
            if (movement == "RIGHT") hoofd.xPos++;


            if (hoofd.xPos == obstacleXpos && hoofd.yPos == obstacleYpos)
            {
                score++;
                obstacleXpos = randomnummer.Next(1, screenwidth - 2);
                obstacleYpos = randomnummer.Next(1, screenheight - 2);
                eaten = true;
            }

            teljePositie.Insert(0, hoofd.xPos);
            teljePositie.Insert(1, hoofd.yPos);

            if (!eaten)
            {
                teljePositie.RemoveAt(teljePositie.Count - 1);
                teljePositie.RemoveAt(teljePositie.Count - 1);
            }

            if (hoofd.xPos <= 0 || hoofd.xPos >= screenwidth - 1 || hoofd.yPos <= 0 || hoofd.yPos >= screenheight - 1)
            {
                GameOverScreen(screenwidth, screenheight, score);
            }

            for (int i = 2; i < teljePositie.Count; i += 2)
            {
                if (hoofd.xPos == teljePositie[i] && hoofd.yPos == teljePositie[i + 1])
                {
                    GameOverScreen(screenwidth, screenheight, score);
                }
            }

            Thread.Sleep(100);
        }
    }

    static void GameOverScreen(int w, int h, int score)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.SetCursorPosition(w / 5, h / 2);
        Console.WriteLine("KONIEC GRY"); // Tłumaczenie
        Console.SetCursorPosition(w / 5, h / 2 + 1);
        Console.WriteLine("Twój wynik: " + score); // Tłumaczenie
        Console.SetCursorPosition(w / 5, h / 2 + 3);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Naciśnij Enter, aby zamknąć..."); // Instrukcja

        // Czekaj na klawisz, zamiast zamykać od razu
        Console.ReadLine();
        Environment.Exit(0);
    }
}