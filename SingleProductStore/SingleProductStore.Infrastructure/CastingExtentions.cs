using System;

namespace SingleProductStore.Infrastructure
{
    public static class CastingExtentions
    {
        public static int ToInt(this Enum enumerator)
        {
            return Convert.ToInt32(enumerator);
        }
    }
}
