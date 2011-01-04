/* $Id$
 * 
 * Description: Tests for the TzaarProtocol message classes.
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
using TzaarGame;

namespace TestSuite
{
    abstract partial class TzaarGameTests
    {
        public abstract class Messages
        {
            public static void Run()
            {
                Console.WriteLine("Testing TzaarMessage...");
                TestBoardStateMessage();
                TestYourTurnMessage();
                TestYourColorMessage();
                TestGameOverMessage();
                TestMoveMessage();
            }

            // Test the protocol components related to the 'board state' message
            // type. Throws Exception if a 'board state' message is generated 
            // which is inconsistent with the physical board state, or if a 
            // board state defined by a 'board state' message is physically 
            // inconsistent with the desired state.
            private static void TestBoardStateMessage()
            {
                // Create a new empty game board.
                TzaarBoard board = new TzaarBoard(true);

                // Create some game pieces of various types.
                TzaarPiece p1 = new TzaarPiece.Tzaar(TzaarColor.BLACK);
                TzaarPiece p2 = new TzaarPiece.Tzarra(TzaarColor.BLACK);
                TzaarPiece p3 = new TzaarPiece.Tott(TzaarColor.BLACK);

                // Add the pieces we created to the board at the target 
                // position.
                board.Add(p1, 0, 0);
                board.Add(p2, 1, 0);
                board.Add(p3, 2, 0);

                TzaarMessage.BoardState message = new TzaarMessage.BoardState(board);

                // Check that the generated 'board state' message matches the 
                // physical state of the board defined above.
                if (((string)message).CompareTo("BoardState{{BLACK,Tzaar},{},{},{},{},{BLACK,Tzarra},{},{},{},{},{},{BLACK,Tott},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{}}") != 0)
                    throw new Exception();

                message = new TzaarMessage.BoardState("BoardState{{BLACK,Tzaar},{},{},{},{},{BLACK,Tzarra},{},{},{},{},{},{BLACK,Tott},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{},{}}");

                // Check that a board state generated from a 'board state' 
                // message is consistent with the desired state.
                Stack<TzaarPiece> S = ((TzaarBoard)message.Board).Query(0, 0);
                if (S == null || S.Count == 0 || S.Peek().GetType() != typeof(TzaarPiece.Tzaar))
                    throw new Exception();
            }

            // Test the protocol components related to the 'your turn' message
            // type. Throws Exception if a 'your turn' message is generated
            // which is inconsistent with the specified game turn.
            private static void TestYourTurnMessage()
            {
                TzaarMessage.YourTurn message = new TzaarMessage.YourTurn();

                // Check that the generated message matches the specified turn.
                if (((string)message).CompareTo("YourTurn{}") != 0)
                    throw new Exception();
            }

            // Test the protocol components related to the 'your color' message 
            // type. Throws Exception if a 'your color' message is generated 
            // which is inconsistent with the specified color.
            private static void TestYourColorMessage()
            {
                TzaarMessage.YourPlayerNumber message = new TzaarMessage.YourPlayerNumber(PlayerColor.GetPlayerFromColor(TzaarColor.BLACK));

                // Check that the generated message matches the specified PIECE
                // color.
                if (PlayerColor.GetColorFromPlayer(message.PlayerNumber) != TzaarColor.BLACK)
                    throw new Exception();

                // Check that the generated string matches the format of the 
                // 'your color' message defined in the protocol for the
                // specified color.
                if (((string)message).CompareTo("YourPlayerNumber{Two}") != 0)
                    throw new Exception();

                // Check the functionality for the other color.
                message = new TzaarMessage.YourPlayerNumber("YourPlayerNumber{One}");

                if (PlayerColor.GetColorFromPlayer(message.PlayerNumber) != TzaarColor.WHITE)
                    throw new Exception();
            }

            // Test the protocol components related to the 'game over' message
            // type. Throws Exception if a 'game over' message is generated 
            // which is inconsistent with the specified game over condition.
            private static void TestGameOverMessage()
            {
                TzaarMessage.GameOver message = new TzaarMessage.GameOver(GameOverCondition.YouWin);

                // Check that the generated message matches the specified game
                // over condition.
                if (message.Condition != GameOverCondition.YouWin)
                    throw new Exception();

                // Check that the generated string matches the format of the
                // 'game over' message defined in the protocol for the specified
                // game over condition.
                if (((string)message).CompareTo("GameOver{YouWin}") != 0)
                    throw new Exception();

                message = new TzaarMessage.GameOver("GameOver{YouLose}");

                // Check the functionality of the other game over condition.
                if (message.Condition != GameOverCondition.YouLose)
                    throw new Exception();
            }

            // Test the protocol components related to the 'move' message type.
            // Throws Exception if a 'move' message is generated which is
            // inconsistent with the specified move.
            private static void TestMoveMessage()
            {
                TzaarMessage.Move message = new TzaarMessage.Move(1, 2, 3, 4);

                // Check that the generated messages match the specified move.
                if (message.FromCol != 1)
                    throw new Exception();

                if (message.FromRow != 2)
                    throw new Exception();

                if (message.ToCol != 3)
                    throw new Exception();

                if (message.ToRow != 4)
                    throw new Exception();

                // Check that the generated string matches the format of the
                // 'move' message defined in the protocol for the specified
                // move.
                if (((string)message).CompareTo("Move{1,2,3,4}") != 0)
                    throw new Exception();

                message = new TzaarMessage.Move("Move{5,6,7,8}");

                // Check the functionality of another arbitrary move.
                if (message.FromCol != 5)
                    throw new Exception();

                if (message.FromRow != 6)
                    throw new Exception();

                if (message.ToCol != 7)
                    throw new Exception();

                if (message.ToRow != 8)
                    throw new Exception();
            }
        }
    }
}