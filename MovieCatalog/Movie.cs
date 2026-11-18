using System;
using System.Collections.Generic;
using System.Text;

namespace MovieCatalog
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int DurationMinutes { get; set; }
        public double Rating { get; set; }
        public string? Comment { get; set; }

        public Movie(int id, string title, int releaseYear,
                     int durationMinutes, double rating, string? comment)
        {
            Id = id;
            Title = title;
            ReleaseYear = releaseYear;
            DurationMinutes = durationMinutes;
            Rating = rating;
            Comment = comment;
        }

        public override string ToString()
        {
            return $"ID: {Id}\n" +
                   $"Название: {Title}\n" +
                   $"Год выпуска: {ReleaseYear}\n" +
                   $"Длительность: {DurationMinutes} мин.\n" +
                   $"Рейтинг: {Rating}\n" +
                   $"Комментарий: {Comment ?? "Отсутствует"}";
        }
    }
}
