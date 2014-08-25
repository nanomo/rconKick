using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using BattleNET;
using System.Collections;

using System.IO;
using System.Net;
namespace rcon_kick
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            string line;
            bool error = false;
            Console.ResetColor();
            CommandLineArgs CommandLine = new CommandLineArgs(args);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====================================");
            Console.WriteLine("ZEDAR.COM.AR // RCon Kicker Started!");
            Console.WriteLine("====================================");
            Console.ForegroundColor = ConsoleColor.Red;

            BattlEyeLoginCredentials loginCredentials;
            loginCredentials = GetLoginCredentials(args);

            if (CommandLine["ip"] == null)
            {
                Console.WriteLine("IP is missing!");
                error = true;
            }

            if (CommandLine["port"] == null)
            {
                Console.WriteLine("Port is missing!");
                error = true;
            }

            if (CommandLine["password"] == null)
            {
                Console.WriteLine("Password is missing!");
                error = true;
            }

            if (CommandLine["kickall"] != null)
            {
                string kam = "Restarting, reconnect in 2 minutes!";
                int kamin = 2;

                if (CommandLine["kickallmsg"] != null)
                {
                    kam = CommandLine["kickallmsg"];
                }
                else
                {
                    Console.WriteLine("kickall message is missing, using the default: " + kam);

                }

                if (CommandLine["kickallmin"] != null)
                {
                    kamin = Convert.ToInt32(CommandLine["kickallmin"]);
                }
                else
                {
                    Console.WriteLine("Minutes of ban missing, using the default: " + kamin);

                }

                if (!error)
                {
                    BattlEyeClient b = new BattlEyeClient(loginCredentials);
                    b.BattlEyeMessageReceived += BattlEyeMessageReceived;
                    b.ReconnectOnPacketLoss = true;
                    b.Connect();
                    // :)
                    for (int i = 0; i <= 100; i++)
                    {
                        //b.SendCommandPacket(EBattlEyeCommand.Kick, Convert.ToString(i));
                        b.SendCommand("ban " + Convert.ToString(i) + " " + kamin + " " + kam);
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

                        BattlEyeClient b = new BattlEyeClient(loginCredentials);
                        b.BattlEyeMessageReceived += BattlEyeMessageReceived;
                        b.ReconnectOnPacketLoss = true;
                        b.Connect();

                        while ((line = file.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            b.SendCommand("#kick " + line);

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

        private static void BattlEyeMessageReceived(BattlEyeMessageEventArgs args)
        {
            Console.WriteLine(args.Message);
        }

        private static BattlEyeLoginCredentials GetLoginCredentials(string[] args)
        {
            BattlEyeLoginCredentials loginCredentials = new BattlEyeLoginCredentials();
            loginCredentials.Host = null;
            loginCredentials.Port = 0;
            loginCredentials.Password = "";

            for (int i = 0; i < args.Length; i = i + 2)
            {
                switch (args[i])
                {
                    case "-host":
                        {
                            try
                            {
                                IPAddress ip = Dns.GetHostAddresses(args[i + 1])[0];
                                loginCredentials.Host = ip;
                            }
                            catch
                            {
                                Console.WriteLine("No valid host given!");
                            }
                            break;
                        }

                    case "-port":
                        {
                            int value;
                            if (int.TryParse(args[i + 1], out value))
                            {
                                loginCredentials.Port = value;
                            }
                            else
                            {
                                Console.WriteLine("No valid port given!");
                            }
                            break;
                        }

                    case "-password":
                        {
                            if (args[i + 1] != "")
                            {
                                loginCredentials.Password = args[i + 1];
                            }
                            else
                            {
                                Console.WriteLine("No password given!");
                            }
                            break;
                        }
                }
            }

            return loginCredentials;
        }
    }
}
