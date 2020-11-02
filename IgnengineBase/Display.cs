using System;
using System.Collections.Generic;
using System.Text;
using IgnengineBase;
using IgnengineBase.GL;

namespace IgnengineBase
{

    public class Display : UIComponents.IContainer
    {
        internal const long NoEventMask = 0L;
        internal const long KeyPressMask = (1L << 0);
        internal const long KeyReleaseMask = (1L << 1);
        internal const long ButtonPressMask = (1L << 2);
        internal const long ButtonReleaseMask = (1L << 3);
        internal const long EnterWindowMask = (1L << 4);
        internal const long LeaveWindowMask = (1L << 5);
        internal const long PointerMotionMask = (1L << 6);
        internal const long PointerMotionHintMask = (1L << 7);
        internal const long Button1MotionMask = (1L << 8);
        internal const long Button2MotionMask = (1L << 9);
        internal const long Button3MotionMask = (1L << 10);
        internal const long Button4MotionMask = (1L << 11);
        internal const long Button5MotionMask = (1L << 12);
        internal const long ButtonMotionMask = (1L << 13);
        internal const long KeymapStateMask = (1L << 14);
        internal const long ExposureMask = (1L << 15);
        internal const long VisibilityChangeMask = (1L << 16);
        internal const long StructureNotifyMask = (1L << 17);
        internal const long ResizeRedirectMask = (1L << 18);
        internal const long SubstructureNotifyMask = (1L << 19);
        internal const long SubstructureRedirectMask = (1L << 20);
        internal const long FocusChangeMask = (1L << 21);
        internal const long PropertyChangeMask = (1L << 22);
        internal const long ColormapChangeMask = (1L << 23);
        internal const long OwnerGrabButtonMask = (1L << 24);

        private uint _height, _width;
        private IntPtr _display;
        internal IntPtr XDisplay
        {
            get
            {
                return _display;
            }
        }
        private IntPtr _root;
        private IntPtr _vi;
        private ulong _cmap;
        private IntPtr _swa;
        private IntPtr _win;
        internal IntPtr Window
        {
            get
            {
                return _win;
            }
        }
        private IntPtr _glc;
        private bool _running = false;

        private IList<UIComponents.IComponent> _components;
        public IList<UIComponents.IComponent> UIComponents
        {
            get
            {
                if (_components == null)
                {
                    _components = new List<UIComponents.IComponent>();
                }
                return _components;
            }
        }

        public Display(string connectionString, uint width, uint height)
        {
            _width = width;
            _height = height;
            _display = Natives.XOpenDisplay(connectionString);
            if (_display == IntPtr.Zero)
            {
                Console.WriteLine("Cannot connect to X server");

            }
            else
            {
                _root = Natives.XDefaultRootWindow(_display);

                _vi = Natives.glXChooseVisual(_display, 0,
                    Natives.GetAttributes());

                var visual = Natives.GetVisualAndDepth(_vi, out var depth);

                _cmap = Natives.XCreateColormap(_display, _root,
                    visual, 0);

                _swa = Natives.GetSwaWith(_cmap,
                    ExposureMask | KeyPressMask | ButtonPressMask | PointerMotionMask);

                _win = Natives.XCreateWindow(_display, _root, 0, 0,
                    _width, _height, 0, depth, 1, visual, 1L << 13 | 1L << 11,
                    _swa);

                Natives.XMapWindow(_display, _win);

                Natives.XStoreName(_display, _win, "Igrok-net engine");

                _glc = Natives.glXCreateContext(_display, _vi,
                    IntPtr.Zero, true);

                if (_glc == IntPtr.Zero)
                {
                    Console.WriteLine("context not found");
                }
                else
                {
                    Natives.glXMakeCurrent(_display, _win, _glc);

                    Natives.EnableCloseEvent(_display, _win);

                    _running = true;
                }
            }
        }

        public Display(uint width, uint height) : this(null, width, height)
        {

        }

