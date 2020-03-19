using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldFinder
{
    public class WordFinder
    {
        #region Properties

        private readonly List<string> _dictionary;
        public char[,] Matrix { get; set; }

        public List<string> Result { get; set; } = new List<string>();

        internal bool Exceed2048
        {
            get { return _dictionary.ToList().Count > 2048; }
        }
        #endregion

        public WordFinder(IEnumerable<string> dictionary)
        {
            _dictionary = dictionary.Any() ? dictionary.ToList() : new List<string>();
        }

        internal bool Exceed64x64(string[] src)
        {
            return src.Any(d => d.Length > 64);
        }

        /// <summary>
        /// Searches the matrix and finds the words from the dictionary.
        /// </summary>
        /// <param name="src">source</param>
        /// <returns>words found</returns>
        public IList<string> Find(IEnumerable<string> src)
        {
            GenerateMatrixBySrc(src.ToList());
            // The other posible result "wind", "cold", "chill" change the order of SearchFromMethod.(1) SearchFromUpToBottom (2) SearchFromLeftToRigth
            SearchFromLeftToRigth();
            SearchFromUpToBottom();

            return Result;
        }

        /// <summary>
        /// Iterate the matrix from up to bottom and search posible values.
        /// </summary>
        private void SearchFromUpToBottom()
        {
            var length = Matrix.GetLength(0);
            int i, j;
            for (i = 0; i < Matrix.GetLength(0); i++)
            {
                for (j = 0; j < Matrix.GetLength(1); j++)
                {
                    StringBuilder sb = new StringBuilder();
                    int nextIndex = j;

                    while (nextIndex < length)
                    {
                        sb.Append(Matrix[nextIndex, i]);
                        if (ExistsAndIsNewWord(sb.ToString()))
                        {
                            Result.Add(sb.ToString());
                        }
                        nextIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// Iterate the matrix from Left to Rigth and search posible values.
        /// </summary>
        private void SearchFromLeftToRigth()
        {
            var length = Matrix.GetLength(1);
            int i, j;
            // Loop the matrix  from top to down.
            for (j = 0; j < Matrix.GetLength(0); j++)
            {
                for (i = 0; i < Matrix.GetLength(1); i++)
                {
                    StringBuilder sb = new StringBuilder();
                    int nextIndex = i;

                    while (nextIndex < length)
                    {
                        sb.Append(Matrix[j, nextIndex]);
                        if (ExistsAndIsNewWord(sb.ToString()))
                        {
                            Result.Add(sb.ToString());
                        }
                        nextIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// Populate the matrix with src data.
        /// </summary>
        /// <param name="src">source</param>
        private void GenerateMatrixBySrc(List<string> src)
        {
            int matrixSize = src.First().Length;
            int i, j;
            Matrix = new char[matrixSize, matrixSize];

            for (i = 0; i < Matrix.GetLength(0); i++)
            {
                string world = src[i];
                for (j = 0; j < Matrix.GetLength(1); j++)
                {
                    Matrix[i, j] = world[j];
                }
            }
        }

        /// <summary>
        /// Verify if exists in dictionary and is a new word
        /// </summary>
        /// <param name="value">word to evaluate</param>
        /// <returns>Yes/No</returns>
        private bool ExistsAndIsNewWord(string value)
        {
            return _dictionary.Exists(c => c == value) && !Result.Any(r => r == value);
        }
    }
}
