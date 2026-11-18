using MovieCatalog;

MovieStorage storage = new MovieStorage();

try
{
    Console.WriteLine("ВСЕ ФИЛЬМЫ");

    var movies = storage.GetAllMovies();

    foreach (Movie movie in movies)
    {
        Console.WriteLine(movie);
        Console.WriteLine("-------------------");
    }

    Console.WriteLine("\nПОИСК ПО ID");

    Movie? foundMovie = storage.GetMovieById(1);

    if (foundMovie != null)
    {
        Console.WriteLine(foundMovie);
    }
    else
    {
        Console.WriteLine("Фильм не найден.");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка: " + ex.Message);
}


try
{
    // 1. Получение всех фильмов
    Console.WriteLine("=== ВСЕ ФИЛЬМЫ ===");

    var movies = storage.GetAllMovies();

    if (movies.Count == 0)
    {
        Console.WriteLine("Фильмов нет.");
    }

    foreach (Movie movie in movies)
    {
        Console.WriteLine(movie);
        Console.WriteLine("-------------------");
    }


    // 2. Поиск фильма по ID
    Console.WriteLine("\n=== ПОИСК ПО ID ===");

    Movie? foundMovie = storage.GetMovieById(1);

    if (foundMovie != null)
    {
        Console.WriteLine(foundMovie);
    }
    else
    {
        Console.WriteLine("Фильм не найден.");
    }


    // 3. Добавление фильма
    Console.WriteLine("\n=== ДОБАВЛЕНИЕ ФИЛЬМА ===");

    Movie newMovie = new Movie(
        0,
        "Матрица",
        1999,
        136,
        8.7,
        "Научная фантастика"
    );

    int addedRows = storage.AddMovie(newMovie);

    Console.WriteLine($"Добавлено строк: {addedRows}");


    // 4. Изменение рейтинга
    Console.WriteLine("\n=== ИЗМЕНЕНИЕ РЕЙТИНГА ===");

    int updatedRows = storage.UpdateRating(1, 9.0);

    if (updatedRows > 0)
    {
        Console.WriteLine("Рейтинг успешно изменён.");

        Movie? updatedMovie = storage.GetMovieById(1);

        Console.WriteLine(updatedMovie);
    }
    else
    {
        Console.WriteLine("Фильм не найден.");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Ошибка: " + ex.Message);
}