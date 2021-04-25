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
            encoding = Encoding.UTF8;
            lengthOfRecords = new List<int>();
        }

        /// <summary>
        /// Записывает данные с потока по строчно в массив.
        /// </summary>
        public string[] WriteAllLines()
        {
            string[] allText = new string[lengthOfRecords.Count]; 
            memory.Seek(0, SeekOrigin.Begin);
            for (int i = 0; i < lengthOfRecords.Count; i++)
            {
                int size = lengthOfRecords[i];
                byte[] logRecord = new byte[size];
                memory.Read(logRecord, 0, size);
                allText[i] = encoding.GetString(logRecord);
            }
            return allText;
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
            memory.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
