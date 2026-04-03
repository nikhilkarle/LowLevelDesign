using System;
using System.Collections.Generic;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Application.Interfaces
{
    public interface IMemberRepository
    {
        void Add(Member member);
        void Update(Member member);
        Member? GetById(Guid id);
        IReadOnlyCollection<Member> GetAll();
    }
}