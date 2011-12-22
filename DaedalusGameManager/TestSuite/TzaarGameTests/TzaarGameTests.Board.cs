/* $Id$
 *
 * Description: Tests for the TzaarBoard class.
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
using System.Linq;
using System.Text;
using DaedalusGameProtocol;
using TzaarGame;

namespace TestSuite
{
    abstract partial class TzaarGameTests
    {
        public abstract class Board
        {
            public static void Run()
            {
                Console.WriteLine("Testing TzaarBoard.Query...");
                TestQuery();

                Console.WriteLine("Testing TzaarBoard.Add...");
                TestAdd();

                Console.WriteLine("Testing TzaarBoard.Take...");
                TestTake();
            }

            // Test the functionality related to 'taking' a piece, or stack of
            // pieces, from a position on the game board. Additionally, it
            // should be possible to attempt to 'take' a piece from an invalid
            // position on the game board without error. Throws Exception if
            // 'taking' the pieces from the board does not result in the
            // specified position being empty.
            private static void TestTake()
            {
                // Create a new empty game board.
                TzaarBoard board = new TzaarBoard(true);

                // Create some game pieces of various types.
                TzaarPiece p1 = new TzaarPiece.Tzaar(TzaarColor.BLACK);
                TzaarPiece p2 = new TzaarPiece.Tzarra(TzaarColor.BLACK);
                TzaarPiece p3 = new TzaarPiece.Tott(TzaarColor.BLACK);

                // Add the pieces we created to the board at the target
                // position.
                board.Add(p1, 0, 0);
                board.Add(p2, 0, 0);
                board.Add(p3, 0, 0);

                // Check that there are 3 pieces on the target position.
                if (board.Query(0, 0).Count != 3)
                    throw new Exception();

                // Take the pieces.
                if (board.Take(0, 0).Count != 3)
                    throw new Exception();

                // Check that there are now 0 pieces on the target position.
                if (board.Query(0, 0).Count != 0)
                    throw new Exception();

                // Try to Take from a spot that doesn't exist.
                board.Take(-1, 8);
            }

            // The goal is to test the board-querying functionality. Throws
            // Exception if the piece(s) returned from the specified position
            // do not match those placed at that position.
            private static void TestQuery()
            {
                // Create a new empty game board.
                TzaarBoard board = new TzaarBoard(true);

                // Create some game pieces of various types.
                TzaarPiece p1 = new TzaarPiece.Tzaar(TzaarColor.BLACK);
                TzaarPiece p2 = new TzaarPiece.Tzarra(TzaarColor.BLACK);
                TzaarPiece p3 = new TzaarPiece.Tott(TzaarColor.BLACK);

                // Add the pieces we created to the board at the target
                // position.
                board.Add(p1, 0, 0);
                board.Add(p2, 0, 0);
                board.Add(p3, 0, 0);

                // Check that there are 3 pieces on the target position.
                Stack<TzaarPiece> pieces = board.Query(0, 0);
                if (pieces.Count != 3)
                    throw new Exception();
            }

            // Check that stacking pieces works properly. Additionally, it
            // should be possible to add a piece to an invalid position on the
            // board without error. Throws Exception if the determined stack
            // size is not equal to the size of the stack placed at the
            // specified position.
            private static void TestAdd()
            {
                // Create a new empty game board.
                TzaarBoard board = new TzaarBoard(true);

                // Create some game pieces of various types.
                TzaarPiece p1 = new TzaarPiece.Tzaar(TzaarColor.BLACK);
                TzaarPiece p2 = new TzaarPiece.Tzarra(TzaarColor.BLACK);
                TzaarPiece p3 = new TzaarPiece.Tott(TzaarColor.BLACK);

                board.Add(p1, 0, 0);
                board.Add(p2, 0, 0);
                board.Add(p3, 0, 0);

                Stack<TzaarPiece> S = board.Query(0, 0);
                if (S.Count != 3)
                    throw new Exception();
                if (S.Peek() != p3)
                    throw new Exception();

                // Try to add to a spot that doesn't exist.
                board.Add((TzaarPiece)null, 8, 8);
            }
        }
    }
}