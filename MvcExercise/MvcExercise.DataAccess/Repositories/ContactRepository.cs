using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcExercise.Core.Models;
using MvcExercise.DataAccess.Context;
using MvcExercise.DataAccess.Interfaces;

namespace MvcExercise.DataAccess.Repositories
{
	public class ContactRepository : IContactRepository
	{
		private readonly MvcExerciseContext _context;

		public ContactRepository()
			: this(new MvcExerciseContext())
		{
		}

		public ContactRepository(MvcExerciseContext context)
		{
			_context = context;
		}

		public List<Contact> GetContactList()
		{
			var result = _context.Contacts.ToList();
			return result;
		}

		public Contact GetContactById(Guid contractId)
		{
			var result = _context.Contacts.FirstOrDefault(x => x.ContactID == contractId);
			return result;
		}

		public bool AddContact(Contact contact)
		{
			try
			{
				if (contact.ContactID == Guid.Empty)
				{
					contact.ContactID = Guid.NewGuid();
				}
				_context.Contacts.Add(contact);
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message);
			}
		}

		public bool AddOrUpdateContact(Contact contact)
		{
			try
			{
				var contactDetails = _context.Contacts.FirstOrDefault(x => x.ContactID == contact.ContactID);
				if (contactDetails != null)
				{
					contactDetails.FirstName = contact.FirstName;
					contactDetails.LastName = contact.LastName;
					contactDetails.Telephone = contactDetails.Telephone;
					contactDetails.EmailAddress = contactDetails.EmailAddress;
					contactDetails.BestTimeToCall = contactDetails.BestTimeToCall;
				}
				else
				{
					_context.Contacts.Add(contact);
				}
				_context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				throw new ApplicationException(ex.Message);
			}
		}

		public Task<bool> DeleteContact(Guid id)
		{
			var contactDetails = _context.Contacts.FirstOrDefault(x => x.ContactID == id);
			if (contactDetails == null)
				return Task.FromResult(false);
			_context.Contacts.Remove(contactDetails);
			_context.SaveChanges();
			return Task.FromResult(true);
		}
	}
}
