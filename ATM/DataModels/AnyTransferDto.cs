using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DataModels
{
    class AnyTransferDto
    {
        public string Discriminator { get; set; }   // either "Interraccount Transaction" or "Mobile Replenishment"
        public string PartnerInfo { get; set; }     // either name of sender/receiever or tel number
        public bool IsIncome { get; set; }          // false if transfer was from the acc, in case of Mobile Replenishment always false
        public double Amount { get; set; }
        public DateTime DatePerformed { get; set; }
    }
}
