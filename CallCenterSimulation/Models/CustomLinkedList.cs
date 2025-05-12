using System;

namespace CallCenterSimulation.Models
{
    // Eleman (Node) yapısı
    public class LinkedListEleman<T>
    {
        public T Veri;
        public LinkedListEleman<T> Ileri;

        public LinkedListEleman(T veri)
        {
            Veri = veri;
            Ileri = null;
        }
    }

    // Bağlı Liste sınıfı
    public class CustomLinkedList<T>
    {
        private LinkedListEleman<T> bas;

        public CustomLinkedList()
        {
            bas = null;
        }

        // Liste boş mu?
        public bool ListeBos()
        {
            return bas == null;
        }

        // Listeye eleman ekle (En sona ekle)
        public void Ekle(T veri)
        {
            LinkedListEleman<T> yeni = new LinkedListEleman<T>(veri);

            if (ListeBos())
            {
                bas = yeni;
            }
            else
            {
                LinkedListEleman<T> temp = bas;
                while (temp.Ileri != null)
                {
                    temp = temp.Ileri;
                }
                temp.Ileri = yeni;
            }
        }

        // Elemanları ekrana yazdırmak için
        public void Listele()
        {
            LinkedListEleman<T> temp = bas;
            while (temp != null)
            {
                Console.WriteLine(temp.Veri);
                temp = temp.Ileri;
            }
        }

        // Eleman sayısı
        public int ElemanSayisi()
        {
            int sayac = 0;
            LinkedListEleman<T> temp = bas;
            while (temp != null)
            {
                sayac++;
                temp = temp.Ileri;
            }
            return sayac;
        }
    }
}
