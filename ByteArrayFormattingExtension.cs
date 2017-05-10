using System;
using System.Text;

namespace DesignPatterns.POC.Service.Core
{
    public static class ByteArrayFormattingExtension
    {
        public static Int32 ConvertToInt16(byte[] b, int start)
        {
            if (null == b)
                return 0;
            if (start < 0) return 0;
            if (b.Length < start + 2)
                return 0;
            return (b[start + 1] << 8) | (b[start]);
        }

        public static Int32 ConvertToInt32(byte[] b, int start)
        {
            if (null == b)
                return 0;
            if (start < 0) return 0;
            if (b.Length < start + 4)
                return 0;
            return (b[start + 3] << 24) | (b[start + 2] << 16) | (b[start + 1] << 8) | (b[start]);
        }

        public static Int32 ConvertToInt64(byte[] b, int start)
        {
            if (null == b)
                return 0;
            if (start < 0) return 0;
            if (b.Length < start + 8)
                return 0;
            return
                (b[start + 7] << 56) |
                (b[start + 6] << 48) |
                (b[start + 5] << 40) |
                (b[start + 4] << 32) |
                (b[start + 3] << 24) |
                (b[start + 2] << 16) |
                (b[start + 1] << 8) |
                (b[start]);
        }



        public static byte[] Ebcdic500ToAscii(this byte[] buffer, int offSet, int length)
        {
            if (buffer == null)
                return new byte[] { };

            if (buffer.Length > offSet + length)
                return new byte[] { };

            int max = offSet + length;
            int position = 0;
            for (position = offSet; position < max; position++)
                //  buffer[position] = ConversionTables.ConversionTableEbcdic500ToAscii437[buffer[position]];
                buffer[position] = ConversionTables.Ebcdic2AsciiTable[buffer[position]];
            return buffer;
        }
        public static byte[] Ebcdic500ToAscii(this byte[] buffer)
        {

            if (buffer == null)
                return new byte[] { };

            byte[] outBuffer = new byte[buffer.Length];
            int position = 0;

            for (position = 0; position < buffer.Length; position++)
               // outBuffer[position] = ConversionTables.ConversionTableEbcdic500ToAscii437[buffer[position]];
                outBuffer[position] = ConversionTables.Ebcdic2AsciiTable[buffer[position]];
            return outBuffer;
        }

        public static byte[] AsciiToEbcdic500(this byte[] buffer, int offSet, int length)
        {

            if (buffer == null)
                return new byte[] { };

            if (buffer.Length > offSet + length)
                return new byte[] { };

            int max = offSet + length;
            int position = 0;

            for (position = offSet; position < max; position++)
                //buffer[position] = ConversionTables.ConversionTableAscii437ToEbcdic500[buffer[position]];
                buffer[position] = ConversionTables.Ascii2EbcdicTable[buffer[position]];

            return buffer;
        }
        public static byte[] AsciiToEbcdic500(this byte[] buffer)
        {
            int position = 0;

            if (buffer == null)
                return new byte[] { };

            byte[] outBuffer = new byte[buffer.Length];

            for (position = 0; position < buffer.Length; position++)
                //outBuffer[position] = ConversionTables.ConversionTableAscii437ToEbcdic500[buffer[position]];
                outBuffer[position] = ConversionTables.Ascii2EbcdicTable[buffer[position]];

            return outBuffer;
        }

        public static byte[] Ebcdic37ToAscii(byte[] buffer, int offSet, int length)
        {
            int position = 0;

            if (buffer == null)
                return new byte[] { };

            if (buffer.Length > offSet + length)
                return new byte[] { };

            int max = offSet + length;

            for (position = offSet; position < max; position++)
                buffer[position] = ConversionTables.ConversionTableEbcdic037ToAscii437[buffer[position]];

            return buffer;
        }
        public static byte[] Ebcdic37ToAscii(byte[] buffer)
        {
            int position = 0;

            if (buffer == null)
                return new byte[] { };

            byte[] outBuffer = new byte[buffer.Length];

            for (position = 0; position < buffer.Length; position++)
                outBuffer[position] = ConversionTables.ConversionTableEbcdic037ToAscii437[buffer[position]];

            return outBuffer;
        }

        public static byte[] AsciiToEbcdic37(byte[] buffer, int offSet, int length)
        {
            int position = 0;

            if (buffer == null)
                return new byte[] { };

            if (buffer.Length > offSet + length)
                return new byte[] { };

            int max = offSet + length;

            for (position = offSet; position < max; position++)
                buffer[position] = ConversionTables.ConversionTableAscii437ToEbcdic037[buffer[position]];

            return buffer;
        }
        public static byte[] AsciiToEbcdic37(byte[] buffer)
        {
            int position = 0;

            if (buffer == null)
                return new byte[] { };

            byte[] outBuffer = new byte[buffer.Length];

            for (position = 0; position < buffer.Length; position++)
                outBuffer[position] = ConversionTables.ConversionTableAscii437ToEbcdic037[buffer[position]];

            return outBuffer;
        }




