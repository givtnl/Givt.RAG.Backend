﻿using NJsonSchema.Annotations;

namespace backend.business.Backers.Models
{
    public class BackerDetailModel
    {
        [NotNull]
        public string Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string EmailAddress { get; set; }
    }
}