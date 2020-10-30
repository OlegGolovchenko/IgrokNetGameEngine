using System;
using System.Runtime.InteropServices;

namespace IgnengineBase
{
    public delegate void RenderFunc(uint width, uint height);
    public class Natives
    {
        #region "IgnNatives"

        [DllImport("libIgnEngineUi.so")]
        public extern static IntPtr CreateWindow(
            [MarshalAs(UnmanagedType.LPStr)] string display
            );
        [DllImport("libIgnEngineUi.so")]
        public extern static void Render(
            IntPtr display, 
            [MarshalAs(UnmanagedType.FunctionPtr)] RenderFunc render
            );
        [DllImport("libIgnEngineUi.so")]
        public extern static void Destroy(
            IntPtr display
            );
        [DllImport("libIgnEngineUi.so")]
        public extern static string GetLastError(
            IntPtr display
            );
        [DllImport("libIgnEngineUi.so")]
        internal extern static IntPtr GetAttributes();
        [DllImport("libIgnEngineUi.so")]
        internal extern static IntPtr GetNextEvent(
            IntPtr display
            );
        [DllImport("libIgnEngineUi.so")]
        [return:MarshalAs(UnmanagedType.Bool)]
        internal extern static bool IsCloseEvent(
            IntPtr xev, 
            IntPtr display
            );
        [DllImport("libIgnEngineUi.so")]
        [return:MarshalAs(UnmanagedType.Bool)]
        internal extern static bool IsExposeEvent(
            IntPtr xev
            );
        [DllImport("libIgnEngineUi.so")]
        internal extern static IntPtr GetVisualAndDepth(
            IntPtr vi, 
            out int depth
            );
        [DllImport("libIgnEngineUi.so")]
        internal extern static IntPtr GetSwaWith(
            ulong cmap, 
            long eventMask
            );
        [DllImport("libIgnEngineUi.so")]
        internal extern static void EnableCloseEvent(
            IntPtr display, 
            IntPtr window
            );
        [DllImport("libIgnEngineUi.so")]
        internal extern static void ClearGLXContext(
            IntPtr dpy, 
            IntPtr win
            );
        [DllImport("libIgnEngineUi.so")]
        internal extern static void AcquireHeightFromWas(
            IntPtr dpy, 
            IntPtr win, 
            out int width, 
            out int height
            );

        [DllImport("libIgnEngineUi.so")]
        internal extern static void GetBlackAndWhitePixel(
            IntPtr display,
            int screen,
            out ulong blackPixel,
            out ulong whitePixel
            );

