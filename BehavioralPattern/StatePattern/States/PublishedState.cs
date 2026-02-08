using StatePattern.Context;

namespace StatePattern.States
{
    public class PublishedState : IDocumentState
    {
        private readonly Document _document;

        public string Name
        {
            get {return "Published";}
        }

        public PublishedState(Document document)
        {
            _document = document;
        }

        public void Edit(string text)
        {
            Console.WriteLine("To edit a document, it should be in Draft State");
        }

        public void SubmitForReview()
        {
            Console.WriteLine("To submit a document, it should be in Draft State");
        }

        public void Approve()
        {
            Console.WriteLine("To approve a document, it should be in Review State");
        }

        public void Reject()
        {
            Console.WriteLine("To approve a document, it should be in Review State");
        }
    }
}