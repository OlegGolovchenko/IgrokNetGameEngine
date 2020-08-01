using System;
using IgnengineBase.Display;

namespace IgnengineRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new Display();
            display.Run(Render);
            display.Destroy();
        }

        static void Render(int width,int height){
            Console.WriteLine("in render");
        }
    }
}
