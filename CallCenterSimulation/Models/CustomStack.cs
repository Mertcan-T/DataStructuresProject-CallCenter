using System;

namespace CallCenterSimulation.Models
{
    // Eleman (Node) yapısı
    public class StackEleman<T>
    {
        public T Veri;
        public StackEleman<T> Ileri;

        public StackEleman(T veri)
        {
            Veri = veri;
            Ileri = null;
        }
    }

    // Stack (Yığın) sınıfı
    public class CustomStack<T>
    {
        private StackEleman<T> tepe; // Stack'in tepe elemanı

        public CustomStack()
        {
            tepe = null;
        }

        // Yığın boş mu?
        public bool StackBos()
        {
            return tepe == null;
        }

        // Yığına eleman ekle (Push)
        public void Push(T veri)
        {
            StackEleman<T> yeni = new StackEleman<T>(veri);
            yeni.Ileri = tepe;
            tepe = yeni;
        }

        // Yığından eleman çıkar (Pop)
        public T Pop()
        {
            if (StackBos())
                throw new InvalidOperationException("Stack boş!");

            T veri = tepe.Veri;
            tepe = tepe.Ileri;
            return veri;
        }

        // Yığının tepesindeki elemanı göster (Peek)
        public T Peek()
        {
            if (StackBos())
                throw new InvalidOperationException("Stack boş!");

            return tepe.Veri;
        }

        // Yığındaki eleman sayısı
        public int ElemanSayisi()
        {
            int sayac = 0;
            StackEleman<T> temp = tepe;
            while (temp != null)
            {
                sayac++;
                temp = temp.Ileri;
            }
            return sayac;
        }
    }
}
