namespace Agrisustain_Jamaica.Models
{
    public class order
    {
        public int id { get; set; }
        public DateTime o_date { get; set; }
        public string cust_name { get; set; }
        public string cust_addr { get; set; }
        public string cust_em {  get; set; }
        public string data { get; set; }
        public string card_type { get; set; }
        public string card_num { get; set; }
    }
}
