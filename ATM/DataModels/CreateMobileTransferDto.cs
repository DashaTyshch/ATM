using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.DataModels
{
    class CreateMobileTransferDto
    {
        public double Amount { get; set; }
        public string FromId { get; set; }
        public string PhNum { get; set; }
    }
}
