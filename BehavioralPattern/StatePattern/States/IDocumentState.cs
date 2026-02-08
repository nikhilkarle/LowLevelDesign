namespace StatePattern.States
{
    public interface IDocumentState
    {
        void Edit(string text);
        void SubmitForReview();
        void Approve();
        void Reject();

        string Name { get; }
    }
}