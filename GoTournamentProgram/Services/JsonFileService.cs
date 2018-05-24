using System.IO;
using System.Runtime.Serialization.Json;

namespace GoTournamentProgram.Services
{
    /// <summary>
    /// Представляет работу с JSON файлами
    /// </summary>
    public class JsonFileService : IFileService
    {
        /// <summary>
        /// Загружает объект турнира из открытого файла JSON формата
        /// </summary>
        /// <param name="filename">Путь к файлу с турниром</param>
        /// <returns>Объект турнира</returns>
        public TournamentModel Open(string filename)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TournamentModel));
            TournamentModel tournament;

            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                tournament = (TournamentModel)jsonFormatter.ReadObject(fs);
            }

            return tournament;
        }

        /// <summary>
        /// Сохраняет турнир в новый файл JSON формата
        /// </summary>
        /// <param name="filename">Путь к файлу</param>
        /// <param name="tournament">Объект турнира</param>
        public void Save(string filename, TournamentModel tournament)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TournamentModel));

            // TODO: Устранить проблемы при сохранении в 
            //       существующий файл (большего размера)
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, tournament);
            }
        }
    }
}
