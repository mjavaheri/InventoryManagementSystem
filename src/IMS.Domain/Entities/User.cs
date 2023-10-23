using IMS.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Domain.Entities
{
    public sealed class User
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public User(string name)
        {
            ValidateName(name);

            Name = name;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("User's name can not be null or whitespace");
        }
    }
}
