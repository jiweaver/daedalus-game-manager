/* $Id: IGameControl.cs 819 2011-01-03 01:46:43Z crosis $
 *
 * Description: The Control interface is optionally implemented in conjunction 
 * with IGameLogic, to provide support for the Control GameMessage.   It allows 
 * Control messages to be handled directly by the game logic, bypassing the 
 * server.  The logic uses the callback delegate to send data to either client 
 * in response to a Control message.
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

namespace DaedalusGameProtocol
{
    // The callback delegate that the game logic uses to respond to Control{}
    // messages.
    public delegate void ControlCallbackDelegate(GamePlayer playerNumber, string msg);

    public interface IGameControl
    {
        // Called by the game server when a Control{} message is received from
        // a client.
        void Control(GamePlayer playerNumber, string controlMsgString, ControlCallbackDelegate aCallBack);
    }
}