namespace ZABCareersAPIs.Models
{
    public class EmailAccount
    {
        public int EmailAccountId { get; set; }

        public string EmailHost { get; set; }

        public int EmailPort { get; set; }

        public string EmailUsername { get; set; }

        public string EmailPassword { get; set; }

        public bool IsDefault { get; set; }

        public int EmailAccountStatus { get; set; }
    }
}
