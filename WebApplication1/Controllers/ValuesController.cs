using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        static CloudQueue cloudQueue;

        // GET api/values
        [SwaggerOperation("GetAll")]
        public IEnumerable<string> Get()
        {

            var connectionString = "DefaultEndpointsProtocol=https;AccountName=storagephdias;AccountKey=gcPE58aaZEzWt1CdUHjkxvxmfQcn1+QJtVRbpZF53TMKsVFHFC8Q0lheFXiK0QqpEwd7/ggeqEIEo/5NAhBWvw==;EndpointSuffix=core.windows.net";
            CloudStorageAccount cloudStorageAccount;

            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {

            }

            // Retrieve a reference to a queue.
            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            cloudQueue = cloudQueueClient.GetQueueReference("facadequeue");

            // Fetch the queue attributes.
            cloudQueue.FetchAttributes();

            // Retrieve the cached approximate message count.
            int? cachedMessageCount = cloudQueue.ApproximateMessageCount;

            String qtd = "MESSAGES IN QUEUE = " + cachedMessageCount.ToString();

            return new string[] { qtd };
        }

        // GET api/values/5
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        public void Post([FromBody]string value)
        {

            //var connectionString = "DefaultEndpointsProtocol=https;AccountName=pablodias;AccountKey=mtbzioaGNvABxjOlnjOyFs82dmH0enTh3RGhGLRQF2vHb8e+iz3sm1T5dAfZcpjlPWZxq/SWRhlS3aXrbGnd4Q==;EndpointSuffix=core.windows.net";
            var connectionString = "DefaultEndpointsProtocol=https;AccountName=storagephdias;AccountKey=gcPE58aaZEzWt1CdUHjkxvxmfQcn1+QJtVRbpZF53TMKsVFHFC8Q0lheFXiK0QqpEwd7/ggeqEIEo/5NAhBWvw==;EndpointSuffix=core.windows.net";
            CloudStorageAccount cloudStorageAccount;

            if (!CloudStorageAccount.TryParse(connectionString, out cloudStorageAccount))
            {

            }

            var cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            cloudQueue = cloudQueueClient.GetQueueReference("facadequeue");

            // Note: Usually this statement can be executed once during application startup or maybe even never in the application.
            //       A queue in Azure Storage is often considered a persistent item which exists over a long time.
            //       Every time .CreateIfNotExists() is executed a storage transaction and a bit of latency for the call occurs.
            cloudQueue.CreateIfNotExists();

            var message = new CloudQueueMessage("new message from api");

            cloudQueue.AddMessage(message);

        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Delete(int id)
        {
        }
    }
}
