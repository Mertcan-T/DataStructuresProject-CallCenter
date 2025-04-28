using System;
namespace CallCenterSimulation.Models
{
    public static class DataStore
    {
        // Kuyruk veri yapımız
        public static Kuyruk<Customer> MusteriKuyrugu = new Kuyruk<Customer>();

        // Müşteri temsilcileri listesi
        public static CustomLinkedList<Representative> Temsilciler = new CustomLinkedList<Representative>();

        // Aktif müşteriler için dictionary (Müşteri ID -> Müşteri)
        public static CustomDictionary<int, Customer> AktifMusteriler = new CustomDictionary<int, Customer>();

        // İşlem geçmişi için stack
        public static CustomStack<string> IslemlerGecmisi = new CustomStack<string>();
    }
}
