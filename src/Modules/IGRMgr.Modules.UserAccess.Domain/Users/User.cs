using IGRMgr.Modules.UserAccess.Domain.UserRegistrations;
using IGRMgr.Modules.UserAccess.Domain.Users.Events;
using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.Users
{
    public class User : Entity
    {
        public UserId Id { get; private set; }

        public string _email;

        public string _firstName;

        public string _lastName;

        public string _middleName;

        public string _name;

        public bool _isActive;

        public string _password;

        public string _phoneNumber;

        public string _gender;

        public string _role;

        private User(UserRegistrationId userRegistrationId, string email, string firstName, string lastName,
            string middleName, string name, string phoneNumber, string gender, string role)
        {
            var password = PasswordCreator.GeneratePassword();
            this.Id = new UserId(userRegistrationId.Value);
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            _name = name;
            _isActive = true;
            _role = role;
            _phoneNumber = phoneNumber;
            _gender = gender;
            _password = password;

            this.AddDomainEvent(new UserCreatedDomainEvent(this.Id, _email, _firstName, _lastName, _middleName, _password, _name, _role, _gender, _phoneNumber)) ;
        }

        internal static User CreateFromUserRegistration(UserRegistrationId userRegistrationId, string email, string firstName,
            string lastName, string middleName, string name, string phoneNumber, string gender, string role)
        {
            return new User(userRegistrationId, email, firstName, lastName, middleName, name, phoneNumber, gender, role);
        }
    }
}
