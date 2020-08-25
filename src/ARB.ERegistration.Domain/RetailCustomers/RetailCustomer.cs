﻿using ARB.ERegistration.BankAccounts;
using ARB.ERegistration.Card;
using ARB.ERegistration.Cards;
using ARB.ERegistration.ModelConstants;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using NN = System.Diagnostics.CodeAnalysis;


namespace ARB.ERegistration.RetailCustomers
{
    public class RetailCustomer : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public string CommercialRegisterNo { get; private set; }
        public string CICNo { get; private set; }
        public string Address { get; private set; }
        public ATMCard ATMCard { get; private set; }
        public ICollection<BankAccount> BankAccounts { get; private set; }

        private RetailCustomer()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal RetailCustomer(
            Guid id,
            [NN.NotNull] string name,
            string pinCode,
            string cardNumber,
            [CanBeNull] string address = null)
            : base(id)
        {
            SetName(name);
            SetATMCard(cardNumber, pinCode);
            this.Address = address;
        }

        internal RetailCustomer ChangeName([NN.NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NN.NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: RetailCustomerConstants.MaxNameLength
            );
        }
        private void SetATMCard(string cardNumber, string pinCode)
        {
            this.ATMCard = new ATMCard(cardNumber, pinCode);
        }
    }
}
