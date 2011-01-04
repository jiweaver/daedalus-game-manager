/* $Id: OwariGameTests.cs 820 2011-01-03 03:05:53Z crosis $
 *
 * Description: Defines and passes the GetNextMove method to the 
 * TwoClientsOneServer tests.
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
using OwariGame;

namespace TestSuite
{
    class OwariGameTests
    {
        private static int whiteIndex;
        private static int blackIndex;

        public static void Run()
        {
            Console.WriteLine();
            Console.WriteLine("#### BEGIN OWARI TESTS ####");

            // Run the two-clients-one-server test.
            whiteIndex = 0;
            blackIndex = 0;
            TwoClientsOneServerTest.Run(GetNextMove);
        }

        // The predefined moves for player white.
        private static string[] whiteMoves = new string[] {
                new OwariMessage.Move(4),
                new OwariMessage.Move(5),
                new OwariMessage.Move(2),
                new OwariMessage.Move(5),
                new OwariMessage.Move(0),
                new OwariMessage.Move(0),
            };

        // The predefined moves for player black.
        private static string[] blackMoves = new string[] {
               new OwariMessage.Move(12),
                new OwariMessage.Move(8),
                new OwariMessage.Move(9),
                new OwariMessage.Move(10),
                new OwariMessage.Move(11),
                new OwariMessage.Move(12),
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
    }
}