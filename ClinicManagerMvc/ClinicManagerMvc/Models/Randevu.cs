namespace ClinicManagerMvc.Models
{
    public class Randevu
    {
        public int RandevuId { get; set; }
        public int PoliklinikId { get; set; }
        public Poliklinik Poliklinik { get; set; }

        public int HastaId { get; set; }
        public int DoktorId { get; set; }

        public DateTime Tarih { get; set; }
        public TimeSpan Saat { get; set; }
    }
}
