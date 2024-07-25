using LibraryManagementSystem.Backend.Enums;
using LibraryManagementSystem.Backend.Models;

namespace LibraryManagementSystem.Backend.Utils
{
    public class DeweyDecimalNumberGeneratorUtil
    {
        private static readonly Dictionary<Genre?, string> GenreToDeweyMapping = new()
        {
            { Genre.Science, "500" },
            { Genre.Literature, "800" },
            { Genre.History, "900" },
            { Genre.Technology, "600" },
            { Genre.Art, "700" },
            { Genre.Music, "780" },
            { Genre.Philosophy, "100" },
            { Genre.Religion, "200" },
            { Genre.SocialScience, "300" },
            { Genre.Language, "400" },
            { Genre.Mathematics, "510" },
            { Genre.Medicine, "610" },
            { Genre.Law, "340" },
            { Genre.Education, "370" },
            { Genre.Psychology, "150" },
            { Genre.Fiction, "820" },
            { Genre.Poetry, "821" },
            { Genre.Drama, "822" },
            { Genre.Travel, "910" },
            { Genre.Biography, "920" },
            { Genre.Business, "650" },
            { Genre.SelfHelp, "158" },
            { Genre.Health, "613" },
            { Genre.ScienceFiction, "823" },
            { Genre.Mystery, "824" }
        };

        public static string GenerateDeweyDecimalNumber(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));

            var genreBaseClass = GenreToDeweyMapping.ContainsKey(book.Genre) ? GenreToDeweyMapping[book.Genre] : "000";

            var titleHash = Math.Abs(book.Title?.Sum(c => c) ?? 0) % 1000;
            var authorHash = Math.Abs(book.Author?.Sum(c => c) ?? 0) % 1000;
            var descriptionHash = Math.Abs(book.Description?.Sum(c => c) ?? 0) % 1000;

            return $"{genreBaseClass}.{titleHash:D3}{authorHash:D3}{descriptionHash:D3}";
        }
    }
}
