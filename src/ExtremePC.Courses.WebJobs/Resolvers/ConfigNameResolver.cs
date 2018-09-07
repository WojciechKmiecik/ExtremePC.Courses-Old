using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremePC.Courses.WebJobs.Resolvers
{
    public class ConfigNameResolver : INameResolver
    {
        private readonly Dictionary<string, string> _nameResolverDictionary;

        public ConfigNameResolver(Dictionary<string, string> nameResolverDictionary)
        {
            _nameResolverDictionary = nameResolverDictionary;
        }

        public string Resolve(string name)
        {
            if (!_nameResolverDictionary.TryGetValue(name, out var resolvedName))
            {
                throw new InvalidOperationException("Cannot resolve " + name);
            }

            return resolvedName;
        }
    }
}
