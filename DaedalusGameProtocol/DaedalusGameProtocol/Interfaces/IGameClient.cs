/* $Id$
 *
 * Description: This is the interface games used to hook in to the Daedalus GUI 
 * Client.
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
using System.Drawing;
using DaedalusGameProtocol;
using System.Windows.Forms;

namespace DaedalusGameProtocol
{
    public interface IGameClient
    {
        // Return a string that uniquely identifies this game.
        string GetGameName();

        // Construct and return a new instance of the game-specific
        // GameClientLogic.
        IGameClientLogic GetNewGameClientLogic(PictureBox aPictureBox, GameMessage.Version versionMsg, GamePlayer playerNumber, GameBoard initialBoard);

        // Given a game-specific board state message string, construct and
        // return a new game-specific BoardState message.
        GameMessage.BoardState GetNewBoardStateGameMessage(string aBoardMsgString);

        // Given a game-specific move message string, construct and return a
        // new game-specific move message.
        GameMessage.Move GetNewMoveGameMessage(string aMoveMsgString);
    }
}