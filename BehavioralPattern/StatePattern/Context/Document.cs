using System;
using StatePattern.States;

namespace StatePattern.Context
{
    public class Document
    {
        private IDocumentState _documentState;
        public string Title {get; set;}
        public string Content {get; set;}

        public Document(string title)
        {
            Title = title;
            Content = "";
            _documentState = new DraftState(this);
        }

        public void ChangeState(IDocumentState newState)
        {
            _documentState = newState;
        }

        public string CurrentStateName
        {
            get {return _documentState.Name;}
        }

        public void Edit(string text)
        {
            _documentState.Edit(text);
        }

        public void SubmitForReview()
        {
            _documentState.SubmitForReview();
        }

        public void Approve()
        {
            _documentState.Approve();
        }

        public void Reject()
        {
            _documentState.Reject();
        }

        internal void PrintStatus()
        {
            Console.WriteLine("Title: " +  Title);
            Console.WriteLine("State: " + _documentState.Name);
            Console.WriteLine("Content: " +  Content);
            Console.WriteLine();
        }
    }
}