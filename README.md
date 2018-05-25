# Introduction 
This repo was created by the Data Collections team within the Education and Skills Fundign Agency. We wanted to explore whether or not validation rules (for ILR files) can all be run as Azure functions.
As a file came in, 600 (or so) requests to azure functions would be built.
In the end this did not work as a technology for us in this space (there are much better patterns). Mostly because of how we could easily saturate the maximum
number of requests and similar.

# Getting Started
You will need an Azure subscription.
You should be aware of Azure functions and how to link them message bus, databases and similar.
You will need to create a storage account and alter the app.config to use the correct URL and SAS tokens.

# Build and Test
You will need to read around azure functions - getting this up and working is quite easy if you understand the "hello world" example that Microsoft Publish.

# Contribute
This repo is NOT maintained and is shared as other people may be looking at very heavy workloads with high concurrency and considering Azure functions.
Azure capabilities are changed at a rapid pace. Our findings (from early 2018) may not be true in early 2019 or beyond.