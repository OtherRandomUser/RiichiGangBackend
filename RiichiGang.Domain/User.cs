using System;

namespace RiichiGang.Domain
{
    public class User : Entity
    {
        public string Username { get; protected set; }
        public string Email { get; protected set; }
        public string PasswordHash { get; protected set; }
        public Stats Stats { get; protected set; }

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