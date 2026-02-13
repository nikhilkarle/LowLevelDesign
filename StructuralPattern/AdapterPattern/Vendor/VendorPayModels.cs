namespace AdapterPattern.Vendor
{
    public class VendorChargeRequest
    {
        public string Pan { get; set; }
        public int AmountInCents { get; set; }
        public string IsoCurrency { get; set; }
    }

    public class VendorChargeResult
    {
        public bool Approved { get; set; }
        public string RefCode { get; set; }
        public string DeclineReason { get; set; }
    }
}