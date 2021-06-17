using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebLab5.Models
{
    public class Patients
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Diagnosis is required")]
        [Display(Name = "Diagnosis")]
        public string Diagnosis { get; set; }

        [Required(ErrorMessage = "Diagnosis is required")]
        [Display(Name = "Age")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Phones")]
        [DataType(DataType.PhoneNumber)]
        public string Phones { get; set; }
    }
}
