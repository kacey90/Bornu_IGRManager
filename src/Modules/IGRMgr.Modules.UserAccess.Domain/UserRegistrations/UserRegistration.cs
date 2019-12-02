
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations.Events;
using IGRMgr.Modules.UserAccess.Domain.UserRegistrations.Rules;
using IGRMgr.Modules.UserAccess.Domain.Users;
using Nubalance.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGRMgr.Modules.UserAccess.Domain.UserRegistrations
{
    public class UserRegistration : Entity, IAggregateRoot
    {
        public UserRegistrationId Id { get; set; }

        private string _email;

        private string _firstName;

        private string _lastName;

        private string _middleName;

        private string _name;

        private string _phoneNumber;

        private string _gender;

        private DateTime _dateRegistered;

        private DateTime? _dateConfirmed;

        private UserRegistrationStatus _status;

        private List<UserRole> _roles;
        

        private UserRegistration()
        {

        }

        private UserRegistration(Guid id,
            string email,
            string firstName,
            string lastName,
            string middleName,
            string gender,
            string phoneNumber,
            string role,
            IUsersCounter usersCounter)
        {
            this.CheckRule(new UserEmailMustBeUniqueRule(usersCounter, email));

            this.Id = new UserRegistrationId(id);
            _email = email;
            _firstName = firstName;
            _lastName = lastName;
            _middleName = middleName;
            _name = string.IsNullOrWhiteSpace(middleName) ? $"{firstName} {lastName}" : $"{lastName}, {firstName} {middleName}";
            _phoneNumber = phoneNumber;
            _gender = gender;
            _dateRegistered = DateTime.Now;

            _roles = new List<UserRole>();
            _roles.Add(GetUserRole(role));
            _status = UserRegistrationStatus.WaitingForConfirmation;

            this.AddDomainEvent(new NewUserRegisteredDomainEvent(this.Id, _email, _firstName, _lastName, _middleName, _name, _dateRegistered));
        }

        public static UserRegistration RegisterNewUser(Guid id,
            string email,
            string firstName,
            string lastName,
            string middleName,
            string gender,
            string phoneNumber,
            string role,
            IUsersCounter usersCounter)
        {
            return new UserRegistration(id, email, firstName, lastName, middleName, gender, phoneNumber, role, usersCounter);
        }

        public User CreateUser()
        {
            this.CheckRule(new UserCannotBeCreatedWhenRegistrationIsNotConfirmedRule(_status));

            return User.CreateFromUserRegistration(Id, _email, _firstName, _lastName, _middleName, _name, _phoneNumber, _gender, 
                _roles.FirstOrDefault().Value);
        }

        private UserRole GetUserRole(string role)
        {
            UserRole userRole = null;
            switch (role)
            {
                case nameof(UserRole.Administrator):
                    userRole = UserRole.Administrator;
                    break;
                case nameof(UserRole.Manager):
                    userRole = UserRole.Manager;
                    break;
                case nameof(UserRole.Staff):
                    userRole = UserRole.Staff;
                    break;
                default:
                    userRole = UserRole.Staff;
                    break;
            }
            return userRole;
        }

        public void Confirm()
        {
            this.CheckRule(new UserRegistrationCannotBeConfirmedMoreThanOnceRule(_status));
            this.CheckRule(new UserRegistrationCannotBeConfirmedAfterExpirationRule(_status));

            _status = UserRegistrationStatus.Confirmed;
            _dateConfirmed = DateTime.Now;

            this.AddDomainEvent(new UserRegistrationConfirmedDomainEvent(this.Id, _firstName, _lastName, _middleName, _email,
                _name, _phoneNumber, _gender, true, _roles.FirstOrDefault().Value));
        }

        public void Expire()
        {
            this.CheckRule(new UserRegistrationCannotBeExpiredMoreThanOnceRule(_status));

            _status = UserRegistrationStatus.Expired;

            this.AddDomainEvent(new UserRegistrationExpiredDomainEvent(this.Id));
        }
    }
}
