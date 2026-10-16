using __LibraryManagement.Data.Models;
using System.Collections.Generic;

namespace Library_Books_Management_System.Service.MemberSV
{
    public interface IMemberService
    {
        bool AddMember(Member member);

        List<Member> GetAllMembers();

        Member? GetMemberById(int memberId);
    }
}
