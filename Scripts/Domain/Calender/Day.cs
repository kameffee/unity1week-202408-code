using System;
using UnitGenerator;

namespace Unity1week202408.Calender
{
    [UnitOf(typeof(int), UnitGenerateOptions.Validate)]
    public readonly partial struct Day
    {
        private partial void Validate()
        {
            if (value is < 0 or > 31)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value, "0-30の範囲内である必要があります");
            }
        }
    }
}