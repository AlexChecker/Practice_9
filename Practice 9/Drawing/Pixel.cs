using System;

namespace Practice_9.Drawing
{
    public class Pixel
    {
        public char c = ' ';
        public ConsoleColor? fColor = null;
        public ConsoleColor? bColor = null;


        public FastDraw.CharInfo getInfo()
        {
            var info = new FastDraw.CharInfo();
            short fc = fColor != null ? (short) fColor : (short)ConsoleColor.White;
            short bc = (short)((bColor != null ? (short)bColor : (short)ConsoleColor.Black) << 4);
            info.Attributes = (short)(fc | bc);
            info.Char = new FastDraw.CharUnion();
            info.Char.UnicodeChar = c;
            return info;
        }

        public Pixel(char c)
        {
            this.c = c;
        }
        public Pixel(char c,ConsoleColor fcol = ConsoleColor.White,ConsoleColor bcol = ConsoleColor.Black)
        {
            this.c = c;
            fColor = fcol;
            bColor = bcol;
        }

        public void delete()
        {
            this.c = ' ';
        }
    }
}