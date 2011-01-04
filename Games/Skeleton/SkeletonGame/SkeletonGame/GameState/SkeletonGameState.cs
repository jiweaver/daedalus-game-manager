/* $Id: SkeletonGameState.cs 835 2011-01-03 15:17:50Z piranther $
 *
 * Description: The game state holds the board state, as well as scoring and
 * turn information.
 *
 * Copyright (c) 2010, Team Daedalus (Mathew Bergt, Jason Buck, Ken Kelley, and
 * Justin Weaver).
 *
 * Distributed under the BSD-new license. For details see the BSD_LICENSE file
 * that should have been included with this distribution. If the source you
 * acquired this distribution from incorrectly removed this file, the license
 * may be viewed at http://www.opensource.org/licenses/bsd-license.php.
 */

using System.Collections.Generic;
using DaedalusGameProtocol;
using System;

namespace SkeletonGame
{
    public class SkeletonGameState : GameState
    {
        public new SkeletonBoard Board
        {
            get
            {
                return (SkeletonBoard)board;
            }
        }

        // Constructor to initialize a new game state using the specified
        // GameBoard.
        public SkeletonGameState(GameBoard aBoard)
            : base(aBoard)
        {
        }

        // Get the player number of the player designated to move this turn.
        public override GamePlayer GetCurrentPlayerNumber()
        {
            throw new Exception("TODO");
        }

        // Return a full copy of the current game state.
        public override GameState Copy()
        {
            SkeletonGameState state = new SkeletonGameState((SkeletonBoard)this.Board.Copy());

            state.totalMoveCount = this.totalMoveCount;
            state.gameIsOver = this.gameIsOver;
            state.gameOverCondition = GameOverCondition.None;
            state.winningPlayerNumber = this.winningPlayerNumber;
            state.history = new Stack<string>(this.history);

            return state;
        }
    }
}