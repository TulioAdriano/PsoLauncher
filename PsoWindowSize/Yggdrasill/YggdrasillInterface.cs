/*
    This file is part of Yggdrasill
    Copyright (C) 2013 Lawrence Sebald

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License version 3 as
    published by  the Free Software Foundation.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;

namespace Yggdrasill
{
    public class YggdrasillInterface : MarshalByRefObject
    {
        public void IsInstalled(Int32 InClientPID)
        {
            Console.WriteLine("Yggdrasill has been installed in target {0}.\r\n", InClientPID);
        }

        public void ReportException(Exception InInfo)
        {
            Console.WriteLine("The target process has reported an error:\r\n" + InInfo.ToString());
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public void WriteLine(string s, object o1)
        {
            Console.WriteLine(s, o1);
        }

        public void WriteLine(string s, params object[] args)
        {
            Console.Write(s);
            foreach (object o in args)
            {
                Console.Write(o);
                Console.Write(" ");
            }
            Console.WriteLine();
        }

        public void Ping()
        {
        }
    }
}
