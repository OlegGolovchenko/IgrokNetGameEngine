using System;
using System.Collections.Generic;
using IgnengineBase.GL;
using System.Linq;

namespace IgnengineBase.UIComponents.UIContainers
{
    public class Panel : UIComponents.IContainer, UIComponents.IComponent
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private float _borderWidth;

        private IList<DrawablePoint> _points;

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
            float height,
            float borderWidth
            float height
            )
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _borderWidth = borderWidth;
            Background = Color.White;
            Visible = true;
            _points = new List<DrawablePoint>(){
                DrawablePoint.GetInstance(x, y, 0),
                DrawablePoint.GetInstance(x+width, y, 0),
                DrawablePoint.GetInstance(x+width, y+height, 0),
                DrawablePoint.GetInstance(x, y+height, 0)
            };
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
                Natives.glPointSize(1);
                Natives.glBegin(GL.GLConsts.GL_QUADS);
                foreach (var point in _points)
                {
                    Background.Apply();
                    Natives.glVertex2f(point.X, point.Y);
                }
                Natives.glEnd();
                GetLastGLError();
                Natives.glPointSize(_borderWidth);
                for (var i = 0; i < _points.Count - 1; i++)
                {
                    var point1 = _points[i];
                    var point2 = _points[i+1];
                    Natives.glBegin(GL.GLConsts.GL_LINES);
                    _borderColor.Apply();
                    Natives.glVertex2f(point1.X, point1.Y);
                    Natives.glVertex2f(point2.X, point2.Y);
                    Natives.glEnd();
                    GetLastGLError();
                }
                var p1 = _points.LastOrDefault();
                var p2 = _points.FirstOrDefault();
                Natives.glBegin(GL.GLConsts.GL_LINES);
                _borderColor.Apply();
                Natives.glVertex2f(p1.X, p1.Y);
                Natives.glVertex2f(p2.X, p2.Y);
                Natives.glEnd();
                GetLastGLError();
                Natives.glPointSize(1);
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