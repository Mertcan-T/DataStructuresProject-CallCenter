namespace CallCenterSimulation.Models
{
    public class Customer
    {
        public int Id { get; set; }              // Müşteri ID
        public string Ad { get; set; }           // Müşteri adı
        public string Talep { get; set; }        // Müşteri talebi
        public int SiraNumarasi { get; set; }    // Müşterinin kuyruktaki sıra numarası
    }
}
