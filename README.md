# Micro services example code, written in .Net

##Overview
There are 3 .net 6 projects included

1. data-collector
Accepts POST data and write it to a Kafka queue

2. hhe-service
Consumes the Kafka queue and saves new messages to it's DB (sqlite local DB). Then provides GET access to the processed messages in the DB.

3. shared-lib
Provides library for common code/abstractions.

##Running
First, create/migrate the hhe-service by executing "dotnet ef update database" in the hhe-service folder.

Then, ensure you have a kafka queue setup with a topic called activations and a group id of foo available. Set the server name/ip and port in the hhe-service/Program.cs file and data-collector/Program.cs. 