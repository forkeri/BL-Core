using System;
using System.IO;
using System.Text;

namespace BL_Core.IO
{
    /// <summary>
    /// ByteArray
    /// </summary>
    public class ByteArray
    {
        //        private MemoryStream _memoryStream;

        //        private int PosIndex = 0;

        //        /*
        //         * 字节顺序: big endian是指低地址存放最高有效字节（MSB），而little endian则是低地址存放最低有效字节（LSB）
        //         * 例如一个int a=10;  在big Endian里是    00000000 00000000 00000000 00001010
        //         *                   在little Endian里是  00001010 00000000 00000000 00000000
        //         */


        //        public ByteArray()
        //        {
        //            _memoryStream = new MemoryStream();
        //        }

        //        public ByteArray(MemoryStream ms)
        //        {
        //            _memoryStream = ms;
        //        }

        //        public ByteArray(byte[] buffer)
        //        {
        //            _memoryStream = new MemoryStream();
        //            _memoryStream.Write(buffer, 0, buffer.Length);
        //            _memoryStream.Position = 0;
        //        }

        //        /// <summary>
        //        /// 释放资源
        //        /// </summary>
        //        public void MSDispose()
        //        {
        //            if (_memoryStream != null)
        //            {
        //                _memoryStream.Close();
        //                _memoryStream.Dispose();
        //            }
        //            _memoryStream = null;
        //        }

        //       /// <summary>
        //       /// 返回当前流的长度
        //       /// </summary>
        //        public uint Length
        //        {
        //            get
        //            {
        //                return (uint)_memoryStream.Length;
        //            }
        //        }

        //        /// <summary>
        //        /// 当前流的位置
        //        /// </summary>
        //        public uint Position
        //        {
        //            get { return (uint)_memoryStream.Position; }
        //            set { _memoryStream.Position = value; }
        //        }

        //        /// <summary>
        //        /// 可用的长度
        //        /// </summary>
        //        public uint BytesAvailable
        //        {
        //            get { return Length - Position; }
        //        }

        //        /// <summary>
        //        /// 返回创建该流的无符号字节数组。
        //        /// </summary>
        //        /// <returns></returns>
        //        public byte[] GetBuffer()
        //        {
        //            return _memoryStream.GetBuffer();
        //        }

        //        /// <summary>
        //        /// 把流的内容写入字节数组，不管System.IO.MemoryStream.Position属性。
        //        /// 即拷贝流数据到字节数组
        //        /// </summary>
        //        /// <returns></returns>
        //        public byte[] ToArray()
        //        {
        //            return _memoryStream.ToArray();
        //        }

        //        /// <summary>
        //        /// 返回节流
        //        /// </summary>
        //        public MemoryStream MemoryStream
        //        {
        //            get
        //            {
        //                return _memoryStream;
        //            }
        //        }

        //       /// <summary>
        //       /// 读取bool
        //       /// </summary>
        //       /// <returns></returns>
        //        public bool ReadBoolean()
        //        {
        //            return BitConverter.ToBoolean(PriReadBytes(1),0) ;
        //        }

        //        /// <summary>
        //        /// 从当前流读取字节
        //        /// </summary>
        //        /// <returns></returns>
        //        public byte ReadByte()
        //        {
        //            _memoryStream.Position = PosIndex;
        //            PosIndex++;
        //            return (byte)_memoryStream.ReadByte();
        //        }


        //        public void ReadBytes()
        //        {

        //        }





        //        /// <summary>
        //        /// 根据该计算机字节高低位顺序 预读字节
        //        /// </summary>
        //        /// <param name="c">字节长度</param>
        //        /// <returns></returns>
        //        private byte[] PriReadBytes(uint c)
        //        {
        //            byte[] a = new byte[c];

        //#if BLANK_BYTE_ARRAY_ENDIAN_BIG

        //                for (uint i = 0; i < c; i++)
        //                {
        //                    a[c - 1 - i] = (byte)_memoryStream.ReadByte();
        //                }
        //#else
        //            for (uint i = 0; i < c; i++)
        //                {

