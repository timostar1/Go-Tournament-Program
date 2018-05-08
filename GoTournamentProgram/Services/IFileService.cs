
namespace GoTournamentProgram.Services
{
    /// <summary>
    /// Представляет работу с файлами
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Загружает объект турнира из открытого файла
        /// </summary>
        /// <param name="filename">Путь к файлу с турниром</param>
        /// <returns>Объект турнира</returns>
        TournamentModel Open(string filename);

        /// <summary>
        /// Сохраняет турнир в новый файл
        /// </summary>
        /// <param name="filename">Путь к файлу</param>
        /// <param name="tournament">Объект турнира</param>
        void Save(string filename, TournamentModel tournament);
    }
}
