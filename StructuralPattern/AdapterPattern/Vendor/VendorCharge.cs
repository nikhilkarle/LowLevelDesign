namespace AdapterPattern.Vendor
{
    public class VendorCharge
    {
        public VendorChargeResult MakeCharge(VendorChargeRequest request)
        {
            if (request.AmountInCents > 50000)
            {
                return new VendorChargeResult
                {
                    Approved = false,
                    RefCode = "",
                    DeclineReason = "Amount exceeds vendor limit."
                };
            }

            return new VendorChargeResult
            {
                Approved = true,
                RefCode = "VENDOR-" + Guid.NewGuid().ToString("N"),
                DeclineReason = ""
            };
        }
    }
}