        //                    a[i] = (byte)_memoryStream.ReadByte();
        //                }
        //#endif
        //            return a;
        //        }

        //        /// <summary>
        //        /// 读取double
        //        /// </summary>
        //        /// <returns>返回double</returns>
        //        public double ReadDouble()
        //        {
        //            byte[] bytes = PriReadBytes(8);
        //            double value = System.BitConverter.ToDouble(bytes, 0);
        //            return value;
        //        }

        //        /// <summary>
        //        /// 读取float
        //        /// </summary>
        //        /// <returns>返回float</returns>
        //        public float ReadFloat()
        //        {
        //            byte[] bytes = PriReadBytes(4);
        //            float value = System.BitConverter.ToSingle(bytes, 0);
        //            return value;
        //        }

        //        /// <summary>
        //        /// 读取int
        //        /// </summary>
        //        /// <returns>返回int</returns>
        //        public int ReadInt()
        //        {
        //            byte[] bytes = PriReadBytes(4);
        //            int value = System.BitConverter.ToInt32(bytes, 0);
        //            return value;
        //        }

        //        /// <summary>
        //        /// 读取short
        //        /// </summary>
        //        /// <returns>返回short</returns>
        //        public short ReadShort()
        //        {
        //            byte[] bytes = PriReadBytes(2);
        //            short value = System.BitConverter.ToInt16(bytes, 0);
        //            return value;
        //        }

        //        /// <summary>
        //        /// 读取string
        //        /// </summary>
        //        /// <returns>返回string</returns>
        //        public string ReadUTF()
        //        {
        //            uint length = (uint)ReadShort();
        //            return ReadUTFBytes(length);
        //        }

        //        /// <summary>
        //        /// 读取string
        //        /// </summary>
        //        /// <param name="length">字节长度</param>
        //        /// <returns></returns>
        //        public string ReadUTFBytes(uint length)
        //        {
        //            if (length == 0)
        //                return string.Empty;
        //            UTF8Encoding utf8 = new UTF8Encoding(false, true);
        //            byte[] encodedBytes = new byte[length];
        //            for (uint i = 0; i < length; i++)
        //            {
        //                encodedBytes[i] = (byte)_memoryStream.ReadByte();
        //            }
        //            string decodedString = utf8.GetString(encodedBytes, 0, encodedBytes.Length);
        //            return decodedString;
        //        }

        //        //=========================================

        //        /// <summary>
        //        /// 写入一个bool数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteBoolean(bool value)
        //        {
        //            WriteByte((byte)value.CompareTo(value));
        //        }


        //        /// <summary>
        //        ///写入一个byte数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteByte(byte value)
        //        {
        //            _memoryStream.WriteByte(value);
        //        }

        //        /// <summary>
        //        /// 写入一个字节数组
        //        /// </summary>
        //        /// <param name="bytes">数组</param>
        //        /// <param name="offset">偏移量</param>
        //        /// <param name="length">字节长度</param>
        //        public void WriteBytes(byte[] bytes, int offset, int length)
        //        {
        //            for (int i = offset; i < offset + length; i++)
        //            {
        //                if (i < bytes.Length)
        //                {
        //                    _memoryStream.WriteByte(bytes[i]);
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //        }

        //        /// <summary>
        //        /// 写入一个字节数组
        //        /// </summary>
        //        /// <param name="bytes"></param>
        //        /// <param name="offset"></param>
        //        /// <param name="length"></param>
        //        public void WriteBytes(ByteArray bytes, int offset, int length)
        //        {
        //            byte[] data = bytes.ToArray();
        //            WriteBytes(data, offset, length);
        //        }

        //        /// <summary>
        //        /// 写入一个Double数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteDouble(double value)
        //        {
        //            byte[] bytes = System.BitConverter.GetBytes(value);
        //            WriteEndian(bytes);
        //        }

