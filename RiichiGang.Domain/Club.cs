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
        public IEnumerable<Tournament> Tournaments { get; protected set; }
        public IEnumerable<Ruleset> Rulesets { get; protected set; }

        private Club()
        {
        }

        public Club(string name, User owner, string website, string contact, string localization)
        {
            SetName(name);
            SetOwner(owner);
            SetWebsite(website);
            SetContact(contact);
            SetLocalization(localization);

            Members = new List<Membership>();
            Tournaments = new List<Tournament>();
            Rulesets = new List<Ruleset>();
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

        public void SetWebsite(string website)
        {
            if (string.IsNullOrWhiteSpace(website))
                throw new ArgumentNullException(
                    "Preencha o site do clube");

            Website = website;
        }

        public void SetContact(string contact)
        {
            if (string.IsNullOrWhiteSpace(contact))
                throw new ArgumentNullException(
                    "Preencha o contato do clube");

            Contact = contact;
        }

        public void SetLocalization(string localization)
        {
            if (string.IsNullOrWhiteSpace(localization))
                throw new ArgumentNullException(
                    "Preencha o contato do clube");

            Localization = localization;
        }
    }
}