using UnitGenerator;

namespace Unity1week202408.Anomaly
{
    [UnitOf(typeof(int))]
    public readonly partial struct AnomalyId
    {
        public static AnomalyId Empty => new(-1);

        public bool IsEmpty => value == -1;
        public bool Valid => value != -1;
    }
}