namespace ExchangeOfCurrencies.Data.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; } = new();
        public int CredentialsId { get; set; }
        public Credentials Credentials { get; set; } = new();

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Client other)
            {
                return ClientId == other.ClientId &&
                    FullName.Equals(other.FullName) &&
                    Email.Equals(other.Email) &&
                    Phone.Equals(other.Phone);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClientId, FullName, Email, Phone);
        }
    }
}
