using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class User : Entity
    {
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }
        public Stats Stats { get; protected set; }

        public IEnumerable<Club> OwnedClubs { get; protected set; }
        public IEnumerable<Membership> Memberships { get; protected set; }
        public IEnumerable<Tournament> Tournaments { get; protected set; }
        public IEnumerable<Notification> Notifications { get; protected set; }

        private User()
        {
        }

        public User(string username, string email, string passwordHash)
        {
            SetUsername(username);
            SetEmail(email);
            SetPasswordHash(passwordHash);
            Stats = new Stats();
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(
                    "Preencha o nome do usuário");

            Username = username;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(
                    "Preencha o email");

            Email = email;
        }

        public void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentNullException(
                    "Preencha a senha");

            PasswordHash = passwordHash;
        }
    }
}