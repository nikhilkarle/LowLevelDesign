using System;
using System.Collections.Generic;
using LibraryManagementSystem.Application.Factories;
using LibraryManagementSystem.Application.Resolvers;
using LibraryManagementSystem.Application.Services;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Domain.Rules;
using LibraryManagementSystem.Domain.Strategies;
using LibraryManagementSystem.Domain.ValueObjects;
using LibraryManagementSystem.Infrastructure.Persistence;
using LibraryManagementSystem.Infrastructure.Repositories;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bookRepository = new InMemoryBookRepository();
            var memberRepository = new InMemoryMemberRepository();
            var loanRepository = new InMemoryLoanRepository();
            var unitOfWork = new InMemoryUnitOfWork();

            var catalogService = new CatalogService(bookRepository, unitOfWork);
            var memberService = new MemberService(memberRepository, loanRepository, unitOfWork);

            var borrowRules = new CompositeBorrowRule(new List<IBorrowRule>
            {
                new ActiveMemberRule(),
                new BookAvailableRule(),
                new MaxBorrowLimitRule(3),
                new NoOverdueLoansRule(),
                new OutstandingFineRule()
            });

            var durationStrategies = new List<ILoanDurationStrategy>
            {
                new RareBookLoanDurationStrategy(),
                new FacultyLoanDurationStrategy(),
                new StudentLoanDurationStrategy(),
                new DefaultLoanDurationStrategy()
            };

            var durationResolver = new LoanDurationStrategyResolver(durationStrategies);
            var loanFactory = new LoanFactory();

            var borrowingService = new BorrowingService(
                bookRepository,
                memberRepository,
                loanRepository,
                borrowRules,
                durationResolver,
                loanFactory,
                unitOfWork
            );

            var book1 = new Book(Guid.NewGuid(), "Clean Code", "Robert C. Martin", "9780132350884", 2008, false);
            var book2 = new Book(Guid.NewGuid(), "Distributed Systems", "Tanenbaum", "9780132143011", 2016, true);

            var student = new Member(
                Guid.NewGuid(),
                "Alice",
                new ContactInfo("alice@example.com", "111-111-1111", "123 Main St"),
                MemberType.Student
            );

            var faculty = new Member(
                Guid.NewGuid(),
                "Dr. Bob",
                new ContactInfo("bob@example.com", "222-222-2222", "456 Elm St"),
                MemberType.Faculty
            );

            catalogService.AddBook(book1);
            catalogService.AddBook(book2);

            memberService.RegisterMember(student);
            memberService.RegisterMember(faculty);

            var loan1 = borrowingService.BorrowBook(student.Id, book1.Id, DateTime.UtcNow);
            Console.WriteLine($"Loan created for {student.Name}: {loan1.Id}, DueDate: {loan1.DueDate:d}");

            var loan2 = borrowingService.BorrowBook(faculty.Id, book2.Id, DateTime.UtcNow);
            Console.WriteLine($"Loan created for {faculty.Name}: {loan2.Id}, DueDate: {loan2.DueDate:d}");

            borrowingService.ReturnBook(student.Id, book1.Id, DateTime.UtcNow);
            Console.WriteLine($"{student.Name} returned {book1.Title}");

            Console.WriteLine("Active loans for faculty:");
            foreach (var loan in loanRepository.GetLoansByMember(faculty.Id))
            {
                Console.WriteLine($"- LoanId: {loan.Id}, BookId: {loan.BookId}, Status: {loan.Status}, Due: {loan.DueDate:d}");
            }
        }
    }
}