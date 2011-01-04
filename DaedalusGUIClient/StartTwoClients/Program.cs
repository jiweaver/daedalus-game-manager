/* $Id$
 * 
 * Description: Automates the process of starting two clients for testing 
 * purposes.
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
using DaedalusGUIClient;
using System.Threading;

namespace StartTwoClients
{
    class Program
    {
        static Thread form1Thread, form2Thread;

        static void Main(string[] args)
        {
            form1Thread = new Thread(Form1ThreadWork);
            form1Thread.Start();
            while (!form1Thread.IsAlive)
                ;

            form2Thread = new Thread(Form2ThreadWork);
            form2Thread.Start();
            while (!form2Thread.IsAlive)
                ;

            Console.WriteLine("\nPress ESC key to exit:");
            while (true)
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                    break;
        }

        private static void Form1ThreadWork()
        {
            DaedalusClientForm form1 = new DaedalusClientForm();
            form1.ShowDialog();
        }

        private static void Form2ThreadWork()
        {
            DaedalusClientForm form2 = new DaedalusClientForm();
            form2.ShowDialog();
        }
    }
}