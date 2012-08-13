using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PrintMouse
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }

        static bool stopFlag = false;
        static void PushStopKey(object sender, KeyboardHookedEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                stopFlag = true;
                SendInputCurrentState(1);
                InputMouseLeftUp();
                SendInputCurrentState(1);
            }
        }
        
        public static void Captcha(Bitmap bitmap, int rest_time)
        {
            stopFlag = false;
            byte[,] pxdata = new byte[bitmap.Width, bitmap.Height];
            for (int i = 0; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width; ++j)
                {
                    Color c = bitmap.GetPixel(j, i);
                    pxdata[j, i] = (byte)((c.R + c.G + c.B) / 3);
                }
            }
            KeyboardHook keyboard_hook = new KeyboardHook(PushStopKey);
            StartPrint(pxdata, bitmap.Width, bitmap.Height, rest_time);
            keyboard_hook.Dispose();
        }

        static void StartPrint(byte[,] pxdata, int width, int height, int rest_time)
        {
            int start_x = Cursor.Position.X;
            int start_y = Cursor.Position.Y;
            bool leftdown = false;
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (stopFlag) { return; }
                    int target_x = ConvertAbsoluteMouseCoord(start_x + j, Screen.PrimaryScreen.Bounds.Width);
                    int target_y = ConvertAbsoluteMouseCoord(start_y + i, Screen.PrimaryScreen.Bounds.Height);

                    if (leftdown)
                    {
                        if (j + 1 == width || (j + 1 < width && pxdata[j + 1, i] != 0))
                        {
                            leftdown = false;
                            InputMove(target_x, target_y);
                            InputMouseLeftUp();
                            SendInputCurrentState(rest_time);
                        }
                    }
                    else
                    {
                        if (pxdata[j, i] == 0)
                        {
                            if (j + 1 == width)
                            {
                                InputMove(target_x, target_y);
                                InputMouseLeftDown();
                                InputMouseLeftUp();
                                SendInputCurrentState(rest_time);
                            }
                            else
                            {
                                leftdown = true;
                                InputMove(target_x, target_y);
                                InputMouseLeftDown();
                                SendInputCurrentState(rest_time);
                            }
                        }
                    }
                }
            }
        }

        static INPUT[] input = new INPUT[3];
        static int current_input_element_index = 0;

        static void InputMove(int x, int y)
        {
            input[current_input_element_index].mi.dwFlags = MOUSEEVENTF_MOVED | MOUSEEVENTF_ABSOLUTE;
            input[current_input_element_index].mi.dx = x;
            input[current_input_element_index].mi.dy = y;
            ++current_input_element_index;
        }

        static void InputMouseLeftDown()
        {
            input[current_input_element_index].mi.dwFlags = MOUSEEVENTF_LEFTDOWN;
            ++current_input_element_index;
        }

        static void InputMouseLeftUp()
        {
            input[current_input_element_index].mi.dwFlags = MOUSEEVENTF_LEFTUP;
            ++current_input_element_index;
        }

        static void SendInputCurrentState(int rest_time)
        {
            SendInput((uint)current_input_element_index, input, Marshal.SizeOf(input[0]));
            System.Threading.Thread.Sleep(rest_time);
            current_input_element_index = 0;
        }

        static int ConvertAbsoluteMouseCoord(int p, int l)
        {
            return (int)(((double)p / (double)l) * (double)screen_length);
        }

        [DllImport("user32.dll")]
        extern static uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]  // アンマネージ DLL 対応用 struct 記述宣言
        struct INPUT
        {
            public int type;  // 0 = INPUT_MOUSE(デフォルト), 1 = INPUT_KEYBOARD
            public MOUSEINPUT mi;
            // Note: struct の場合、デフォルト(パラメータなしの)コンストラクタは、
            //       言語側で定義済みで、フィールドを 0 に初期化する。
        }

        [StructLayout(LayoutKind.Sequential)]  // アンマネージ DLL 対応用 struct 記述宣言
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;  // amount of wheel movement
            public int dwFlags;
            public int time;  // time stamp for the event
            public IntPtr dwExtraInfo;
            // Note: struct の場合、デフォルト(パラメータなしの)コンストラクタは、
            //       言語側で定義済みで、フィールドを 0 に初期化する。
        }

        // dwFlags
        const int MOUSEEVENTF_MOVED = 0x0001;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;  // 左ボタン Down
        const int MOUSEEVENTF_LEFTUP = 0x0004;  // 左ボタン Up
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;  // 右ボタン Down
        const int MOUSEEVENTF_RIGHTUP = 0x0010;  // 右ボタン Up
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;  // 中ボタン Down
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;  // 中ボタン Up
        const int MOUSEEVENTF_WHEEL = 0x0080;
        const int MOUSEEVENTF_XDOWN = 0x0100;
        const int MOUSEEVENTF_XUP = 0x0200;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        const int screen_length = 0x10000;  // for MOUSEEVENTF_ABSOLUTE (この値は固定)
    }
}
