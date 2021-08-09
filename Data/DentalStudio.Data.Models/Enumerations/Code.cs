namespace DentalStudio.Data.Models.Enumerations
{
    using System.ComponentModel.DataAnnotations;

    public enum Code
    {
        [Display(Name = "101")]
        HundredOne = 1,

        [Display(Name = "103")]
        HundredThree = 2,

        [Display(Name = "301")]
        ThreeHundredOne = 3,

        [Display(Name = "508")]
        FiveHundredEight = 4,

        [Display(Name = "509")]
        FiveHundredNine = 5,

        [Display(Name = "332")]
        ThreeHundredThirtyTwo = 6,

        [Display(Name = "333")]
        ThreeHundredThirtyThree = 7,
    }
}
