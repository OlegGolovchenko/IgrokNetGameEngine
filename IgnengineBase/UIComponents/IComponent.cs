using System;
using IgnengineBase.GL;

namespace IgnengineBase.UIComponents{

    public interface IComponent
    {
        float MouseX { get; set; }

        float MouseY { get; set; }

        bool Visible { get; set; }

        Color Background { get; set; }

        float Transperency { get; set; }

        void Render(uint width, uint height);
        

        void KeyPressed(KeySymbols key, KeyMods mod);

        void MousePressed(Buttons btn, int x, int y);

        bool ContainsCoordinates(int x, int y);
    }
}