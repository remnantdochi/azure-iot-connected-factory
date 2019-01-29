
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Microsoft.Azure.IoTSuite.Connectedfactory.WebApp.Configuration;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Azure.IoTSuite.Connectedfactory.WebApp
{
    public partial class Startup
    {
        public Task ConfigureIotHub(CancellationToken ct)
        {
            return Task.Run(async () => await ConnectToIotHubAsync(ct));
        }

        private async Task ConnectToIotHubAsync(CancellationToken ct)
        {
            EventProcessorHost eventProcessorHost;
            EventProcessorHost eventProcessorHost2;

            // Get configuration settings
            string iotHubTelemetryConsumerGroup = ConfigurationProvider.GetConfigurationSettingValue("IotHubTelemetryConsumerGroup");
            string iotHubEventHubName = ConfigurationProvider.GetConfigurationSettingValue("IotHubEventHubName");
            string iotHubEventHubEndpointIotHubOwnerConnectionString = ConfigurationProvider.GetConfigurationSettingValue("IotHubEventHubEndpointIotHubOwnerConnectionString");
            string solutionStorageAccountConnectionString = ConfigurationProvider.GetConfigurationSettingValue("SolutionStorageAccountConnectionString");

            //eventhub 합침
            string EventHubConnectionString = "Endpoint=sb://king012101man.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=7dm6eJ3uPa2HEZufnENhXabjHltsKkycwYdy8Mp0+Ns=";
            string EventHubName = "kingsman03";
            //이름2로 바꿈.
            string StorageContainerName2 = "containerking06";
            string StorageAccountName = "king012101man";
            string StorageAccountKey = "jY67taiqQoDM8L5Od7vVR8N3Ki+ONj2T+xTbBMjxXrQVFXMNt801H9Kxg2XSmjv/pHEeWqqmpdJzzaKEAi39mA==";
            string StorageConnectionString = string.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", StorageAccountName, StorageAccountKey);


        // Initialize EventProcessorHost. 
        Trace.TraceInformation("Creating EventProcessorHost for IoTHub: {0}, ConsumerGroup: {1}, ConnectionString: {2}, StorageConnectionString: {3}",
                iotHubEventHubName, iotHubTelemetryConsumerGroup, iotHubEventHubEndpointIotHubOwnerConnectionString, solutionStorageAccountConnectionString);
            string StorageContainerName = "telemetrycheckpoints";
            eventProcessorHost = new EventProcessorHost(
                    iotHubEventHubName,
                    iotHubTelemetryConsumerGroup,
                    iotHubEventHubEndpointIotHubOwnerConnectionString,
                    solutionStorageAccountConnectionString,
                    StorageContainerName2);

            //eventhub 합침
            eventProcessorHost2 = new EventProcessorHost(
                EventHubName,
                PartitionReceiver.DefaultConsumerGroupName,
                EventHubConnectionString,
                StorageConnectionString,
                StorageContainerName);

            // Registers the Event Processor Host and starts receiving messages.
            EventProcessorOptions options = new EventProcessorOptions();
            options.InitialOffsetProvider = ((partitionId) => DateTime.UtcNow);
            options.SetExceptionHandler(EventProcessorHostExceptionHandler);

            EventProcessorOptions options2 = new EventProcessorOptions();
            options2.InitialOffsetProvider = ((partitionId) => DateTime.UtcNow);
            options2.SetExceptionHandler(EventProcessorHostExceptionHandler);
            try
            {   //eventhub 합침.

                await eventProcessorHost2.RegisterEventProcessorAsync<SimpleEventProcessor>(options2);
                await eventProcessorHost.RegisterEventProcessorAsync<MessageProcessor>(options);
                

                Trace.TraceInformation($"EventProcessor successfully registered");
                
            }
            catch (Exception e)
            {
                Trace.TraceInformation($"Exception during register EventProcessorHost '{e.Message}'");
            }

            // Wait till shutdown.
            while (true)
            {
                if (ct.IsCancellationRequested)
                {
                    Trace.TraceInformation($"Application is shutting down. Unregistering EventProcessorHost...");
                    await eventProcessorHost2.UnregisterEventProcessorAsync();
                    await eventProcessorHost.UnregisterEventProcessorAsync();
                    
                    return;
                }
                await Task.Delay(2000);
            }
        }

        public void EventProcessorHostExceptionHandler(ExceptionReceivedEventArgs args)
        {
            Trace.TraceInformation($"EventProcessorHostException: {args.Exception.Message}");
        }
    }
}