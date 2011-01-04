/* $Id: OwariMessage.BoardState.cs 820 2011-01-03 03:05:53Z crosis $
 *
 * Description: Signals to the player the initial state of the game board.
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
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using DaedalusGameProtocol;

namespace OwariGame
{
    public abstract partial class OwariMessage : GameMessage
    {
        public new class BoardState : GameMessage.BoardState
        {
            private OwariBoard board;
            public override GameBoard Board
            {
                get
                {
                    return (OwariBoard)board;
                }
            }

            // Constructor to instantiate a new BoardState based on the 
            // specified OwariBoard object.
            public BoardState(OwariBoard aBoard)
            {
                this.data = GetStringFromBoardState(aBoard);

                this.board = (OwariBoard)aBoard.Copy();
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

            // Parse a message string into a OwariBoard object.
            private OwariBoard GetBoardStateFromString(string msg)
            {
                OwariBoard board = new OwariBoard();

                // Strip packet wrapper.
                string boardString = OwariMessage.GetMessageData(msg);

                // Parse the string.
                string[] pits = Regex.Split(boardString, ",");

                for (int i = 0; i < board.Buckets.Length; i++)
                    board.Buckets[i] = Int32.Parse(pits[i]);

                return board;
            }

            // Returns a string representation of the board state in the format
            // of the BoardState message, defined in the Protocol BNF.
            private string GetStringFromBoardState(GameBoard aBoard)
            {
                string stateString = "";
                OwariBoard oBoard = (OwariBoard)aBoard;
                // Create the stateString.
                for (int i = 0; i < oBoard.Buckets.Length; i++)
                    stateString += oBoard.Buckets[i] + ",";
                // Remove the last comma.
                stateString = stateString.Substring(0, stateString.Length - 1);
                return typeof(GameMessage.BoardState).Name.ToString() + "{" + stateString + "}";
            }
        }
    }
}