/* $Id: OwariClientInterface.cs 820 2011-01-03 03:05:53Z crosis $
 *
 * Description: Implements the IGameClient interface to play a game of Owari.
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

namespace OwariGame
{
    public class OwariClientInterface : IGameClient
    {
        public GameMessage.Move GetNewMoveGameMessage(string aMoveMsgString)
        {
            return new OwariMessage.Move(aMoveMsgString);
        }

        public GameMessage.BoardState GetNewBoardStateGameMessage(string aBoardMsgString)
        {
            return new OwariMessage.BoardState(aBoardMsgString);
        }

        public string GetGameName()
        {
            return "Owari";
        }

        public IGameClientLogic GetNewGameClientLogic(PictureBox aPictureBox, GameMessage.Version versionMsg, GamePlayer playerNumber, GameBoard initialBoard)
        {
            return new OwariClientLogic(aPictureBox, versionMsg, playerNumber, initialBoard);
        }
    }
}