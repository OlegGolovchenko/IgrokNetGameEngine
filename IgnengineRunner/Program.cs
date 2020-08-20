using System;
using IgnengineBase.Display;
using IgnengineBase.UIComponents;
using IgnengineBase.UIComponents.UIContainers;

namespace IgnengineRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var display = new Display(600, 600);
            var panel = new Panel(20, 20, 100, 100, 10);
            panel.Background = Color.Blue;
            display.AddComponent(panel);
            display.Run(Render);
            display.RemoveComponent(panel);
            display.Destroy();
        }

        static void Render(uint width,uint height){
        }
    }
}
