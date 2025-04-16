using System.Text;

namespace Utils;

public class LocalizationHelper
{
    public static string ToCyrillicLetters(string str)
    {
        var sb = new StringBuilder(str);
        for (var i = 0; i < str.Length; i++)
        {
            if (IsLatinLetter(str[i]))
            {
                char cyrillicLetter = LatinToCyrillicMap[str[i]];
                sb[i] = cyrillicLetter;
            }
        }
        return sb.ToString();
    }

    private static bool IsLatinLetter(char c) => char.IsLetter(c) && c is >= 'A' and <= 'Z' || c is >= 'a' and <= 'z';

    private static readonly IReadOnlyDictionary<char, char> LatinToCyrillicMap = new Dictionary<char, char>
    {
        { 'A', 'А' },
        { 'B', 'В' },
        { 'E', 'Е' },
        { 'K', 'К' },
        { 'M', 'М' },
        { 'H', 'Н' },
        { 'O', 'О' },
        { 'P', 'Р' },
        { 'C', 'С' },
        { 'T', 'Т' },
        { 'Y', 'У' },
        { 'X', 'Х' }
    };
}