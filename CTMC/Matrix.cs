using System;
using System.Globalization;
using System.IO;
namespace CTMC
{
    public class Matrix
    {
        private readonly int _rows;
        private readonly int _cols;
        private double time;
        private double[,] Data;

        public double this[int i, int j]
        {
            get { return Data[i, j]; }
            set { Data[i, j] = value; }
        }
        
        public Matrix(int rows, int cols)
        {
            if (rows < 1 || cols < 1)
            {
                throw new Exception(
                    $"Row and column dimensions must be greater than 1. Currently Rows = {rows} and Cols = {cols}");
            }
            _rows = rows;
            _cols = cols;
            Data = new double[rows, cols];
            Fill(0);
        }

        public Matrix(string path) //Read matrix from file specified in path. First line should contain Rows and Cols. Following should be Rows many lines containing Cols many doubles separated by spaces.
        {
            using (System.IO.StreamReader reader = new StreamReader(path))
            {
                string[] dims = reader.ReadLine().Split(' ');
                _rows = int.Parse(dims[0], NumberStyles.Integer);
                _cols = int.Parse(dims[1], NumberStyles.Integer);
                Data = new double[_rows, _cols];

                for (int i = 0; i < _rows; i++)
                {
                    string[] row = reader.ReadLine().Split(' ');
                    if (row.Length != _cols)
                    {
                        throw new Exception("Length of all rows must match the given number of cols");
                    }
                    for (int j = 0; j < _cols; j++)
                    {
                        Data[i,j] = Double.Parse(row[j], NumberStyles.Float);
                    }
                }
            }
            
        }

        public Matrix(Matrix oldMatrix) //Copy constructor, ensuring that we fully copy over the Data array.
        {
            _rows = oldMatrix._rows;
            _cols = oldMatrix._cols;
            Data = new double[_rows, _cols];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Data[i, j] = oldMatrix.Data[i, j];
                }
            }
        }
        
        public void Fill(double num)
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Data[i, j] = num;
                }
            }
        }

        public void FillRandom()
        {
            Random u = new Random();
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    Data[i, j] = u.NextDouble();
                }
            }
        }

        public bool IsGenerator()
        {
            if (_rows != _cols)
            {
                return false;
            }
            for (int i = 0; i < _rows; i++)
            {
                double sum = 0;
                double diagonal = Data[i, i];
                
                if (diagonal > 0)
                {
                    return false;
                }
                
                if (diagonal == 0)
                {
                    for (int j = 0; j < _cols; j++)
                    {
                        if (Data[i, j] != 0)
                        {
                            return false;
                        }
                    }
                }

                else
                {
                    for (int j = 0; j < _cols; j++)
                    {
                        if (j != i)
                        {
                            sum += Data[i, j];
                        }
                    }

                    if (sum + diagonal != 0)
                    {
                        return false;
                    }
                }
                
            }

            return true;
        }
        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < _rows; i++)
            {
                output += "[";
                for (int j = 0; j < _cols; j++)
                {
                    output += Data[i, j].ToString();
                    if (j != _cols - 1)
                    {
                        output = output + " ,";
                    }

                    else
                    {
                        output = output + "]\n";
                    }
                    
                } 
            }
            return output;
        }

        public (int, int) Shape() // Returns a tuple containing the dimensions of the matrix
        {
            return (_rows, _cols);
        }

        public int GetCols()
        {
            return _cols;
        }

        public int GetRows()
        {
            return _rows;
        }
     }
}