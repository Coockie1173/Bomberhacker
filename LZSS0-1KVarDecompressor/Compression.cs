using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LZSS0_1KVarDecompressor
{
    public class Compression
    {
        /* Moving all compression and decompression algorithms here
         * 
         * 
         */

        public static int N = 1024;
        public static int F = 16;


        public static byte[] CompressInflate(byte[] Data)
        {
            //level 0 compression, new compressor has been made but has not been implemented yet
            int CodeWordCount = (int)Math.Ceiling((float)(Data.Length / 8));
            int CompSize = (int)(Data.Length + CodeWordCount);
            byte[] CompBuf = new byte[CompSize + 1];
            byte[] dst = CompBuf;

            int DestPlace = 0;
            int SrcPlace = 0;

            while (DestPlace < CompSize)
            {
                if (DestPlace == 0 || DestPlace % 9 == 0)
                {
                    dst[DestPlace] = 0xFF;
                }
                else
                {
                    dst[DestPlace] = Data[SrcPlace];
                    SrcPlace++;
                }
                DestPlace++;
            }

            return dst;
        }

        public static byte[] Decompress(byte[] data, UInt32 ComSize, UInt32 OutSize, UInt32 Pos)
        {
            byte[] OutPut = new byte[OutSize];
            byte[] fbuffer = new byte[N + F - 1];

            UInt32 flag;
            UInt32 buf_1;
            UInt32 inpos;
            UInt32 outpos;

            UInt64 val;
            UInt64 size;
            UInt64 back;

            buf_1 = 0;
            flag = 0;
            inpos = Pos;
            val = 0;
            size = 0;
            outpos = 0;
            back = 0;


            for (UInt32 i = 0; i < 0x400; i++) //1KB buffer
            {
                fbuffer[i] = 0x00;
            }
            buf_1 = 0x3BE;

            while (inpos <= ComSize - 1)
            {
                flag >>= 1;
                if (flag < 2)
                {
                    flag = (Convert.ToUInt32(data[inpos++]) & 0xFF) | 0x100;
                    if (flag < 0)
                    {
                        break;
                    }
                }
                if ((flag & 1) != 0)
                {
                    val = Convert.ToUInt64(data[inpos++]) & 0xFF;
                    if (val < 0)
                    {
                        break;
                    }
                    OutPut[outpos++] = Convert.ToByte(val);
                    fbuffer[buf_1] = Convert.ToByte(val);
                    buf_1 += 1;
                    buf_1 &= 0x3FF;
                }
                else if (inpos <= ComSize - 1)
                {
                    back = data[inpos++];
                    size = data[inpos++];

                    back |= ((size << 2) & 0x300);
                    size &= 0x3F;

                    for (UInt64 i = 0; i < size + 0x3; i++)
                    {
                        val = fbuffer[back];
                        OutPut[outpos++] = Convert.ToByte(val);
                        fbuffer[buf_1] = Convert.ToByte(val);

                        back += 1;
                        back &= 0x3FF;

                        buf_1 += 1;
                        buf_1 &= 0x3FF;
                    }
                }
            }
            return OutPut;
        }
    }
}
