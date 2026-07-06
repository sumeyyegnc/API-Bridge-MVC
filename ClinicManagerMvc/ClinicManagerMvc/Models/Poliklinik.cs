namespace ClinicManagerMvc.Models
{
    public class Poliklinik
    {

        public int PoliklinikId { get; set; }
        public string Ad { get; set; }

        public List<Randevu> Randevular { get; set; }
    }
}
