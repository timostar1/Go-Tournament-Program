
namespace GoTournamentProgram.Services
{
    /// <summary>
    /// Позволяет работать с диалоговыми окнами
    /// </summary>
    public interface IDialogService
    {
        // TODO: Изменить комментарии
        /// <summary>
        /// Вывод сообщения
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        void ShowMessage(string message);

        /// <summary>
        /// Путь к файлу для открытия или сохранения
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Вызов диалога открытия файла
        /// </summary>
        /// <returns>Статус операции открытия</returns>
        bool OpenFileDialog();


        /// <summary>
        /// Вызов диалога сохранения файла
        /// </summary>
        /// <returns>Статус операции сохранения</returns>
        bool SaveFileDialog();

        bool SaveFileDialog(string filename);
    }
}
