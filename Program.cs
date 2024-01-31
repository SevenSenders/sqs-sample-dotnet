using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace sqs_sample_dotnet
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
                {
                    // The first parameter is your API key. Enter '---' as the second parameter
                    var credentials = new BasicAWSCredentials("<API-KEY>", "---");
                    var config = new AmazonSQSConfig {
                        ServiceURL = "https://analytics-api.7senders.com"
                    };

                    // Example for receiving messages
                    var client = new AmazonSQSClient(credentials, config);

                    var receiveMessageRequest = new ReceiveMessageRequest {
                        QueueUrl = "https://analytics-api.7senders.com/queue.xml",
                        MaxNumberOfMessages = 10
                    };

                    var response = await client.ReceiveMessageAsync(receiveMessageRequest);
                    var messages = response.HttpStatusCode == HttpStatusCode.OK ? response.Messages : new List<Message>();

                    Console.WriteLine("Received " + messages.Count + " messages\n");
                    if (messages.Count == 0) {
                        Thread.Sleep(5000);
                    }

                    foreach (var message in messages) {
                        Console.WriteLine("Message ID: " + message.MessageId);
                        Console.WriteLine(message.Body + "\n\n");

                        // Example for deleting messages one by one
                        var deleteRequest = new DeleteMessageRequest
                        {
                            QueueUrl = "https://analytics-api.7senders.com/queue.xml",
                            ReceiptHandle = message.ReceiptHandle
                        };

                        await client.DeleteMessageAsync(deleteRequest);
                        Console.WriteLine($"Message ID: {message.MessageId} deleted.\n");
                    }

                    // Example for deleting messages in batch
                    var entries = messages.Select(m => new DeleteMessageBatchRequestEntry
                    {
                        Id = m.MessageId,
                        ReceiptHandle = m.ReceiptHandle
                    }).ToList();

                    var deleteBatchRequest = new DeleteMessageBatchRequest
                    {
                        QueueUrl = "https://sqs.sevensenders.com/api/v1/shipment-events",
                        Entries = entries
                    };

                    var deleteBatchResponse = await client.DeleteMessageBatchAsync(deleteBatchRequest);
                    foreach (var successEntry in deleteBatchResponse.Successful)
                    {
                        Console.WriteLine($"Message ID: {successEntry.Id} deleted in batch.\n");
                    }

                    // ...

                })
                .GetAwaiter().GetResult();
        }
    }
}
