/* $Id$
 *
 * Description: A simple text based client that connects to the Daedalus Game
 * Manager and allows the user to manually type and send Daedalus Game Manager
 * Protocol messages.
 *
 * Copyright (c) 2010-2011, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and
 * Justin Weaver).
 *
 * Distributed under the BSD-new license. For details see the BSD_LICENSE file
 * that should have been included with this distribution. If the source you
 * acquired this distribution from incorrectly removed this file, the license
 * may be viewed at http://www.opensource.org/licenses/bsd-license.php.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using DaedalusGameProtocol;

namespace DaedalusTextClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Daedalus Text Client.");

            // Acquire an IP address.
            Console.Write("Game Server IP address: ");
            IPAddress host = IPAddress.Parse(Console.ReadLine());

            TcpClient client = new TcpClient(host.ToString(), 2525);

            if (!client.Connected)
                throw new Exception();

            Console.WriteLine("Connected.\n");

            // Create our reader/writer streams.
            NetworkStream ns = client.GetStream();
            StreamWriter sw = new StreamWriter(ns);
            StreamReader sr = new StreamReader(ns);

            // Immediately flush data we put into the write buffer.
            sw.AutoFlush = true;

            while (true)
            {
                String message;
                try
                {
                    message = sr.ReadLine();
                    if (message == "")
                        continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("Read Error.  Connection lost?");
                    break;
                }

                if (GameMessage.IsVersion(message))
                {
                    // Version Info.
                    Console.WriteLine("Received Version Message: " + message);
                }
                else if (GameMessage.IsChat(message))
                {
                    // Chat message.
                    Console.WriteLine("Received Chat Message: " + message);
                }
                else if (GameMessage.IsBoardState(message))
                {
                    // Board State.
                    Console.WriteLine("Received Initial Board State: " + message);
                }
                else if (GameMessage.IsMove(message))
                {
                    // Other player's move.
                    Console.WriteLine("Received: " + message);
                }
                else if (GameMessage.IsYourTurn(message))
                {
                    // Your Move.
                    Console.Write("YourMove> ");
                    sw.WriteLine(Console.ReadLine());
                }
                else if (GameMessage.IsGameOver(message))
                {
                    // You Win.
                    Console.WriteLine("Game Over: " + new GameMessage.GameOver(message).Condition);
                    break;
                }
                else if (GameMessage.IsYourPlayerNumber(message))
                {
                    // Your Player Number.
                    Console.WriteLine("You are player " + new GameMessage.YourPlayerNumber(message).PlayerNumber);
                }
                else
                {
                    Console.WriteLine("Unrecognized message!");
                    break;
                }
            }

            sr.Close();
            sw.Close();
            client.Close();

            Console.WriteLine("\nPress ESC key to exit:");
            while (true)
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
        }
    }
}