        /// <summary>
        /// Uns the pack.
        /// </summary>
        /// <param name="binaryData">The binary data.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string UnPack(this byte[] value)
        {
            if (value != null)
            {
                if (value.Length > 0)
                {
                    byte[] result = new byte[value.Length * 2];
                    int loop = 0;
                    for (loop = 0; loop < value.Length; loop++)
                    {
                        result[loop * 2] = ConversionTables.HexChars[(value[loop] >> 4) & 0x0F];
                        result[(loop * 2) + 1] = ConversionTables.HexChars[value[loop] & 0x0F];
                    }
                    return Encoding.Default.GetString(result);
                }
            }
            //Trace.TraceError("UnPack returns error");
            return @"<<EMPTY ARRAY>>";
        }
        /// <summary>
        /// Pads an array with given padding char to a given length
        /// </summary>
        /// <param name="value">Array to be padded</param>
        /// <param name="totalLength">Final length of the array</param>
        /// <param name="paddingChar">Padding character</param>
        /// <returns></returns>
        public static byte[] Pad(this byte[] value, int totalLength, byte paddingChar)
        {
            if (value == null) return value;
            //if total length equals to value length returns same array
            if (value.Length == totalLength) return value;
            // Define array to return constructed data
            byte[] tailedBytes;
            if (value.Length != 0)
            {
                // Initialize array one byte longer than original
                tailedBytes = new byte[totalLength];
                // Copy all bytes
                Buffer.BlockCopy(value, 0, tailedBytes, 0, value.Length);
                // Add new byte
                for (int i = 0; i < totalLength - value.Length; i++)
                    tailedBytes[i + value.Length] = paddingChar;
            }
            else
            {
                // Construct a one byte array
                tailedBytes = new byte[totalLength];
                for (int i = 0; i < totalLength; i++)
                    tailedBytes[i] = paddingChar;
            }
            return tailedBytes;
        }

        public static byte[] LeftPad(this byte[] value, int totalLength, byte paddingChar)
        {
            if (null == value || (value.Length == totalLength || totalLength <= 0) || totalLength < value.Length)
                return value;
            byte[] numArray = new byte[totalLength];
            value.CopyTo((Array)numArray, totalLength - value.Length);
            for (int index = 0; index < totalLength - value.Length; ++index)
                numArray[index] = paddingChar;
            return numArray;
        }


        /// <summary>
        /// Equalses the specified value.
        /// </summary>
        /// <param name="source">The value.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
        public static bool Equals(this byte[] source, byte[] destination)
        {
            if (null == source || null == destination)
                return false;
            if (source.Length != destination.Length)
                return false;
            return source.Equals(0, destination, 0, source.Length);
        }

        /// <summary>
        /// Equalses the specified value.
        /// </summary>
        /// <param name="source">The value.</param>
        /// <param name="sourceOffSet">The SRC off set.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="destinationOffSet">The DST off set.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static bool Equals(this byte[] source, int sourceOffSet, byte[] destination, int destinationOffSet, int length)
        {
            if (null == source || null == destination)
                return false;
            //if (value.Length != destination.Length)
            //    return false;
            if (length > destination.Length)
                return false;
            if (length > source.Length)
                return false;
            if (sourceOffSet > source.Length)
                return false;
            if (destinationOffSet > destination.Length)
                return false;
            if (sourceOffSet + length > source.Length)
                return false;
            if (destinationOffSet + length > destination.Length)
                return false;
            for (int i = 0; i < length; i++)
            {
                if (source[i + sourceOffSet] != destination[i + destinationOffSet])
                    return false;
            }
            return true;
        }

        public static string UnPack(this byte[] binaryData, short offSet, short length)
        {
            return UnPack(binaryData.SubVector(offSet, length));
        }

        public static string UnPack(this byte[] binaryData, int maxLineLength)
        {
            string s = UnPack(binaryData);
            int len = s.Length;
            for (int i = 0; i < len / maxLineLength; i++)
                s = s.Insert((i + 1) * maxLineLength + i * 2, "\r\n");
            return s;
        }

