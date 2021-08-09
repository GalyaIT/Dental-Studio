namespace DentalStudio.Common
{
    public class Constraints
    {
        public const int UsernameMaxLength = 256;

        public const int FirstNameMinLength = 3;
        public const int FirstNameMaxLength = 50;

        public const int LastNameMinLength = 3;
        public const int LastNameMaxLength = 50;

        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const int SpecialtyMinLength = 5;
        public const int SpecialtyMaxLength = 30;

        public const int AddressMinLength = 3;
        public const int AddressMaxLength = 100;

        public const int GradeMaxLength = 2000;

        public const int ProcedureMinLength = 10;
        public const int ProcedureMaxLength = 300;

        public const string MinPrice = "0.01";
        public const string MaxPrice = "79228162514264337593543950335";

        public const int TitleMinLength = 5;
        public const int TitleMaxLength = 100;

        public const int ContentMinLength = 20;
        public const int ContentMaxLength = 10000;

        // PostsController
        public const int ItemsPerPage = 4;
    }
}
