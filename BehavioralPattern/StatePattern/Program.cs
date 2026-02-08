using StatePattern.Context;

namespace StatePattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document("First Title");

            doc.Edit("NEW CONETNT1");

            doc.PrintStatus();

            doc.Approve();

            doc.PrintStatus();
            doc.SubmitForReview();

            doc.PrintStatus();
            doc.Reject();

            doc.Edit("KINDLY APPROVE");
            doc.SubmitForReview();
            doc.PrintStatus();

            doc.Approve();
            doc.PrintStatus();
        }
    }
}