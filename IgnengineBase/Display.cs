using System;
using IgnengineBase;
using IgnengineBase.GL;

namespace IgnengineBase.Display
{

    public class Display
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

        private IntPtr _display;
        private IntPtr _root;
        private IntPtr _vi;
        private ulong _cmap;
        private IntPtr _swa;
        private IntPtr _win;
        private IntPtr _glc;
        private bool _running = false;
        public Display(string connectionString)
        {
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
                    ExposureMask | KeyPressMask);

                _win = Natives.XCreateWindow(_display, _root, 0, 0,
                    600, 600, 0, depth, 1, visual, 1L << 13 | 1L << 11,
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

        public Display() : this(null)
        {

        }

        public void Run(RenderFunc render)
        {
            Natives.glEnable(GLConsts.GL_DEPTH_TEST);
            while (_running)    
            {
                Natives.AcquireHeightFromWas(_display, _win,
                    out var width, out var height);

                IntPtr xev = Natives.GetNextEvent(_display);
                if (Natives.IsExposeEvent(xev))
                {
                    Natives.glViewport(0, 0, width, height);

                    Natives.glClearColor(1f, 1f, 1f, 1f);

                    Natives.glClear(
                        GLConsts.GL_COLOR_BUFFER_BIT | 
                        GLConsts.GL_DEPTH_BUFFER_BIT
                        );

                    Natives.glMatrixMode(GLConsts.GL_PROJECTION);
                    Natives.glLoadIdentity();
                    Natives.gluPerspective(70,width/(height*1.0),-1,1);
                    Natives.glMatrixMode(GLConsts.GL_MODELVIEW);
                    Natives.glLoadIdentity();

                    render?.Invoke(width, height);
                    
                    Natives.glLoadIdentity();

                    Natives.glXSwapBuffers(_display, _win);
                }
                if (Natives.IsCloseEvent(xev, _display))
                {
                    _running = false;
                }
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
    }
}