using System;

namespace IgnengineBase.UIEvents{
    public class MouseEnteringEventArgs : EventArgs
    {
        private object _sender;

        public object sender { get => _sender; set => _sender = value; }

        public MouseEnteringEventArgs(object sender)
        {
            
        }
    }

    public class MouseLeavingEventArgs: EventArgs
    {
        private object _sender;

        public object sender { get => _sender; set => _sender = value; }
        
        public MouseLeavingEventArgs(object sender)
        {
            
        }
    }
}