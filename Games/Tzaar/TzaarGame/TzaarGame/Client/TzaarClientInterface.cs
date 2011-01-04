/* $Id$
 *
 * Description: Implements the IGameClient interface to play a game of Tzaar.
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

namespace TzaarGame
{
    public class TzaarClientInterface : IGameClient
    {
        public GameMessage.Move GetNewMoveGameMessage(string moveMsg)
        {
            return new TzaarMessage.Move(moveMsg);
        }

        public GameMessage.BoardState GetNewBoardStateGameMessage(string boardMsg)
        {
            return new TzaarMessage.BoardState(boardMsg);
        }

        public string GetGameName()
        {
            return "Tzaar";
        }

        public IGameClientLogic GetNewGameClientLogic(PictureBox aPictureBox, GameMessage.Version versionMsg, GamePlayer playerNumber, GameBoard initialBoard)
        {
            return new TzaarClientLogic(aPictureBox, versionMsg, playerNumber, initialBoard);
        }
    }
}