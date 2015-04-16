using System.Collections.Generic;
using System.Linq;
using CacheAspect;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace TestCache
{
    [TestClass]
    public class TestUserRepository
    {

        UserRepository _target;

        UserGetRepository _targetGetRepo;
        UserAddRepository _targetAddRepo;

        IUserDal _dal;
        ICache _cache;

        // Use TestInitialize to run code before running each test 
        [TestInitialize]
        public void SetUp()
        {
            _dal = MockRepository.GenerateStrictMock<IUserDal>();
            _cache = new InProcessMemoryCache();

            CacheService.Cache = _cache;
            _target = new UserRepository {Dal = _dal};

            _targetAddRepo = new UserAddRepository {Dal = _dal};
            _targetGetRepo = new UserGetRepository {Dal = _dal};
        }

        [TestMethod]
        public void GetAllUsers_TryRetrievingDataTwice_DalShouldBeHitOnce()
        {
            //Arrange
            _dal.Expect(d => d.GetAllUsers()).Return(GetUsers());

            //Act
            _target.GetAllUsers();
            _target.GetAllUsers();

            //Assert
            _dal.VerifyAllExpectations();

        }

        [TestMethod]
        public void GetUserById_TryRetrievingDataTwice_DalShouldBeHitOnce()
        {
            //Arrange
            int id = 1;
            _dal.Expect(d => d.GetUserById(id)).Return(GetUsers().First());

            //Act
            _target.GetUserById(id);
            _target.GetUserById(id);

            //Assert
            _dal.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserById_TryRetrievingUsingDifferentIds_DalShouldBeHitTwice()
        {
            //Arrange
            int id1 = 1;
            int id2 = 2;
            _dal.Expect(d => d.GetUserById(id1)).Return(GetUsers().First());
            _dal.Expect(d => d.GetUserById(id2)).Return(GetUsers().Last());

            //Act
            _target.GetUserById(id1);
            _target.GetUserById(id2);

            //Assert
            _dal.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetAllUsers_AddUserAfterCaching_CacheShouldBeInvalidated()
        {
            //Arrange
            _dal.Expect(d => d.GetAllUsers())
                .Return(GetUsers())
                .Repeat.Twice();                    //Second call expected after cache is invalidated
            _dal.Expect(d => d.AddUser(null)).IgnoreArguments();

            //Act
            _target.GetAllUsers();
            _target.AddUser(new User { Id = 1234 });   //Should trigger invalidation
            _target.GetAllUsers();

            //Assert
            _dal.VerifyAllExpectations();
        }

        /// <summary>
        /// New Tests
        /// </summary>
        [TestMethod]
        public void GetAllUsers_AddUserAfterCaching_GroupCacheShouldBeInvalidated_CrossObjects()
        {
            //Arrange
            _dal.Expect(d => d.GetAllUsers())
                .Return(GetUsers())
                .Repeat.Twice();                    //Second call expected after cache is invalidated
            _dal.Expect(d => d.AddUser(null)).IgnoreArguments();

            //Act
            _targetGetRepo.GetAllUsers();
            _targetAddRepo.AddUser(new User { Id = 1234 });   //Should trigger invalidation
            _targetGetRepo.GetAllUsers();

            //Assert
            _dal.VerifyAllExpectations();
        }

        [TestMethod]
        public void GetUserBySuperId_ByTwoIdsTwiceShouldHitCacheOnSecondTryWithCorrectUser()
        {
            //Arrange
            _dal.Expect(d => d.GetUserBySuperId(1))
                .Return(GetUsers()[0]);          
            _dal.Expect(d => d.GetUserBySuperId(2))
                .Return(GetUsers()[1]);

            //Act
            var ret1 = _targetGetRepo.GetUserBySuperId(1);
            var ret2 = _targetGetRepo.GetUserBySuperId(2);
            var ret1a = _targetGetRepo.GetUserBySuperId(1);
            var ret2a = _targetGetRepo.GetUserBySuperId(2);

            
            //Assert
            _dal.VerifyAllExpectations();
            Assert.AreEqual(ret1, ret1a);
            Assert.AreEqual(ret2, ret2a);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private List<User> GetUsers()
        {
            return new List<User>
            {
                new User {Id = 1},
                new User {Id = 2}
            };
        }
    }
}
