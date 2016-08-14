using System;
using System.Security.Cryptography;
using System.Text;

namespace Giacomelli.Unity.Metadata.Infrastructure.Framework.Serialization
{
	/// <summary>
	/// File identifier util.
	/// </summary>
    public static class FileIdUtil
    {
		/// <summary>
		/// Get the file id from specified type.
		/// </summary>
		/// <returns>The type.</returns>
		/// <param name="type">Type.</param>
        public static int FromType(Type type)
        {
            // http://forum.unity3d.com/threads/yaml-fileid-hash-function-for-dll-scripts.252075/#post-1695479
            string toBeHashed = "s\0\0\0" + type.Namespace + type.Name;

            using (HashAlgorithm hash = new MD4())
            {
                byte[] hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));

                int result = 0;

                for (int i = 3; i >= 0; --i)
                {
                    result <<= 8;
                    result |= hashed[i];
                }

                return result;
            }
        }
    }
}