using System;
namespace PrismMauiTesting.Tests
{
    /// <summary>
    /// Fixture Base For <typeparamref name="TSut"/>.
    /// </summary>
    /// <typeparam name="TSut">
    /// Type of class to test
    /// </typeparam>
    public abstract class FixtureBase<TSut>
    {
        private readonly Lazy<TSut> lazySut;

        protected FixtureBase()
        {
            lazySut = new Lazy<TSut>(CreateSystemUnderTest, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        /// <summary>
        /// Gets the System Under Test (Sut), the class which is currently being tested.
        /// </summary>
        protected TSut Sut => lazySut.Value;

        /// <summary>
        /// Factory method for creating <typeparamref name="TSut"/>.
        /// </summary>
        /// <returns>
        /// Instance of <typeparamref name="TSut"/>.
        /// </returns>
        protected abstract TSut CreateSystemUnderTest();
    }
}

