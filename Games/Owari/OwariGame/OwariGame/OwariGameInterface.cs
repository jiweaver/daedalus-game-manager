/* $Id: OwariGameInterface.cs 819 2011-01-03 01:46:43Z crosis $
 * 
 * Description: This is how the game plugs in to the manager.
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

namespace OwariGame
{
    public class OwariGameInterface : IGame, IGameConfig
    {
        // Return a string that uniquely identifies this game.
        public string GetGameName()
        {
            return "Owari";
        }

        // Construct and return a new instance of the game-specific GamePainter.
        public IGamePainter GetNewGamePainter(System.Windows.Forms.PictureBox aPictureBox)
        {
            return new OwariPainter(aPictureBox);
        }

        // Construct and return a new instance of the game-specific IGameLogic.
        public IGameLogic GetNewGameLogic(GameState aState)
        {
            return new OwariLogic(aState);
        }

        // Construct and return a new instance of the game-specific GameState.
        public GameState GetNewGameState()
        {
            return new OwariGameState(new OwariBoard());
        }

        public GameState GetNewGameState(GameBoard aBoard)
        {
            return new OwariGameState(aBoard);
        }

        // Given a game-specific board object, construct and return a new
        // game-specific BoardState message.
        public GameMessage.BoardState GetNewBoardStateGameMessage(GameBoard aBoard)
        {
            return new OwariMessage.BoardState((OwariBoard)aBoard);
        }

        public GameMessage.BoardState GetNewBoardStateGameMessage(string aBoardMsgString)
        {
            return new OwariMessage.BoardState(aBoardMsgString);
        }

        /***************/
        /* IGameConfig */
        /***************/

        // Return the Owari property grid.
        public PropertyGrid GetPropertyGrid()
        {
            PropertyGrid p = new PropertyGrid();
            p.SelectedObject = new OwariProperties();
            return p;
        }

        public string GetConfigString()
        {
            return OwariConfiguration.GetConfig();
        }
    }
}