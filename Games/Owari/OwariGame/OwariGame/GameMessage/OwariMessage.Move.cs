/* $Id$
 *
 * Description: When sent from a client to a server, a Move message delivers a
 * players intended move coordinates.  When sent from the server to a client,
 * the Move message delivers an opponents completed move.
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
using DaedalusGameProtocol;

namespace OwariGame
{
    public abstract partial class OwariMessage : GameMessage
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

            // Constructor instantiates a OwariMessage of type Move from the
            // specified origin and destination coordinates.
            public Move(int command)
            {
                // A Capture or Stack Move.
                this.command = command;
                this.data = string.Format("{0}{{{1}}}", typeof(OwariMessage.Move).Name.ToString(), this.command);
            }

            // Constructor instantiates a OwariMessage of type Move from the
            // specified message string. Throws an exception is the message
            // string does not contain a valid OwariMessage of type Move.
            public Move(string msg)
            {
                if (!GameMessage.IsMove(msg))
                    throw new Exception("This is not a Move message.");

                // Remove all whitespace from the message string.
                string msgString = RemoveAllWhiteSpace(msg);

                this.data = msgString;

                // Strip the packet enclosure.
                string commandString = OwariMessage.GetMessageData(msgString);

                // Parse the data.
                this.command = Int32.Parse(commandString);
            }
        }
    }
}