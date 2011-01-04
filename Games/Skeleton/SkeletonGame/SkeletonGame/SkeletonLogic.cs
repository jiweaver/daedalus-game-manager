/* $Id: SkeletonLogic.cs 820 2011-01-03 03:05:53Z crosis $
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
using SkeletonGame;
using DaedalusGameProtocol;

namespace SkeletonGame
{
    public class SkeletonLogic : IGameLogic
    {
        // The game state.
        private SkeletonGameState State;

        // A game can be constructed with the default board, or a specified
        // board.
        public SkeletonLogic()
        {
            // Create a new default board.
            SkeletonBoard board = new SkeletonBoard();
            // Create the game state using the board we generated.
            this.State = new SkeletonGameState(board);
        }

        public SkeletonLogic(GameState aState)
        {
            this.State = (SkeletonGameState)aState;
        }

        // Return a reference to the current game state.
        public GameState GetGameState()
        {
            return State;
        }

        // Return true if the game state alone tells us that the game is over.
        public bool IsGameOver()
        {
            throw new Exception("TODO");
        }

        public void MakeMove(GamePlayer playerNumber, string msg)
        {
            // Parse it as a move command.
            SkeletonMessage.Move move;
            try
            {
                move = new SkeletonMessage.Move(msg);
            }
            catch (Exception)
            {
                // Junk message.
                throw new Exception(playerNumber + " forfeits, because they sent a move command that could not be parsed.");
            }

            // Now execute the command we received.
            throw new Exception("TODO");
        }

        public GameMessage.BoardState GetBoardStateMessage()
        {
            return new SkeletonMessage.BoardState((SkeletonBoard)this.State.Board.Copy());
        }
    }
}