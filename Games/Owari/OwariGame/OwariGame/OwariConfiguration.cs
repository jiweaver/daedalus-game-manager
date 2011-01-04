/* $Id: OwariConfiguration.cs 819 2011-01-03 01:46:43Z crosis $
 * 
 * Description: Allows the Game Manager to get and set configuration options for
 * Owari (starting number of seeds in each pit, etc).
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
using System.ComponentModel;

namespace OwariGame
{
    public abstract class OwariConfiguration
    {
        public static string GetConfig()
        {
            return OwariSettings.MaxSeedsForCapture + ":" + OwariSettings.MinSeedsForCapture + ":" + OwariSettings.StartingSeedsPerTray;
        }

        public static void SetConfig(string msg)
        {
            string[] S = msg.Split(':');

            if (S.Length != 3)
                throw new Exception("I wasn't able to parse the string into the correct number of parameters.");

            OwariSettings.MaxSeedsForCapture = Int32.Parse(S[0]);
            OwariSettings.MinSeedsForCapture = Int32.Parse(S[1]);
            OwariSettings.StartingSeedsPerTray = Int32.Parse(S[2]);
        }
    }

    [DefaultPropertyAttribute("Name")]
    class OwariProperties
    {
        [CategoryAttribute("Game Configuration"), DescriptionAttribute("Initial Number of Seeds Per Tray")]
        public int StartingSeedsPerTray
        {
            get
            {
                return OwariSettings.StartingSeedsPerTray;
            }
            set
            {
                OwariSettings.StartingSeedsPerTray = value;
            }
        }

        [CategoryAttribute("Game Configuration"), DescriptionAttribute("After a move, this is the minimum number of seeds required to be in a tray that is captured.")]
        public int MinSeedsForCapture
        {
            get
            {
                return OwariSettings.MinSeedsForCapture;
            }
            set
            {
                OwariSettings.MinSeedsForCapture = value;
            }
        }

        [CategoryAttribute("Game Configuration"), DescriptionAttribute("After a move, this is the maximum number of seeds allowed to be in a tray that is captured.")]
        public int MaxSeedsForCapture
        {
            get
            {
                return OwariSettings.MaxSeedsForCapture;
            }
            set
            {
                OwariSettings.MaxSeedsForCapture = value;
            }
        }
    }

    static class OwariSettings
    {
        private static int startingSeedsPerTray = 3;

        private static int minSeedsForCapture = 1;
        private static int maxSeedsForCapture = 1;

        public static int MinSeedsForCapture
        {
            get
            {
                return OwariSettings.minSeedsForCapture;
            }
            set
            {
                OwariSettings.minSeedsForCapture = value;
            }
        }

        public static int MaxSeedsForCapture
        {
            get
            {
                return OwariSettings.maxSeedsForCapture;
            }
            set
            {
                OwariSettings.maxSeedsForCapture = value;
            }
        }

        public static int StartingSeedsPerTray
        {
            get
            {
                return startingSeedsPerTray;
            }
            set
            {
                startingSeedsPerTray = value;
            }
        }
    }
}