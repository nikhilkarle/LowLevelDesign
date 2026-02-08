using StatePattern.Context;

namespace StatePattern.States
{
    public class DraftState : IDocumentState
    {
        private readonly Document _document;

        public string Name
        {
            get { return "Draft"; }
        }

        public DraftState(Document document)
        {
            _document = document;
        }

        public void Edit(string text)
        {
            _document.Content = text;
            Console.WriteLine("Edited the document");

        }

        public void SubmitForReview()
        {
            _document.ChangeState(new ReviewState(_document));
            Console.WriteLine("Submitted the document for review");
        }

        public void Approve()
        {
            Console.WriteLine("To approve a document, it should be in Review state");
        }

        public void Reject()
        {
            Console.WriteLine("To reject a document, it should be in Review state");
        }
    }
}