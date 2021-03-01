namespace keepr.Models
{
    public class Keep
    {
        public int id { get; set; }
        public string creatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string img { get; set; }
        public int views { get; set; }
        //through external social media
        public int shares { get; set; }
        //how many vaults it's in
        public int keeps { get; set; }
        public Profile creator { get; set; }
    }

        public class KeepVaultViewModel : Keep
    {
        public int vaultKeepId{ get; set;}
    }
}