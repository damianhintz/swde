using System;

namespace swde
{
    static class KontrolerKontekstu
    {
        public static void Zapewnij(bool warunek, string przyczyna)
        {
            if (!warunek)
            {
                throw new InvalidOperationException(
                    string.Format("Określona operacja nie może zostać wykonana. {0}", przyczyna));
            }
        }
    }
}
