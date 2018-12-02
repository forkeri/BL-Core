using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL_Core.IO
{
    /// <summary>
    /// 字节数组与常用类型转换
    /// </summary>
    internal class ByteUtils
    {
        #region 其他数据类型转换为字节数组
        /// <summary>
        /// 转换：bool => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(bool value)
        {
            return new byte[] { (byte)(value ? 1 : 0) };
        }

        /// <summary>
        /// 转换：byte => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(byte value)
        {
            return new byte[] { value };
        }

        /// <summary>
        /// 转换：sbyte => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(sbyte value)
        {
            return new byte[] {
                (byte)(value & 0xff)
            };
        }

        /// <summary>
        /// 转换：char => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(char value)
        {
            return new byte[] {
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：short => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(short value)
        {
            return new byte[] {
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：ushort => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(ushort value)
        {
            return new byte[] {
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：int => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(int value)
        {
            return new byte[] {
                (byte)((value & 0xff000000) >> 24),
                (byte)((value & 0xff0000) >> 16),
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：uint => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(uint value)
        {
            return new byte[] {
                (byte)((value & 0xff000000) >> 24),
                (byte)((value & 0xff0000) >> 16),
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：float => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(float value)
        {
            byte[] array = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                return new byte[] { array[3], array[2], array[1], array[0] };
            }
            return array;
        }

        /// <summary>
        /// 转换：long => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(long value)
        {
            return new byte[] {
                (byte)(((ulong)value & 0xff00000000000000) >> 56),
                (byte)((value & 0xff000000000000) >> 48),
                (byte)((value & 0xff0000000000) >> 40),
                (byte)((value & 0xff00000000) >> 32),
                (byte)((value & 0xff000000) >> 24),
                (byte)((value & 0xff0000) >> 16),
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：ulong => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(ulong value)
        {
            return new byte[] {
                (byte)((value & 0xff00000000000000) >> 56),
                (byte)((value & 0xff000000000000) >> 48),
                (byte)((value & 0xff0000000000) >> 40),
                (byte)((value & 0xff00000000) >> 32),
                (byte)((value & 0xff000000) >> 24),
                (byte)((value & 0xff0000) >> 16),
                (byte)((value & 0xff00) >> 8),
                (byte)(value & 0x00ff)
            };
        }

        /// <summary>
        /// 转换：double => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(double value)
        {
            byte[] tempArray = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                return new byte[] { tempArray[7], tempArray[6], tempArray[5], tempArray[4], tempArray[3], tempArray[2], tempArray[1], tempArray[0] };
            }
            return tempArray;
        }

        /// <summary>
        /// 转换：string => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(string value)
        {
            byte[] strArr = Encoding.UTF8.GetBytes(value);
            if (string.IsNullOrEmpty(value))
            {
                return strArr;
            }
            byte[] lengthArr = GetBytes(strArr.Length);
            byte[] array = new byte[strArr.Length + 4];
            Array.Copy(lengthArr, array, 4);
            Array.Copy(strArr, 0, array, 4, strArr.Length);
            return array;
        }

        /// <summary>
        /// 转换：string => byte[]
        /// </summary>
        /// <param name="value">转换对象</param>
        /// <param name="encoding">字符串编码</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(string value, Encoding encoding)
        {
            byte[] strArr = encoding.GetBytes(value);
            byte[] lengthArr = GetBytes(strArr.Length);
            byte[] array = new byte[strArr.Length + 4];
            Array.Copy(lengthArr, array, 4);
            Array.Copy(strArr, 0, array, 4, strArr.Length);
            return array;
        }

        #endregion
        #region 字节数组转换为指定数据类型
        /// <summary>
        /// 转换：byte[] => byte
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static bool GetBoolean(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 1 || offset + 1 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            bool value = (array[offset] & 1) > 0;
            offset += 1;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => byte
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static byte GetUInt8(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 1 || offset + 1 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            byte value = array[offset];
            offset += 1;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => sbyte
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static sbyte GetInt8(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 1 || offset + 1 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            sbyte value = (sbyte)array[offset];
            offset += 1;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => char
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static char GetChar(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 2 || offset + 2 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            char value = (char)((array[offset] << 8) + array[offset + 1]);
            offset += 2;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => short
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static short GetInt16(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 2 || offset + 2 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            short value = (short)((array[offset] << 8) + array[offset + 1]);
            offset += 2;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => ushort
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static ushort GetUInt16(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 2 || offset + 2 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            ushort value = (ushort)((array[offset] << 8) + array[offset + 1]);
            offset += 2;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => int
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static int GetInt32(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 4 || offset + 4 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            int value = (int)((array[offset] << 24) + (array[offset + 1] << 16) + (array[offset + 2] << 8) + array[offset + 3]);
            offset += 4;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => uint
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static uint GetUInt32(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 4 || offset + 4 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            uint value = (uint)((array[offset] << 24) + (array[offset + 1] << 16) + (array[offset + 2] << 8) + array[offset + 3]);
            offset += 4;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => float
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static float GetSingle(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 4 || offset + 4 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            byte[] tempArray =
                BitConverter.IsLittleEndian ?
                new byte[] { array[offset + 3], array[offset + 2], array[offset + 1], array[offset] } :
                new byte[] { array[offset], array[offset + 1], array[offset + 2], array[offset + 3] };

            float value = BitConverter.ToSingle(tempArray, 0);
            offset += 4;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => long
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static long GetInt64(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 8 || offset + 8 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            long value = ((long)array[offset] << 56) + ((long)array[offset + 1] << 48) + ((long)array[offset + 2] << 40) + ((long)array[offset + 3] << 32) + (array[offset + 4] << 24) + (array[offset + 5] << 16) + (array[offset + 6] << 8) + array[offset + 7];
            offset += 8;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => ulong
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static ulong GetUInt64(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 8 || offset + 8 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            ulong value = ((ulong)array[offset] << 56) + ((ulong)array[offset + 1] << 48) + ((ulong)array[offset + 2] << 40) + ((ulong)array[offset + 3] << 32) + ((ulong)array[offset + 4] << 24) + ((ulong)array[offset + 5] << 16) + ((ulong)array[offset + 6] << 8) + (ulong)array[offset + 7];
            offset += 8;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => double
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static double GetDouble(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 8 || offset + 8 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            byte[] tempArr = BitConverter.IsLittleEndian ?
                new byte[] { array[offset + 7], array[offset + 6], array[offset + 5], array[offset + 4], array[offset + 3], array[offset + 2], array[offset + 1], array[offset] } :
                new byte[] { array[offset], array[offset + 1], array[offset + 2], array[offset + 3], array[offset + 4], array[offset + 5], array[offset + 6], array[offset + 7] };

            double value = BitConverter.ToDouble(tempArr, 0);
            offset += 8;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => string
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <returns>转换值</returns>
        public static string GetUTF8(byte[] array, ref int offset, int length)
        {
            if (array.Length < offset + 4 || offset + 4 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            int size = GetInt32(array, ref offset, length);
            if (array.Length < offset + size || offset + size > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            string value = Encoding.UTF8.GetString(array, offset, size);
            offset += size;
            return value;
        }

        /// <summary>
        /// 转换：byte[] => string
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="offset">偏移量</param>
        /// <param name="encoding">字符编码</param>
        /// <returns>转换值</returns>
        public static string GetString(byte[] array, ref int offset, Encoding encoding,int length)
        {
            if (array.Length < offset + 4 || offset + 4 > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            int size = GetInt32(array, ref offset, length);
            if (array.Length < offset + size || offset + size > length)
            {
                throw new OverflowException("读取数据错误，数组中没有足够数据！数组长度：" + array.Length + "，偏移量：" + offset + "，数据内容长度：" + length);
            }
            string value = encoding.GetString(array, offset, size);
            offset += size;
            return value;
        }
        #endregion
    }
}