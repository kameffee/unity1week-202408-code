using UnitGenerator;

namespace Unity1week202408
{
    [UnitOf(typeof(int), UnitGenerateOptions.Validate | UnitGenerateOptions.Comparable | UnitGenerateOptions.ImplicitOperator)]
    public partial struct Percent
    {
        private partial void Validate()
        {
            if (value is < 0 or > 100)
            {
                throw new System.ArgumentOutOfRangeException(nameof(value), value, "0 から 100 の間である必要があります");
            }
        }
    }
}