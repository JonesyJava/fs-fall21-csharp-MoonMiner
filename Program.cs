using System;
using System.Collections.Generic;

namespace fs_fall21_csharp_MoonMiner
{
    class Program
    {
        static void Main(string[] args)
        {
            new MoonMiner();
        }

        class Upgrade
        {
            public String Name { get; private set; }
            public String Type { get; private set; }
            public int Price { get; set; }
            public decimal Quantity { get; set; }
            public decimal Multiplier { get; set; }
            public Upgrade(string name, string type, int price, decimal quantity, decimal multiplier)
            {
                Name = name;
                Type = type;
                Price = price;
                Quantity = quantity;
                Multiplier = multiplier;
            }

        }

        class MoonMiner
        {

            public bool Running { get; set; }
            public decimal Cheese { get; set; }
            public bool inShopWindow { get; set; }
            public List<Upgrade> Shop { get; set; }

            public Dictionary<string, decimal> Stats { get; set; }

            public List<Upgrade> ClickUpgrades { get; set; }
            public MoonMiner()
            {
                Running = true;
                Shop = new List<Upgrade>() { };
                ClickUpgrades = new List<Upgrade>() { };
                Stats = new Dictionary<string, decimal>() { };

                Shop.Add(new Upgrade("Justin's Twilight Cheese Knife", "click", 1, 0, 1));
                Shop.Add(new Upgrade("Harrison's Cheese Golf Club", "click", 5, 0, 2));
                Shop.Add(new Upgrade("Jonesy's Cheese Wiz Wizardry", "click", 15, 0, 5));
                Shop.Add(new Upgrade("Kurtis' Cheesecalibur", "click", 100, 0, 25));
                Shop.Add(new Upgrade("Mick's Cat", "click", 2000, 0, 50));

                drawGameScreen();
                PlayGame();
            }

            public void PlayGame()
            {
                while (Running)
                {
                    string input = getPlayerInput().Key.ToString().ToLower();
                    switch (input)
                    {
                        case "spacebar":
                            Mine();
                            break;
                        case "s":
                            goToShop();
                            break;
                        case "escape":
                            Running = false;
                            Console.Clear();
                            break;
                    }
                }
            }

            public void goToShop()
            {
                inShopWindow = true;
                Console.Clear();
                System.Console.WriteLine("Whaddya buying, stranger?");
                string message = "";
                for (int i = 0; i < Shop.Count; i++)
                {
                    Upgrade item = Shop[i];
                    message += $"{i + 1}. {item.Name}:\n Price: {item.Price},\n Multiplier: {item.Multiplier}\n";
                }
                Console.WriteLine(message);
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int selection) && selection > 0 && selection <= Shop.Count)
                {
                    BuyUpgrade(selection - 1);
                }
            }

            public void BuyUpgrade(int shopIndex)
            {
                Upgrade item = Shop[shopIndex];
                if (Cheese >= item.Price)
                {
                    Cheese -= item.Price;
                    item.Price += item.Price;
                    if (item.Type == "click")
                    {
                        int index = ClickUpgrades.FindIndex(i => i.Name == item.Name);
                        if (index == -1)
                        {
                            ClickUpgrades.Add(item);
                            index = ClickUpgrades.Count - 1;
                        }
                        ClickUpgrades[index].Quantity++;
                        Stats[item.Name] = ClickUpgrades[index].Quantity;
                    }
                    Console.WriteLine("Purchase Successful...Heh heh heh, come back any time!");
                }
                else
                {
                    Console.WriteLine("Not enough cash, homeless stranger!");
                }
                Console.WriteLine("Press any key to exit!");
                Console.ReadKey();
                inShopWindow = false;
            }

            public void Mine()
            {
                Cheese++;
                ClickUpgrades.ForEach(m =>
                {
                    Cheese += m.Quantity * m.Multiplier;
                });
            }

            public ConsoleKeyInfo getPlayerInput()
            {
                Console.Clear();
                drawGameScreen();
                var userChoice = Console.ReadKey();
                return userChoice;
            }
            public void drawGameScreen()
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string moon = @"
                                                                                                      
                              %%&&&&&&%                                         
                       %%%%%%%&%%%%&&&&&&&&&&&                                  
                   (###(((###%###%%&%&&%&%###(%&&%                              
                 ,***///(####((///(%%%##%%%&&%&&&%&%                            
                 .,*,*/(/#(/*//((/(///(///(%#&(((#%&&&                          
                  ..,,*/////**////////(///(((#(#((#%&&&&                        
                  ....,*/**(/**/*/#(**////(%#%%##(#%%&%&&                       
                    ..,.,,#/***,*/(##%(((%(##%%%%%%%%&&&&&                      
                     ...,,,,,*//((####(#&%%%&%###(%%&&&&&&&                     
                      ...,,..,/*/(((####%#%%%%%&#%%%&&@@&&&                     
                        .....,**////#((##%#%%%%&%&%&&&&&&&%                     
                         ....,,**////((((##%##&%%%%%&&&%%&%                     
                            .....,***//#((###%&%#%%%%&&%&%%                     
                              ......**((#######%#%%%%%%%&%                      
                                .....,*(#(#(##%((#%%%%%&%                       
                                  ...,**/##/(#%###%##%%%                        
                                      * * /#(((((% #%#                          
                                         ..*,@* /(%&                                                                     
        ";
                Console.WriteLine(moon);
                string message = $@"Mine (Spacebar), Shop (S), Quit (ESC)
                Cheese: {Cheese};
                Inventory:";
                foreach (var stats in Stats)
                {
                    message += $"\n {stats.Key}: {stats.Value}";
                }
                Console.WriteLine(message);
            }
        }



    }
}