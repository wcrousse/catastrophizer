
using CatastrofizerGenerator;
using System;
using System.Collections.Immutable;

namespace CatastrofizerLiveDemo.Models
{
    public partial class AuthorizationScope : ICatastrofizable
    {
        public Guid? ConcurrencyToken { get; set; } = Guid.NewGuid();

        public string? Description { get; set; }

        public IImmutableDictionary<string, string>? Descriptions { get; set; }

        public string? DisplayName { get; set; }

        public IImmutableDictionary<string, string>? DisplayNames { get; set; }

        public int? ScopeId { get; set; }

        public string Name { get; set; } = default!;

        public IImmutableDictionary<string, string>? Properties { get; set; }

        public IImmutableList<string>? Resources { get; set; }
    }
    public partial class Product : ICatastrofizable
    {

        public decimal ? Price { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
    }
}