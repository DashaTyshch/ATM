using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ATM.DataModels
{
    class Account
    {
        public string AccountNumber { get; set; }
        public double Balance { get; set; }
        public bool IsBlocked { get; set; }
    }
}
