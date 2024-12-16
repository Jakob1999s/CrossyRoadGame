using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad
{
    class RandomNumber
    {
        public static int GenerateSecureRandomNumber(int min, int max)
        {
            byte[] randomBytes = new byte[4];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            // Konvertiere die Bytes in eine Ganzzahl
            int value = BitConverter.ToInt32(randomBytes, 0) & int.MaxValue;

            // Passe an den gewünschten Bereich an
            return min + (value % (max - min));
        }

    }
}
