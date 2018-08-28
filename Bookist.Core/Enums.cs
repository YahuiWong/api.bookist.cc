using System;

namespace Bookist.Core
{
    public enum Role : byte
    {
        User = 0,
        Admin = 1
    }

    public enum BookStatus : byte
    {
        Default = 0,
        Published = 1
    }

    public enum BookFormat
    {
        PDF = 1,
        AZW3 = 2,
        EPUB = 3,
        MOBI = 4,
        ONLINE = 8,
    }
}
