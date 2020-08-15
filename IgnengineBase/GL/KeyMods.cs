using System;
namespace IgnengineBase.GL
{
    [Flags]
    public enum KeyMods : uint
    {
        None = 0,
        ShiftMask = (1 << 0),
        LockMask = (1 << 1),
        ControlMask = (1 << 2),
        Mod1Mask = (1 << 3),
        Mod2Mask = (1 << 4),
        Mod3Mask = (1 << 5),
        Mod4Mask = (1 << 6),
        Mod5Mask = (1 << 7)
    }
}