using System;
using System.Threading.Tasks;
using WebApiClient.Model;
using Xunit;

namespace IntegrationTests
{
    public class CowsApiTests : BaseTest
    {
        [Fact]
        public void CreateCow()
        {
            var newCow = new CreateCowCommand
            {
                FarmId = Guid.NewGuid(),
                State = CowState.Open
            };
            var cow = CowsApi.ApiV1CowsPost(newCow);

            Assert.Equal(newCow.FarmId, cow.FarmId);
            Assert.Equal(newCow.State, cow.State);
            Assert.NotEqual(Guid.Empty, cow.Id);
        }

        [Fact]
        public void GetCow()
        {
            var newCow = new CreateCowCommand
            {
                FarmId = Guid.NewGuid(),
                State = CowState.Pregnant
            };
            var cow = CowsApi.ApiV1CowsPost(newCow);
            var result = CowsApi.ApiV1CowsIdGet(cow.Id);

            Assert.Equal(newCow.FarmId, cow.FarmId);
            Assert.Equal(newCow.State, cow.State);
            Assert.Equal(cow.FarmId, result.FarmId);
            Assert.Equal(cow.State, result.State);
            Assert.Equal(cow.Id, result.Id);
        }

        [Fact]
        public void UpdateCow()
        {
            var newCow = new CreateCowCommand
            {
                FarmId = Guid.NewGuid(),
                State = CowState.Pregnant
            };
            var cow = CowsApi.ApiV1CowsPost(newCow);
            newCow.FarmId = Guid.NewGuid();
            newCow.State = CowState.Dry;

            CowsApi.ApiV1CowsIdPut(cow.Id, newCow);

            var result = CowsApi.ApiV1CowsIdGet(cow.Id);

            Assert.Equal(newCow.FarmId, result.FarmId);
            Assert.Equal(newCow.State, result.State);
            Assert.Equal(cow.Id, result.Id);
        }

        [Fact]
        public void DeleteCow()
        {
            var newCow = new CreateCowCommand
            {
                FarmId = Guid.NewGuid(),
                State = CowState.Inseminated
            };
            var cow = CowsApi.ApiV1CowsPost(newCow);
            var result = CowsApi.ApiV1CowsIdGet(cow.Id);

            Assert.Equal(newCow.FarmId, cow.FarmId);
            Assert.Equal(newCow.State, cow.State);
            Assert.Equal(cow.FarmId, result.FarmId);
            Assert.Equal(cow.State, result.State);
            Assert.Equal(cow.Id, result.Id);

            CowsApi.ApiV1CowsIdDelete(cow.Id);
            result = CowsApi.ApiV1CowsIdGet(cow.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCowsAsync()
        {
            await DataBaseHelper.ClearDatabaseAsync();
            var newFirstCow = new CreateCowCommand
            {
                FarmId = Guid.NewGuid(),
                State = CowState.Open
            };
            var newSecondCow = new CreateCowCommand
            {
                FarmId = Guid.NewGuid(),
                State = CowState.Pregnant
            };
            var firstCow = CowsApi.ApiV1CowsPost(newFirstCow);
            var secondCow = CowsApi.ApiV1CowsPost(newSecondCow);

            var result = CowsApi.ApiV1CowsGet();

            Assert.Equal(2, result.Count);
            Assert.Equal(firstCow.Id, result[0].Id);
            Assert.Equal(firstCow.FarmId, result[0].FarmId);
            Assert.Equal(firstCow.State, result[0].State);
            Assert.Equal(secondCow.Id, result[1].Id);
            Assert.Equal(secondCow.FarmId, result[1].FarmId);
            Assert.Equal(secondCow.State, result[1].State);
        }

        [Fact]
        public async Task GetCount()
        {
            //Setup
            await DataBaseHelper.ClearDatabaseAsync();
            var farmId = Guid.NewGuid();
            await DataBaseHelper.CowEventStore.CreateAsync(
                new Domain.Models.Cows.Cow { FarmId = farmId, State = Domain.Models.Cows.CowState.Open },
                DateTimeOffset.Parse("10/22/2020 5:00:00 PM"));
            await DataBaseHelper.CowEventStore.CreateAsync(
                new Domain.Models.Cows.Cow { FarmId = farmId, State = Domain.Models.Cows.CowState.Pregnant },
                DateTimeOffset.Parse("10/22/2020 5:20:00 PM"));
            await DataBaseHelper.CowEventStore.CreateAsync(
                new Domain.Models.Cows.Cow { FarmId = farmId, State = Domain.Models.Cows.CowState.Open }, 
                DateTimeOffset.Parse("10/23/2020 2:00:00 AM"));
            var id = Guid.NewGuid();
            await DataBaseHelper.CowEventStore.CreateAsync(
                new Domain.Models.Cows.Cow { FarmId = farmId, State = Domain.Models.Cows.CowState.Open },
                DateTimeOffset.Parse("10/23/2020 3:00:00 AM"));
            await DataBaseHelper.CowEventStore.CreateAsync(
                new Domain.Models.Cows.Cow { FarmId = farmId, State = Domain.Models.Cows.CowState.Open },
                DateTimeOffset.Parse("10/23/2020 3:10:00 AM"));
            await DataBaseHelper.CowEventStore.UpdateAsync(
                new Domain.Models.Cows.Cow { Id = id, FarmId = farmId, State = Domain.Models.Cows.CowState.Pregnant }, 
                DateTimeOffset.Parse("10/23/2020 4:00:00 AM"));
            
            var cowForDelete = new Domain.Models.Cows.Cow {FarmId = farmId, State = Domain.Models.Cows.CowState.Open };
            await DataBaseHelper.CowEventStore.CreateAsync(
                cowForDelete,
                DateTimeOffset.Parse("10/23/2020 5:30:00 AM"));
            await DataBaseHelper.CowEventStore.DeleteAsync(
                cowForDelete,
                DateTimeOffset.Parse("10/23/2020 5:35:00 AM"));
            
            //Act
            var count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/23/2020 11:59:59 PM"), CowState.Open);
            Assert.Equal(4, count);
            count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/23/2020 3:05:00 AM"), CowState.Open);
            Assert.Equal(3, count); 
            count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/23/2020 2:30:00 AM"), CowState.Open);
            Assert.Equal(2, count);
            count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/23/2020 4:00:00 AM"), CowState.Pregnant);
            Assert.Equal(2, count);
            count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/22/2020 5:20:00 PM"), CowState.Open);
            Assert.Equal(1, count);
            count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/22/2020 7:20:35 PM"), CowState.Pregnant);
            Assert.Equal(1, count);
            count = CowsApi.ApiV1CowsCountGet(farmId, DateTime.Parse("10/22/2020 2:00:00 PM"), CowState.Open);
            Assert.Equal(0, count);
            count = CowsApi.ApiV1CowsCountGet(Guid.NewGuid(), DateTime.Parse("10/22/2020 6:00:00 PM"), CowState.Open);
            Assert.Equal(0, count);
        }
    }
}
