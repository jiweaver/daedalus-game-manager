/* $Id$
 * 
 * Description: This just runs the whole test suite in sequence.
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
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;

namespace TestSuite
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine("-- TESTS STARTING --");
            Console.WriteLine("--------------------");

            string gameName = DaedalusGameManager.GameChoice.CurrentGame.GetGameName();
            if (gameName.Equals("Tzaar"))
                TzaarGameTests.Run();
            else if (gameName.Equals("Owari"))
                OwariGameTests.Run();
            else
                throw new Exception("The Test Suite does not support this game.");

            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine("-- TESTS COMPLETE --");
            Console.WriteLine("--------------------");

            // Ok, game setup is all done.
            Console.WriteLine("\nPress ESC key to exit:");
            while (true)
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
        }
    }
}