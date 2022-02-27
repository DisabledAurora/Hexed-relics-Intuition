using System.ComponentModel.DataAnnotations;

namespace Hexed_Relics.Models
{
    public class Transactions
    {
        [Key]
        public int? Id { get; set; }
        public float? Amount { get; set; }
        public int? AccNumber { get; set; }
        public DateTime? DtTst { get; set; }
        public bool? IsDeposit { get; set; }

        public Transactions() { }
        public Transactions(float a, int accn, DateTime? dt, bool d)
        {
            Amount = a;
            AccNumber = accn;
            DtTst = dt;
            IsDeposit = d;
        }
    }
}
