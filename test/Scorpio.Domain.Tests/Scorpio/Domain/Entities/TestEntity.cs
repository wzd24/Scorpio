
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Domain.Entities
{
    public class TestEntity<T> : Entity<T>
    {
        public TestEntity()
        {

        }

        public TestEntity(T id) : base(id)
        {

        }
    }
    public class TestEntity2<T> : Entity<T>
    {
        public TestEntity2()
        {

        }

        public TestEntity2(T id) : base(id)
        {

        }
    }
}
