# Seven Senders SQS .NET Example

A simple C# CLI application demonstrating the usage of Seven Senders SQS Proxy with the .NET AWS SDK.

## Requirements

.NET/.NET Core

## Installation

Either build the project from within your choice of IDE or run the following command from the command line:

```bash
dotnet build
```

## Credentials

Enter your Seven Senders API key in the `Program.cs` file:

```csharp
    var credentials = new BasicAWSCredentials("<API KEY HERE>", "---");
```

## Running The Application

This sample application connects to The Seven Senders SQS Proxy, and retrieves a batch of messages from the queue (if there are any).

```bash
dotnet build
```

Example output:

```
Received 10 messages

Message ID: ebd0674d-9a68-4fe1-a38c-40d6d220d058
{"id":"s272762270","order_id":"QA_LAPI_78bf63a","tracking_code":"323288888800000003354030","carrier":"bpost","carrier_country":"be","status":"Pickup","status_time":"03.04.2020 20:22:30","tickets":[],"events":[]}

Message ID: 61bcca33-b71b-4ed0-86f5-4cb8a9075007
{"id":"s272762272","order_id":"QA_LAPI_cb6606d","tracking_code":"111174220003303","carrier":"brt","carrier_country":"it","status":"Pickup","status_time":"03.04.2020 20:22:35","tickets":[],"events":[]}

Message ID: 8f0bc3e5-98f7-4aa8-ae98-99c750a8e176
{"id":"s272762289","order_id":"QA_LAPI_cfdaf6b","tracking_code":"00112345600000050687","carrier":"postnord","carrier_country":"dk","status":"Pickup","status_time":"03.04.2020 20:23:13","tickets":[],"events":[]}
```
