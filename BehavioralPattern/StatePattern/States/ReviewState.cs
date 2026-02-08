using StatePattern.Context;

namespace StatePattern.States
{
    public class ReviewState : IDocumentState
    {
        private readonly Document _document;

        public string Name
        {
            get {return "Review";}
        }

        public ReviewState(Document document)
        {
            _document = document;
        }

        public void Edit(string text)
        {
            Console.WriteLine("To edit a document, it should be in Draft State");

        }

        public void SubmitForReview()
        {
            Console.WriteLine("Document already in Review");
        }

        public void Approve()
        {
            _document.ChangeState(new PublishedState(_document));
            Console.WriteLine("Document Published");
        }

        public void Reject()
        {
            _document.ChangeState(new DraftState(_document));
            Console.WriteLine("Document Rejected");
        }
    }
}