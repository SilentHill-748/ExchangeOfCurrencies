using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeOfCurrencies.Logging
{
    public class Logger : IDisposable
    {
        private readonly MemoryStream memory;
        private readonly Encoding encoding;
        private readonly List<int> lengthOfRecords;

        public Logger()
        {
            memory = new MemoryStream();
            encoding = Encoding.Unicode;
            lengthOfRecords = new List<int>();
        }

        /// <summary>
        /// Записывает данные с потока в указанный файл. Если файл не существует, то создастся новый.
        /// </summary>
        public void WriteToFile(string filename)
        {
            int position = 0;
            using StreamWriter stream = new(filename);
            foreach (int size in lengthOfRecords)
            {
                byte[] logRecord = new byte[size];
                memory.Read(logRecord, position, size);
                stream.WriteLine(encoding.GetString(logRecord));
                position += size;
            }
        }

        /// <summary>
        /// Записывает все данные с потока в строку.
        /// </summary>
        /// <returns></returns>
        public string WriteAllText()
        {
            return encoding.GetString(memory.ToArray());
        }

        /// <summary>
        /// Записывает в поток указанную строку данных.
        /// </summary>
        /// <param name="content"></param>
        public void Write(string content)
        {
            byte[] bytesOfContent = encoding.GetBytes(content);
            lengthOfRecords.Add(bytesOfContent.Length);
            memory.Write(bytesOfContent, 0, bytesOfContent.Length);
        }

        public void Dispose()
        {
            memory.Close();
            GC.SuppressFinalize(this);
        }
    }
}
