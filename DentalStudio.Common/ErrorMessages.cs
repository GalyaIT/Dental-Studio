namespace DentalStudio.Common
{
    public class ErrorMessages
    {
        //public const string RequiredErrorMessage = "Please fill in \"{0}\".";

        //public const string ValidErrorMessage = "Please fill valid \"{0}\".";

        //public const string CompareErrorMessage = "\"{1}\" and \"{0}\" do not match.";

        //public const string StringLengthErrorMessage = "\"{0}\" must be at least {2} and at max {1} characters long.";

        //public const string MaxLengthErrorMessage = "\"{0}\" can be maximum {1} characters long.";

        //public const string RangeErrorMessage = "\"{0}\" must be between {1} and {2}.";

        public const string RequiredErrorMessage = "Моля въведете \"{0}\".";

        public const string ValidErrorMessage = "Моля въведете валиден \"{0}\".";

        public const string InValidErrorMessage = "Невалиден \"{0}\".";

        public const string CompareErrorMessage = "Паролите не съвпадат.";

        public const string StringLengthErrorMessage = "\"{0}\" трябва да е поне {2} и не повече от {1} символа.";

        public const string MaxLengthErrorMessage = "\"{0}\" трябва да бъде не повече от {1} символа.";

        public const string PriceErrorMessage = "\"{0}та\" трябва да e положително число.";

        // Patient messages
        public const string UpdateErrorMessage = "You have to fill required input fields!";

        // Appointments messages
        public const string CreateErrorMessage = "Date or time are busy. Try again.";
        public const string AppointmentCreateErrorMessage = "Date or time for D-r. \"{0}\" are busy. Try again.";
        public const string EditErrorMessage = "Date or time for D-r. \"{0}\" are busy. Try again.";
    }
}
