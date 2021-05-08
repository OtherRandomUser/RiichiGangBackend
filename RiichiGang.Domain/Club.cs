using System;
using System.Collections.Generic;

namespace RiichiGang.Domain
{
    public class Club : Entity
    {
        public string Name { get; protected set; }
        public string Website { get; set; }
        public string Contact { get; set; }
        public string Localization { get; set; }

        public User Owner { get; protected set; }
        public int OwnerId { get; protected set; }

        public IEnumerable<Membership> Members { get; protected set; }

        private Club()
        {
        }

        public Club(string name, User owner, string website, string contact, string localization)
        {
            SetName(name);
            SetOwner(owner);
            Website = website;
            Contact = contact;
            Localization = localization;

            Members = new List<Membership>();
        }

        public void SetOwner(User owner)
        {
            OwnerId = owner?.Id ?? throw new ArgumentNullException(
                "O dono do clube não pode ser nulo");
            Owner = owner;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(
                    "Preencha o nome do clube");

            Name = name;
        }
    }
}