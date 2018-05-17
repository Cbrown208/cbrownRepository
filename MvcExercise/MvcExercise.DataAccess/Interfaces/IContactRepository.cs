using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcExercise.Core.Models;

namespace MvcExercise.DataAccess.Interfaces
{
	public interface IContactRepository
	{
		List<Contact> GetContactList();
		Contact GetContactById(Guid contractId);
		bool AddContact(Contact contact);
		bool AddOrUpdateContact(Contact contact);
		Task<bool> DeleteContact(Guid id);
	}
}
