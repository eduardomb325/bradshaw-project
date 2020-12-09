using BradshawProject.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Objects
{
    public class LastTransaction
    {
        public bool Approved { get; set; } = false;

        public double NewLimit { get; set; } = 0;

        public List<string> DeniedReasons { get; set; } = new List<string>();

        public LastTransaction()
        {

        }

        public void UpdateNewLimit(double limit)
        {
            NewLimit = limit;
        }
    }
}