        //        /// <summary>
        //        ///  根据该计算机字节高低位顺序 根据写入数据
        //        /// </summary>
        //        /// <param name="bytes"></param>
        //        private void WriteEndian(byte[] bytes)
        //        {
        //            if (bytes == null)
        //                return;
        //#if BLANK_BYTE_ARRAY_ENDIAN_BIG
        //                for (int i = bytes.Length - 1; i >= 0; i--)
        //                {
        //                    WriteByte(bytes[i]);
        //                }
        //#else
        //            for (int i = 0; i < bytes.Length; i++)
        //                {
        //                    _memoryStream.WriteByte(bytes[i]);
        //                }
        //#endif
        //        }

        //        /// <summary>
        //        /// 写入一个float数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteFloat(float value)
        //        {
        //            byte[] bytes = System.BitConverter.GetBytes(value);
        //            WriteEndian(bytes);
        //        }

        //        /// <summary>
        //        /// 写入一个uint数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteUInt32(uint value)
        //        {
        //            byte[] bytes = System.BitConverter.GetBytes(value);
        //            WriteEndian(bytes);
        //        }

        //        /// <summary>
        //        /// 写入一个int数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteInt(int value)
        //        {
        //            byte[] bytes = System.BitConverter.GetBytes(value);
        //            WriteEndian(bytes);
        //        }

        //        /// <summary>
        //        /// 写入一个ushort数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteShort(ushort value)
        //        {
        //            byte[] bytes = System.BitConverter.GetBytes(value);
        //            WriteEndian(bytes);
        //        }

        //        /// <summary>
        //        /// 写入一个short数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteShort(short value)
        //        {
        //            byte[] bytes = System.BitConverter.GetBytes(value);
        //            WriteEndian(bytes);
        //        }

        //        /// <summary>
        //        /// 写入一个string数据
        //        /// </summary>
        //        /// <param name="value"></param>
        //        public void WriteUTF(string value)
        //        {
        //            UTF8Encoding utf8Encoding = new UTF8Encoding();
        //            int byteCount = utf8Encoding.GetByteCount(value);
        //            byte[] buffer = utf8Encoding.GetBytes(value);
        //            this.WriteShort((short)byteCount);
        //            if (buffer != null && buffer.Length > 0)
        //            {
        //                this.WriteBytes(buffer, 0, buffer.Length);
        //            }
        //        }

        //    }

        /// <summary>
        /// 字节数组
        /// </summary>
        byte[] bytes;
 
        int readPos = 0;//读取的位置
        int writePos = 0;//写入的位置

        const int DEF_LENGTH= 1024;//默认长度
       const float GROWC  = 0.75f;//增长因子

        public ByteArray()
        {
            bytes = new byte[DEF_LENGTH];
        }



       

        /// <summary>
        /// 返回字节数组
        /// </summary>
        public byte[] GetBytes
        {
            get { return bytes; }
        }

        /// <summary>
        /// 返回字节长度，即写入的位置
        /// </summary>
        public int BytesCount
        {
            get { return writePos; }
        }

        
        public byte[] BytesCounts
        {
            get
            {
                return BitConverter.GetBytes(writePos);
            }
        }

        /// <summary>
        /// 检查
        /// </summary>
        /// <returns>-1：数据读取超出，错误的数据 ;>=0:还未读取的长度</returns>
        public int Check()
        {
            if (readPos > writePos)
            {
                return -1;
            }
            else
            {
                return writePos - readPos;
            }
        }

        //清空【读完长度（长度+包体的长度）时】
        public void Clear()
        {
            Array.Clear(bytes, 0, bytes.Length);//(bytes,0,4)
            writePos = 0;//清空就置零
            readPos = 0;
        }


        //============read================ 

        /// <summary>
        /// 读byte 数据
        /// </summary>
        /// <returns></returns>
        public byte ReadByte()
        {
            readPos += 1;
            return (byte)bytes[readPos];
        }

        ///// <summary>
        ///// 读 sbyte 数据
        ///// </summary>
        ///// <returns></returns>
        //public SByte ReadSBtye() {
        //    readPos += 1;
        //    return (SByte)bytes[readPos];
        //}

        /// <summary>
        /// 读char 数据
        /// </summary>
        /// <returns></returns>
        public char ReadChar() {
            char c = BitConverter.ToChar(bytes,readPos);
            readPos += 1; //char 1 字节
            return c;
        }