        [DllImport("libIgnEngineUi.so")]
        internal extern static bool IsKeyPress(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static uint GetKeyCode(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static uint GetKeyMod(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static ulong GetKeyDescription(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static bool IsMouseButtonPressed(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static bool IsMouseMoved(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static uint GetButton(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static uint GetButtonMod(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static int GetMouseX(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static int GetMouseY(IntPtr evnt);
        
        [DllImport("libIgnEngineUi.so")]
        internal extern static int GetMouseXOnMove(IntPtr evnt);

        [DllImport("libIgnEngineUi.so")]
        internal extern static int GetMouseYOnMove(IntPtr evnt);

        #endregion

        #region "Xlib"

        [DllImport("libX11.so")]
        internal extern static IntPtr XOpenDisplay(
            [MarshalAs(UnmanagedType.LPStr)] string connstr
            );
        [DllImport("libX11.so")]
        internal extern static IntPtr XDefaultRootWindow(
            IntPtr display
            );
        //Alloc all 1
        //Alloc none 0
        [DllImport("libX11.so")]
        internal extern static ulong XCreateColormap(
            IntPtr display, 
            IntPtr root, 
            IntPtr visual, 
            int alloc
            );
        [DllImport("libX11.so")]
        internal extern static IntPtr XCreateWindow(
            IntPtr display, 
            IntPtr window, 
            int x, 
            int y, 
            uint w, 
            uint h, 
            uint borderW, 
            int depth, 
            uint cls, 
            IntPtr visual, 
            ulong vmask, 
            IntPtr swa
            );
        [DllImport("libX11.so")]
        internal extern static int XMapWindow(
            IntPtr display, 
            IntPtr window
            );
        [DllImport("libX11.so")]
        internal extern static int XStoreName(
            IntPtr display, 
            IntPtr window, 
            [MarshalAs(UnmanagedType.LPStr)] string title
            );
        [DllImport("libX11.so")]
        internal extern static int XDestroyWindow(
            IntPtr display, 
            IntPtr win
            );
        [DllImport("libX11.so")]
        internal extern static int XCloseDisplay(
            IntPtr display
            );
        [DllImport("libX11.so")]
        internal extern static int XNextEvent(
            IntPtr display, 
            out IntPtr xev
            );

        [DllImport("libX11.so")]
        internal extern static IntPtr XCreateSimpleWindow(
            IntPtr display,
            IntPtr window,
            int x,
            int y,
            uint width,
            uint height,
            uint borderW,
            ulong border,
            ulong background
            );
        
        [DllImport("libX11.so")]
        internal extern static int XSelectInput(
            IntPtr display,
            IntPtr window,
            long eventMask
            );
        
        #endregion

        #region "Glx and OpenGL staff"

        [DllImport("libGL.so")]
        internal extern static IntPtr glXChooseVisual(
            IntPtr display, 
            int screen, 
            IntPtr attributes
            );

        [DllImport("libGL.so")]
        ///Parameters in order 
        ///Display,
        ///XVisualInfo,
        ///GLXContext pointer, 
        ///direct
        internal extern static IntPtr glXCreateContext(
            IntPtr display, 
            IntPtr vis, 
            IntPtr sharedContext, 
            [MarshalAs(UnmanagedType.Bool)] bool direct
            );

        [DllImport("libGL.so")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal extern static bool glXMakeCurrent(
            IntPtr display, 
            IntPtr window, 
            IntPtr context
            );

        [DllImport("libGL.so")]
        internal extern static void glxDestroyContext(
            IntPtr display, 
            IntPtr glc
            );
        [DllImport("libGL.so")]
        internal extern static void glXSwapBuffers(
            IntPtr display, 
            IntPtr window
            );
        [DllImport("libGL.so")]
        internal extern static void glViewport(
            int x, 
            int y, 
            int width, 
            int height
            );
        [DllImport("libGL.so")]
        internal extern static void glEnable(
            int cap
            );
        [DllImport("libGL.so")]
        internal extern static void glClearColor(
            float red,
            float gren,
            float blue,
            float alpha
            );
        [DllImport("libGL.so")]
        internal extern static void glClear(
            uint mask
            );

        [DllImport("libGL.so")]
        internal extern static void glMatrixMode(
            uint mode
            );
        
        [DllImport("libGL.so")]
        internal extern static void glLoadIdentity();

        [DllImport("libGL.so")]
        internal extern static void glBegin(uint mode);

        [DllImport("libGL.so")]
        internal extern static void glEnd();

        [DllImport("libGL.so")]
        internal extern static void glVertex3f(
            float x, 
            float y, 
            float z
            );

        [DllImport("libGL.so")]
        internal extern static void glVertex2f(
            float x,
            float y
            );

        [DllImport("libGL.so")]
        internal extern static void glOrtho(
            double left, 
            double right, 
            double bottom, 
            double top, 
            double near_val, 
            double far_val
            );

        [DllImport("libGL.so")]
        internal extern static uint glGetError();

        [DllImport("libGL.so")]
        internal extern static void glColor4f(
            float r,
            float g, 
            float b, 
            float a
            );


        [DllImport("libGL.so")]
        internal extern static void glPointSize(float size);

        [DllImport("libGLU.so")]
        internal extern static void gluPerspective(
            double fov,
            double aspect,
            double zNear,
            double zFar
            );

        #endregion
    }
}
