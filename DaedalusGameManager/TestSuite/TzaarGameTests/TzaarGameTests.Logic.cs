/* $Id: TzaarGameTests.Logic.cs 820 2011-01-03 03:05:53Z crosis $
 * 
 * Description: Tests for TzaarLogic.
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
using TzaarGame;
using DaedalusGameProtocol;

namespace TestSuite
{
    abstract partial class TzaarGameTests
    {
        public abstract class Logic
        {
            public static void Run()
            {
                TestBoardInitialization();

                TestCapture();

                TestStack();

                TestUpdatePieceCount();

                TestGameOver();
            }

            // The goal is to verify components which initialize a new game by
            // checking that the required number of pieces have been placed in a
            // manner consistent with the Tzaar game rules. Throws Exception if
            // an incorrect number of pieces appear on the game board at
            // initialization.
            private static void TestBoardInitialization()
            {
                TzaarLogic game = new TzaarLogic();

                int[] blackPieceCount = new int[3];
                int[] whitePieceCount = new int[3];

                // Scan the board and query.
                for (int i = 0; i < 9; i++)
                    for (int j = 0; j < 9; j++)
                    {
                        Stack<TzaarPiece> s = (((TzaarBoard)game.GetGameState().Board).Query(i, j));
                        if (s == null)
                            break;
                        // There must be only one piece at each position on the 
                        // board at the start of a new game.
                        if (s.Count != 1)
                            throw new Exception();
                        // First check the color, then increment the respective
                        // piece count.
                        if (s.Peek().Color == TzaarColor.BLACK)
                        {
                            if (s.Peek().GetType() == typeof(TzaarPiece.Tzaar))
                                blackPieceCount[0]++;
                            else if (s.Peek().GetType() == typeof(TzaarPiece.Tzarra))
                                blackPieceCount[1]++;
                            else
                                blackPieceCount[2]++;
                        }
                        else
                        {
                            if (s.Peek().GetType() == typeof(TzaarPiece.Tzaar))
                                whitePieceCount[0]++;
                            else if (s.Peek().GetType() == typeof(TzaarPiece.Tzarra))
                                whitePieceCount[1]++;
                            else
                                whitePieceCount[2]++;
                        }
                    }

                // Check if the piece counts for each player are equal to the
                // required piece counts for a new game, as dictated by the
                // Tzaar game rules.
                if (blackPieceCount[0] != 6 || whitePieceCount[0] != 6)
                    throw new Exception();
                if (blackPieceCount[1] != 9 || whitePieceCount[1] != 9)
                    throw new Exception();
                if (blackPieceCount[2] != 15 || whitePieceCount[2] != 15)
                    throw new Exception();
            }

            // Test components controlling the logic for one player to 'capture'
            // the piece of another player. Throws Exception if a valid 
            // 'capture' results in an inconsistent game state, if an invalid 
            // capture is allowed, or if a player is allowed to move the
            // opposite player's game pieces.
            private static void TestCapture()
            {
                // Test a simple, valid capture.
                TzaarBoard board = new TzaarBoard(true);
                TzaarPiece whitePiece = new TzaarPiece.Tzaar(TzaarColor.WHITE);
                board.Add(whitePiece, 2, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 2, 3);
                TzaarGameState state = new TzaarGameState(board);
                TzaarLogic game = new TzaarLogic(state);

                game.Move(2, 2, 2, 3);

                // The destination position should now contain the white piece 
                // which captured the black piece.
                if (board.Query(2, 3).Peek() != whitePiece)
                    throw new Exception();

                // The origin position should no longer contain any pieces.
                if (board.Query(2, 2).Count() != 0)
                    throw new Exception();

                // Test an illegal capture.  The capture is illegal because the
                // stack in the target position is more powerful than the stack
                // at the source.
                board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 2, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 2, 3);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 2, 3);
                state = new TzaarGameState(board);
                game = new TzaarLogic(state);

                bool passedTest = false;
                try
                {
                    game.Move(2, 2, 2, 3);
                }
                catch
                {
                    passedTest = true;
                }
                if (!passedTest)
                {
                    throw new Exception();
                }

                // Now try controlling some of the opponent's pieces. The game
                // should not allow it!
                board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 2, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 2, 3);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 2, 3);
                state = new TzaarGameState(board);
                game = new TzaarLogic(state);

                passedTest = false;
                try
                {
                    game.Move(2, 3, 2, 2);
                }
                catch
                {
                    passedTest = true;
                }
                if (!passedTest)
                {
                    throw new Exception();
                }
            }

            // Test the components which control functionality to 'stack' pieces
            // on one another. Throws Exception if an attempt to 'stack' pieces
            // results in a stack size not equal to the total number of pieces
            // being 'stacked'.
            private static void TestStack()
            {
                TzaarBoard board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 2, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 2, 3);
                TzaarGameState state = new TzaarGameState(board, 4);
                TzaarLogic game = new TzaarLogic(state);

                game.Move(2, 2, 2, 3);

                Stack<TzaarPiece> S = board.Query(2, 3);

                // Stacked a total of 2 pieces, so stack size should be 2.
                if (S.Count() != 2)
                    throw new Exception();

                // Stacked a Tzaar on top of another Tzaar, so the top piece
                // should be a Tzaar.
                if (S.Peek().GetType() != typeof(TzaarPiece.Tzaar))
                    throw new Exception();

                // Stacked a white piece on top of another white piece, so the
                // top piece should be white.
                if (S.Peek().Color != TzaarColor.WHITE)
                    throw new Exception();
            }

            // The goal is to verify that a change to the board state in the
            // form of a 'capture' or 'stack' operation results in a correct
            // update to the counts for each piece remaining on the board.
            // Throws Exception if a piece count is inconsistent with the actual
            // game state following the specified operation.
            private static void TestUpdatePieceCount()
            {
                TzaarBoard board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 2, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 2, 3);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 3, 4);
                TzaarGameState state = new TzaarGameState(board);
                TzaarLogic game = new TzaarLogic(state);
                int BlackTzaarCount = ((TzaarBoard)game.GetGameState().Board).BlackTzaarCount;

                game.Move(2, 2, 2, 3);

                // A Black Tzaar was 'captured', so the piece count should be
                // decremented by 1.
                if (BlackTzaarCount - ((TzaarBoard)game.GetGameState().Board).BlackTzaarCount != 1)
                    throw new Exception();
            }

            // Test the components which determine an end-game state. Throws 
            // Exception if the actual game state is an end-game state and the 
            // game is not determined to be over.
            private static void TestGameOver()
            {
                // Black has no Totts; black loses.
                TzaarBoard board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 0, 0);
                board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 0, 1);
                board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 0, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 1, 1);
                board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 1, 2);
                TzaarLogic game = new TzaarLogic(board);
                if (!game.WhiteCanCapture())
                    throw new Exception();
                if (!game.BlackCanCapture())
                    throw new Exception();
                if (game.WhiteIsOutOfPieces())
                    throw new Exception();
                if (!game.BlackIsOutOfPieces())
                    throw new Exception();
                if (!game.IsGameOver())
                    throw new Exception();

                // Black and White have enough pieces to play, but white can't
                // make any more moves.
                board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 0, 0);
                board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 0, 1);
                board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 0, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 1, 0);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 1, 0);
                board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 8, 0);
                board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 8, 1);
                game = new TzaarLogic(board);
                if (game.WhiteCanCapture())
                    throw new Exception();
                if (!game.BlackCanCapture())
                    throw new Exception();
                if (game.WhiteIsOutOfPieces())
                    throw new Exception();
                if (game.BlackIsOutOfPieces())
                    throw new Exception();
                if (!game.IsGameOver())
                    throw new Exception();

                // Neither player can make a move, so we don't know who won; 
                // this is ok, because we can handle this case in the server
                // based on who made the final move, and which turn of theirs it
                // was. As far as the game logic is concerned, the game is over,
                // but neither player won.
                board = new TzaarBoard(true);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.WHITE), 0, 0);
                board.Add(new TzaarPiece.Tzarra(TzaarColor.WHITE), 0, 1);
                board.Add(new TzaarPiece.Tott(TzaarColor.WHITE), 0, 2);
                board.Add(new TzaarPiece.Tzaar(TzaarColor.BLACK), 8, 0);
                board.Add(new TzaarPiece.Tzarra(TzaarColor.BLACK), 8, 1);
                board.Add(new TzaarPiece.Tott(TzaarColor.BLACK), 8, 2);
                game = new TzaarLogic(board);
                if (game.WhiteCanCapture())
                    throw new Exception();
                if (game.BlackCanCapture())
                    throw new Exception();
                if (game.WhiteIsOutOfPieces())
                    throw new Exception();
                if (game.BlackIsOutOfPieces())
                    throw new Exception();
                if (!game.IsGameOver())
                    throw new Exception();
            }
        }
    }
}