        public void Run(RenderFunc render)
        {
            Natives.glEnable(GLConsts.GL_DEPTH_TEST);
            while (_running)
            {
                KeySymbols code = 0;
                KeyMods mod = KeyMods.None;
                var modDesc = "";
                IntPtr xev = Natives.GetNextEvent(_display);
                if (Natives.IsKeyPress(xev))
                {
                    code = (KeySymbols)Natives.GetKeyDescription(xev);
                    mod = (KeyMods)Natives.GetKeyMod(xev);
                    if (mod.HasFlag(KeyMods.ShiftMask))
                    {
                        modDesc = modDesc + "|Shift";
                    }
                    if (mod.HasFlag(KeyMods.ControlMask))
                    {
                        modDesc = modDesc + "|Ctrl";
                    }
                    if (mod.HasFlag(KeyMods.LockMask))
                    {
                        modDesc = modDesc + "|Lock";
                    }
                    if (mod.HasFlag(KeyMods.Mod1Mask))
                    {
                        modDesc = modDesc + "|Mod1";
                    }
                    if (mod.HasFlag(KeyMods.Mod2Mask))
                    {
                        modDesc = modDesc + "|Mod2";
                    }
                    if (mod.HasFlag(KeyMods.Mod3Mask))
                    {
                        modDesc = modDesc + "|Mod3";
                    }
                    if (mod.HasFlag(KeyMods.Mod4Mask))
                    {
                        modDesc = modDesc + "|Mod4";
                    }
                    if (mod.HasFlag(KeyMods.Mod5Mask))
                    {
                        modDesc = modDesc + "|Mod5";
                    }
                    Console.WriteLine($"key {code} modifier {modDesc}");
                }
                if (Natives.IsMouseButtonPressed(xev))
                {
                    var btn = Natives.GetButton(xev);
                    mod = (KeyMods)Natives.GetButtonMod(xev);
                    var x = Natives.GetMouseX(xev);
                    var y = Natives.GetMouseY(xev);
                    foreach (var component in UIComponents)
                    {
                        if (component.ContainsCoordinates(x, y))
                        {
                            switch (btn)
                            {
                                case 1:
                                    component.MousePressed(Buttons.Button1, x, y);
                                    break;
                                case 2:
                                    component.MousePressed(Buttons.Button2, x, y);
                                    break;
                                case 3:
                                    component.MousePressed(Buttons.Button3, x, y);
                                    break;
                                case 4:
                                    component.MousePressed(Buttons.Button4, x, y);
                                    break;
                                case 5:
                                    component.MousePressed(Buttons.Button5, x, y);
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine($"mouse x {component.MouseX} mouse y {component.MouseY}");
                        }
                    }
                }
                if (Natives.IsMouseMoved(xev))
                {
                    var x = Natives.GetMouseXOnMove(xev);
                    var y = Natives.GetMouseYOnMove(xev);
                    foreach(var component in _components)
                    {
                        component.EvaluateMouseLeaving(x,y);
                    }
                }
                try
                {
                    Natives.glViewport(0, 0, ((int)_width), ((int)_height));

                    GetLastGLError();

                    Natives.glClearColor(1f, 1f, 1f, 1f);

                    GetLastGLError();

                    Natives.glClear(
                        GLConsts.GL_COLOR_BUFFER_BIT |
                        GLConsts.GL_DEPTH_BUFFER_BIT
                        );

                    GetLastGLError();

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    Natives.glMatrixMode(GLConsts.GL_PROJECTION);

                    GetLastGLError();

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    Natives.glOrtho(0, _width, _height, 0, -1, 1);

                    GetLastGLError();

                    Natives.glMatrixMode(GLConsts.GL_MODELVIEW);

                    GetLastGLError();

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    RenderUi(_width, _height);

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    Natives.glMatrixMode(GLConsts.GL_PROJECTION);

                    GetLastGLError();

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    Natives.gluPerspective(70, _width / (_height * 1.0), -1, 1);

                    GetLastGLError();

                    Natives.glMatrixMode(GLConsts.GL_MODELVIEW);

                    GetLastGLError();

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    render?.Invoke(_width, _height);

                    Natives.glLoadIdentity();

                    GetLastGLError();

                    Natives.glXSwapBuffers(_display, _win);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    _running = false;
                }
                if (Natives.IsCloseEvent(xev, _display))
                {
                    _running = false;
                }
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

        private void RenderUi(uint width, uint height)
        {
            foreach (var component in UIComponents)
            {
                component.Render(width, height);
            }
        }

        public void Destroy()
        {
            if (_display != IntPtr.Zero &&
                _win != IntPtr.Zero)
            {
                Natives.ClearGLXContext(_display, _win);

                if (_glc != IntPtr.Zero)
                {
                    Natives.glxDestroyContext(_display, _glc);
                }
                Natives.XDestroyWindow(_display, _win);

                Natives.XCloseDisplay(_display);
            }
        }

        public void AddComponent(UIComponents.IComponent component)
        {
            UIComponents.Add(component);
        }

        public void RemoveComponent(UIComponents.IComponent component)
        {
            UIComponents.Remove(component);
        }
    }
}