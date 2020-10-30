#include "IgneDisplay.h"

int * GetAttributes()
{
    return new GLint[5]{GLX_RGBA, 
                        GLX_DEPTH_SIZE, 
                        24, 
                        GLX_DOUBLEBUFFER, 
                        None};
}

XEvent * GetNextEvent(
    Display * dpy
    )
{
    XEvent * xev = new XEvent();
    XNextEvent(dpy, xev);
    return xev;
}

Bool IsExposeEvent(
    XEvent * event
    )
{
    return event->type == Expose;
}

Bool IsCloseEvent(
    XEvent *xev, 
    Display * dpy
    )
{
    if (xev->type == ClientMessage)
    {
        long int wmDeleteMessage = XInternAtom(
            dpy, "WM_DELETE_WINDOW", False);
        if (xev->xclient.data.l[0] == wmDeleteMessage)
        {
            return True;
        }
    }
    return False;
}

Bool IsKeyPress(XEvent *event)
{
    if(event->type == KeyPress)
    {
        return True;
    }
    return False;
}

unsigned int GetKeyCode(XEvent *event)
{
    return event->xkey.keycode;
}

unsigned int GetKeyMod(XEvent *event)
{
    return event->xkey.state;
}

unsigned long GetKeyDescription(XEvent *xev)
{
    XKeyEvent xke = xev->xkey;
    KeySym ks = XLookupKeysym(&xke,0);
    return ks;
}

Bool IsMouseButtonPressed(XEvent *event)
{
    if(event->type == ButtonPress)
    {
        return true;
    }
    return false;
}

Bool IsMouseMoved(XEvent * event)
{
    if(event->type == MotionNotify){
        return true;
    }
    return false;
}

unsigned int GetButton(XEvent * event)
{
    return event->xbutton.button;
}

unsigned int GetButtonMod(XEvent * event)
{
    return event->xbutton.state;
}

int GetMouseX(XEvent * event)
{
    XButtonEvent btnEvent = event->xbutton;
    return btnEvent.x;
}

int GetMouseY(XEvent * event)
{
    XButtonEvent btnEvent = event->xbutton;
    return btnEvent.y;
}

int GetMouseXOnMove(XEvent * event)
{
    XMotionEvent btnEvent = event->xmotion;
    return btnEvent.x;
}

int GetMouseYOnMove(XEvent * event)
{
    XMotionEvent btnEvent = event->xmotion;
    return btnEvent.y;
}

Visual * GetVisualAndDepth(
    XVisualInfo * vi, 
    int depth
    )
{
    if (vi == NULL)
    {
        depth = -1;
        return NULL;
    }
    depth = vi->depth;
    return vi->visual;
}

XSetWindowAttributes *GetSwaWith(
    Colormap cmap, 
    long event_mask
    )
{
    XSetWindowAttributes * swa = new XSetWindowAttributes();
    swa->colormap = cmap;
    swa->event_mask = event_mask;
    return swa;
}

void EnableCloseEvent(
    Display * dpy, 
    Window win
    )
{
    Atom vmDeleteMessage = XInternAtom(
        dpy, "WM_DELETE_WINDOW", GL_FALSE);
    XSetWMProtocols(dpy, win, &vmDeleteMessage, GL_TRUE);
}

void ClearGLXContext(
    Display * dpy, 
    Window win
    )
{
    glXMakeCurrent(dpy, win, NULL);
}

void AcquireHeightFromWas(
    Display * dpy, 
    Window win, 
    int width, 
    int height
    )
{
    XWindowAttributes gwa;
    XGetWindowAttributes(dpy, win, &gwa);
    width = gwa.width;
    height = gwa.height;
}

void GetBlackAndWhitePixel(
        Display *display,
        int screen,
        unsigned long blackPixel,
        unsigned long whitePixel
        )
{
    blackPixel = BlackPixel(display,screen);
    whitePixel = WhitePixel(display,screen);
}