using System;
using System.Collections.Generic;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Enums;
using LibraryManagementSystem.Domain.ValueObjects;

namespace LibraryManagementSystem.Application.Services
{
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(
            IMemberRepository memberRepository,
            ILoanRepository loanRepository,
            IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _loanRepository = loanRepository;
            _unitOfWork = unitOfWork;
        }

        public void RegisterMember(Member member)
        {
            _memberRepository.Add(member);
            _unitOfWork.Commit();
        }

        public void UpdateMember(Guid memberId, string name, ContactInfo contactInfo, MemberType memberType)
        {
            var member = _memberRepository.GetById(memberId)
                ?? throw new InvalidOperationException("Member not found.");

            member.UpdateDetails(name, contactInfo, memberType);
            _memberRepository.Update(member);
            _unitOfWork.Commit();
        }

        public IReadOnlyCollection<Loan> GetBorrowingHistory(Guid memberId)
        {
            return _loanRepository.GetLoansByMember(memberId);
        }

        public Member? GetMember(Guid memberId)
        {
            return _memberRepository.GetById(memberId);
        }
    }
}