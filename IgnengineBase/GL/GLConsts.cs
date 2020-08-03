using System;
namespace IgnengineBase.GL
{
    internal class GLConsts
    {
        internal const int GL_DEPTH_TEST = 0x0B71;

        internal const uint GL_COLOR_BUFFER_BIT = 0x00004000;

        internal const uint GL_DEPTH_BUFFER_BIT = 0x00000100;

        internal const uint GL_MODELVIEW = 0x1700;
        internal const uint GL_PROJECTION = 0x1701;

        internal const uint GL_POINTS = 0x0000;
        internal const uint GL_LINES = 0x0001;
        internal const uint GL_LINE_LOOP = 0x0002;
        internal const uint GL_LINE_STRIP = 0x0003;
        internal const uint GL_TRIANGLES = 0x0004;
        internal const uint GL_TRIANGLE_STRIP = 0x0005;
        internal const uint GL_TRIANGLE_FAN = 0x0006;
        internal const uint GL_QUADS = 0x0007;
        internal const uint GL_QUAD_STRIP = 0x0008;
        internal const uint GL_POLYGON = 0x0009;
    }

    internal class GLErrors{
        internal const uint GL_NO_ERROR = 0;
        internal const uint GL_INVALID_ENUM = 0x0500;
        internal const uint GL_INVALID_VALUE = 0x0501;
        internal const uint GL_INVALID_OPERATION = 0x0502;
        internal const uint GL_STACK_OVERFLOW = 0x0503;
        internal const uint GL_STACK_UNDERFLOW = 0x0504;
        internal const uint GL_OUT_OF_MEMORY = 0x0505;
    }
}