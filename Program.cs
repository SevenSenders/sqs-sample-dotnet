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
                        
                    }
                })
                .GetAwaiter().GetResult();
        }
    }
}
