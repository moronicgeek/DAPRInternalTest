# InternalTest

I used DAPR as a locally installed instance in docker which gave me a few services to use such as the pub sub component and state component. 

To run the application you will need to follow these steps : 

Run the backend services
1. Run dapr init
2. dapr run -f . (this will start the multi run app)

Run the Console App
1. cd ConsoleApp
2. dapr run --app-id consoleapp  -- dotnet run