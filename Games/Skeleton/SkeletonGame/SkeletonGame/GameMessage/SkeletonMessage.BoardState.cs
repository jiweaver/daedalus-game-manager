/* $Id$
 *
 * Description: Signals to the player the initial state of the game board. For a
 * particular game, how the contents of such a message are constructed or
 * interpreted is entirely up to you, the game implementor.  The protocol
 * requires only that it begin with "BoardState{" and ends with "}".
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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using DaedalusGameProtocol;

namespace SkeletonGame
{
    public abstract partial class SkeletonMessage : GameMessage
    {
        public new class BoardState : GameMessage.BoardState
        {
            private SkeletonBoard board;
            public override GameBoard Board
            {
                get
                {
                    return (SkeletonBoard)board;
                }
            }

            // Constructor to instantiate a new BoardState based on the
            // specified SkeletonBoard object.
            public BoardState(SkeletonBoard aBoard)
            {
                this.data = GetStringFromBoardState(aBoard);

                this.board = (SkeletonBoard)aBoard.Copy();
            }

            // Constructor parses a message string into a BoardState object.
            // Throws an exception if the message string does not represent a
            // valid BoardState message.
            public BoardState(string msg)
            {
                if (!GameMessage.IsBoardState(msg))
                    throw new Exception("This is not a BoardState message.");

                // Remove all whitespace from the message string.
                string msgString = RemoveAllWhiteSpace(msg);

                this.data = msgString;

                // Strip packet wrapper.
                string boardString = GameMessage.GetMessageData(msg);

                boardString = boardString.Substring(1, boardString.Length - 2);

                this.board = GetBoardStateFromString(msgString);
            }

            // Parse a message string into a SkeletonBoard object.
            private SkeletonBoard GetBoardStateFromString(string msg)
            {
                SkeletonBoard board = new SkeletonBoard();

                // Strip packet wrapper.
                string boardString = SkeletonMessage.GetMessageData(msg);

                boardString = boardString.Substring(1, boardString.Length - 2);

                // Parse the string
                throw new Exception("TODO");
            }

            // Returns a string representation of the board state in the format
            // of the BoardState message, defined in the Protocol BNF.
            private string GetStringFromBoardState(GameBoard aBoard)
            {
                string stateString = "";

                // TODO - Create the stateString.

                string result = typeof(GameMessage.BoardState).Name.ToString() + "{" + stateString + "}";

                throw new Exception("TODO");
            }
        }
    }
}