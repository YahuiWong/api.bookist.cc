using Bookist.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookist.Core.Services
{
    public class TagService : BaseService<Tag>
    {
        public TagService(DefaultDbContext context) : base(context)
        {
        }
    }
}
