using System;
using System.Text;

namespace ChatCoreTest
{
    internal class Program
    {
        private static byte[] m_PacketData;
        private static uint m_Pos;
        private static byte[] new_Array;



        public static void Main(string[] args)
        {
            m_PacketData = new byte[1024];
            m_Pos = 0;
            new_Array = new byte[1024];


            Write(109);
            Write(109.99f);
            Write("Hello!");
            byte[] byteData2 = AddLength(m_Pos);


            for (int i = 0; i < m_Pos + 4; i++)
            {
                if (i < 4)
                    m_PacketData[i] = byteData2[i];//[0,0,0,24]
                else
                {
                    m_PacketData[i] = new_Array[i - 4];
                }
            }


            Console.Write($"Output Byte array(length:{m_Pos}): ");
            for (var i = 0; i < m_Pos; i++)
            {
                Console.Write(m_PacketData[i] + ", ");
            }


            ReadMessage(m_PacketData);
            Console.ReadLine();
        }



        //write an integer into a byte array
        private static bool Write(int i)
        {
            //convert int to byte array
            var bytes = BitConverter.GetBytes(i);
            _Write(bytes);
            return true;
        }

        // write a float into a byte array




        private static bool Write(float f)
        {
            // convert int to byte array
            var bytes = BitConverter.GetBytes(f);
            _Write(bytes);
            return true;
        }

        //write a string into a byte array
        private static bool Write(string s)
        {
            //convert string to byte array
            var bytes = Encoding.Unicode.GetBytes(s);



            // write byte array length to packet's byte array
            if (Write(bytes.Length) == false)
            {
                return false;
            }

            _Write(bytes);
            return true;
        }



        //write a byte array into packet's byte array
        private static void _Write(byte[] byteData)
        {
            // converter little-endian to network's big-endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteData);
            }

            byteData.CopyTo(new_Array, m_Pos);
            m_Pos += (uint)byteData.Length;


        }



        static byte[] AddLength(uint length3)
        {

            var length2 = BitConverter.GetBytes(length3);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(length2);
            }

            return length2;
        }


        static void ReadMessage(byte[] byteData)
        {

            byte[] showLength1 = new byte[] { byteData[0], byteData[1], byteData[2], byteData[3] };//after reading message length might change
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(showLength1);
            }

            uint x = BitConverter.ToUInt32(showLength1, 0);
            Console.WriteLine("length:" + x);


            byte[] showLength2 = new byte[] { byteData[4], byteData[5], byteData[6], byteData[7] };
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(showLength2);
            }

            float y = BitConverter.ToInt32(showLength2, 0);
            Console.WriteLine("int:" + y);

            byte[] showLength3 = new byte[] { byteData[8], byteData[9], byteData[10], byteData[11] };
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(showLength3);
            }
            float z = BitConverter.ToSingle(showLength3, 0);
            Console.WriteLine("float:" + z);



            byte[] showLength4 = new byte[] { byteData[16], byteData[17], byteData[18], byteData[19], byteData[20], byteData[21], byteData[22], byteData[23], byteData[24], byteData[25], byteData[26], byteData[27] };
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(showLength4);
            }
            string a = Encoding.Unicode.GetString(showLength4);
            Console.WriteLine("string:" + a);

        }
    }
}



