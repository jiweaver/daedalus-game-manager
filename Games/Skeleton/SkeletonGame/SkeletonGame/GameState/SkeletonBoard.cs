/* $Id$
 * 
 * Description: Holds a snapshot of a board state.
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
using DaedalusGameProtocol;

namespace SkeletonGame
{
    public class SkeletonBoard : GameBoard
    {
        // Construct a standard/default initial game board state.
        public SkeletonBoard()
        {
            throw new Exception("TODO");
        }

        // Return a full copy.
        public override GameBoard Copy()
        {
            throw new Exception("TODO");
        }
    }
}