        /// <summary>
        /// Concatenates two arrays of same element type
        /// </summary>
        /// <remarks>
        /// All elements of source arrays are copied in the result.
        /// If two arrays are null, null returned.
        /// If one of the parameter is null, then other array is returned.
        /// Zero length arrays operates as null.
        /// </remarks>
        /// <exception cref="NotSupportedException">
        /// If array ranks are not equal to one
        /// </exception>
        /// <exception cref="ArrayTypeMismatchException">
        /// If two array types have not same element type
        /// </exception>
        /// <param name="mainArray">First array</param>
        /// <param name="tailArray">Second array</param>
        /// <returns>New tailed array</returns>
        public static byte[] TailTo(this byte[] tailArray, byte[] mainArray)
        {
            byte[] tailedArray;

            // Be sure at least one array exists, at least with one element
            if ((mainArray == null || mainArray.Length == 0) &&
                (tailArray == null || tailArray.Length == 0))
                return mainArray;

            // Return the main array if tail array not exists
            if (tailArray == null || tailArray.Length == 0)
            {
                return mainArray;
            }
            // Return the tail array if main array not exists
            if (mainArray == null || mainArray.Length == 0)
            {
                return tailArray;
            }

            // Check the array rank for a vector
            if (mainArray.Rank != 1 || tailArray.Rank != 1)
                throw (new NotSupportedException("Vector sýnýfý çok boyutlu dizileri desteklemiyor"));


            // Create a new array with total size of two arrays
            tailedArray = new byte[mainArray.Length + tailArray.Length];

            // Copy source elements to tailed array
            Buffer.BlockCopy(mainArray, 0, tailedArray, 0, mainArray.Length);
            Buffer.BlockCopy(tailArray, 0, tailedArray, mainArray.Length, tailArray.Length);
            return tailedArray;
        }

        ///// <summary>
        ///// Concatenates several count of byte arrays with fast Buffer.BlockCopy method
        ///// </summary>
        ///// <param name="vectors">Vectors to be concatenated in byte[] form</param>
        ///// <returns></returns>
        //public static byte[] TailTo(params byte[] vectors)
        //{
        //    if (vectors.Length == 0)
        //        return null;

        //    int totalLength = 0;
        //    for (int i = 0; i < vectors.Length; i++)
        //    {
        //        totalLength += vectors[i];
        //    }
        //    if (totalLength == 0)
        //        return null;

        //    byte[] data = new byte[totalLength];

        //    int actualOffset = 0;
        //    foreach (byte[] vector in vectors)
        //    {
        //        Buffer.BlockCopy(vector, 0, data, actualOffset, vector.Length);
        //        actualOffset += vector.Length;
        //    }
        //    return data;
        //}

        /// <summary>
        /// TailTo one byte to a given array
        /// </summary>
        /// <param name="sourceBytes">Array to be tailed</param>
        /// <param name="tailByte">Byte value to tail</param>
        /// <returns></returns>
        public static byte[] TailTo(this byte[] sourceBytes, byte tailByte)
        {
            // Define array to return constructed data
            byte[] tailedBytes;
            if (sourceBytes != null && sourceBytes.Length != 0)
            {
                // Initialize array one byte longer than original
                tailedBytes = new byte[sourceBytes.Length + 1];
                // Copy all bytes
                sourceBytes.CopyTo(tailedBytes, 0);
                // Add new byte
                tailedBytes[sourceBytes.Length] = tailByte;
            }
            else
            {
                // Construct a one byte array
                tailedBytes = new byte[1];
                tailedBytes[0] = tailByte;
            }
            return tailedBytes;
        }

        /// <summary>
        /// Subs the vector.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="start">The start.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static byte[] SubVector(this byte[] array, int start, int length)
        {
            // Check the existance of array and element to be tailed
            if ((array == null || array.Length == 0)) return array;
            // Check for invalid length special conditions.
            if (0x00 == length)
                return new byte[] { };
            // Check for invalid start position
            if (start < 0 || start >= array.Length)
                throw (new ArgumentException("wrong index", "start"));
            // Check for invalid length
            if (length < 0)
                throw (new ArgumentException("wrong length", "length"));

            byte[] subArray = new byte[length];
            Buffer.BlockCopy(array, start, subArray, 0, length);
            return subArray;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        public static String UTF8ToString(this Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Adds the PKCS 5 padding to message
        /// PKCS#5 padding works as follows: the bytes remaining to fill a block are assigned a number, 
        /// which is the number of bytes that were added to fill the block. 
        /// For instance, if we have an 16-byte block, and only 11 bytes are filled, 
        /// then we have 5 bytes to pad. 
        /// Those 5 bytes are all assigned the value "5", for the 5 bytes of padding.
        /// </summary>
        /// <param name="message">The message to be padded</param>
        /// <returns>padded data</returns>
        public static byte[] PadWithPkcs5(this byte[] message)
        {
            int gap = message.Length % 8;
            byte filler = (byte)(8 - gap);
            for (int i = 0; i < 8 - gap; i++)
            {
                message = message.TailTo(filler);
            }
            return message;
        }

    }
}