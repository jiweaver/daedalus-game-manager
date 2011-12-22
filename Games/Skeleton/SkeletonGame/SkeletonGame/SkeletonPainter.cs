/* $Id$
 *
 * Description: The SkeletonPainter class takes a PictureBox as a parameter to
 * its constructor and latches onto it; then it draws the display within that
 * box whenever a game state is passed to the Draw method.
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using DaedalusGameProtocol;
using SkeletonGame;

namespace SkeletonGame
{
    public class SkeletonPainter : IGamePainter
    {
        protected PictureBox pictureBox1;

        public SkeletonPainter(PictureBox aPictureBox)
        {
            this.pictureBox1 = aPictureBox;
        }

        public void Draw(Graphics g, GameState gState)
        {
            SkeletonGameState state = (SkeletonGameState)gState;

            // Draw the state.
            throw new Exception("TODO");
        }
    }
}