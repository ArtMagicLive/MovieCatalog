using System;
using System.Collections.Generic;
using System.Text;

using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MovieCatalog
{
    internal class MovieStorage
    {
        private SqlConnection GetConnection()
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings["MovieCatalog"];

            if (settings == null)
                throw new Exception("Строка подключения MovieCatalog не найдена!");

            return new SqlConnection(settings.ConnectionString);
        }

        private Movie MapRowToMovie(SqlDataReader reader)
        {
            int commentIndex = reader.GetOrdinal("Comment");

            string? comment = reader.IsDBNull(commentIndex)
                ? null
                : reader.GetString(commentIndex);

            return new Movie(
                reader.GetInt32(reader.GetOrdinal("Id")),
                reader.GetString(reader.GetOrdinal("Title")),
                reader.GetInt32(reader.GetOrdinal("ReleaseYear")),
                reader.GetInt32(reader.GetOrdinal("DurationMinutes")),
                reader.GetDouble(reader.GetOrdinal("Rating")),
                comment
            );
        }

        public List<Movie> GetAllMovies()
        {
            List<Movie> movies = new List<Movie>();

            string query = @"SELECT Id, Title, ReleaseYear,
                            DurationMinutes, Rating, Comment
                     FROM Movies";

            using SqlConnection connection = GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Movie movie = MapRowToMovie(reader);
                movies.Add(movie);
            }

            return movies;
        }

        public Movie? GetMovieById(int id)
        {
            string query = @"SELECT Id, Title, ReleaseYear,
                            DurationMinutes, Rating, Comment
                     FROM Movies
                     WHERE Id = @id";

            using SqlConnection connection = GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return MapRowToMovie(reader);
            }

            return null;
        }

      
        public int AddMovie(Movie movie)
        {
            string query = @"INSERT INTO Movies
                     (Title, ReleaseYear, DurationMinutes, Rating, Comment)
                     VALUES
                     (@title, @year, @duration, @rating, @comment)";

            using SqlConnection connection = GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@title", SqlDbType.NVarChar, 120).Value = movie.Title;
            command.Parameters.Add("@year", SqlDbType.Int).Value = movie.ReleaseYear;
            command.Parameters.Add("@duration", SqlDbType.Int).Value = movie.DurationMinutes;
            command.Parameters.Add("@rating", SqlDbType.Float).Value = movie.Rating;

            command.Parameters.Add("@comment", SqlDbType.NVarChar, 255).Value =
                string.IsNullOrWhiteSpace(movie.Comment)
                ? DBNull.Value
                : movie.Comment;

            return command.ExecuteNonQuery();
        }

        public int UpdateRating(int id, double rating)
        {
            if (rating < 0 || rating > 10 || double.IsNaN(rating))
            {
                throw new ArgumentOutOfRangeException(
                    nameof(rating),
                    "Рейтинг должен быть от 0 до 10."
                );
            }

            string query = @"UPDATE Movies
                     SET Rating = @rating
                     WHERE Id = @id";

            using SqlConnection connection = GetConnection();

            connection.Open();

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Add("@id", SqlDbType.Int).Value = id;
            command.Parameters.Add("@rating", SqlDbType.Float).Value = rating;

            return command.ExecuteNonQuery();
        }
        
    }
}
