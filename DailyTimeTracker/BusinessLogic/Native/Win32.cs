using System;
using System.Runtime.InteropServices;

namespace DailyTimeTracker.Native {
    internal struct LASTINPUTINFO {
        public uint cbSize;
        public uint dwTime;
    }
    public class Win32 {

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static int GetIdleTime() {
            return TimeSpan.FromMilliseconds(((uint)Environment.TickCount - GetLastInputTime())).Seconds;
        }

        public static long GetLastInputTime() {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)Marshal.SizeOf(lastInPut);
            if (!GetLastInputInfo(ref lastInPut)) {
                throw new Exception(GetLastError().ToString());
            }
            return lastInPut.dwTime;
        }
    }
}
