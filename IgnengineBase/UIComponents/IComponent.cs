using System;

namespace IgnengineBase.UIComponents{

    public interface IComponent
    {
        bool Visible { get; set; }

        Color Background { get; set; }

        float Transperency { get; set; }

        void Render(uint width, uint height);
    }
}