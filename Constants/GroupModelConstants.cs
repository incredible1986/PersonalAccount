using PersonalAccount.Models;
using PersonalAccount.Utils;

namespace PersonalAccount.Constants;

public static class GroupModelConstants
{
    public static readonly GroupModel NoGroup = new()
    {
        Name = "Без группы",
        Description = "",
        ImageUrl =
            "https://img.magnific.com/free-photo/lanscape-lake-sunlight_395237-270.jpg?semt=ais_hybrid&w=740&q=80"
                .ToUri()
    };
}