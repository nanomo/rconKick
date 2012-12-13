using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using BattleNET;
using System.Collections;

using System.IO;
namespace rcon_kick
{
    class Program
    {
         [STAThread]
        static void Main(string[] args)
        {
            string line;
            bool error=false;
            Console.ResetColor();
            CommandLineArgs CommandLine = new CommandLineArgs(args);

            BattlEyeLoginCredentials loginCredentials = new BattlEyeLoginCredentials();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====================================");
            Console.WriteLine("ZEDAR.COM.AR // RCon Kicker Started!");
            Console.WriteLine("====================================");
            Console.ForegroundColor = ConsoleColor.Red;
            
            if (CommandLine["ip"] != null)
            {
                loginCredentials.Host = CommandLine["ip"];
            }
            else
            {
                Console.WriteLine("IP is missing!");
                error = true;
            }

            if (CommandLine["port"] != null)
            {
                loginCredentials.Port = Convert.ToInt32( CommandLine["port"]);
            }
            else
            {
                Console.WriteLine("Port is missing!");
                error = true;
            }

            if (CommandLine["password"] != null)
            {
                loginCredentials.Password =CommandLine["password"];
            }
            else
            {
                Console.WriteLine("Password is missing!");
                error = true;
            }

            if (CommandLine["kickall"] != null)
            {
                if (!error)
                {
                    IBattleNET b = new BattlEyeClient(loginCredentials);
                    b.MessageReceivedEvent += DumpMessage;
                    b.DisconnectEvent += Disconnected;
                    b.ReconnectOnPacketLoss(true);
                    b.Connect();
                    // :)
                    for (int i = 0; i <= 100; i++)
                    {
                        b.SendCommandPacket(EBattlEyeCommand.Kick, Convert.ToString(i));

                    }
                    b.Disconnect();
                    
                }

                Environment.Exit(0);
            }

            Console.ResetColor();

            if (CommandLine["file"] != null)
            {
                if (!error)
                {
                    try
                    {
                       System.IO.StreamReader file = new System.IO.StreamReader(CommandLine["file"]);

                        IBattleNET b = new BattlEyeClient(loginCredentials);
                        b.MessageReceivedEvent += DumpMessage;
                        b.DisconnectEvent += Disconnected;
                        b.ReconnectOnPacketLoss(true);
                        b.Connect();

                        while ((line = file.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            b.SendCommandPacket("#kick "+ line);

                        }
                        b.Disconnect();
                        file.Close();

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error: " + ex);
                        Console.ResetColor();

                    }
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File path is missing!");
                error = true;
                Console.ResetColor();
            }
             
            // Console.ReadLine();
        }


           private static void Disconnected(BattlEyeDisconnectEventArgs args)
        {
          // Console.WriteLine(args.Message);
        }

         private static void DumpMessage(BattlEyeMessageEventArgs args)
         {
            Console.WriteLine(args.Message);
         }
    }
}
