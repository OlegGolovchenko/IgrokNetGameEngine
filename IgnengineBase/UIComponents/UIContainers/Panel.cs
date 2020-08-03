using System;
using System.Collections.Generic;
using IgnengineBase.GL;

namespace IgnengineBase.UIComponents.UIContainers
{
    public class Panel : UIComponents.IContainer, UIComponents.IComponent
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;

        private IList<UIComponents.IComponent> _components;
        public IList<UIComponents.IComponent> UIComponents
        {
            get
            {
                if(_components == null){
                    _components = new List<UIComponents.IComponent>();
                }
                return _components;
            }
        }
        private bool _visible;
        public bool Visible 
        { 
            get => _visible;
            set
            {
                _visible = value;
            }
        }

        private Color _background;
        public Color Background
        {
            get => _background;
            set
            {
                _background = value;
            }
        }

        private Color _borderColor = Color.Black;

        public float Transperency { get => Background.A; set => Background.ApplyTransperency(value); }

        public Panel(
            float x,
            float y,
            float width,
            float height
            )
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            Background = Color.White;
            Visible = true;
        }

        public void AddComponent(UIComponents.IComponent component)
        {
            _components.Add(component);
        }

        public void RemoveComponent(UIComponents.IComponent component)
        {
            _components.Remove(component);
        }

        public void Render(uint width, uint height)
        {
            if (Visible)
            {
                var one = _x;
                var two = _x + _width;
                var three = _y;
                var four = _y + _height;
                Natives.glBegin(GL.GLConsts.GL_QUADS);
                Background.Apply();
                Natives.glVertex2f(one, three);
                Background.Apply();
                Natives.glVertex2f(two, three);
                Background.Apply();
                Natives.glVertex2f(two, four);
                Background.Apply();
                Natives.glVertex2f(one, four);
                Natives.glEnd();
                GetLastGLError();
                Natives.glBegin(GL.GLConsts.GL_LINES);
                Natives.glVertex3f(one,three,1);
                Natives.glEnd();
                GetLastGLError();
                Natives.glBegin(GL.GLConsts.GL_LINES);
                Natives.glVertex3f(two,three,1);
                Natives.glEnd();
                GetLastGLError();
                Natives.glBegin(GL.GLConsts.GL_LINES);
                Natives.glVertex3f(two,four,1);
                Natives.glEnd();
                GetLastGLError();
                Natives.glBegin(GL.GLConsts.GL_LINES);
                Natives.glVertex3f(one,four,1);
                Natives.glEnd();
                GetLastGLError();
            }
        }   


        
        private void GetLastGLError()
        {
            var error = Natives.glGetError();
            if (error == GLErrors.GL_OUT_OF_MEMORY)
            {
                throw new OutOfMemoryException("out of memory in opengl thrown");
            }
            else if (error == GLErrors.GL_STACK_OVERFLOW)
            {
                throw new StackOverflowException("Stack overflow in opengl");
            }
            else if (error == GLErrors.GL_STACK_UNDERFLOW)
            {
                throw new Exception("Stuck underflow in opengl");
            }
            else if (error == GLErrors.GL_INVALID_ENUM)
            {
                throw new Exception("Invalid enum in opengl");
            }
            else if (error == GLErrors.GL_INVALID_OPERATION)
            {
                throw new InvalidOperationException("Invalid operation in opengl");
            }
            else if (error == GLErrors.GL_INVALID_VALUE)
            {
                throw new Exception("Invalid value in opengl");
            }
        }

    }
}