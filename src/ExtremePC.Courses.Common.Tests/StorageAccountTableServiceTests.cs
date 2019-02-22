using ExtremePC.Courses.Common.Services;
using ExtremePC.Courses.Models.ConfigOptions;
using Microsoft.Extensions.Options;
using Xunit;

namespace ExtremePC.Courses.Common.Tests
{
    public class StorageAccountTableServiceTests
    {
        [Fact]
        public void AnyTest_Boooring_SadProgrammerFace()
        {
            var registerStudentJobOptions = new RegisterStudentJobOptions();
            var optionsWrapper = new OptionsWrapper<RegisterStudentJobOptions>(registerStudentJobOptions);
            //prepare
            var sut = new StorageAccountTableService(optionsWrapper);
             
            
            //act

            //test
        }
    }
}
