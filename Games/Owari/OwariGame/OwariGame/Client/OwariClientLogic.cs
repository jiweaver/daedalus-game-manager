/* $Id$
 *
 * Description: The client logic is the code necessary for the GUI client to
 * interact with the game.
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
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DaedalusGameProtocol;

namespace OwariGame
{
    public class OwariClientLogic : IGameClientLogic
    {
        private OwariPainter painter;
        private OwariLogic logic;

        private GamePlayer playerNumber;
        private PictureBox pictureBox1;
        private GameMessage.Version versionMsg;

        public OwariClientLogic(PictureBox aPictureBox, GameMessage.Version versionMsg, GamePlayer playerNumber, GameBoard initialBoard)
        {
            this.playerNumber = 0;

            this.pictureBox1 = aPictureBox;
            this.versionMsg = versionMsg;

            // Make the configuration active.
            try
            {
                OwariConfiguration.SetConfig(versionMsg.GameConfig);
            }
            catch
            {
                Console.WriteLine("[OwariClientLogic] WARNING: unable to set configuration from string '" + versionMsg.GameConfig + "'");
            }

            this.painter = new OwariPainter(this.pictureBox1);
            this.logic = new OwariLogic();

            this.playerNumber = playerNumber;

            this.logic = new OwariLogic(new OwariGameState(initialBoard));
        }

        public GameState GetGameState()
        {
            // Ensure a consistent state when reading, by handing back a copy.
            return this.logic.GetGameState().Copy();
        }

        public GameMessage.Move HandleClick(GamePlayer playerNumber, Point clickLoc)
        {
            OwariGameState state = (OwariGameState)this.logic.GetGameState();
            if (state.GameIsOver)
                throw new Exception("Game is over.");

            if (this.logic.GetGameState().GetCurrentPlayerNumber() != playerNumber)
                throw new Exception("Not our turn");

            int pos = this.painter.PictureBoxCoordsToBoardPosition(clickLoc);

            // Don't allow selection of the two scoring buckets or the other
            // player's positions.
            if (pos == 6 || pos == 13)
                return null;
            else if (playerNumber == GamePlayer.One && pos > 5)
                return null;
            else if (playerNumber == GamePlayer.Two && pos < 7)
                return null;

            if (((OwariGameState)this.logic.GetGameState()).Board.Buckets[pos] == 0)
                // Ignore clicks on empty pits.
                return null;

            return new OwariMessage.Move(pos);
        }

        public void HandleMove(GamePlayer playerNumber, GameMessage.Move moveMsg)
        {
            this.logic.MakeMove(playerNumber, moveMsg);

            this.logic.GetGameState().AddMove(moveMsg);
        }

        public void Paint(Graphics g, GameState gameState)
        {
            this.painter.Draw(g, gameState);
        }

        public void EndGame(GamePlayer winner, GameOverCondition condition)
        {
            this.logic.GetGameState().SetGameOver(winner, condition);
        }
    }
}