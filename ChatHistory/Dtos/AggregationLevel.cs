namespace ChatHistory.Api.Dtos
{
    public class AggregationLevel
    {
        private AggregationLevel(string value) { Value = value; }

        public string Value { get; private set; }

        public static AggregationLevel Continuously { get { return new AggregationLevel("Continuously"); } }
        public static AggregationLevel Hourly { get { return new AggregationLevel("Hourly"); } }
        public static AggregationLevel Daily { get { return new AggregationLevel("Daily"); } }

        public override string ToString()
        {
            return Value;
        }
    }
}