        /// <summary>
        /// 读取ushort数据
        /// </summary>
        /// <returns></returns>
        public ushort ReadUshot() {
            ushort n = BitConverter.ToUInt16(bytes, readPos);
            readPos += 2;//一个short 2字节
            return n;
        }

        /// <summary>
        /// 读short数据
        /// </summary>
        /// <returns></returns>
        public short ReadShort()
        {
            short n = BitConverter.ToInt16(bytes, readPos);
            readPos += 2;//一个short 2字节
            return n;
        }

        /// <summary>
        /// 读取uint 数据
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt() {
            uint n = BitConverter.ToUInt32(bytes, readPos);
            readPos += 4;//一个int 4字节
            return n;
        }

        /// <summary>
        /// 读int数据
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            int n = BitConverter.ToInt32(bytes, readPos);
            readPos += 4;//一个int 4字节
            return n;
        }


        /// <summary>
        /// 读ulong 数据
        /// </summary>
        /// <returns></returns>
        public ulong ReadULong()
        {
            ulong lon = BitConverter.ToUInt64(bytes, readPos);
            readPos += 4;
            return lon;
        }

        /// <summary>
        /// 读long 数据
        /// </summary>
        /// <returns></returns>
        public long ReadLong()
        {
            long lon = BitConverter.ToInt64(bytes, readPos);
            readPos += 4;// long 4字节
            return lon;
        }

        /// <summary>
        /// 读float数据
        /// </summary>
        /// <returns></returns>
        public float ReadFloat() {
            float f = BitConverter.ToSingle(bytes,readPos);
            readPos += 4;//float 4字节
            return f;
        }

       

        /// <summary>
        /// 读都double 数据
        /// </summary>
        /// <returns></returns>
        public double ReadDouble()
        {
            double n = BitConverter.ToDouble(bytes, readPos);
            readPos += 8;// double 8字节
            return n;
        }

        /// <summary>
        /// 读取bool 数据
        /// </summary>
        /// <returns></returns>
        public bool ReadBool() {
            bool b = BitConverter.ToBoolean(bytes,readPos);
            readPos += 1; //一个bool 1字节
            return b;
        }

        /// <summary>
        /// 读string数据
        /// </summary>
        /// <returns></returns>
        public string ReadString()
        {
            int length = this.ReadInt();//先读字符串的长度
            string value = Encoding.UTF8.GetString(bytes, readPos, length);
            readPos += length;//读了多少字节就累增多少字节
            return value;
        }




 //==================================write====================================

        /// <summary>
        /// 写入字节数组
        /// </summary>
        /// <param name="bs">字节数组</param>
        /// <param name="length">字节数组长度</param>
        private void WriteBytes(byte[] bs, int length)
        {
            for (int i = 0; i < length; i++)
            {
                bytes[writePos] = bs[i];
                writePos++;
            }
        }

        /// <summary>
        /// 写入byte 数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteUByte(byte value) {
            writePos++;
           bytes[writePos]=value;
        }

        ///// <summary>
        ///// 写入sbyte 数据
        ///// </summary>
        ///// <param name="value"></param>
        //public void WriteByte(sbyte value)
        //{
        //    writePos++;
        //    bytes[writePos] = (byte)value;
        //}

        /// <summary>
        /// 写入Char类型的数据
        /// </summary>
        public void WriteChar(char value) {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b,b.Length);

        }

        /// <summary>
        /// 写入ushort数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteUShort(ushort value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入short数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteShort(short value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入uint数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteUInt(uint value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入int数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteInt(int value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入ulong数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteULong(ulong value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入long数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteLong(long value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入float数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteFloat(float value) {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入double数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteDouble(double value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入bool数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteBool(bool value)
        {
            byte[] b = BitConverter.GetBytes(value);
            this.WriteBytes(b, b.Length);
        }

        /// <summary>
        /// 写入string数据
        /// </summary>
        /// <param name="value"></param>
        public void WriteString(string value)
        {
            byte[] b = Encoding.UTF8.GetBytes(value);
            this.WriteInt(b.Length);//先把字符串长度写进去
            this.WriteBytes(b, b.Length);

        }

    }
}
