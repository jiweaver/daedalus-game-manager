/* $Id: OwariBoard.cs 820 2011-01-03 03:05:53Z crosis $
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

namespace OwariGame
{
    public class OwariBoard : GameBoard
    {
        // Data structure to represent the contents of each position on the game
        // board.
        private int[] buckets;

        public int[] Buckets
        {
            get
            {
                return buckets;
            }
            set
            {
                buckets = value;
            }
        }

        public OwariBoard()
        {
            int n = OwariSettings.StartingSeedsPerTray;
            this.buckets = new int[] { n, n, n, n, n, n, 0, n, n, n, n, n, n, 0 };
        }

        // Return a full copy.
        public override GameBoard Copy()
        {
            OwariBoard b = new OwariBoard();

            for (int i = 0; i < this.buckets.Length; i++)
            {
                b.buckets[i] = this.buckets[i];
            }
            return b;
        }
    }
}