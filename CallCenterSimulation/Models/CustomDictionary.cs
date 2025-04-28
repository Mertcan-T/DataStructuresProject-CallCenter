using System;

namespace CallCenterSimulation.Models
{
    // Anahtar - Değer çifti
    public class AnahtarDeger<TAnahtar, TDeger>
    {
        public TAnahtar Anahtar;
        public TDeger Deger;
        public AnahtarDeger<TAnahtar, TDeger> Ileri;

        public AnahtarDeger(TAnahtar anahtar, TDeger deger)
        {
            Anahtar = anahtar;
            Deger = deger;
            Ileri = null;
        }
    }

    // Basit bir Dictionary yapısı
    public class CustomDictionary<TAnahtar, TDeger>
    {
        private AnahtarDeger<TAnahtar, TDeger> bas;

        public CustomDictionary()
        {
            bas = null;
        }

        // Ekleme
        public void Ekle(TAnahtar anahtar, TDeger deger)
        {
            AnahtarDeger<TAnahtar, TDeger> yeni = new AnahtarDeger<TAnahtar, TDeger>(anahtar, deger);

            if (bas == null)
            {
                bas = yeni;
            }
            else
            {
                AnahtarDeger<TAnahtar, TDeger> temp = bas;
                while (temp.Ileri != null)
                {
                    temp = temp.Ileri;
                }
                temp.Ileri = yeni;
            }
        }

        // Anahtar ile Değer bulma
        public TDeger Bul(TAnahtar anahtar)
        {
            AnahtarDeger<TAnahtar, TDeger> temp = bas;
            while (temp != null)
            {
                if (temp.Anahtar.Equals(anahtar))
                    return temp.Deger;

                temp = temp.Ileri;
            }
            throw new Exception("Anahtar bulunamadı.");
        }

        // Listeleme
        public void Listele()
        {
            AnahtarDeger<TAnahtar, TDeger> temp = bas;
            while (temp != null)
            {
                Console.WriteLine($"{temp.Anahtar} : {temp.Deger}");
                temp = temp.Ileri;
            }
        }
    }
}
