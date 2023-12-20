# InternalTest

DAPR : locally installed instance in docker 

To run the application you will need to follow these steps : 

Run the backend services at the root of the project
1. Run dapr init
2. dapr run -f . (this will start the multi run app)

Run the Console App
1. cd ConsoleApp
2. dapr run --app-id consoleapp  -- dotnet run


To publish to redis 
1.  dapr run --app-id alertprocessor --dapr-http-port 3601
2. dapr publish --publish-app-id orderprocessing --pubsub pubsub --topic alerts --data '{"AlertType":"BlahAlert", "PublishRequestTime" :"2023/01/01"}'