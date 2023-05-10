using System.Linq;
using Validators.Handlers;
using Validators.Test.Models;
using Validators.Test.Models.Rules;
using Validators.Types;

namespace Validators.Test
{
    [TestClass]
    public class SequentialHandlerValidatorTests
    {
        SequentialHandler handler;
        Car testData;

        [TestInitialize]
        public void TestStartup()
        {
            handler = new SequentialHandler();
            testData = new Car()
            {
                Age = 3,
                FourWheelDrive = true,
                Price = 300,
                Color = "White"
            };
        }
         

        [TestMethod]
        public async Task Sequential_Handler_Must_Be_Not_Valid()
        {
            // Arrange 
            var checkList = new CheckList(handler)
            {
                Rules = new List<ICheckRule>()
                {
                    new MustBeCheap(testData,200),
                    new MustHaveNiceColor(testData),
                    new MustNotBeOld(testData),
                    new EmptyRule()

                }
            };

            //Act
            await checkList.Run();

            //Assert
            Assert.IsTrue(!checkList.Valid);
            Assert.IsTrue(checkList.Rules.ElementAt(0).State == CheckRuleState.Valid);
            Assert.IsTrue(checkList.Rules.ElementAt(1).State == CheckRuleState.InValid);
            Assert.IsTrue(checkList.Rules.ElementAt(2).State == CheckRuleState.Valid);
            Assert.IsTrue(checkList.Rules.ElementAt(3).State == CheckRuleState.Valid);

        }

        [TestMethod]
        public async Task Sequential_Handler_Must_Be_Valid()
        {
            // Arrange 
            testData.Color = "Black";
            var checkList = new CheckList(handler)
            {
                Rules = new List<ICheckRule>()
                {
                    new MustHaveNiceColor(testData),
                    new MustBeCheap(testData,100),
                    new MustNotBeOld(testData),
                    new EmptyRule()
                }
            };

            //Act
            await checkList.Run();

            //Assert
            Assert.IsTrue(checkList.Valid);
            Assert.IsTrue(checkList.Rules.All(d => d.State == CheckRuleState.Valid));
            
        }
         
        [TestMethod]
        public async Task Sequential_Handler_Order_Check()
        {
            // Arrange 
            testData.Color = "Black";
            var checkList = new CheckList(handler)
            {
                Rules = new List<ICheckRule>()
                {
                    new MustHaveNiceColor(testData),
                    new MustBeCheap(testData,100),
                    new MustNotBeOld(testData),
                    new EmptyRule()
                }
            };

            //Act
            await checkList.Run();

            //Assert            
            var baseRules = checkList.Rules.Cast<BaseTestRule>();
            Assert.IsTrue(baseRules.ElementAt(1).ValidationTime > baseRules.ElementAt(0).ValidationTime);
            Assert.IsTrue(baseRules.ElementAt(2).ValidationTime > baseRules.ElementAt(1).ValidationTime);
            Assert.IsTrue(baseRules.ElementAt(3).ValidationTime > baseRules.ElementAt(2).ValidationTime);

        }
    }
}