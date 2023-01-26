namespace Ucip.Models
{
    public class ResultPrediction
    {
        public FutureResponse Lstm { get; set; }
        public FutureResponse Gru { get; set; }
        public PastResponse Dtc { get; set; }
        public PastResponse Rfc { get; set; }
        public PastResponse Lrc { get; set; }
    }

    public class FutureResponse
    {
        public decimal? Minute05 { get; set; }
        public decimal? Minute10 { get; set; }
        public decimal? Minute20 { get; set; }
        public decimal? Minute30 { get; set; }
        public decimal? Minute45 { get; set; }
        public decimal? Minute60 { get; set; }
    }

    public class PastResponse
    {
        public int? MinuteMinus4 { get; set; }
        public int? MinuteMinus3 { get; set; }
        public int? MinuteMinus2 { get; set; }
        public int? MinuteMinus1 { get; set; }
        public int? MinuteMinus0 { get; set; }
    }
}
