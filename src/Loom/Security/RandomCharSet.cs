#region Using Directives

using System;
using System.Diagnostics;

#endregion

namespace Loom.Security
{
    /// <summary>
    /// </summary>
    public static class RandomCharSet
    {
        private const string Captcha = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghkmnopsuvwxyz23456789@#&=";
        private const string LowercaseCharacters = "abcdefghijkmnopqrstuvwxyz";

        // Uppercase characters. No uppercase I or O for readability in some fonts.

        // Numbers.
        private const string Numbers = "123456789";

        private const string SpecialCharacters = "+?*$()!@#%&_=:;<>,";
        private const string UppercaseCharacters = "ABCDEFGHJKLMNPQRSTUVWXYZ";

        private static readonly Random Random = new Random(new Random().Next(Process.GetCurrentProcess().Id));

        /// <summary>
        ///     Gets a new random character in the  <see cref="CharacterSet" /> specified.
        /// </summary>
        /// <param name="characters">
        ///     The <see cref="CharacterSet" /> to retrieve characters from.
        /// </param>
        /// <returns></returns>
        public static char Next(CharacterSet characters = CharacterSet.Lower)
        {
            return PrivateNext(characters);
        }

        private static char PrivateNext(CharacterSet characters)
        {
            char randomChar;
            switch (characters)
            {
                case CharacterSet.All:
                    randomChar = RetrieveRandomCharacter(Random.Next(0, 4));
                    break;
                case CharacterSet.Alphanumeric:
                    randomChar = RetrieveRandomCharacter(Random.Next(0, 3));
                    break;
                case CharacterSet.Letter:
                    randomChar = RetrieveRandomCharacter(Random.Next(0, 2));
                    break;
                case CharacterSet.Special:
                    randomChar = RetrieveRandomCharacter(3);
                    break;
                case CharacterSet.Number:
                    randomChar = RetrieveRandomCharacter(2);
                    break;
                case CharacterSet.Upper:
                    randomChar = RetrieveRandomCharacter(1);
                    break;
                case CharacterSet.Captcha:
                    randomChar = RetrieveRandomCharacter(4);
                    break;
                default:
                    randomChar = RetrieveRandomCharacter(0);
                    break;
            }
            return randomChar;
        }

        private static char RetrieveRandomCharacter(int index)
        {
            char randomChar;
            switch (index)
            {
                case 0:
                    randomChar = LowercaseCharacters[Random.Next(0, LowercaseCharacters.Length)];
                    break;
                case 1:
                    randomChar = UppercaseCharacters[Random.Next(0, UppercaseCharacters.Length)];
                    break;
                case 2:
                    randomChar = Numbers[Random.Next(0, Numbers.Length)];
                    break;
                case 3:
                    randomChar = SpecialCharacters[Random.Next(0, SpecialCharacters.Length)];
                    break;
                case 4:
                    randomChar = Captcha[Random.Next(0, Captcha.Length)];
                    break;
                default:
                    randomChar = LowercaseCharacters[Random.Next(0, LowercaseCharacters.Length)];
                    break;
            }
            return randomChar;
        }

        // Lowercase characters. No lowercase L for readability in some fonts.

        // Captcha.
    }
}