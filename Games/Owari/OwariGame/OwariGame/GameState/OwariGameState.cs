/* $Id$
 *
 * Description: The game state holds the board state, as well as scoring and
 * turn information.
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
using DaedalusGameProtocol;

namespace OwariGame
{
    public class OwariGameState : GameState
    {
        public new OwariBoard Board
        {
            get
            {
                return (OwariBoard)board;
            }
        }

        // Constructor to initialize a new game state using the specified
        // GameBoard.
        public OwariGameState(GameBoard aBoard)
            : base(aBoard)
        {
        }

        // Get the player number of the player designated to move this turn.
        public override GamePlayer GetCurrentPlayerNumber()
        {
            int index = this.totalMoveCount % 2;
            return (index == 0) ? GamePlayer.One : GamePlayer.Two;
        }

        // Return a full copy of the current game state.
        public override GameState Copy()
        {
            OwariGameState state = new OwariGameState((OwariBoard)this.Board.Copy());

            state.totalMoveCount = this.totalMoveCount;
            state.gameIsOver = this.gameIsOver;
            state.gameOverCondition = GameOverCondition.None;
            state.winningPlayerNumber = this.winningPlayerNumber;
            state.history = new Stack<string>(this.history);

            return state;
        }
    }
}