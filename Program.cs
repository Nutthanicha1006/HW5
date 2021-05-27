using System;
using System.IO;

namespace HW5
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageData, imageCon, Output;

            Console.Write("Enter the address of the image data file : ");
            imageData = Console.ReadLine();

            double[,] imageDataArray = ReadImageDataFromFile(imageData);

            double[,] imageData7Array = ImageData7Array(imageDataArray);

            Console.Write("Enter the address of the Convolution data file : ");
            imageCon = Console.ReadLine();
            ReadImageDataFromFile(imageCon);
            
            double[,] imageConArray = ReadImageDataFromFile(imageCon);

            Console.Write("Enter the resulting image data file address : ");
            Output = Console.ReadLine();

            
            double[,] Outputarray = OPArray(imageConArray, imageData7Array);
            WriteImageDataToFile(Output, Outputarray);

            Console.ReadLine();
        }

        static double[,] ReadImageDataFromFile(string imageDataFilePath)
        {
            string[] lines = System.IO.File.ReadAllLines(imageDataFilePath);
            int imageHeight = lines.Length;
            int imageWidth = lines[0].Split(',').Length;
            double[,] imageDataArray = new double[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                string[] items = lines[i].Split(',');
                for (int j = 0; j < imageWidth; j++)
                {
                    imageDataArray[i, j] = double.Parse(items[j]);
                }
            }

            return imageDataArray;
        }

        static double[,] ImageData7Array(double[,] imageDataArray)
        {
            double[,] imageData7Array = new double[7, 7];
            for (int i = 0; i <= 6; i++)
            {
                for (int j = 0; j <= 6; j++)
                {
                    if (i <= 5 && i >= 1 && j <= 5 && j >= 1)
                    {
                        imageData7Array[i, j] = imageDataArray[i - 1, j - 1];
                    }
                    else if (i == 0 && j >= 1 && j <= 5)
                    {
                        imageData7Array[i, j] = imageDataArray[i + 4, j - 1];
                    }
                    else if (i == 6 && j >= 1 && j <= 5)
                    {
                        imageData7Array[i, j] = imageDataArray[i - 6, j - 1];
                    }
                    else if (i >= 1 && i <= 5 && j == 0)
                    {
                        imageData7Array[i, j] = imageDataArray[i - 1, j + 4];
                    }
                    else if (i >= 1 && i <= 5 && j == 6)
                    {
                        imageData7Array[i, j] = imageDataArray[i - 1, j - 6];
                    }
                    else if (i == 6 && j == 6)
                    {
                        imageData7Array[i, j] = imageDataArray[0, 0];
                    }
                    else if (i == 6 && j == 0)
                    {
                        imageData7Array[i, j] = imageDataArray[0, 4];
                    }
                    else if (i == 0 && j == 6)
                    {
                        imageData7Array[i, j] = imageDataArray[4, 0];
                    }
                    else
                    {
                        imageData7Array[i, j] = imageDataArray[4, 4];
                    }
                }
            }
            return imageData7Array;
        }

        static double[,] OPArray(double[,] ImageData7Array, double[,] imageConArray)
        {
            double[,] OPArray = new double[5, 5];
            for (int i = 0; i <= 4; i++)
            {
                for (int j = 0; j <= 4; j++)
                {
                    double X1, X2, X3, X4, X5, X6, X7, X8, X9, Sum;

                    X1 = ImageData7Array[i , j ] * imageConArray[0, 0];
                    X2 = ImageData7Array[i , j + 1] * imageConArray[0, 1];
                    X3 = ImageData7Array[i , j + 2] * imageConArray[0, 2];
                    X4 = ImageData7Array[i + 1, j ] * imageConArray[1, 0];
                    X5 = ImageData7Array[i + 1, j + 1] * imageConArray[1, 1];
                    X6 = ImageData7Array[i + 1, j + 2] * imageConArray[1, 2];
                    X7 = ImageData7Array[i + 2, j ] * imageConArray[2, 0];
                    X8 = ImageData7Array[i + 2, j + 1] * imageConArray[2, 1];
                    X9 = ImageData7Array[i + 2, j + 2] * imageConArray[2, 2];

                    Sum = X1 + X2 + X3 + X4 + X5 + X6 + X7 + X8 + X9;
                    
                    OPArray[i - 1, j - 1] = Sum;

                }
            }
            return OPArray;
        }
        

        static void WriteImageDataToFile(string imageDataFilePath,double[,] imageDataArray)
        {
            string imageDataString = "";
            for (int i = 0; i < imageDataArray.GetLength(0); i++)
            {
                for (int j = 0; j < imageDataArray.GetLength(1) - 1; j++)
                {
                    imageDataString += imageDataArray[i, j] + ", ";
                }
                imageDataString += imageDataArray[i,imageDataArray.GetLength(1) - 1];
                imageDataString += "\n";
            }
            System.IO.File.WriteAllText(imageDataFilePath, imageDataString);
        }
    }
}
