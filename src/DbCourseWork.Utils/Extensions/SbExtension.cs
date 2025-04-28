using System.Text;

namespace Utils.Extensions;

public static class SbExtension
{
    public static StringBuilder EndSql(this StringBuilder sb)
    {
        if (sb is [.., ',', _])
        {
            sb.Remove(sb.Length - 2, 1);
        }
        sb.AppendLine(";");
        return sb;
    }
}