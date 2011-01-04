/* $Id: TzaarGameTests.cs 820 2011-01-03 03:05:53Z crosis $
 * 
 * Description: Defines the GetNextMove and MakeTheBoard methods and passes them
 * to the TwoServerOneClientTest.
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
using System.Linq;
using System.Text;
using DaedalusGameProtocol;
using TzaarGame;

namespace TestSuite
{
    abstract partial class TzaarGameTests
    {
        private static int whiteIndex;
        private static int blackIndex;

        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("#### BEGIN TZAAR TESTS ####");

            TzaarGameTests.Board.Run();

            TzaarGameTests.Messages.Run();

            // Run tests on the board logic.
            TzaarGameTests.Logic.Run();

            // Run the boardMap tests.
            TzaarGameTests.BoardMap.Run();

            // Run the two-clients-one-server test.
            whiteIndex = 0;
            blackIndex = 0;
            TwoClientsOneServerTest.Run(GetNextMove, MakeTheBoard());
        }

        // The predefined moves for player white.
        private static string[] whiteMoves = new string[] {
                new TzaarMessage.Move(0,0,0,1),

                new TzaarMessage.Move(2,6,1,5),
                new TzaarMessage.Move(1,5,1,4),

                new TzaarMessage.Move(2,4,2,5),
                new TzaarMessage.Move(),

                new TzaarMessage.Move(2,2,2,1),
                new TzaarMessage.Move(2,1,2,0),

                new TzaarMessage.Move(8,1,7,2),
                new TzaarMessage.Move(7,2,7,1),

                new TzaarMessage.Move(4,0,4,1),
                new TzaarMessage.Move(4,1,3,0),

                new TzaarMessage.Move(4,4,4,5),
                new TzaarMessage.Move(4,5,3,6),

                new TzaarMessage.Move(5,4,5,3),
                new TzaarMessage.Move(5,3,6,2),

                new TzaarMessage.Move(7,1,8,0),
                new TzaarMessage.Move(8,0,8,2),

                new TzaarMessage.Move(7,3,6,4),
                new TzaarMessage.Move(6,4,6,5),

                new TzaarMessage.Move(6,2,5,2),
                new TzaarMessage.Move(5,2,4,2),

                new TzaarMessage.Move(3,0,3,3),
                new TzaarMessage.Move(3,3,3,5),

                new TzaarMessage.Move(6,5,5,6),
                new TzaarMessage.Move(5,6,5,7),

                new TzaarMessage.Move(3,5,2,5),
                new TzaarMessage.Move(2,5,3,6),
            };

        // The predefined moves for player black.
        private static string[] blackMoves = new string[] {
               new TzaarMessage.Move(0,3,0,2),
               new TzaarMessage.Move(0,2,1,3),

               new TzaarMessage.Move(1,3,1,4),
               new TzaarMessage.Move(1,4,0,4),

               new TzaarMessage.Move(2,3,2,5),
               new TzaarMessage.Move(),

               new TzaarMessage.Move(1,1,1,2),
               new TzaarMessage.Move(1,2,0,1),

               new TzaarMessage.Move(4,7,4,6),
               new TzaarMessage.Move(4,6,5,6),

               new TzaarMessage.Move(6,1,6,0),
               new TzaarMessage.Move(6,0,7,0),

               new TzaarMessage.Move(5,1,5,0),
               new TzaarMessage.Move(5,0,5,2),

               new TzaarMessage.Move(0,4,3,4),
               new TzaarMessage.Move(3,4,0,1),

               new TzaarMessage.Move(3,1,3,2),
               new TzaarMessage.Move(3,2,3,3),

               new TzaarMessage.Move(6,6,7,5),
               new TzaarMessage.Move(7,5,5,7),

               new TzaarMessage.Move(7,4,8,3),
               new TzaarMessage.Move(8,3,8,4),

               new TzaarMessage.Move(4,3,1,0),
               new TzaarMessage.Move(1,0,6,3),

               new TzaarMessage.Move(8,4,8,2),
               new TzaarMessage.Move(8,2,5,5),

               new TzaarMessage.Move(5,5,5,7),
               new TzaarMessage.Move(),
            };

        private static string GetNextMove(int playerNumber)
        {
            if (playerNumber == 1)
            {
                // White's turn.
                if (whiteIndex < whiteMoves.Length)
                    return whiteMoves[whiteIndex++];
                else
                    throw new Exception("White has no more moves!");
            }
            else
            {
                // Black's turn.
                if (blackIndex < blackMoves.Length)
                    return blackMoves[blackIndex++];
                else
                    throw new Exception("Black has no more moves!");
            }
        }

        // Place pieces on the board in pre-defined positions.
        private static TzaarBoard MakeTheBoard()
        {
            TzaarBoard board = new TzaarBoard(true);

            // Col 0.
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 0, 0);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 0, 1);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 0, 2);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 0, 3);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 0, 4);

            // Col 1.
            board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 1, 0);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 1, 1);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 1, 2);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 1, 3);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 1, 4);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 1, 5);

            // Col 2.
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 2, 0);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 2, 1);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 2, 2);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 2, 3);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 2, 4);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 2, 5);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 2, 6);

            // Col 3.
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 3, 0);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 3, 1);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 3, 2);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 3, 3);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 3, 4);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 3, 5);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 3, 6);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 3, 7);

            // Col 4.
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 4, 0);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 4, 1);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 4, 2);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 4, 3);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 4, 4);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 4, 5);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 4, 6);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 4, 7);

            // Col 5.
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 5, 0);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 5, 1);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 5, 2);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 5, 3);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 5, 4);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 5, 5);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 5, 6);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 5, 7);

            // Col 6.
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 6, 0);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 6, 1);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 6, 2);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 6, 3);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 6, 4);
            board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 6, 5);
            board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 6, 6);

            // Col 7.
            board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 7, 0);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 7, 1);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 7, 2);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 7, 3);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 7, 4);
            board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 7, 5);

            // Col 8.
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 8, 0);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 8, 1);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 8, 2);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 8, 3);
            board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 8, 4);

            return board;
        }
    }
}