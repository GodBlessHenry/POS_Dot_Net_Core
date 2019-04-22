using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POS.Core.Tests.Helpers
{
    // Setup for doing BDD 
    [TestClass]
    public abstract class SpecsBase<TSut> where TSut : class
    {
        private readonly AutoMocker _autoMocker;

        protected SpecsBase()
        {
            this._autoMocker = new AutoMocker();
        }

        protected TSut Sut { get; set; }

        [TestInitialize]
        public void SetupFixture()
        {
            this.Sut = this.CreateSut();
            this.Given();
            this.When();
        }

        [TestCleanup]
        public virtual void CleanupFixture()
        {
        }

        protected virtual TSut CreateSut()
        {
            return this._autoMocker.Create<TSut>();
        }

        protected abstract void Given();

        protected abstract void When();

        protected TDependency Mock<TDependency>()
        {
            return this._autoMocker.Mock<TDependency>();
        }

        protected void RegisterDependency<T>(T dependency)
        {
            this._autoMocker.Register<T>(dependency);
        }
    }
}