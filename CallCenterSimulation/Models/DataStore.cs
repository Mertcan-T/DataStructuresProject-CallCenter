namespace CallCenterSimulation.Models
{
    public static class DataStore
    {
        public static Kuyruk<Customer> MusteriKuyrugu = new Kuyruk<Customer>();
        public static Dictionary<int, Customer> AktifMusteriler = new Dictionary<int, Customer>(); // Key olarak Id kullanalım
        public static Stack<string> IslemGecmisi = new Stack<string>(); // İşlem geçmişi
        public static LinkedList<string> TemsilciLoglari = new LinkedList<string>(); // Temsilci logları
    }
}
