using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Infrastructure.Repositories
{
    public class InMemoryMemberRepository : IMemberRepository
    {
        private readonly Dictionary<Guid, Member> _members = new();

        public void Add(Member member)
        {
            _members[member.Id] = member;
        }

        public void Update(Member member)
        {
            _members[member.Id] = member;
        }

        public Member? GetById(Guid id)
        {
            return _members.TryGetValue(id, out var member) ? member : null;
        }

        public IReadOnlyCollection<Member> GetAll()
        {
            return _members.Values.ToList().AsReadOnly();
        }
    }
}