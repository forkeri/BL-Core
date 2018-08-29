using System;
using System.IO;
using System.Text;

namespace BL_Core.IO
{
    public class ByteArray
    {
        private MemoryStream _memoryStream;


        /// <summary>
        /// 字节存储次序
        /// </summary>
        public string endian;

        /*
         * 字节顺序: big endian是指低地址存放最高有效字节（MSB），而little endian则是低地址存放最低有效字节（LSB）
         * 例如一个int a=10;  在big Endian里是    00000000 00000000 00000000 00001010
         *                   在little Endian里是  00001010 00000000 00000000 00000000
         */

        public static string BIG_ENDIAN = "BIG_ENDIAN";
        public static string LITTLE_ENDIAN = "LITTLE_ENDIAN";

        public ByteArray()
        {
            _memoryStream = new MemoryStream();
            //设置该计算机高地位
            if (BitConverter.IsLittleEndian)
            {
                endian = ByteArray.LITTLE_ENDIAN;
            }
            else {
                endian = ByteArray.BIG_ENDIAN;
            }
        }

        public ByteArray(MemoryStream ms)
        {
            _memoryStream = ms;
        }

        public ByteArray(byte[] buffer)
        {
            _memoryStream = new MemoryStream();
            _memoryStream.Write(buffer, 0, buffer.Length);
            _memoryStream.Position = 0;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void MSDispose()
        {
            if (_memoryStream != null)
            {
                _memoryStream.Close();
                _memoryStream.Dispose();
            }
            _memoryStream = null;
        }

       /// <summary>
       /// 返回当前流的长度
       /// </summary>
        public uint Length
        {
            get
            {
                return (uint)_memoryStream.Length;
            }
        }

        /// <summary>
        /// 当前流的位置
        /// </summary>
        public uint Position
        {
            get { return (uint)_memoryStream.Position; }
            set { _memoryStream.Position = value; }
        }

        /// <summary>
        /// 可用的长度
        /// </summary>
        public uint BytesAvailable
        {
            get { return Length - Position; }
        }

        /// <summary>
        /// 返回创建该流的无符号字节数组。
        /// </summary>
        /// <returns></returns>
        public byte[] GetBuffer()
        {
            return _memoryStream.GetBuffer();
        }

        /// <summary>
        /// 把流的内容写入字节数组，不管System.IO.MemoryStream.Position属性。
        /// 即拷贝流数据到字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToArray()
        {
            return _memoryStream.ToArray();
        }

        /// <summary>
        /// 返回节流
        /// </summary>
        public MemoryStream MemoryStream
        {
            get
            {
                return _memoryStream;
            }
        }

       /// <summary>
       /// 读取bool
       /// </summary>
       /// <returns></returns>
        public bool ReadBoolean()
        {
            return ReadByte().Equals(true);
        }

        /// <summary>
        /// 从当前流读取字节
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            return (byte)_memoryStream.ReadByte();
        }

        /// <summary>
        /// 从当前流读取一个字节块，并将数据写入缓冲区。
        /// </summary>
        /// <param name="bytes">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="length">读取的字节数</param>
        public void ReadBytes(byte[] bytes, uint offset, uint length)
        {
            _memoryStream.Read(bytes, (int)offset, (int)length);
        }

        /// <summary>
        /// 从当前流读取一个字节块，并将数据写入缓冲区。
        /// </summary>
        /// <param name="bytes">缓冲区</param>
        /// <param name="offset">偏移量</param>
        /// <param name="length">读取的字节数</param>
        public void ReadBytes(ByteArray bytes, uint offset, uint length)
        {
            uint tmp = bytes.Position;
            int count = (int)(length != 0 ? length : BytesAvailable);
            for (int i = 0; i < count; i++)
            {
                bytes._memoryStream.Position = i + offset;
                //从当前流读取字节,再将字节写入当前位置的当前流。
                bytes._memoryStream.WriteByte(ReadByte());
            }
            bytes.Position = tmp;
        }

       
        /// <summary>
        /// 根据该计算机字节高低位顺序 预读字节
        /// </summary>
        /// <param name="c">字节长度</param>
        /// <returns></returns>
        private byte[] PriReadBytes(uint c)
        {
            byte[] a = new byte[c];

#if BLANK_BYTE_ARRAY_ENDIAN_BIG
            
                for (uint i = 0; i < c; i++)
                {
                    a[c - 1 - i] = (byte)_memoryStream.ReadByte();
                }
#else
            for (uint i = 0; i < c; i++)
                {
                    a[i] = (byte)_memoryStream.ReadByte();
                }
#endif
            return a;
        }

