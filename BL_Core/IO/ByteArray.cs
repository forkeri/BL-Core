using System;
using System.IO;
using System.Text;
using static BL_Core.IO.ByteUtils;

namespace BL_Core.IO
{
    /// <summary>
    /// ByteArray
    /// @author wrb
    /// </summary>
    public class ByteArray
    {
        /// <summary>
        /// 默认尺寸
        /// </summary>
        public const int DEFAULT_SIZE = 1024;
        /// <summary>
        /// 增长因子
        /// </summary>
        private readonly float _factor = 0.75f;
        /// <summary>
        /// 偏移位置：该值不会大于_length
        /// </summary>
        private int _offset = 0;
        /// <summary>
        /// 数据长度：该值大于等于_offset
        /// </summary>
        private int _length = 0;
        /// <summary>
        /// 数据对象
        /// </summary>
        private byte[] _datas = null;

        /// <summary>
        /// 增长因子
        /// </summary>
        public float Factor => _factor;
        /// <summary>
        /// 偏移量
        /// </summary>
        public int Offset => _offset;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int Length => _length;
        /// <summary>
        /// 数据容器大小
        /// </summary>
        public int Size => _datas.Length;
        /// <summary>
        /// 源数据的拷贝
        /// </summary>
        public byte[] Datas
        {
            get
            {
                byte[] array = new byte[Size];
                Array.Copy(_datas, 0, array, 0, Size);
                return array;
            }
        }

        /// <summary>
        /// 构建一个默认的ByteArray对象
        /// </summary>
        public ByteArray() : this(null, 0, 0, 0.75f) { }

        /// <summary>
        /// 以一个字符数组初始化一个ByteArray对象
        /// </summary>
        /// <param name="array"></param>
        public ByteArray(byte[] _array) : this(_array, 0, _array == null ? 0 : _array.Length, 0.75f) { }

        /// <summary>
        /// 以一个字符数组初始化一个ByteArray对象并设置增长因子
        /// </summary>
        /// <param name="array"></param>
        /// <param name="factor"></param>
        public ByteArray(byte[] array, float factor) : this(array, 0, array == null ? 0 : array.Length, factor) { }

        /// <summary>
        /// 创建一个默认的ByteArray对象并设置增长因子
        /// </summary>
        /// <param name="factor"></param>
        public ByteArray(float factor) : this(null, 0, 0, factor) { }

        public ByteArray(byte[] array, int offset, int length, float factor)
        {
            if (array == null)
            {
                array = new byte[DEFAULT_SIZE];
            }
            if (length > array.Length) length = array.Length;
            if (offset > length) throw new OverflowException("偏移量不能大于数组长度！");
            if (factor < 0) throw new ArgumentNullException("增长因子不能为负！");
            _datas = array;
            _offset = offset;
            _length = length;
            _factor = factor;
        }

        #region 数据写入

        /// <summary>
        /// 写入一个字节数组到ByteArray对象中
        /// </summary>
        /// <param name="array">字节数组</param>
        public void Write(byte[] array,int startIndex = 0)
        {
            if (array == null)
                return;
            Write(array, startIndex, array.Length);
        }

        /// <summary>
        /// 写入一个字节数组到对象中
        /// </summary>
        /// <param name="array">字节数组</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="endIndex">终止位置</param>
        public void Write(byte[] array, int startIndex, int length)
        {
            if (array == null)
                return;
            CheckFactor(_length + length);
            Array.Copy(array, startIndex, _datas, _length, length);
            _length += length;
        }

        /// <summary>
        /// 写入：byte
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(bool value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：byte
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(byte value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：sbyte
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(sbyte value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：char
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(char value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：short
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(short value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：ushort
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(ushort value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：int
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(int value)
        {
            Write(GetBytes(value));
        }

        public void Write(uint value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：float
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(float value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：long
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(long value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：ulong
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(ulong value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：double
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(double value)
        {
            Write(GetBytes(value));
        }

        /// <summary>
        /// 写入：string
        /// </summary>
        /// <param name="value">写入的值</param>
        public void Write(string value)
        {
            Write(GetBytes(value));
        }

        public void Write(string value, Encoding encoding)
        {
            Write(GetBytes(value, encoding));
        }

        #endregion
        #region 数据读取

        /// <summary>
        /// 读取：bool
        /// </summary>
        /// <returns></returns>
        public bool ReadBoolean()
        {
            return GetBoolean(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：byte
        /// </summary>
        /// <returns></returns>
        public byte ReadUInt8()
        {
            return GetUInt8(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：sbyte
        /// </summary>
        /// <returns></returns>
        public sbyte ReadInt8()
        {
            return GetInt8(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：char
        /// </summary>
        /// <returns></returns>
        public char ReadChar()
        {
            return GetChar(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：ushort
        /// </summary>
        /// <returns></returns>
        public ushort ReadUInt16()
        {
            return GetUInt16(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：short
        /// </summary>
        /// <returns></returns>
        public short ReadInt16()
        {
            return GetInt16(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：int
        /// </summary>
        /// <returns></returns>
        public int ReadInt32()
        {
            return GetInt32(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：uint
        /// </summary>
        /// <returns></returns>
        public uint ReadUInt32()
        {
            return GetUInt32(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：float
        /// </summary>
        /// <returns></returns>
        public float ReadSingle()
        {
            return GetSingle(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：long
        /// </summary>
        /// <returns></returns>
        public long ReadInt64()
        {
            return GetInt64(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：ulong
        /// </summary>
        /// <returns></returns>
        public ulong ReadUInt64()
        {
            return GetUInt64(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：double
        /// </summary>
        /// <returns></returns>
        public double ReadDouble()
        {
            return GetDouble(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：string
        /// </summary>
        /// <returns></returns>
        public string ReadUTF8()
        {
            return GetUTF8(_datas, ref _offset, _length);
        }

        /// <summary>
        /// 读取：string
        /// </summary>
        /// <returns></returns>
        public string ReadString(Encoding encoding)
        {
            return GetString(_datas, ref _offset, encoding, _length);
        }
        #endregion

        /// <summary>
        /// 检查容量
        /// </summary>
        /// <param name="length"></param>
        void CheckFactor(int length)
        {
            int size = _datas.Length;
            while (size < length)
            {
                int add = (int)(size * _factor);
                add = add < 1 ? 1 : add;
                size += add;
            }
            if (size != _datas.Length)
            {
                byte[] datas = new byte[size];
                Array.Copy(_datas, datas, _datas.Length);
                _datas = datas;
            }
        }

        /// <summary>
        /// 重置：仅重置偏移量
        /// </summary>
        public void Reset()
        {
            _offset = 0;
        }

        /// <summary>
        /// 清空信息：重置长度
        /// </summary>
        public void Clear() {
            _length = 0;
            _offset = 0;
        }
    }
}