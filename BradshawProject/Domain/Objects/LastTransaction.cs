using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Objects
{
    public class LastTransaction : Transaction
    {
        public bool Approved { get; set; }

        public double NewLimit { get; set; }

        public List<string> DeniedReasons { get; set; }

        public LastTransaction()
        {

        }
    }
}
