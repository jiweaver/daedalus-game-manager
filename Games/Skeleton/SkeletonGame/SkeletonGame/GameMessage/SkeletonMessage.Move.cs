/* $Id$
 *
 * Description: This is where you implement the Move command.  How the contents
 * of a move command are constructed and interpretted for a particular game is
 * up to you, that games implementor. All the protocol requires is that the
 * command begin with "Move{" and end with "}".
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
using DaedalusGameProtocol;

namespace SkeletonGame
{
    public abstract partial class SkeletonMessage : GameMessage
    {
        public new class Move : GameMessage.Move
        {
            private int command;

            public int Command
            {
                get
                {
                    return command;
                }
            }

            // Constructor instantiates a SkeletonMessage of type Move
            public Move(int command)
            {
                this.command = command;
                this.data = string.Format("{0}{{{1}}}", typeof(SkeletonMessage.Move).Name.ToString(), this.command);
            }

            // Constructor instantiates a SkeletonMessage of type Move from the
            // specified message string. Throws an exception is the message
            // string does not contain a valid SkeletonMessage of type Move.
            public Move(string msg)
            {
                if (!GameMessage.IsMove(msg))
                    throw new Exception("This is not a Move message.");

                // Remove all whitespace from the message string.
                string msgString = RemoveAllWhiteSpace(msg);

                // Strip the packet enclosure.
                string commandString = SkeletonMessage.GetMessageData(msgString);

                // Parse the data.
                this.command = Int32.Parse(commandString);

                this.data = msgString;
            }
        }
    }
}