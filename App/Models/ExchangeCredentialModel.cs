using System;

namespace App.Models
{
    public class ExchangeCredentialModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string ApiKey { get; set; }  

        public string ApiSecret { get; set; }

        public string Name { get; set; }
    }
}