using System;
using System.Threading.Tasks;
using WebApiClient.Api;
using WebApiClient.Model;
using Xunit;

namespace IntegrationTests
{
    public class SensorsApiTests : BaseTest
    {
        [Fact]
        public void CreateSensor()
        {
            var newSensor = new CreateSensorCommand
            {
                FarmId = Guid.NewGuid(),
                State = SensorState.Deployed
            };
            var Sensor = SensorsApi.ApiV1SensorsPost(newSensor);

            Assert.Equal(newSensor.FarmId, Sensor.FarmId);
            Assert.Equal(newSensor.State, Sensor.State);
            Assert.NotEqual(Guid.Empty, Sensor.Id);
        }

        [Fact]
        public void GetSensor()
        {
            var newSensor = new CreateSensorCommand
            {
                FarmId = Guid.NewGuid(),
                State = SensorState.Refurbished
            };
            var Sensor = SensorsApi.ApiV1SensorsPost(newSensor);
            var result = SensorsApi.ApiV1SensorsIdGet(Sensor.Id);

            Assert.Equal(newSensor.FarmId, Sensor.FarmId);
            Assert.Equal(newSensor.State, Sensor.State);
            Assert.Equal(Sensor.FarmId, result.FarmId);
            Assert.Equal(Sensor.State, result.State);
            Assert.Equal(Sensor.Id, result.Id);
        }

        [Fact]
        public void UpdateSensor()
        {
            var newSensor = new CreateSensorCommand
            {
                FarmId = Guid.NewGuid(),
                State = SensorState.Inventory
            };
            var Sensor = SensorsApi.ApiV1SensorsPost(newSensor);
            newSensor.FarmId = Guid.NewGuid();
            newSensor.State = SensorState.Deployed;

            SensorsApi.ApiV1SensorsIdPut(Sensor.Id, newSensor);

            var result = SensorsApi.ApiV1SensorsIdGet(Sensor.Id);

            Assert.Equal(newSensor.FarmId, result.FarmId);
            Assert.Equal(newSensor.State, result.State);
            Assert.Equal(Sensor.Id, result.Id);
        }

        [Fact]
        public void DeleteSensor()
        {
            var newSensor = new CreateSensorCommand
            {
                FarmId = Guid.NewGuid(),
                State = SensorState.FarmerTriage
            };
            var Sensor = SensorsApi.ApiV1SensorsPost(newSensor);
            var result = SensorsApi.ApiV1SensorsIdGet(Sensor.Id);

            Assert.Equal(newSensor.FarmId, Sensor.FarmId);
            Assert.Equal(newSensor.State, Sensor.State);
            Assert.Equal(Sensor.FarmId, result.FarmId);
            Assert.Equal(Sensor.State, result.State);
            Assert.Equal(Sensor.Id, result.Id);

            SensorsApi.ApiV1SensorsIdDelete(Sensor.Id);
            result = SensorsApi.ApiV1SensorsIdGet(Sensor.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllSensorsAsync()
        {
            await DataBaseHelper.ClearDatabaseAsync();
            var newFirstSensor = new CreateSensorCommand
            {
                FarmId = Guid.NewGuid(),
                State = SensorState.Deployed
            };
            var newSecondSensor = new CreateSensorCommand
            {
                FarmId = Guid.NewGuid(),
                State = SensorState.Refurbished
            };
            var firstSensor = SensorsApi.ApiV1SensorsPost(newFirstSensor);
            var secondSensor = SensorsApi.ApiV1SensorsPost(newSecondSensor);

            var result = SensorsApi.ApiV1SensorsGet();

            Assert.Equal(2, result.Count);
            Assert.Equal(firstSensor.Id, result[0].Id);
            Assert.Equal(firstSensor.FarmId, result[0].FarmId);
            Assert.Equal(firstSensor.State, result[0].State);
            Assert.Equal(secondSensor.Id, result[1].Id);
            Assert.Equal(secondSensor.FarmId, result[1].FarmId);
            Assert.Equal(secondSensor.State, result[1].State);
        }
    }
}
