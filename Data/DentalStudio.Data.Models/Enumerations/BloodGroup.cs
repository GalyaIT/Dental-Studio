using System.ComponentModel.DataAnnotations;

namespace DentalStudio.Data.Models.Enumerations
{
    public enum BloodGroup
    {
        [Display(Name = "0+")]
        OPositive = 1,
        [Display(Name = "A+")]
        APositive = 2,
        [Display(Name = "B+")]
        BPositive = 3,
        [Display(Name = "AB+")]
        ABPositive = 4,
        [Display(Name = "0-")]
        ONegative = 5,
        [Display(Name = "A-")]
        ANegative = 6,
        [Display(Name = "B-")]
        BNegative = 7,
        [Display(Name = "AB-")]
        ABNegative = 8,
    }
}
