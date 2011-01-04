/* $Id: DaedalusGameManagerConfiguration.cs 819 2011-01-03 01:46:43Z crosis $
 *
 * Description: Daedalus Game Manager configuration and default settings.
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

namespace DaedalusGameManager
{
    public static class DaedalusConfig
    {
        // The port number that the server will listen on that next time it is
        // started.
        private static int portNumber = 2525;

        public static int PortNumber
        {
            get
            {
                return portNumber;
            }
            set
            {
                portNumber = value;
            }
        }
    }

    // The property grid front-end.
    [DefaultPropertyAttribute("Name")]
    public class DaedalusGameManagerProperties
    {
        [CategoryAttribute("Daedalus Game Manager Configuration"), DescriptionAttribute("Port number used to listen for client connections.")]
        public int PortNumber
        {
            get
            {
                return DaedalusConfig.PortNumber;
            }
            set
            {
                DaedalusConfig.PortNumber = value;
            }
        }
    }
}