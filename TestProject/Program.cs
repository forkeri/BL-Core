using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL_Core.IO;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            TestByteArray();
            Console.ReadKey();
        }

        private static void TestByteArray()
        {
            ByteArray data = new ByteArray();
            data.Write("写入、读取测试！");
            data.Write(true);
            data.Write((byte)254);
            data.Write((sbyte)-127);
            data.Write('c');
            data.Write((ushort)65534);
            data.Write((short)-32767);
            data.Write(-2147483647);
            data.Write(4294967294);
            data.Write(-9223372036854775807);
            data.Write(18446744073709551614);
            data.Write(123.455f);
            data.Write(123.4567891);
            Console.WriteLine(data.ReadUTF8());
            Console.WriteLine(data.ReadBoolean());
            Console.WriteLine(data.ReadUInt8());
            Console.WriteLine(data.ReadInt8());
            Console.WriteLine(data.ReadChar());
            Console.WriteLine(data.ReadUInt16());
            Console.WriteLine(data.ReadInt16());
            Console.WriteLine(data.ReadInt32());
            Console.WriteLine(data.ReadUInt32());
            Console.WriteLine(data.ReadInt64());
            Console.WriteLine(data.ReadUInt64());
            Console.WriteLine(data.ReadSingle());
            Console.WriteLine(data.ReadDouble());
            try
            {
                Console.WriteLine(data.ReadInt8());
            }
            catch (Exception)
            {

                Console.WriteLine("错误读取会导致异常！");
            }
            Console.WriteLine(string.Format("Factor = {0}，Offset = {1}，Length={2}，Size = {3}", data.Factor, data.Offset, data.Length, data.Size));
            data.Reset();
            Console.WriteLine(string.Format("Factor = {0}，Offset = {1}，Length={2}，Size = {3}", data.Factor, data.Offset, data.Length, data.Size));
            data.Clear();
            Console.WriteLine(string.Format("Factor = {0}，Offset = {1}，Length={2}，Size = {3}", data.Factor, data.Offset, data.Length, data.Size));
        }
    }
}
