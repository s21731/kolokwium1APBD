using System;
using System.Collections.Generic;

namespace Kolokwium01.Models
{
    public class Medicament
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public IEnumerable<Prescription> Prescriptions { get; set; }


    }
}
