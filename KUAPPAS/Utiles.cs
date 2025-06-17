using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KUAPPAS
{
    public struct RECT
    {
        public int bottom;
        public int left;
        public int right;
        public int top;

        public RECT(int l, int t, int r, int b)
        {
            left = l;
            top = t;
            right = r;
            bottom = b;
        }

        public RECT(Rectangle r)
        {
            left = r.Left;
            top = r.Top;
            right = r.Right;
            bottom = r.Bottom;
        }
    }

    public class Win32Util
    {
        public static void ExtTextOut(IntPtr hdc, int x, int y, Rectangle clip, string str)
        {
            RECT rect1;
            rect1.top = clip.Top;
            rect1.left = clip.Left;
            rect1.bottom = clip.Bottom;
            rect1.right = clip.Right;
            Win32API.ExtTextOut(hdc, x, y, 4, ref rect1, str, str.Length, IntPtr.Zero);
        }

        public static void FillRect(IntPtr hdc, Rectangle clip, Color color)
        {
            RECT rect1;
            rect1.top = clip.Top;
            rect1.left = clip.Left;
            rect1.bottom = clip.Bottom;
            rect1.right = clip.Right;
            int num1 = (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R;
            IntPtr ptr1 = Win32API.CreateSolidBrush(num1);
            Win32API.FillRect(hdc, ref rect1, ptr1);
        }

        public static int GET_X_LPARAM(int lParam)
        {
            return (lParam & 0xffff);
        }

        public static int GET_Y_LPARAM(int lParam)
        {
            return (lParam >> 0x10);
        }

        public static Point GetPointFromLPARAM(int lParam)
        {
            return new Point(GET_X_LPARAM(lParam), GET_Y_LPARAM(lParam));
        }

        public static Size GetTextExtent(IntPtr hdc, string str)
        {
            SIZE size1;
            size1.cx = 0;
            size1.cy = 0;
            Win32API.GetTextExtentPoint32(hdc, str, str.Length, ref size1);
            return new Size(size1.cx, size1.cy);
        }

        public static void SelectObject(IntPtr hdc, IntPtr handle)
        {
            Win32API.SelectObject(hdc, handle);
        }

        public static void SetBkColor(IntPtr hdc, Color color)
        {
            int num1 = (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R;
            Win32API.SetBkColor(hdc, num1);
        }

        public static void SetBkMode(IntPtr hdc, int mode)
        {
            Win32API.SetBkMode(hdc, mode);
        }

        public static void SetTextColor(IntPtr hdc, Color color)
        {
            int num1 = (((color.B & 0xff) << 0x10) | ((color.G & 0xff) << 8)) | color.R;
            Win32API.SetTextColor(hdc, num1);
        }

        public static void SendMessage(object control, ref int msg, ref int wparam, ref int lparam)
        {
            Win32API.SendMessage(new HandleRef(control, ((Control) control).Handle), msg, wparam, lparam);
        }

        public static int CreateCaret(IntPtr hwnd, IntPtr hbm, int cx, int cy)
        {
            return Win32API.CreateCaret(hwnd, hbm, cx, cy);
        }

        public static int DestroyCaret()
        {
            return Win32API.DestroyCaret();
        }

        public static int HideCaret(IntPtr hwnd)
        {
            return Win32API.HideCaret(hwnd);
        }

        public static int ShowCaret(IntPtr hwnd)
        {
            return Win32API.ShowCaret(hwnd);
        }

        public static int SetCaretPos(int x, int y)
        {
            return Win32API.SetCaretPos(x, y);
        }

        #region Nested type: RECT

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion

        #region Nested type: SIZE

        [StructLayout(LayoutKind.Sequential)]
        private struct SIZE
        {
            public int cx;
            public int cy;
        }

        #endregion

        #region Nested type: Win32API

        private class Win32API
        {
            // Methods
            [DllImport("gdi32")]
            internal static extern IntPtr CreateSolidBrush(int crColor);

            [DllImport("gdi32.dll")]
            public static extern int ExtTextOut(IntPtr hdc, int x, int y, int options, ref RECT clip, string str,
                                                int len, IntPtr spacings);

            [DllImport("User32.dll", CharSet = CharSet.Auto)]
            internal static extern int FillRect(IntPtr hDC, ref RECT rect, IntPtr hBrush);

            [DllImport("gdi32.dll")]
            public static extern int GetTextExtentPoint32(IntPtr hdc, string str, int len, ref SIZE size);

            [DllImport("gdi32.dll")]
            public static extern int SelectObject(IntPtr hdc, IntPtr hgdiObj);

            [DllImport("gdi32.dll")]
            public static extern int SetBkColor(IntPtr hdc, int color);

            [DllImport("gdi32.dll")]
            public static extern int SetBkMode(IntPtr hdc, int mode);

            [DllImport("gdi32.dll")]
            public static extern int SetTextColor(IntPtr hdc, int color);

            [DllImport("gdi32.dll")]
            public static extern int SetTextBkColor(IntPtr hdc, int color);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, int lParam);

            [DllImport("user32.dll")]
            public static extern int CreateCaret(IntPtr hwnd, IntPtr hbm, int cx, int cy);

            [DllImport("user32.dll")]
            public static extern int DestroyCaret();

            [DllImport("user32.dll")]
            public static extern int HideCaret(IntPtr hwnd);

            [DllImport("user32.dll")]
            public static extern int ShowCaret(IntPtr hwnd);

            [DllImport("user32.dll")]
            public static extern int SetCaretPos(int x, int y);
        }

        #endregion
    }

    public class XPTheme
    {
        // Methods
        [DllImport("uxtheme.dll")]
        public static extern void CloseThemeData(IntPtr ht);

        [DllImport("uxtheme.dll")]
        public static extern void DrawThemeBackground(IntPtr ht, IntPtr hd, int iPartId, int iStateId, ref RECT rect,
                                                      ref RECT cliprect);

        [DllImport("uxtheme.dll")]
        public static extern int IsThemeActive();

        [DllImport("uxtheme.dll")]
        public static extern IntPtr OpenThemeData(IntPtr h, [MarshalAs(UnmanagedType.LPTStr)] string pszClassList);
    }
}