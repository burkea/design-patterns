namespace DesignPatterns.POC.Service.Core
{
    public static class ByteArrayUnaryOperatorExtension
    {
        /// <summary>
        /// bitwise complement operation on its operand
        /// </summary>
        /// <param name="array">The array which operation requested.</param>
        /// <returns>if array is null or length is zero returns array.otherwise returns complemented array.</returns>
        public static byte[] BitwiseComplement(this byte[] array)
        {
            if (null == array) return array;
            if (0 == array.Length) return array;
            byte[] complementArray = new byte[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                complementArray[i] = (byte)~array[i];

            }
            return complementArray;
        }  
    }

}
