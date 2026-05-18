namespace PersonalAccount.Models.Students
{
    public class StudentAuthModel : StudentModel
    {
        public string PasswordHash { get; set; } = string.Empty;
    }
}

// Client Auth [Email, Password] -> Server [Email, bycript(Password)] INTERNAL USE

// Пльзователь вводит логин и пароль
// логин - открытая информация
// пароль - приватная

// Сервер проверят, что пользователь с таким логином существует, и что его пароль именно такой, как введенный
// В БД вместо пароля лежит хэш от пароля