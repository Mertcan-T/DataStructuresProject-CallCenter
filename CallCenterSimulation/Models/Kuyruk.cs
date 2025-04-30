using System;
using System.Collections.Generic;

namespace CallCenterSimulation.Models
{
    // Eleman sınıfı (Node yapısı)
    public class Eleman<T>
    {
        public T Veri;            // Veri tutacak
        public Eleman<T> Ileri;    // Sonraki elemana işaret eder

        public Eleman(T veri)
        {
            Veri = veri;
            Ileri = null;
        }
    }

    // Kuyruk sınıfı
    public class Kuyruk<T>
    {
        private Eleman<T> bas; // Kuyruğun başı
        private Eleman<T> son; // Kuyruğun sonu

        public Kuyruk()
        {
            bas = null;
            son = null;
        }

        // Kuyruk boş mu?
        public bool KuyrukBos()
        {
            return bas == null;
        }

        // Kuyruğa eleman ekle (En sona eklenir)
        public void KuyrugaEkle(T veri)
        {
            Eleman<T> yeni = new Eleman<T>(veri);

            if (KuyrukBos())
            {
                bas = yeni;
                son = yeni;
            }
            else
            {
                son.Ileri = yeni;
                son = yeni;
            }
        }

        // Kuyruktan eleman çıkar (Baştan çıkarılır)
        public T KuyrukSil()
        {
            if (KuyrukBos())
                throw new InvalidOperationException("Kuyruk boş!");

            Eleman<T> sonuc = bas;
            bas = bas.Ileri;

            if (bas == null) // Eğer kuyruk tamamen boşaldıysa
                son = null;

            return sonuc.Veri;
        }

        // Kuyruğun başındaki elemanı sadece görmek
        public T Peek()
        {
            if (KuyrukBos())
                throw new InvalidOperationException("Kuyruk boş!");

            return bas.Veri;
        }

        // Kuyruktaki eleman sayısı
        public int ElemanSayisi()
        {
            int sayac = 0;
            Eleman<T> temp = bas;
            while (temp != null)
            {
                sayac++;
                temp = temp.Ileri;
            }
            return sayac;
        }

        // Kuyruktaki tüm elemanları liste olarak döner
        public List<T> ElemanlariGetir()
        {
            List<T> liste = new List<T>();
            Eleman<T> temp = bas;
            while (temp != null)
            {
                liste.Add(temp.Veri);
                temp = temp.Ileri;
            }
            return liste;
        }
        // Verilen müşterinin sıra numarasını (indeksini) döner (1'den başlar)
        public int SiraNumarasiniGetir(T veri)
        {
            int sira = 1;
            Eleman<T> temp = bas;

            while (temp != null)
            {
                if (temp.Veri.Equals(veri))
                {
                    return sira;
                }
                sira++;
                temp = temp.Ileri;
            }

            return -1; // Bulunamadıysa
        }

    }
}
