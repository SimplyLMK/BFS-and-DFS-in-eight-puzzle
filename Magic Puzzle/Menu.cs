using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Puzzle
{
    public class Menu
    {
        // menu class handle choice selection
        Bitmap bg;

        private static Menu instance;
        

        private Menu()
        {
            // Private constructor to prevent external instantiation
        }

        public static Menu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Menu();
                }
                return instance;
            }
        }
        public enum Options
        {
            log_in,
            create_account,
            m_puzzle,
            warship
        }
        
        //method check if the box is clicked 
        public bool Clicked(Rectangle box)
        {
            return SplashKit.MouseClicked(MouseButton.LeftButton) && SplashKit.PointInRectangle(SplashKit.MousePosition(), box);
        }

       // method open the window, create the UI as well as console account validation
        public void Run(M_Puzzle game1)
        {
            Window window = SplashKit.OpenWindow("Magic Puzzle", 300, 200);
            bool Is_Running = true;
            bool is_Valid = false;

            while (Is_Running)
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen(Color.White);

                bg = SplashKit.LoadBitmap("bg", "images\\ambient_bg.jpg");
                SplashKit.DrawBitmap(bg, 0, 0);

                SplashKit.DrawText("All of puzzles", Color.Aquamarine, 85, 50);

                Rectangle logInBox = new Rectangle { X = 70, Y = 100, Width = 150, Height = 20 };
                Rectangle createAccountBox = new Rectangle { X = 70, Y = 140, Width = 150, Height = 20 };

                SplashKit.FillRectangle(Color.Aqua, logInBox);
                SplashKit.DrawText("Log In", Color.Black, 120, 105);

                SplashKit.FillRectangle(Color.Aqua, createAccountBox);
                SplashKit.DrawText("Create Account!", Color.Black, 90, 145);

               
                if (Clicked(logInBox))
                {                

                    do
                    {
                        //Console.WriteLine("Log in clicked!");
                        Console.WriteLine("Enter Username:");
                        string username = Console.ReadLine();
                        Console.WriteLine("Enter Password:");
                        string password = Console.ReadLine();

                        if (ValidateAccount(username, password)) // call the validation method with pass&username param
                        {
                            Console.WriteLine("Login successful!");
                            window.Close();

                            M_Puzzle menu = new M_Puzzle();
                            game1.Run(game1);
                            

                        }
                        else
                        {

                            Console.WriteLine("Invalid username or password!");
                        }

                    }
                    while (!is_Valid);

                    
                }
                else if (Clicked(createAccountBox))
                {
                    string username;
                    string password;
                    string path = "account\\accounts.txt";
                    Console.WriteLine("Create account clicked!");
                    Console.WriteLine("Enter username:");
                    username = Console.ReadLine();
                    Console.WriteLine("Enter password");
                    password = Console.ReadLine();

                    if (!File.Exists(path))
                    {
                        File.Create(path);
                        using (var tw = new StreamWriter(path))
                        {
                            tw.WriteLine(username);
                            tw.WriteLine(password);
                        }
                    }
                    else
                    {
                        using (var sw = new StreamWriter(path, true))
                        {
                            sw.WriteLine(username);
                            sw.WriteLine(password);
                        }
                    }
                    Console.WriteLine("Account created successfully!");
                }

                SplashKit.RefreshScreen();

                if (SplashKit.WindowCloseRequested(window) || SplashKit.QuitRequested())
                    Is_Running = false;
            }

            SplashKit.CloseWindow(window);
        }

        private bool ValidateAccount(string username, string password)
        {
            string path = "account\\accounts.txt";
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                for (int i = 0; i < lines.Length; i += 2) // iterate through all the lines by +=2 (pair) every increment
                {
                    string storedUsername = lines[i]; // if is is 1st lime then is username
                    string storedPassword = lines[i + 1]; // else password
                    if (username == storedUsername && password == storedPassword)
                        return true;
                }
            }
            return false;
        }
    }
}
