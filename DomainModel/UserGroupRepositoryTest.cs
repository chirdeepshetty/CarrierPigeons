using System.Collections;
using NUnit.Framework;

namespace DomainModel
{
    [TestFixture]
    public class UserGroupRepositoryTest
    {
        [Test]
        public void ShouldLoadAllUserGroups()
        {
            UserGroupRepository userGroupRepository = UserGroupRepository.Instance;

            ArrayList expected = new ArrayList(4);
            expected.Add(new UserGroup { Id = 1, Name = "Pune" });
            expected.Add(new UserGroup { Id = 2, Name = "Bangalore" });
            expected.Add(new UserGroup { Id = 3, Name = "Chennai" });
            expected.Add(new UserGroup { Id = 4, Name = "Global" });

            IList groups = userGroupRepository.LoadGroups();
            Assert.AreEqual(4,groups.Count);
            Assert.AreEqual(expected, groups);


            
        }
    }
}