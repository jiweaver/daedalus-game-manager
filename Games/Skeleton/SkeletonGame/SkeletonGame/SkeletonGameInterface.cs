/* $Id$
 * 
 * Description: This is how the game plugs in to the manager.  If you are
 * implementing a new game, all you will likely need to do in this file is
 * change every occurrence of "Skeleton" to the name of your game.
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
using System.Windows.Forms;

namespace SkeletonGame
{
    public class SkeletonGameInterface : IGame
    {
        // Return a string that uniquely identifies this game.
        public string GetGameName()
        {
            return "Skeleton";
        }

        // Construct and return a new instance of the game-specific GamePainter.
        public IGamePainter GetNewGamePainter(System.Windows.Forms.PictureBox aPictureBox)
        {
            return new SkeletonPainter(aPictureBox);
        }

        public IGameLogic GetNewGameLogic(GameState aState)
        {
            return new SkeletonLogic(aState);
        }

        // Construct and return a new instance of the game-specific GameState.
        public GameState GetNewGameState()
        {
            return new SkeletonGameState(new SkeletonBoard());
        }

        public GameState GetNewGameState(GameBoard aBoard)
        {
            return new SkeletonGameState(aBoard);
        }
        // Given a game-specific board object, construct and return a new
        // game-specific BoardState message.
        public GameMessage.BoardState GetNewBoardStateGameMessage(GameBoard aBoard)
        {
            return new SkeletonMessage.BoardState((SkeletonBoard)aBoard);
        }

        public GameMessage.BoardState GetNewBoardStateGameMessage(string aBoardMsgString)
        {
            return new SkeletonMessage.BoardState(aBoardMsgString);
        }
    }
}