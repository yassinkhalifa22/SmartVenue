using __LibraryManagement.Data.Models;
using System.Collections.Generic;

namespace Library_Books_Management_System.Service.MemberSV
{
    public class MemberService : IMemberService
    {
        private readonly List<Member> members = new List<Member>();

        public bool AddMember(Member member)
        {
            if (member == null)
                return false;

            if (GetMemberById(member.MemberId) != null)
                return false;

            members.Add(member);
            return true;
        }

        public List<Member> GetAllMembers()
        {
            return members;
        }

        public Member? GetMemberById(int memberId)
        {
            foreach (Member member in members)
            {
                if (member.MemberId == memberId)
                    return member;
            }

            return null;
        }
    }
}