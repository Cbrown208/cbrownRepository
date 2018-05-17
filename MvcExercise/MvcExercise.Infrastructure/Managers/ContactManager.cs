using System;
using MvcExercise.Core.Models;
using MvcExercise.DataAccess.Interfaces;
using MvcExercise.Infrastructure.Interfaces;

namespace MvcExercise.Infrastructure.Managers
{
	public class ContactManager : IContactManager
	{
		private readonly IContactRepository _contactRepository;
		public ContactManager(IContactRepository contactRepository)
		{
			_contactRepository = contactRepository;
		}

		public bool AddContact(Contact contact)
		{
			try
			{
				_contactRepository.AddContact(contact);
				return true;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
