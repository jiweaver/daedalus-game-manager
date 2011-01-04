/* $Id: TzaarGameTests.BoardMap.cs 820 2011-01-03 03:05:53Z crosis $
 * 
 * Description: Tests for the TzaarBoardMap class.
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
        public abstract class BoardMap
        {
            public static void Run()
            {
                Console.WriteLine("Testing BoardMap...");
                TestBoardMapPathing();
            }

            // The goal is to test various representative valid and invalid
            // moves for correctness. Throws Exception if a validly specified
            // path is determined to be invalid, or if an invalidly specified
            // path is determined to be valid.
            private static void TestBoardMapPathing()
            {
                // Create a new empty game board.
                TzaarBoardMap boardMap = new TzaarBoardMap();
                TzaarBoard board = new TzaarBoard(true);

                // Create some game pieces of various types.
                TzaarPiece p1 = new TzaarPiece.Tzaar(TzaarColor.BLACK);
                TzaarPiece p2 = new TzaarPiece.Tzarra(TzaarColor.BLACK);
                TzaarPiece p3 = new TzaarPiece.Tott(TzaarColor.BLACK);

                // Operate on this specific position.
                int col = 8;
                int row = 0;

                // Add the pieces we created to the board at the target
                // position.
                board.Add(p1, col, row);
                board.Add(p2, col, row);
                board.Add(p3, col, row);

                // Check that a valid move is reported as valid.
                if (!boardMap.IsValidPath(board, 0, 0, 0, 1))
                    throw new Exception();

                // Check that an invalid move is reported as invalid.
                if (boardMap.IsValidPath(board, 4, 3, 4, 4))
                    throw new Exception();

                board.Add(p1, 2, 2);
                board.Add(p2, 2, 3);

                // Check that an invalid move is reported as invalid.
                if (!boardMap.IsValidPath(board, 2, 2, 2, 3))
                    throw new Exception();

                // Check that passing through another piece is reported as
                // invalid.
                board.Add(p1, 1, 1);
                if (boardMap.IsValidPath(board, 0, 0, 2, 2))
                    throw new Exception();

                // Remove the obstructing piece and verify that (0, 0) and 
                // (2, 2) are now connected.
                board.Take(1, 1);
                if (!boardMap.IsValidPath(board, 0, 0, 2, 2))
                    throw new Exception();
            }
        }
    }
}