        /// <summary>
        /// 读取double
        /// </summary>
        /// <returns>返回double</returns>
        public double ReadDouble()
        {
            byte[] bytes = PriReadBytes(8);
            double value = System.BitConverter.ToDouble(bytes, 0);
            return value;
        }

        /// <summary>
        /// 读取float
        /// </summary>
        /// <returns>返回float</returns>
        public float ReadFloat()
        {
            byte[] bytes = PriReadBytes(4);
            float value = System.BitConverter.ToSingle(bytes, 0);
            return value;
        }

        /// <summary>
        /// 读取int
        /// </summary>
        /// <returns>返回int</returns>
        public int ReadInt()
        {
            byte[] bytes = PriReadBytes(4);
            int value = System.BitConverter.ToInt32(bytes, 0);
            return value;
        }

        /// <summary>
        /// 读取short
        /// </summary>
        /// <returns>返回short</returns>
        public short ReadShort()
        {
            byte[] bytes = PriReadBytes(2);
            short value = System.BitConverter.ToInt16(bytes, 0);
            return value;
        }

        /// <summary>
        /// 读取string
        /// </summary>
        /// <returns>返回string</returns>
        public string ReadUTF()
        {
            uint length = (uint)ReadShort();
            return ReadUTFBytes(length);
        }

        /// <summary>
        /// 读取string
        /// </summary>
        /// <param name="length">字节长度</param>
        /// <returns></returns>
        public string ReadUTFBytes(uint length)
        {
            if (length == 0)
                return string.Empty;
            UTF8Encoding utf8 = new UTF8Encoding(false, true);
            byte[] encodedBytes = new byte[length];
            for (uint i = 0; i < length; i++)
            {
                encodedBytes[i] = (byte)_memoryStream.ReadByte();
            }
            string decodedString = utf8.GetString(encodedBytes, 0, encodedBytes.Length);
            return decodedString;
        }

        //=========================================

        /// <summary>
        /// 写入一个bool数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteBoolean(bool value)
        {
            WriteByte((byte)value.CompareTo(value));
        }


        /// <summary>
        ///写入一个byte数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteByte(byte value)
        {
            _memoryStream.WriteByte(value);
        }

        /// <summary>
        /// 写入一个字节数组
        /// </summary>
        /// <param name="bytes">数组</param>
        /// <param name="offset">偏移量</param>
        /// <param name="length">字节长度</param>
        public void WriteBytes(byte[] bytes, int offset, int length)
        {
            for (int i = offset; i < offset + length; i++)
            {
                if (i < bytes.Length)
                {
                    _memoryStream.WriteByte(bytes[i]);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 写入一个字节数组
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void WriteBytes(ByteArray bytes, int offset, int length)
        {
            byte[] data = bytes.ToArray();
            WriteBytes(data, offset, length);
        }

        /// <summary>
        /// 写入一个Double数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteDouble(double value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            WriteEndian(bytes);
        }

        /// <summary>
        ///  根据该计算机字节高低位顺序 根据写入数据
        /// </summary>
        /// <param name="bytes"></param>
        private void WriteEndian(byte[] bytes)
        {
            if (bytes == null)
                return;
#if BLANK_BYTE_ARRAY_ENDIAN_BIG
                for (int i = bytes.Length - 1; i >= 0; i--)
                {
                    WriteByte(bytes[i]);
                }
#else
            for (int i = 0; i < bytes.Length; i++)
                {
                    _memoryStream.WriteByte(bytes[i]);
                }
#endif
        }

        /// <summary>
        /// 写入一个float数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(float value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            WriteEndian(bytes);
        }

        /// <summary>
        /// 写入一个uint数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt32(uint value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            WriteEndian(bytes);
        }

        /// <summary>
        /// 写入一个int数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt(int value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            WriteEndian(bytes);
        }

        /// <summary>
        /// 写入一个ushort数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteShort(ushort value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            WriteEndian(bytes);
        }

        /// <summary>
        /// 写入一个short数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteShort(short value)
        {
            byte[] bytes = System.BitConverter.GetBytes(value);
            WriteEndian(bytes);
        }

        /// <summary>
        /// 写入一个string数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteUTF(string value)
        {
            UTF8Encoding utf8Encoding = new UTF8Encoding();
            int byteCount = utf8Encoding.GetByteCount(value);
            byte[] buffer = utf8Encoding.GetBytes(value);
            this.WriteShort((short)byteCount);
            if (buffer != null && buffer.Length > 0)
            {
                this.WriteBytes(buffer, 0, buffer.Length);
            }
        }

    }

}
