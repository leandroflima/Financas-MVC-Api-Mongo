using FinancasMVC.Models.Enum;

namespace FinancasMVC.Models
{
    public class Account : ModelBase
    {
        public int Bank { get; set; }

        public string Agency { get; set; }

        public string Number { get; set; }

        public AccountStatus Status { get; set; }

        public AccountType Type { get; set; }
    }
}
