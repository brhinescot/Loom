#region Using Directives

using System.Collections.Generic;
using Loom.Security;

#endregion

namespace Loom.Data
{
    public class RandomData
    {
        private readonly CryptoRandom random;

        private RandomData(string format)
        {
            random = new CryptoRandom(format);
        }

        public static RandomData Email
        {
            get
            {
                RandomData data = new RandomData("{g:10}@{g:8}.com");
                return data;
            }
        }

        public static RandomData SocialSecurityNumber
        {
            get
            {
                RandomData data = new RandomData("{N:3}-{N:2}-{N:4}");
                return data;
            }
        }

        public static RandomData PhoneNumber
        {
            get
            {
                RandomData data = new RandomData("({N:3}){N:3}-{N:4}");
                return data;
            }
        }

        public static RandomData Street
        {
            get
            {
                RandomData data = new RandomData("{N:4} {A:1} {A:9} {A:2}, Apt {N:3}");
                return data;
            }
        }

        public IEnumerable<string> Generate(int count)
        {
            for (int i = 0; i < count; i++)
                yield return random.Generate();
        }

        public string Generate()
        {
            return random.Generate();
        }
    }
}