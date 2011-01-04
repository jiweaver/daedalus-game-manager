/* $Id$
 * 
 * Description: An implementation of this interface draws the current state of a
 * gameboard into the picture box.
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
using System.Windows.Forms;
using System.Drawing;

namespace DaedalusGameProtocol
{
    public interface IGamePainter
    {
        void Draw(Graphics g, GameState state);
    }
}