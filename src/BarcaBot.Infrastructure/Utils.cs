using System.Globalization;
using System.Linq;
using System.Text;

namespace BarcaBot.Infrastructure
{
    public static class Utils
    {
        public static string NormalizePlayerName(string s)
            => string.Concat(s.Normalize(NormalizationForm.FormD).Where(
                c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
    }
}