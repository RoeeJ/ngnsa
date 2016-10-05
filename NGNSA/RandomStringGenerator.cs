// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomStringGenerator.cs" company="">
//   Nero Sule
// </copyright>
// <summary>
//   Class for random string generation. Does not include ambiguous characters like
//   I, 1 and l.
//   Every four characters include one lower case character, one upper case character,
//   one number and a special symbol in a random order. String will start with
//   an alphanumeric character.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NGNSA
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Class for random string generation. Does not include ambiguous characters like
    /// I, 1 and l.
    /// Every four characters include one lower case character, one upper case character,
    /// one number and a special symbol in a random order. String will start with
    /// an alphanumeric character.
    /// </summary>
    public static class RandomStringGenerator
    {
        // Default min and max string lengths

        // Supported characters divided into groups.
        #region Constants and Fields

        /// <summary>
        /// The characters lowercase.
        /// </summary>
        private const string CharactersLowercase = "abcdefgijkmnopqrstwxyz";

        /// <summary>
        /// The characters numeric.
        /// </summary>
        private const string CharactersNumeric = "23456789";

        /// <summary>
        /// The characters special.
        /// </summary>
        private const string CharactersSpecial = "*$-+?_&=!%@";

        /// <summary>
        /// The characters uppercase.
        /// </summary>
        private const string CharactersUppercase = "ABCDEFGHJKLMNPQRSTWXYZ";

        /// <summary>
        /// The default max string length.
        /// </summary>
        private const int DefaultMaxStringLength = 10;

        /// <summary>
        /// The default min string length.
        /// </summary>
        private const int DefaultMinStringLength = 8;

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates random string. Length will be determined at random.
        /// </summary>
        /// <returns>
        /// Randomly generated string
        /// </returns>
        public static string Generate()
        {
            return Generate(DefaultMinStringLength, DefaultMaxStringLength);
        }

        /// <summary>
        /// Generates a random string of specific length
        /// </summary>
        /// <param name="length">
        /// Length of string
        /// </param>
        /// <returns>
        /// Randomly generated string
        /// </returns>
        public static string Generate(int length)
        {
            return Generate(length, length);
        }

        /// <summary>
        /// Generates a random string. Length is random depending on max and min length.
        /// </summary>
        /// <param name="minLength">
        /// Minimum string length
        /// </param>
        /// <param name="maxLength">
        /// Maximum string length
        /// </param>
        /// <returns>
        /// Randomly generated string
        /// </returns>
        public static string Generate(int minLength, int maxLength)
        {
            // Parameter validation
            if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
            {
                return null;
            }

            // Local array of all supported characters grouped by type
            var charGroups = new[]
                {
                    CharactersLowercase.ToCharArray(), CharactersUppercase.ToCharArray(), CharactersNumeric.ToCharArray(),
                    CharactersSpecial.ToCharArray()
                };

            // Array used to track the number of remaining characters in each group
            var charsLeftInGroup = new int[charGroups.Length];

            // No characters are used initially
            for (int i = 0; i < charsLeftInGroup.Length; i++)
            {
                charsLeftInGroup[i] = charGroups[i].Length;
            }

            // Used to iterate through unused character groups
            var leftGroupsOrder = new int[charGroups.Length];

            // 'Really strong' randomizer
            for (int i = 0; i < leftGroupsOrder.Length; i++)
            {
                leftGroupsOrder[i] = i;
            }

            // 4-byte array to fill with random bytes and convert into integer
            var randomBytes = new byte[4];

            // Generates 4 random bytes
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert those 4 bytes into 32 bit integer
            int seed = (randomBytes[0] & 0x7f) << 24 | randomBytes[1] << 16 | randomBytes[2] << 8 | randomBytes[3];

            var random = new Random(seed);

            // String array
            char[] str = null;

            // Allocate memory for the string
            if (minLength < maxLength)
            {
                str = new char[random.Next(minLength, maxLength)];
            }
            else
            {
                str = new char[minLength];
            }

            // Index of next character to be added to password
            int nextCharIndex;

            // Index of next group to process
            int nextGroupIndex;

            // Index used to tracek unprocessed character groups
            int nextLeftGroupsOrderIndex;

            // Index of the last unprocessed character in group
            int lastCharIndex;

            // Index of last unprocessed group
            int lastLeftGroupsOrderIndex = leftGroupsOrder.Length - 1;

            // Generate string one character at a time
            for (int i = 0; i < str.Length; i++)
            {
                if (lastLeftGroupsOrderIndex == 0)
                {
                    nextLeftGroupsOrderIndex = 0;
                }
                else
                {
                    nextLeftGroupsOrderIndex = random.Next(0, lastLeftGroupsOrderIndex);
                }

                nextGroupIndex = leftGroupsOrder[nextLeftGroupsOrderIndex];
                lastCharIndex = charsLeftInGroup[nextGroupIndex] - 1;

                // Get the index of the last unprocessed character in the group
                if (lastCharIndex == 0)
                {
                    nextCharIndex = 0;
                }
                else
                {
                    nextCharIndex = random.Next(0, lastCharIndex);
                }

                str[i] = charGroups[nextGroupIndex][nextCharIndex];

                if (lastCharIndex == 0)
                {
                    charsLeftInGroup[nextGroupIndex] = charGroups[nextGroupIndex].Length;
                }
                else
                {
                    if (lastCharIndex != nextCharIndex)
                    {
                        char temp = charGroups[nextGroupIndex][lastCharIndex];
                        charGroups[nextGroupIndex][lastCharIndex] = charGroups[nextGroupIndex][nextCharIndex];
                        charGroups[nextGroupIndex][nextCharIndex] = temp;
                    }

                    charsLeftInGroup[nextGroupIndex]--;
                }

                if (lastLeftGroupsOrderIndex == 0)
                {
                    lastLeftGroupsOrderIndex = leftGroupsOrder.Length - 1;
                }
                else
                {
                    if (lastLeftGroupsOrderIndex != nextLeftGroupsOrderIndex)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIndex];
                        leftGroupsOrder[lastLeftGroupsOrderIndex] = leftGroupsOrder[nextLeftGroupsOrderIndex];
                        leftGroupsOrder[nextLeftGroupsOrderIndex] = temp;
                    }

                    lastLeftGroupsOrderIndex--;
                }
            }

            return new string(str);
        }

        #endregion
    }
}