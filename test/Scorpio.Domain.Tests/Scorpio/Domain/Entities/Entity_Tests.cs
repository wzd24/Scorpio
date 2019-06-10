using System;
using System.Collections.Generic;
using System.Text;

using Shouldly;
using Xunit;
namespace Scorpio.Domain.Entities
{
    public class Entity_Tests
    {
        [Theory]
        [MemberData(nameof(IsTransientData))]
        public void IsTransient(Entity entity, bool expected)
        {
            entity.IsTransient().ShouldBe(expected);
        }

        [Fact]
        public void EntityEquals()
        {
            var entity = new TestEntity<int>();
            entity.Equals(entity).ShouldBeTrue();
            new TestEntity<int>().Equals(new TestEntity<int>()).ShouldBeFalse();
            new TestEntity<int>().Equals(new object()).ShouldBeFalse();
            new TestEntity<int>().Equals(new TestEntity<int>(10)).ShouldBeFalse();
            new TestEntity<int>(10).Equals(new TestEntity<int>(10)).ShouldBeTrue();
            new TestEntity<int>(10).Equals(new TestEntity2<int>(10)).ShouldBeFalse();
        }
        [Fact]
        public void EntityEqualsop()
        {
            var entity = new TestEntity<int>();
            var entity2 = entity;
            (entity == entity2).ShouldBeTrue();
            (new TestEntity<int>() == new TestEntity<int>()).ShouldBeFalse();
            (null == new TestEntity<int>()).ShouldBeFalse();
            (new TestEntity<int>(10) == new TestEntity<int>(10)).ShouldBeTrue();
            (entity != entity2).ShouldBeFalse();
            (new TestEntity<int>() != new TestEntity<int>()).ShouldBeTrue();
            (null != new TestEntity<int>()).ShouldBeTrue();
            (new TestEntity<int>(10) != new TestEntity<int>(10)).ShouldBeFalse();
        }

        [Fact]
        public void EntityGetHashCode()
        {
            new TestEntity<int>(10).GetHashCode().ShouldBe(10.GetHashCode());
        }

        public static IEnumerable<object[]> IsTransientData()
        {
            yield return new object[] { new TestEntity<Guid>(), true };
            yield return new object[] { new TestEntity<Guid>(Guid.NewGuid()), false };
            yield return new object[] { new TestEntity<int>(), true };
            yield return new object[] { new TestEntity<int>(-1), true };
            yield return new object[] { new TestEntity<int>(100), false };
            yield return new object[] { new TestEntity<long>(), true };
            yield return new object[] { new TestEntity<long>(-1), true };
            yield return new object[] { new TestEntity<long>(1000), false };
        }


    }
}
