/* $Id$
 *
 * Description: The "2-client-1-server" test.  It starts a game server, and
 * spawns two client threads that connect and play a game by requesting each new
 * move from a callback delegate.
 *
 * Copyright (c) 2010, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and 
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
using System.Threading;
using System.Windows.Forms;
using DaedalusGameManager;
using DaedalusGameProtocol;

namespace TestSuite
{
    internal abstract class TwoClientsOneServerTest
    {
        public delegate string GetNextMoveDelegate(int playerNumber);
        public delegate int GetTotalMovesDelegate(int playerNumber);
        public delegate int GetMoveNumberDelegate(int playerNumber);

        private const int DelayBetweenMoves = 0; // In ms.

        private static DaedalusGameManagerForm f;

        private static GetNextMoveDelegate GetNextMove;
        private static GetTotalMovesDelegate GetTotalMoves;
        private static GetMoveNumberDelegate GetMoveNumber;

        public static void Run(GetNextMoveDelegate getNextMove, GetTotalMovesDelegate getTotalMoves, GetMoveNumberDelegate getMoveNumber)
        {
            Run(getNextMove, getTotalMoves, getMoveNumber, null);
        }

        // Make the board, start the server, and connect the clients.
        public static void Run(GetNextMoveDelegate getNextMove, GetTotalMovesDelegate getTotalMoves, GetMoveNumberDelegate getMoveNumber, GameBoard aBoard)
        {
            GetNextMove = getNextMove;
            GetTotalMoves = getTotalMoves;
            GetMoveNumber = getMoveNumber;
            f = new DaedalusGameManagerForm(aBoard);
            while (!f.ServerIsRunning)
                ;

            IPAddress host = IPAddress.Parse("127.0.0.1");

            Client client1 = new Client(host, 2525);
            Client client2 = new Client(host, 2525);

            f.ShowDialog();
        }

        private class Client
        {
            private Thread clientThread;

            // The server IP address.
            private IPAddress host;
            // The server port.
            private int port;

            // Constructor instantiates a Client object and starts a
            // client thread.
            public Client(IPAddress host, int port)
            {
                this.host = host;
                this.port = port;
                clientThread = new Thread(new ThreadStart(ClientWork));
                clientThread.Start();
                while (!clientThread.IsAlive)
                    ;
            }

            // Connect the client to the server, begin a new game, and make
            // the predefined moves for the game.
            private void ClientWork()
            {
                // Try to connect to the server.
                TcpClient client = new TcpClient(host.ToString(), port);

                // Create our reader/writer streams.
                NetworkStream ns = client.GetStream();

                StreamWriter sw = new StreamWriter(ns);
                StreamReader sr = new StreamReader(ns);

                // Immediately flush data we put into the write buffer.
                sw.AutoFlush = true;

                // First we should receive the Version message.
                string msg;
                try
                {
                    msg = sr.ReadLine();
                }
                catch
                {
                    return;
                }

                GameMessage.Version versionMsg = new GameMessage.Version(msg);

                Console.WriteLine(string.Format("Received Version Message: {0}", versionMsg));

                // Next we should receive our player number.
                try
                {
                    msg = sr.ReadLine();
                }
                catch
                {
                    return;
                }

                GamePlayer myPlayerNumber = new GameMessage.YourPlayerNumber(msg).PlayerNumber;

                Console.WriteLine(string.Format("[{0}] Received YourPlayerNumber: {1}", myPlayerNumber.ToString(), msg));

                // Next we receive the board state.
                try
                {
                    msg = sr.ReadLine();
                }
                catch
                {
                    return;
                }

                if (!GameMessage.IsBoardState(msg))
                    throw new Exception();

                Console.WriteLine(string.Format("[{0}] Received BoardState: {1}", myPlayerNumber.ToString(), GameMessage.GetMessageData(msg)));

                while (true)
                {
                    // Ok, now we wait for a message.
                    try
                    {
                        msg = sr.ReadLine();
                    }
                    catch
                    {
                        // Connection lost.
                        break;
                    }

                    // Connection lost.
                    if (msg == null)
                        break;

                    Console.WriteLine(string.Format("[{0}] Received Message: {1}", myPlayerNumber.ToString(), msg));

                    if (GameMessage.IsYourTurn(msg))
                    {
                        string nextMove;

                        try
                        {
                            nextMove = GetNextMove((myPlayerNumber == GamePlayer.One) ? 1 : 2);
                        }
                        catch
                        {
                            // Out of moves!
                            continue;
                        }

                        Thread.Sleep(DelayBetweenMoves);

                        // Send our next move.
                        Console.WriteLine(string.Format("[{0}] (move {1} of {2}) Sending Message: {3}", myPlayerNumber.ToString(), GetMoveNumber((myPlayerNumber == GamePlayer.One) ? 1 : 2), GetTotalMoves((myPlayerNumber == GamePlayer.One) ? 1 : 2), nextMove));
                        sw.WriteLine(nextMove);
                    }
                    else if (GameMessage.IsGameOver(msg))
                    {
                        break;
                    }
                }

                sr.Close();
                sw.Close();

                client.Close();
            }
        }
    }
}