#include<stdio.h>
#include<stdlib.h>
#include<X11/X.h>
#include<X11/Xlib.h>
#include<GL/gl.h>
#include<GL/glx.h>
#include<GL/glu.h>

extern "C" {
    int* GetAttributes();

    XEvent * GetNextEvent(
        Display * dpy
        );

    Bool IsExposeEvent(
        XEvent * event
        );

    Bool IsCloseEvent(
        XEvent * event, 
        Display * dpy
        );

    Bool IsKeyPress(XEvent *event);

    unsigned int GetKeyCode(XEvent * event);

    unsigned int GetKeyMod(XEvent * event);

    unsigned long GetKeyDescription(XEvent * event);

    Bool IsMouseButtonPressed(XEvent * event);

    unsigned int GetButton(XEvent * event);

    unsigned int GetButtonMod(XEvent * event);

    int GetMouseX(XEvent * event);

    int GetMouseY(XEvent * event);

    Visual * GetVisualAndDepth(
        XVisualInfo * vi, 
        int depth
        );

    XSetWindowAttributes * GetSwaWith(
        Colormap cmap, 
        long event_mask
        );

    void EnableCloseEvent(
        Display * dpy, 
        Window win
        );

    void ClearGLXContext(
        Display * dpy, 
        Window win
        );

    void AcquireHeightFromWas(
        Display * dpy, 
        Window win, 
        int width, 
        int height
        );

    void GetBlackAndWhitePixel(
        Display *display,
        int screen,
        unsigned long blackPixel,
        unsigned long whitePixel
        );
}