/* $Id: OwariLogic.cs 819 2011-01-03 01:46:43Z crosis $
 * 
 * Description: The game class contains the game state and the logic for making 
 * moves and checking the status of the game.
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
using DaedalusGameProtocol;
using System.Reflection;

namespace OwariGame
{
    public class OwariLogic : IGameLogic
    {
        private bool whiteIsSouth = true;

        // The game state.
        private OwariGameState state;

        // A game can be constructed with the default board, or a specified
        // board.
        public OwariLogic()
        {
            // Create a new default board.
            GameBoard board = new OwariBoard();
            // Create the game state using the board we generated.
            this.state = new OwariGameState(board);
        }

        public OwariLogic(GameState aState)
        {
            this.state = (OwariGameState)aState;
        }

        // Return a reference to the current game state.
        public GameState GetGameState()
        {
            return this.state;
        }

        private OwariMessage.Move GetMove(GamePlayer playerNumber, string msg)
        {
            // Parse it as a move command.
            OwariMessage.Move move;
            try
            {
                move = new OwariMessage.Move(msg);
            }
            catch (Exception)
            {
                // Junk message.
                throw new Exception(playerNumber + " forfeits, because they sent a move command that could not be parsed.");
            }
            return move;
        }

        private void CheckMoveValidity(bool southsTurn, int moveMaker)
        {
            // Check for invalid source pit.
            if (southsTurn && (moveMaker < 0 || moveMaker > 5))
                throw new Exception("Invalid source position.");
            else if (!southsTurn && (moveMaker < 7 || moveMaker > 12))
                throw new Exception("Invalid source position.");
            else if (state.Board.Buckets[moveMaker] == 0)
                throw new Exception("Source position is empty");
        }

        private int Redistribute(int moveMaker, int numberOfStonesToMove, bool southsTurn)
        {
            this.state.Board.Buckets[moveMaker] = 0;
            for (; numberOfStonesToMove > 0; numberOfStonesToMove--)
            {
                moveMaker++;

                if (moveMaker == 13 && southsTurn)
                    moveMaker = 0;
                else if (!southsTurn && moveMaker == 6)
                    moveMaker++;

                this.state.Board.Buckets[moveMaker]++;

                if (moveMaker == 13 && numberOfStonesToMove != 1)
                    moveMaker = -1;
            }
            return moveMaker;
        }

        private void Capture(int moveMaker, bool southsTurn)
        {
            if (moveMaker != 6 && moveMaker != 13)
            {
                if (southsTurn && moveMaker < 6)
                {
                    this.state.Board.Buckets[6] += this.state.Board.Buckets[12 - moveMaker];
                    this.state.Board.Buckets[12 - moveMaker] = 0;
                }
                else if (!southsTurn && moveMaker > 6)
                {
                    this.state.Board.Buckets[13] += this.state.Board.Buckets[12 - moveMaker];
                    this.state.Board.Buckets[12 - moveMaker] = 0;
                }
            }
        }

        private void EndGame()
        {
            bool southWentOut = true;

            for (int i = 0; i < 6; i++)
            {
                if (this.state.Board.Buckets[i] != 0)
                {
                    southWentOut = false;
                    break;
                }
            }

            if (!southWentOut)
            {
                for (int i = 0; i < 6; i++)
                {
                    this.state.Board.Buckets[6] += this.state.Board.Buckets[i];
                    this.state.Board.Buckets[i] = 0;
                }
            }
            else
            {
                for (int i = 7; i < 13; i++)
                {
                    this.state.Board.Buckets[13] += this.state.Board.Buckets[i];
                    this.state.Board.Buckets[i] = 0;
                }
            }
        }

        // Prints the current board state.
        private void printBoardState()
        {
            Console.Write("   ");
            for (int i = 12; i > 6; i--)
                Console.Write(state.Board.Buckets[i] + " ");
            Console.WriteLine("\n" + state.Board.Buckets[13] + "              " + state.Board.Buckets[6]);
            Console.Write("   ");
            for (int i = 0; i < 6; i++)
                Console.Write(state.Board.Buckets[i] + " ");
            Console.WriteLine();
        }

        // Return true if the game state alone tells us that the game is over.
        public bool IsGameOver()
        {
            bool piecesRemainSouth = false;
            bool piecesRemainNorth = false;

            for (int i = 0; i < 6; i++)
            {
                if (this.state.Board.Buckets[i] != 0)
                    piecesRemainSouth = true;
                if (this.state.Board.Buckets[i + 7] != 0)
                    piecesRemainNorth = true;
            }

            // If it is true that both players have pieces, then the game is not
            // over, so return false.
            return !(piecesRemainSouth && piecesRemainNorth);
        }

        public void MakeMove(GamePlayer playerNumber, string msg)
        {
            // Parse it as a move command.
            OwariMessage.Move move = this.GetMove(playerNumber, msg);

            // Now execute the command we received.
            int moveMaker = move.Command;
            int numberOfStonesToMove = state.Board.Buckets[moveMaker];
            bool southsTurn;

            if (state.GetCurrentPlayerNumber() == GamePlayer.One && whiteIsSouth)
                southsTurn = true;
            else
                southsTurn = false;

            this.CheckMoveValidity(southsTurn, moveMaker);

            moveMaker = this.Redistribute(moveMaker, numberOfStonesToMove, southsTurn);

            if (state.Board.Buckets[moveMaker] >= OwariSettings.MinSeedsForCapture && state.Board.Buckets[moveMaker] <= OwariSettings.MaxSeedsForCapture)
                this.Capture(moveMaker, southsTurn);

            if (IsGameOver())
            {
                this.EndGame();

                // Whoever has the most points wins
                if (this.state.Board.Buckets[13] == this.state.Board.Buckets[6])
                    this.state.SetGameOver(GamePlayer.None, GameOverCondition.Draw);
                else if (this.state.Board.Buckets[13] > this.state.Board.Buckets[6])
                    this.state.SetGameOver(GamePlayer.Two, GameOverCondition.YouWin);
                else
                    this.state.SetGameOver(GamePlayer.One, GameOverCondition.YouWin);
            }
        }
    }
}