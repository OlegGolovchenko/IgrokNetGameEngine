using System;
using System.Collections.Generic;

namespace IgnengineBase.UIComponents
{

    public interface IContainer
    {
        IList<IComponent> UIComponents { get; }

        void AddComponent(IComponent component);

        void RemoveComponent(IComponent component);
    }

}